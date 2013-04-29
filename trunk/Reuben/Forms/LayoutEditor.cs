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
    public partial class LayoutEditor : Form
    {
        PatternTable CurrentTable;
        public LayoutEditor()
        {
            InitializeComponent();

            CmbLayouts.DisplayMember = CmbGraphics1.DisplayMember = CmbGraphics2.DisplayMember = CmbPalettes.DisplayMember = CmbDefinitions.DisplayMember = "Name";
            foreach (var g in ProjectController.GraphicsManager.GraphicsInfo)
            {
                CmbGraphics1.Items.Add(g);
                CmbGraphics2.Items.Add(g);
            }

            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                CmbPalettes.Items.Add(p);
            }

            foreach (var l in ProjectController.LevelManager.LevelTypes)
            {
                CmbDefinitions.Items.Add(l);
            }

            foreach (var l in ProjectController.LayoutManager.BlockLayouts)
            {
                if (!l.IsDefault)
                    CmbLayouts.Items.Add(l);
            }

            if (CmbLayouts.SelectedIndex != -1)
            {
                CmbLayouts.SelectedIndex = 0;
            }

            CurrentTable = ProjectController.GraphicsManager.BuildPatternTable(0);
            BlsFrom.CurrentTable = CurrentTable;
            BlsTo.CurrentTable = CurrentTable;
            BlsFrom.BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];

            CmbGraphics1.SelectedIndex = 8;
            CmbGraphics2.SelectedIndex = 0x64;
            CmbPalettes.SelectedIndex = 0;
            CmbDefinitions.SelectedIndex = 0;

            BlsFrom.SpecialPalette = BlsTo.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;
            BlsFrom.SpecialTable = BlsTo.SpecialTable = ProjectController.SpecialManager.SpecialTable;

            ProjectController.LayoutManager.LayoutAdded += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutAdded);
            ProjectController.LayoutManager.LayoutRemoved += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutRemoved);
        }

        void LayoutManager_LayoutRemoved(object sender, TEventArgs<BlockLayout> e)
        {
            CmbLayouts.Items.Remove(e.Data);
            if (CmbLayouts.Items.Count > 0)
            {
                CmbLayouts.SelectedIndex = 0;
            }
        }

        void LayoutManager_LayoutAdded(object sender, TEventArgs<BlockLayout> e)
        {
            CmbLayouts.Items.Add(e.Data);
            CmbLayouts.SelectedIndex = CmbLayouts.Items.Count - 1;
        }

        private void CmbGraphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics1.SelectedIndex]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics1.SelectedIndex + 1]);
            LblHexGraphics1.Text = "x" + CmbGraphics1.SelectedIndex.ToHexString();
        }

        private void CmbGraphics2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics2.SelectedIndex]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics2.SelectedIndex + 1]);
            LblHexGraphics2.Text = "x" + CmbGraphics2.SelectedIndex.ToHexString();
        }

        private void CmbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlsView.CurrentPalette = BlsTo.CurrentPalette = BlsFrom.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
        }

        private void CmbDefinitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbLayouts.SelectedIndex == -1) return;
            BlsFrom.CurrentDefiniton = BlsTo.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(CmbDefinitions.SelectedIndex);
        }

        private void BtnSaveClose_Click(object sender, EventArgs e)
        {
            ProjectController.Save();
            this.Close();
        }

        private BlockLayout CurrentLayout;
        private void CmbLayouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentLayout != null)
            {
                CurrentLayout.Renamed -= CurrentLayout_Renamed;
            }

            if (CmbLayouts.SelectedItem != null)
            {
                BlsFrom.CurrentDefiniton = BlsTo.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(CmbDefinitions.SelectedIndex);
                CurrentLayout = BlsTo.BlockLayout = (BlockLayout) CmbLayouts.SelectedItem;
                BtnDelete.Enabled = BtnRename.Enabled = true;
            }
            else
            {
                CurrentLayout = null;
                BlsTo.BlockLayout = null;
                BtnDelete.Enabled = BtnRename.Enabled = false;
            }

            if (CurrentLayout != null)
            {
                CurrentLayout.Renamed += new EventHandler<TEventArgs<string>>(CurrentLayout_Renamed);
            }
        }

        private void CurrentLayout_Renamed(object sender, TEventArgs<string> e)
        {
            int index = CmbLayouts.SelectedIndex;
            CmbLayouts.Items.Remove(CurrentLayout);
            CmbLayouts.Items.Insert(index, CurrentLayout);
            CmbLayouts.SelectedIndex = index;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            InputForm iForm = new InputForm();
            string newName = iForm.GetInput("Please enter the name of the layout");
            if (newName != null)
            {
                ProjectController.LayoutManager.CreateNewLayout(newName);
            }
        }

        private void BlsTo_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            if(x < 0 || x > 15 || y < 0 || y > 15) return;
            if (CurrentLayout == null) return;

            if (e.Button == MouseButtons.Left)
            {
                CurrentLayout.Layout[(y * 16) + x] = BlsFrom.SelectedIndex;
                BlsTo.UpdateSelection();
            }
            else if (e.Button == MouseButtons.Right)
            {
                CurrentLayout.Layout[(y * 16) + x] = -1;
                BlsTo.UpdateSelection();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if (cForm.Confirm("Are you sure you want to remove this layout?"))
            {
                ProjectController.LayoutManager.RemoveLayout(CmbLayouts.SelectedItem as BlockLayout);
            }
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            InputForm iForm = new InputForm();
            iForm.StartPosition = FormStartPosition.CenterParent;
            iForm.Owner = ReubenController.MainWindow;

            string newName = iForm.GetInput("Please enter the name of the layout");
            if (newName != null)
            {
                (CmbLayouts.SelectedItem as BlockLayout).Name = newName;
            }
        }

        private void ChkShowSpecials_CheckedChanged(object sender, EventArgs e)
        {
            BlsTo.ShowSpecialBlocks = BlsFrom.ShowSpecialBlocks = ChkShowSpecials.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowDialog(int definitionIndex, int graphics1, int graphics2, int paletteIndex, BlockLayout layout)
        {
            CmbGraphics1.SelectedIndex = graphics1;
            CmbGraphics2.SelectedIndex = graphics2;
            CmbPalettes.SelectedIndex = paletteIndex;
            CmbLayouts.SelectedItem = layout;
            CmbDefinitions.SelectedIndex = definitionIndex;
            CmbDefinitions_SelectedIndexChanged(null, null);
            this.ShowDialog();
        }

        int PreviousFromX, PreviousFromY;
        private void BlsFrom_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            if (PreviousFromX == x && PreviousFromY == y) return;
            PreviousFromX = x;
            PreviousFromY = y;
            LayoutToolTip.SetToolTip(BlsFrom, ProjectController.BlockManager.GetBlockString(CmbDefinitions.SelectedIndex + 1, ((y * 16) + x)));
        }

        int PreviousToX, PreviousToY;
        private void BlsTo_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if (PreviousToX == PreviousToY) return;
            PreviousToX = x;
            PreviousToY = y;
            int index = ((y * 16) + x);
            if (BlsTo.BlockLayout != null)
            {
                int tile = BlsTo.BlockLayout.Layout[index];
                if (tile != -1)
                {
                    LayoutToolTip.SetToolTip(BlsTo, ProjectController.BlockManager.GetBlockString(CmbDefinitions.SelectedIndex + 1, tile));
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            BlsFrom.ShowBlockSolidity = BlsTo.ShowBlockSolidity = ChkBlockProperties.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GrpHelp.Visible)
            {
                GrpHelp.Visible = false;
                PnlHelp.Height = 30;
                BtnHelp.Text = "Show Help";
            }
            else
            {
                GrpHelp.Visible = true;
                PnlHelp.Height = 110;
                BtnHelp.Text = "Hide Help";
            }
        }

    }
}
