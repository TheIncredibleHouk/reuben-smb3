using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Daiz.Library;
using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public partial class PaletteManager : Form
    {

        public PaletteManager()
        {
            InitializeComponent();
            CmbPalettes.DisplayMember = "Name";
            CmbPalettes.Items.Add(ProjectController.SpecialManager.SpecialPalette);

            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                CmbPalettes.Items.Add(p);
            }

            CmbPalettes.SelectedIndex = 1;

            ProjectController.PaletteManager.PaletteAdded += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteAdded);
            ProjectController.PaletteManager.PaletteRemoved += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteRemoved);
            FpsFull.SelectedPaletteChanged += new EventHandler(FpsFull_SelectedPaletteChanged);
        }

        void PaletteManager_PaletteRemoved(object sender, TEventArgs<PaletteInfo> e)
        {
            int index = CmbPalettes.SelectedIndex;
            CmbPalettes.Items.Remove(e.Data);
            if (CmbPalettes.Items.Count == 2)
            {
                BtnRemove.Enabled = BtnRename.Enabled = false;
            }
            else
            {
                if (CmbPalettes.Items.Count - 1 < index)
                {
                    CmbPalettes.SelectedIndex = CmbPalettes.Items.Count - 1;
                }
                else
                {
                    CmbPalettes.SelectedIndex = index;
                }
            }
        }

        void PaletteManager_PaletteAdded(object sender, TEventArgs<PaletteInfo> e)
        {
            CmbPalettes.Items.Add(e.Data);
            CmbPalettes.SelectedItem = e.Data;
        }

        void FpsFull_SelectedPaletteChanged(object sender, EventArgs e)
        {
            if (FpsFull.SelectedColor >= 0)
            {
                LblSelected.Text = string.Format("Selected:  {0:X2}", FpsFull.SelectedColor);
            }
            else
            {
                LblSelected.Text = "Selected: None";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            NewPaletteForm iForm = new NewPaletteForm();
            iForm.StartPosition = FormStartPosition.CenterParent;

            string name = iForm.GetInput("Enter a name for this palette");
            if (name != null)
            {
                if (iForm.UseCurentPalette)
                {
                    ProjectController.PaletteManager.AddNewPalette(name, (PaletteInfo)CmbPalettes.SelectedItem);
                }
                else
                {
                    ProjectController.PaletteManager.AddNewPalette(name);
                }
            }
        }

        private void CmbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CmbPalettes.SelectedItem == null)
            {
                PslCurrent.CurrentPalette = null;
                BtnRemove.Enabled = BtnRename.Enabled = false;
            }
            else
            {
                PaletteInfo pi = CmbPalettes.SelectedItem as PaletteInfo;
                pi.NameChanged -= CurrentPalette_NameChanged;
                PslCurrent.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
                PslCurrent.CurrentPalette.NameChanged += new EventHandler<TEventArgs<string>>(CurrentPalette_NameChanged);
                BtnRemove.Enabled = BtnRename.Enabled = CmbPalettes.SelectedIndex != 0;
            }

            LblTransparent.Visible = CmbPalettes.SelectedIndex == 0;
        }

        void CurrentPalette_NameChanged(object sender, TEventArgs<string> e)
        {
            int index = CmbPalettes.SelectedIndex;
            object o = CmbPalettes.SelectedItem;
            CmbPalettes.Items.RemoveAt(index);
            CmbPalettes.Items.Insert(index, o);
            CmbPalettes.SelectedIndex = index;
        }

        private void PslCurrent_MouseDown(object sender, MouseEventArgs e)
        {
            if (PslCurrent.CurrentPalette == null) return;
            int offset = (e.X % 64) / 16;
            int index = ((e.Y / 16) * 4) + (e.X / 64);
            if (index == 4 && CmbPalettes.SelectedIndex != 0) return;
            if (offset == 0 && CmbPalettes.SelectedIndex != 0)
            {
                PslCurrent.CurrentPalette.Background = FpsFull.SelectedColor;
            }
            else
            {
                
                if (ModifierKeys == Keys.Control)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i != 4)
                        {
                            if (MouseButtons == MouseButtons.Right && PslCurrent.CurrentPalette.IsSpecial)
                            {
                                PslCurrent.CurrentPalette[i, offset] = 0x40;
                            }
                            else
                            {
                                PslCurrent.CurrentPalette[i, offset] = FpsFull.SelectedColor;
                            }
                        }
                    }
                }
                else
                {
                    if (MouseButtons == MouseButtons.Right && PslCurrent.CurrentPalette.IsSpecial)
                    {
                        PslCurrent.CurrentPalette[index, offset] = 0x40;
                    }
                    else
                    {
                        PslCurrent.CurrentPalette[index, offset] = FpsFull.SelectedColor;
                    }
                }
            }
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            InputForm iForm = new InputForm();
            iForm.StartPosition = FormStartPosition.CenterParent;
            iForm.Owner = ReubenController.MainWindow;

            string newName = iForm.GetInput("Enter a name for this palette");
            if (newName != null)
            {
                (CmbPalettes.SelectedItem as PaletteInfo).Name = newName;
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if (cForm.Confirm("Are you sure you want to remove this palette? Any\nlevel referring to this palette will resort to the first\npalette in your list."))
            {
                ProjectController.PaletteManager.RemovePalette(CmbPalettes.SelectedItem as PaletteInfo);
            }
            cForm.Dispose();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            ProjectController.Save();
            ProjectController.PaletteManager.PaletteAdded -= PaletteManager_PaletteAdded;
            ProjectController.PaletteManager.PaletteRemoved -= PaletteManager_PaletteRemoved;
            FpsFull.SelectedPaletteChanged -= FpsFull_SelectedPaletteChanged;
            PslCurrent.CurrentPalette.NameChanged -= CurrentPalette_NameChanged;
            this.Close();
        }

        public void ShowDialog(int palette)
        {
            CmbPalettes.SelectedIndex = palette + 1;
            this.ShowDialog();
        }

        private void FpsFull_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if (x < 0 || y < 0 || x > 0x0F || y > 0x03) return;

            LblHover2.Text = "Color: " + ((y * 16) + x).ToHexString();
        }

        private void PslCurrent_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if (x < 0 || y < 0 || x > 0x0F || y > 0x01) return;

            LblPaletteHover.Text = "Color: " + PslCurrent.CurrentPalette[(y * 4) + (x / 4), x % 4].ToHexString();
        }
    }
}