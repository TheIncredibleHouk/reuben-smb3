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
    public partial class Map16Editor : Form
    {
        PatternTable CurrentTable;
        public Map16Editor()
        {
            InitializeComponent();

            CmbGraphics1.DisplayMember = CmbGraphics2.DisplayMember = CmbPalettes.DisplayMember = CmbDefinitions.DisplayMember = "Name";
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

            CurrentTable = ProjectController.GraphicsManager.BuildPatternTable(0);
            PtvTable.CurrentTable = CurrentTable;
            BlsBlocks.CurrentTable = CurrentTable;
            BlsBlocks.BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];
            BlvCurrent.CurrentTable = CurrentTable;
            BlsBlocks.SpecialTable = ProjectController.SpecialManager.SpecialTable;
            BlsBlocks.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;

            CmbGraphics1.SelectedIndex = 8;
            CmbGraphics2.SelectedIndex = 0x64;
            CmbPalettes.SelectedIndex = 0;
            CmbDefinitions.SelectedIndex = 0;
            BlsBlocks.SelectionChanged += new EventHandler<TEventArgs<MouseButtons>>(BlsBlocks_SelectionChanged);
            BlsBlocks.SelectedIndex = 0;
        }

        void BlsBlocks_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            BlvCurrent.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            BlvCurrent.CurrentBlock = BlsBlocks.SelectedBlock;
            PtvTable.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            LblBlockSelected.Text = "Selected: " + BlsBlocks.SelectedIndex.ToHexString();
            if (BlvCurrent.CurrentBlock != null)
            {
                BlockProp1.Checked = (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Solid) > 0;
                BlockProp2.Checked = (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.SolidTop) > 0;
                BlockProp3.Checked = (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Water) > 0;
                BlockProp5.Checked = (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Foreground) > 0;
                SpecialList.SelectedIndex = (int)(BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.MaskLow);
                BlockDescription.Text = BlsBlocks.SelectedBlock.Description;
            }
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
            PtvTable.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            BlsBlocks.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            PlsView.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            BlvCurrent.CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
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
            if (x < 0 || y < 0 || x > 2 || y > 2) return;
            BlvCurrent.SetTile(x, y, (byte)PtvTable.SelectedIndex);
            BlvCurrent.Focus();
            BlsBlocks.UpdateSelection();
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
            ProjectController.BlockManager.SaveBlockStrings(ProjectController.RootDirectory + @"\strings.xml");
            this.Close();
        }

        private void ChkShowSpecials_CheckedChanged(object sender, EventArgs e)
        {
            BlsBlocks.ShowSpecialBlocks = ChkShowSpecials.Checked;
        }

        public void ShowDialog(int definitionIndex, int selectedTileIndex, int graphics1, int graphics2, int paletteIndex)
        {
            CmbDefinitions.SelectedIndex = definitionIndex;
            CmbGraphics1.SelectedIndex = graphics1;
            CmbGraphics2.SelectedIndex = graphics2;
            CmbPalettes.SelectedIndex = paletteIndex;
            BlsBlocks.SelectedTileIndex = selectedTileIndex;
            BlsBlocks.Focus();
            this.ShowDialog();
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
                    TSAToolTip.SetToolTip(BlsBlocks, ProjectController.BlockManager.GetBlockString(defIndex, index) + "\n(" + (index).ToHexString() + ")\n" + ProjectController.BlockManager.AllDefinitions[CmbDefinitions.SelectedIndex][index].BlockProperty);
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
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if (cForm.Confirm("Are you sure you want to apply this definiton to block " + BlsBlocks.SelectedTileIndex.ToHexString() + " in every definition set?"))
            {
                bool first = true;
                foreach (BlockDefinition bDef in ProjectController.BlockManager.AllDefinitions)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    Block b = bDef[BlsBlocks.SelectedTileIndex];
                    b[0, 0] = BlvCurrent.CurrentBlock[0, 0];
                    b[0, 1] = BlvCurrent.CurrentBlock[0, 1];
                    b[1, 0] = BlvCurrent.CurrentBlock[1, 0];
                    b[1, 1] = BlvCurrent.CurrentBlock[1, 1];
                    b.Description = BlockDescription.Text;
                    b.BlockProperty = (BlockProp1.Checked ? BlockProperty.Solid : BlockProperty.Background) |
                        (BlockProp2.Checked ? BlockProperty.SolidTop : BlockProperty.Background) |
                        (BlockProp3.Checked ? BlockProperty.Foreground : BlockProperty.Background) |
                        (BlockProp5.Checked ? BlockProperty.Water : BlockProperty.Background) |
                        ((BlockProperty)((int)(BlsBlocks.SelectedBlock.BlockProperty & BlockProperty.MaskHi) | SpecialList.SelectedIndex));
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

        private void ChkBlockProperties_CheckedChanged(object sender, EventArgs e)
        {
            BlsBlocks.ShowBlockProperties = ChkBlockProperties.Checked;
        }

        private void BlockProp1_CheckedChanged(object sender, EventArgs e)
        {
            SetBlockProperty(BlockProperty.Solid, BlockProp1.Checked);
        }

        private void SetBlockProperty(BlockProperty b, bool value)
        {
            BlockProperty bp = BlsBlocks.SelectedBlock.BlockProperty;
            bp = bp | b;
            if (!value)
            {
                bp = bp ^ b;
            }

            BlsBlocks.SelectedBlock.BlockProperty = bp;
            BlsBlocks.UpdateSelection();
        }

        private void BlockProp2_CheckedChanged(object sender, EventArgs e)
        {
            SetBlockProperty(BlockProperty.SolidTop, BlockProp2.Checked);
        }

        private void BlockProp3_CheckedChanged(object sender, EventArgs e)
        {
            SetBlockProperty(BlockProperty.Water, BlockProp3.Checked);
        }

        private void BlockProp5_CheckedChanged(object sender, EventArgs e)
        {
            SetBlockProperty(BlockProperty.Foreground, BlockProp5.Checked);
        }

        private void SpecialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlsBlocks.SelectedBlock.BlockProperty = (BlockProperty)((int)(BlsBlocks.SelectedBlock.BlockProperty & BlockProperty.MaskHi) | SpecialList.SelectedIndex);
            BlsBlocks.UpdateSelection();
        }

        private void BlockDescription_TextChanged(object sender, EventArgs e)
        {
            BlsBlocks.SelectedBlock.Description = BlockDescription.Text;
        }
    }
}
