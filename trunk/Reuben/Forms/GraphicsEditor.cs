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
    public partial class GraphicsEditor : Form
    {
        SelectionMode SelectionMode;
        PatternTable CompositeTable;

        public GraphicsEditor()
        {
            InitializeComponent();
            CompositeTable = ProjectController.GraphicsManager.BuildPatternTable(0);
            CmbPalettes.DisplayMember = "Name";

            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                CmbPalettes.Items.Add(p);
            }

            
            CmbGraphics1.DisplayMember = CmbGraphics2.DisplayMember = CmbGraphics3.DisplayMember = CmbGraphics4.DisplayMember = "Name";
            foreach (var g in ProjectController.GraphicsManager.GraphicsInfo)
            {
                CmbGraphics1.Items.Add(g);
                CmbGraphics2.Items.Add(g);
                CmbGraphics3.Items.Add(g);
                CmbGraphics4.Items.Add(g);
            }

            CmbGraphics1.SelectedIndex = 0;
            CmbGraphics2.SelectedIndex = 1;
            CmbGraphics3.SelectedIndex = 2;
            CmbGraphics4.SelectedIndex = 3;

            CmbPalettes.SelectedIndex = 0;

            PtvTileSelector.CurrentTable= CompositeTable;
            SelectionMode = SelectionMode.Quarter;
            PslView.SelectedIndexChanged += new EventHandler(PslView_SelectedIndexChanged);
            PslView.SelectedOffsetChanged += new EventHandler(PslView_SelectedOffsetChanged);
            PtvTileSelector.SelectionChanged += new EventHandler(PtvTileSelector_SelectionChanged);
            PtvTileSelector.UpdateSelection();
        }

        void PslView_SelectedOffsetChanged(object sender, EventArgs e)
        {
            TlvEditTiles.SelectedOffset = (byte)PslView.SelectedOffset;
        }

        void PtvTileSelector_SelectionChanged(object sender, EventArgs e)
        {
            TlvEditTiles.UpdateTile(0, PtvTileSelector.SelectedTiles[0]);
            TlvEditTiles.UpdateTile(1, PtvTileSelector.SelectedTiles[1]);
            TlvEditTiles.UpdateTile(2, PtvTileSelector.SelectedTiles[2]);
            TlvEditTiles.UpdateTile(3, PtvTileSelector.SelectedTiles[3]);
        }

        void PslView_SelectedIndexChanged(object sender, EventArgs e)
        {
            PtvTileSelector.PaletteIndex = PslView.SelectedIndex;
            TlvEditTiles.PaletteIndex = PslView.SelectedIndex;
        }

        private void CmbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PtvTileSelector.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            PslView.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            TlvEditTiles.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
        }

        private void CmbGraphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectionMode == SelectionMode.Full)
            {
                if (CmbGraphics1.SelectedIndex > 252)
                {
                    CmbGraphics1.SelectedIndex = 252;
                    return;
                }
            }
            else if (SelectionMode == SelectionMode.Half)
            {
                if (CmbGraphics1.SelectedIndex > 254)
                {
                    CmbGraphics1.SelectedIndex = 254;
                    return;
                }
            }

            GraphicsInfo gi = CmbGraphics1.SelectedItem as GraphicsInfo;
            TxtGName1.Text = gi.Name;
            CompositeTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[gi.Bank]);

            if (SelectionMode == SelectionMode.Full)
            {
                CmbGraphics2.SelectedIndex = CmbGraphics1.SelectedIndex + 1;
                CmbGraphics3.SelectedIndex = CmbGraphics1.SelectedIndex + 2;
                CmbGraphics4.SelectedIndex = CmbGraphics1.SelectedIndex + 3;
            }
            else if (SelectionMode == SelectionMode.Half)
            {
                CmbGraphics2.SelectedIndex = CmbGraphics1.SelectedIndex + 1;
            }

            PtvTileSelector.UpdateSelection();
            LblHexGraphics1.Text = "x" + CmbGraphics1.SelectedIndex.ToHexString();
        }

        private void CmbGraphics2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics2.SelectedItem as GraphicsInfo;
            TxtGName2.Text = gi.Name;
            CompositeTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[gi.Bank]);
            PtvTileSelector.UpdateSelection();
            LblHexGraphics2.Text = "x" + CmbGraphics2.SelectedIndex.ToHexString();
        }

        private void CmbGraphics3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectionMode == SelectionMode.Half)
            {
                if (CmbGraphics3.SelectedIndex > 254)
                {
                    CmbGraphics3.SelectedIndex = 254;
                    return;
                }
            }

            GraphicsInfo gi = CmbGraphics3.SelectedItem as GraphicsInfo;
            TxtGName3.Text = gi.Name;
            CompositeTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[gi.Bank]);

            if (SelectionMode == SelectionMode.Half)
            {
                CmbGraphics4.SelectedIndex = CmbGraphics3.SelectedIndex + 1;
            }
            PtvTileSelector.UpdateSelection();
            LblHexGraphics3.Text = "x" + CmbGraphics3.SelectedIndex.ToHexString();
        }

        private void CmbGraphics4_SelectedIndexChanged(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics4.SelectedItem as GraphicsInfo;
            TxtGName4.Text = gi.Name;
            CompositeTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[gi.Bank]);
            PtvTileSelector.UpdateSelection();
            LblHexGraphics4.Text = "x" + CmbGraphics4.SelectedIndex.ToHexString();
        }


        private void UpdateGraphicsNames(GraphicsInfo gi)
        {
            bool reselect = false;
            int index = 0;
            index = CmbGraphics1.Items.IndexOf(gi);
            reselect = CmbGraphics1.SelectedIndex == index;
            CmbGraphics1.Items.RemoveAt(index);
            CmbGraphics1.Items.Insert(index, gi);
            if (reselect) CmbGraphics1.SelectedIndex = index;

            index = CmbGraphics2.Items.IndexOf(gi);
            reselect = CmbGraphics2.SelectedIndex == index;
            CmbGraphics2.Items.RemoveAt(index);
            CmbGraphics2.Items.Insert(index, gi);
            if (reselect) CmbGraphics2.SelectedIndex = index;

            index = CmbGraphics3.Items.IndexOf(gi);
            reselect = CmbGraphics3.SelectedIndex == index;
            CmbGraphics3.Items.RemoveAt(index);
            CmbGraphics3.Items.Insert(index, gi);
            if (reselect) CmbGraphics3.SelectedIndex = index;

            index = CmbGraphics4.Items.IndexOf(gi);
            reselect = CmbGraphics4.SelectedIndex == index;
            CmbGraphics4.Items.RemoveAt(index);
            CmbGraphics4.Items.Insert(index, gi);
            if (reselect) CmbGraphics4.SelectedIndex = index;
        }

        private void BtnRename1_Click(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics1.SelectedItem as GraphicsInfo;
            gi.Name = TxtGName1.Text;
            UpdateGraphicsNames(gi);
        }

        private void BtnRename2_Click(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics2.SelectedItem as GraphicsInfo;
            gi.Name = TxtGName2.Text;
            UpdateGraphicsNames(gi);
        }

        private void BtnRename3_Click(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics3.SelectedItem as GraphicsInfo;
            gi.Name = TxtGName3.Text;
            UpdateGraphicsNames(gi);
        }

        private void BtnRename4_Click(object sender, EventArgs e)
        {
            GraphicsInfo gi = CmbGraphics4.SelectedItem as GraphicsInfo;
            gi.Name = TxtGName4.Text;
            UpdateGraphicsNames(gi);
        }

        private void RdoQuarter_CheckedChanged(object sender, EventArgs e)
        {
            TxtGName1.Enabled = BtnRename1.Enabled = TxtGName2.Enabled = BtnRename2.Enabled = TxtGName3.Enabled = BtnRename3.Enabled = TxtGName4.Enabled = BtnRename4.Enabled = CmbGraphics1.Enabled = CmbGraphics2.Enabled = CmbGraphics3.Enabled = CmbGraphics4.Enabled = true;
            SelectionMode = SelectionMode.Quarter;
        }

        private void RdoHalf_CheckedChanged(object sender, EventArgs e)
        {
            TxtGName1.Enabled = BtnRename1.Enabled = TxtGName3.Enabled = BtnRename3.Enabled = CmbGraphics1.Enabled = CmbGraphics3.Enabled = true;
            TxtGName2.Enabled = BtnRename2.Enabled = TxtGName4.Enabled = BtnRename4.Enabled = CmbGraphics2.Enabled = CmbGraphics4.Enabled = false;
            SelectionMode = SelectionMode.Half;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CmbGraphics1.Enabled = true;
            TxtGName2.Enabled = BtnRename2.Enabled = TxtGName3.Enabled = BtnRename3.Enabled = TxtGName4.Enabled = BtnRename4.Enabled = CmbGraphics2.Enabled = CmbGraphics3.Enabled = CmbGraphics4.Enabled = false;
            SelectionMode = SelectionMode.Full;
        }

        private void RdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            PtvTileSelector.ArrangementMode = ArrangementMode.Normal;
            PtvTileSelector.UpdateSelection();
        }

        private void RdoSixteen_CheckedChanged(object sender, EventArgs e)
        {
            PtvTileSelector.ArrangementMode = ArrangementMode.Map16;
            PtvTileSelector.UpdateSelection();
        }

        private void ChkTileGrid_CheckedChanged(object sender, EventArgs e)
        {
            TlvEditTiles.ShowGrid = ChkTileGrid.Checked;
        }

        private void ChkTableGrid_CheckedChanged(object sender, EventArgs e)
        {
            PtvTileSelector.ShowGrid = ChkTableGrid.Checked;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (!ProjectController.GraphicsManager.SaveGraphics(ProjectController.RootDirectory + @"\" + ProjectController.ProjectName + ".chr"))
            {
                if (MessageBox.Show("The graphics file has been modified by an outside program since loading them in Reuben. Are you sure you want to overwrite them?", "Modification Alert", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ProjectController.GraphicsManager.SaveGraphics(ProjectController.RootDirectory + @"\" + ProjectController.ProjectName + ".chr", true, true);
                }
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void ShowDialog(int graphics1, int graphics2, int palette)
        {
            RdoHalf.Checked = true;
            CmbGraphics1.SelectedIndex = graphics1;
            CmbGraphics3.SelectedIndex = graphics2;
            CmbPalettes.SelectedIndex = palette;
            this.ShowDialog();
        }
    }

    public enum SelectionMode
    {
        Quarter,
        Half,
        Full
    }
}
