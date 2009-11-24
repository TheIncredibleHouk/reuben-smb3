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
    public partial class SpecialEditor : Form
    {
        PatternTable CurrentTable;
        public SpecialEditor()
        {
            InitializeComponent();

            CmbDefinitions.DisplayMember = "Name";
           
            foreach (var l in ProjectController.LevelManager.LevelTypes)
            {
                CmbDefinitions.Items.Add(l);
            }

            CurrentTable = ProjectController.SpecialManager.SpecialTable;
            PtvTable.CurrentTable = CurrentTable;
            BlsBlocks.CurrentTable = CurrentTable;
            BlsBlocks.BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];
            BlvCurrent.CurrentTable = CurrentTable;
            BlsBlocks.SpecialTable = ProjectController.SpecialManager.SpecialTable;
            PtvTable.CurrentPalette = BlsBlocks.CurrentPalette = ProjectController.SpecialManager.SpecialPalette;
            
            CmbDefinitions.SelectedIndex = 0;
            BlsBlocks.SelectionChanged += new EventHandler<TEventArgs<MouseButtons>>(BlsBlocks_SelectionChanged);
        }

        void BlsBlocks_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            BlvCurrent.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            BlvCurrent.CurrentBlock = BlsBlocks.SelectedBlock;
            PtvTable.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            LblBlockSelected.Text = "Selected: " + BlsBlocks.SelectedIndex.ToHexString();
        }

        private void CmbDefinitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlsBlocks.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(CmbDefinitions.SelectedIndex);
            BlsBlocks.SpecialDefnitions = ProjectController.SpecialManager.GetSpecialDefinition(CmbDefinitions.SelectedIndex);
        }

        private void BlvCurrent_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if(x < 0 || y < 0 || x > 2 || y > 2) return;
            BlvCurrent.SetTile(x, y, (byte) PtvTable.SelectedIndex);
            BlvCurrent.Focus();
        }

        private void RdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            PtvTable.ArrangementMode = ArrangementMode.Normal;
        }

        private void RdoMap16_CheckedChanged(object sender, EventArgs e)
        {
            PtvTable.ArrangementMode = ArrangementMode.Map16;
        }

        private void BtnSaveClose_Click(object sender, EventArgs e)
        {
            ProjectController.BlockManager.SaveDefinitions(ProjectController.RootDirectory + @"\" + ProjectController.ProjectName + ".tsa");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int PreviousBlockX, PreviousBlockY;
        private void BlsBlocks_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            if (PreviousBlockX == x && PreviousBlockY == y) return;
            PreviousBlockX = x;
            PreviousBlockY = y;
            int index = (y * 16) + x;
            int defIndex = CmbDefinitions.SelectedIndex;

            if (index > -1 && index < 256)
            {
                if (defIndex == 0)
                {
                    TSAToolTip.SetToolTip(BlsBlocks, ProjectController.BlockManager.GetBlockString(defIndex, index) + "\n(" + (index).ToHexString() + ")");
                }

                else
                {
                    TSAToolTip.SetToolTip(BlsBlocks, ProjectController.BlockManager.GetBlockString(defIndex, index) + "\n(" + (index).ToHexString() + ")\n" + ProjectController.SpecialManager.GetProperty(defIndex, index));
                }
            }
        }

        private void BlsBlocks_MouseDown(object sender, MouseEventArgs e)
        {
            BlsBlocks.Focus();
        }

        Block copyBlock = null;
        private void BlsBlocks_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                copyBlock = BlsBlocks.SelectedBlock;
            }

            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (copyBlock == null) return;
                BlvCurrent.SetTile(0, 0, copyBlock[0, 0]);
                BlvCurrent.SetTile(1, 0, copyBlock[1, 0]);
                BlvCurrent.SetTile(0, 1, copyBlock[0, 1]);
                BlvCurrent.SetTile(1, 1, copyBlock[1, 1]);
            }
        }

        private void BtnApplyGlobally_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            if (cForm.Confirm("Are you sure you want to apply this definiton to block " + BlsBlocks.SelectedTileIndex.ToHexString() + " in every definition set?"))
            {
                foreach (BlockDefinition bDef in ProjectController.BlockManager.AllDefinitions)
                {
                    bDef[BlsBlocks.SelectedTileIndex][0, 0] = BlvCurrent.CurrentBlock[0, 0];
                    bDef[BlsBlocks.SelectedTileIndex][0, 1] = BlvCurrent.CurrentBlock[0, 1];
                    bDef[BlsBlocks.SelectedTileIndex][1, 0] = BlvCurrent.CurrentBlock[1, 0];
                    bDef[BlsBlocks.SelectedTileIndex][1, 1] = BlvCurrent.CurrentBlock[1, 1];
                }
            }
        }

        int PreviousTileX, PreviousTileY;
        private void PtvTable_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if (PreviousTileX == x && PreviousTileY == y) return;
            PreviousTileX = x;
            PreviousTileY = y;
            TSAToolTip.SetToolTip(PtvTable, ((y * 16) + x).ToHexString());
        }
    }
}
