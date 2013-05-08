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
        private Dictionary<int, BlockProperty> solidityMap = new Dictionary<int, BlockProperty>();

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
            solidityMap[0] = BlockProperty.Background;
            solidityMap[1] = BlockProperty.Foreground;
            solidityMap[2] = BlockProperty.Water;
            solidityMap[3] = BlockProperty.Water | BlockProperty.Foreground;
            solidityMap[4] = BlockProperty.SolidTop;
            solidityMap[5] = BlockProperty.SolidBottom;
            solidityMap[6] = BlockProperty.SolidAll;
            solidityMap[7] = BlockProperty.CoinBlock;
            for (int i = 0; i < 16; i++)
            {
                SpecialTypes.Add(((BlockProperty)(0xF0 | i)).ToString());
            }
        }

        private List<string> NotSolidInteractionTypes = new List<string>()
        {
            "No Interaction",
            "Harmful",
            "Deplete Air",
            "Current Left",
            "Current Right",
            "Current Up",
            "Current Down",
            "Unused",
            "Unused",
            "Unused",
            "Unused",
            "Climbable",
            "Coin",
            "Door",
            "Cherry",
            "Unsued"
        };
        private List<string> SolidInteractionTypes = new List<string>()
        {
            "No Interaction",
            "Harmful",
            "Slick",
            "Conveyor Left",
            "Conveyor Right",
            "Conveyor Up",
            "Conveyor Down",
            "Unstable",
            "Vertical Pipe Left",
            "Vertical Pipe Right",
            "Horizontal Pipe Bottom",
            "Unused",
            "Unused",
            "Stone",
            "PSwitch",
            "Unused",
        };

        private List<string> MapInteractionTypes = new List<string>()
        {
            "Boundary",
            "Traversable",
            "Enterble and Traversable",
        };

        private List<string> SpecialTypes = new List<string>();

        private bool updating;
        private void UpdateInteractionSpecialList()
        {
            updating = true;
            if (CmbDefinitions.SelectedIndex == 0)
            {
                CmdInteraction.DataSource = MapInteractionTypes;
                LblSolidity.Visible = CmbSolidity.Visible = false;
            }
            else if (CmbSolidity.SelectedIndex == 7)
            {
                CmdInteraction.DataSource = SpecialTypes;
                LblSolidity.Visible = CmbSolidity.Visible = true;
            }
            else if (CmbSolidity.SelectedIndex <= 3)
            {
                CmdInteraction.DataSource = NotSolidInteractionTypes;
                LblSolidity.Visible = CmbSolidity.Visible = true;
            }

            else
            {
                CmdInteraction.DataSource = SolidInteractionTypes;
                LblSolidity.Visible = CmbSolidity.Visible = true;
            }

            updating = false;
            var b = (int)(BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Cherry);
            if (b >= CmbSolidity.Items.Count)
            {
                b = 0;
            }

            CmdInteraction.SelectedIndex = b;
        }

        void BlsBlocks_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            BlvCurrent.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            BlvCurrent.CurrentBlock = BlsBlocks.SelectedBlock;
            PtvTable.PaletteIndex = BlsBlocks.SelectedIndex / 0x40;
            LblBlockSelected.Text = "Selected: " + BlsBlocks.SelectedIndex.ToHexString();
            if (BlvCurrent.CurrentBlock != null)
            {
                CmbSolidity.SelectedIndex = solidityMap.Values.ToList().IndexOf(BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.MaskHi);
                if (CmbSolidity.SelectedIndex == -1)
                {
                    CmbSolidity.SelectedIndex = 0;
                }

                int b = (int)(BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Cherry); ;
                if (b > CmdInteraction.Items.Count)
                {
                    b = 0;
                }
                CmdInteraction.SelectedIndex = b;
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
            if (CmbDefinitions.SelectedIndex != 0)
            {
                FillBlockForTransitions();
                LoadBlockTransitions();
            }
        }

        private void FillBlockForTransitions()
        {

            fbF1.Items.Clear();
            fbF2.Items.Clear();
            fbF3.Items.Clear();
            fbF4.Items.Clear();
            fbT1.Items.Clear();
            fbT2.Items.Clear();
            fbT3.Items.Clear();
            fbT4.Items.Clear();
            ibF1.Items.Clear();
            ibF2.Items.Clear();
            ibF3.Items.Clear();
            ibF4.Items.Clear();
            ibT1.Items.Clear();
            ibT2.Items.Clear();
            ibT3.Items.Clear();
            ibT4.Items.Clear();
            psF1.Items.Clear();
            psF2.Items.Clear();
            psF3.Items.Clear();
            psF4.Items.Clear();
            psF5.Items.Clear();
            psF6.Items.Clear();
            psF7.Items.Clear();
            psF8.Items.Clear();
            psT1.Items.Clear();
            psT2.Items.Clear();
            psT3.Items.Clear();
            psT4.Items.Clear();
            psT5.Items.Clear();
            psT6.Items.Clear();
            psT7.Items.Clear();
            psT8.Items.Clear();
            pSwitchTile.Items.Clear();
            vineTile.Items.Clear();
            for (int i = 0; i < 256; i++)
            {
                fbF1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbF2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbF3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbF4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbT1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbT2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbT3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                fbT4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibF1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibF2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibF3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibF4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibT1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibT2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibT3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                ibT4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF5.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF6.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF7.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psF8.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT1.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT2.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT3.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT4.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT5.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT6.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT7.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                psT8.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                vineTile.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
                pSwitchTile.Items.Add(i.ToHexString() + " - " + BlsBlocks.CurrentDefiniton[i].Description);
            }
        }

        private void LoadBlockTransitions()
        {
            fbF1.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[0].FromValue;
            fbF2.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[1].FromValue;
            fbF3.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[2].FromValue;
            fbF4.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[3].FromValue;
            fbT1.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[0].ToValue;
            fbT2.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[1].ToValue;
            fbT3.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[2].ToValue;
            fbT4.SelectedIndex = BlsBlocks.CurrentDefiniton.FireBallTransitions[3].ToValue;
            ibF1.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[0].FromValue;
            ibF2.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[1].FromValue;
            ibF3.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[2].FromValue;
            ibF4.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[3].FromValue;
            ibT1.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[0].ToValue;
            ibT2.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[1].ToValue;
            ibT3.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[2].ToValue;
            ibT4.SelectedIndex = BlsBlocks.CurrentDefiniton.IceBallTransitions[3].ToValue;
            psF1.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[0].FromValue;
            psF2.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[1].FromValue;
            psF3.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[2].FromValue;
            psF4.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[3].FromValue;
            psF5.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[4].FromValue;
            psF6.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[5].FromValue;
            psF7.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[6].FromValue;
            psF8.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[7].FromValue;
            psT1.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[0].ToValue;
            psT2.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[1].ToValue;
            psT3.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[2].ToValue;
            psT4.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[3].ToValue;
            psT5.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[4].ToValue;
            psT6.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[5].ToValue;
            psT7.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[6].ToValue;
            psT8.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTransitions[7].ToValue;
            vineTile.SelectedIndex = BlsBlocks.CurrentDefiniton.VineTile;
            pSwitchTile.SelectedIndex = BlsBlocks.CurrentDefiniton.PSwitchTile;
        }

        private void CommitBlockTransitions()
        {
            BlsBlocks.CurrentDefiniton.FireBallTransitions[0].FromValue = fbF1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[1].FromValue = fbF2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[2].FromValue = fbF3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[3].FromValue = fbF4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[0].ToValue = fbT1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[1].ToValue = fbT2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[2].ToValue = fbT3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.FireBallTransitions[3].ToValue = fbT4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[0].FromValue = ibF1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[1].FromValue = ibF2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[2].FromValue = ibF3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[3].FromValue = ibF4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[0].ToValue = ibT1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[1].ToValue = ibT2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[2].ToValue = ibT3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.IceBallTransitions[3].ToValue = ibT4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[0].FromValue = psF1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[1].FromValue = psF2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[2].FromValue = psF3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[3].FromValue = psF4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[4].FromValue = psF5.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[5].FromValue = psF6.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[6].FromValue = psF7.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[7].FromValue = psF8.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[0].ToValue = psT1.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[1].ToValue = psT2.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[2].ToValue = psT3.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[3].ToValue = psT4.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[4].ToValue = psT5.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[5].ToValue = psT6.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[6].ToValue = psT7.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTransitions[7].ToValue = psT8.SelectedIndex;
            BlsBlocks.CurrentDefiniton.VineTile = (byte)vineTile.SelectedIndex;
            BlsBlocks.CurrentDefiniton.PSwitchTile = (byte)pSwitchTile.SelectedIndex;
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
            CommitBlockTransitions();
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
                    TSAToolTip.SetToolTip(BlsBlocks, ProjectController.BlockManager.GetBlockString(defIndex, index));
                }

                else
                {
                    TSAToolTip.SetToolTip(BlsBlocks, ProjectController.BlockManager.GetBlockString(defIndex, index));
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

            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) > Keys.None)
            {
                if (copyBlock == null) return;
                if ((e.Modifiers & Keys.Shift) == Keys.None)
                {
                    BlvCurrent.SetTile(0, 0, copyBlock[0, 0]);
                    BlvCurrent.SetTile(1, 0, copyBlock[1, 0]);
                    BlvCurrent.SetTile(0, 1, copyBlock[0, 1]);
                    BlvCurrent.SetTile(1, 1, copyBlock[1, 1]);
                    BlockDescription.Text = BlvCurrent.CurrentBlock.Description = copyBlock.Description;
                }

                BlvCurrent.CurrentBlock.BlockProperty = copyBlock.BlockProperty;
                BlsBlocks_SelectionChanged(null, null);
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
                    b.BlockProperty = BlvCurrent.CurrentBlock.BlockProperty;
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
            BlsBlocks.ShowBlockSolidity = ChkBlockProperties.Checked;
        }

        private void SpecialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                BlsBlocks.SelectedBlock.BlockProperty = (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.MaskHi) | (BlockProperty)CmdInteraction.SelectedIndex;
                BlsBlocks.UpdateSelection();
            }
        }

        private void BlockDescription_TextChanged(object sender, EventArgs e)
        {
            BlsBlocks.SelectedBlock.Description = BlockDescription.Text;
        }

        private void CmbSolidity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlsBlocks.SelectedBlock.BlockProperty = solidityMap[CmbSolidity.SelectedIndex] | (BlvCurrent.CurrentBlock.BlockProperty & BlockProperty.Cherry);
            UpdateInteractionSpecialList();
            BlsBlocks.UpdateSelection();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommitBlockTransitions();
        }
    }
}
