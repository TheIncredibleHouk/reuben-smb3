using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Controllers;
using Reuben.Model;

namespace Reuben.UI
{
    public partial class BlockEditor : Form
    {
        public BlockEditor()
        {
            InitializeComponent();
        }

        GraphicsController gfxController;
        StringController stringController;
        Palette overlayPalette;
        ProjectController localProjectController;
        public Block[] Overlays { get; set; }

        public List<LevelType> LocalLevelTypes { get; private set; }

        public void Initialize(ProjectController projectController, LevelController levels, GraphicsController graphics, StringController strings, int selectedLevelType = 1)
        {
            localProjectController = projectController;
            var overlayTable = graphics.MakeExtraPatternTable(4, 5, 6, 7);
            LocalLevelTypes = levels.LevelData.Types.MakeCopy();
            gfxController = graphics;
            Overlays = levels.LevelData.Overlays.MakeCopy();
            overlayPalette = levels.LevelData.OverlayPalette;

            delayUpdate = true;
            for (int i = 0; i < 256; i++)
            {
                graphics1.Items.Add(i.ToString("X2"));
                graphics2.Items.Add(i.ToString("X2"));
            }

            stringController = strings;
            solidity.Items.AddRange(strings.GetStringList("solidity").ToArray());

            levelTypes.Items.AddRange(LocalLevelTypes.Select(l => l.Name ?? "").ToArray());
            levelTypes.Items.Add("Overlays");

            paletteList.Palettes = graphics.GraphicsData.Palettes.OrderBy(p => p.Name.ToLower()).ToList();
            patternTable.ColorReference = paletteList.ColorReference = graphics.GraphicsData.Colors;

            blockList.Update(colors: graphics.GraphicsData.Colors, overlayBlocks: Overlays, overlayPalette: overlayPalette, overlayTable: overlayTable);
            blockView.Update(colors: graphics.GraphicsData.Colors);

            paletteList.UpdateList();
            delayUpdate = false;
            levelTypes.SelectedIndex = selectedLevelType;

            blockView.Block = selectedBlock;

            blockView.UpdateGraphics();
            blockList.SelectedBlock = 0;
        }


        public void SelectBlock(int levelType, int block)
        {
            levelTypes.SelectedIndex = levelType;
            blockList.SelectedBlock = block;
        }

        private bool delayUpdate;
        private void graphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!delayUpdate && graphics1.SelectedIndex != -1 && graphics2.SelectedIndex != -1 && graphics1.Enabled && graphics2.Enabled)
            {
                var palette = paletteList.SelectedPalette;
                var newPatternTable = gfxController.MakePatternTable(graphics1.SelectedIndex, graphics1.SelectedIndex + 1, graphics2.SelectedIndex, graphics2.SelectedIndex + 1);

                
                patternTable.Palette = palette;
                patternTable.PatternTable = newPatternTable;
                blockList.Update(palette: palette, patternTable: newPatternTable);
                blockView.Update(patternTable: newPatternTable, palette: palette);

                patternTable.UpdateGraphics();
                blockList_SelectedBlockChanged(null, null);
            }
        }

        private void levelTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            delayUpdate = true;
            if ((string)levelTypes.SelectedItem == "Overlays")
            {
                typeName.Enabled = setDefaultButton.Enabled = paletteList.Enabled = graphics1.Enabled = graphics2.Enabled = false;

                var newPatternTable = gfxController.MakeExtraPatternTable(4, 5, 6, 7);
                typeName.Text = "Overlays";
                blockList.Update(blockList: Overlays, palette: overlayPalette, patternTable: newPatternTable);

                patternTable.Palette = overlayPalette;
                patternTable.PatternTable = newPatternTable;

                blockView.Update(palette: overlayPalette, patternTable: newPatternTable);
                blockList.SelectedBlock = blockList.SelectedBlock;
                patternTable.UpdateGraphics();
                blockView.UpdateGraphics();
            }
            else
            {
                typeName.Enabled = setDefaultButton.Enabled = paletteList.Enabled = graphics1.Enabled = graphics2.Enabled = true;

                typeName.Text = LocalLevelTypes[levelTypes.SelectedIndex].Name;

                blockList.Update(blockList: LocalLevelTypes[levelTypes.SelectedIndex].Blocks);

                blockList.SelectedBlock = blockList.SelectedBlock;

                graphics1.SelectedIndex = LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID;
                graphics2.SelectedIndex = LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID2;
                delayUpdate = false;
                paletteList.SelectedPalette = gfxController.GraphicsData.Palettes.Where(p => p.ID == LocalLevelTypes[levelTypes.SelectedIndex].DefaultPaletteID).FirstOrDefault();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (typeName.Enabled)
            {
                LocalLevelTypes[levelTypes.SelectedIndex].Name = typeName.Text;
                int oldIndex = levelTypes.SelectedIndex;
                delayUpdate = true;
                levelTypes.Items[levelTypes.SelectedIndex] = typeName.Text;
                levelTypes.SelectedIndex = oldIndex;
                delayUpdate = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID = graphics1.SelectedIndex;
            LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID2 = graphics2.SelectedIndex;
            LocalLevelTypes[levelTypes.SelectedIndex].DefaultPaletteID = paletteList.SelectedPalette.ID;

        }

        private void blockList_SelectedBlockChanged(object sender, EventArgs e)
        {
            if ((string)levelTypes.SelectedItem != "Overlays")
            {
                selectedBlock = blockView.Block = LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock];
            }
            else
            {
                selectedBlock = blockView.Block = Overlays[blockList.SelectedBlock];
            }

            patternTable.PaletteIndex = blockView.PaletteIndex = (blockList.SelectedBlock & 0xC0) >> 6;
            patternTable.UpdateGraphics();
            blockView.UpdateGraphics();

            blockUpdating = true;
            switch (blockView.Block.BlockSolidity)
            {
                case 0x00:
                case 0x10:
                case 0x20:
                case 0x30:
                    solidity.SelectedIndex = (blockView.Block.BlockSolidity & 0xF0) >> 4;
                    break;

                case 0x40:
                    solidity.SelectedIndex = 4;
                    break;

                case 0x80:
                    solidity.SelectedIndex = 5;
                    break;

                case 0xC0:
                    solidity.SelectedIndex = 6;
                    break;


                case 0xF0:
                    solidity.SelectedIndex = 7;
                    break;
            }

            interaction.SelectedIndex = blockView.Block.BlockInteraction & 0x0F;
            blockUpdating = false;
        }

        private int selectedTile;
        private void patternTable_MouseDown(object sender, MouseEventArgs e)
        {
            int col = e.X / 16;
            var row = e.Y / 16;

            patternTable.SelectionRectangle = new Rectangle(col * 16, row * 16, 15, 15);
            selectedTile = col + (row * 16);
        }

        private Block selectedBlock;
        private void blockView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int col = e.X / 32;
                int row = e.Y / 32;
                if (col == 0 && row == 0)
                {
                    selectedBlock.UpperLeft = selectedTile;
                }

                else if (col == 1 && row == 0)
                {
                    selectedBlock.UpperRight = selectedTile;
                }

                else if (col == 0 && row == 1)
                {
                    selectedBlock.LowerLeft = selectedTile;
                }

                else if (col == 1 && row == 1)
                {
                    selectedBlock.LowerRight = selectedTile;
                }

                blockView.UpdateGraphics();
                blockList.UpdateBlock(blockList.SelectedBlock % 16, blockList.SelectedBlock / 16);
            }
        }

        private bool blockUpdating;
        private void solidity_SelectedIndexChanged(object sender, EventArgs e)
        {
            interaction.Items.Clear();
            switch (solidity.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    interaction.Items.AddRange(stringController.GetStringList("interaction").ToArray());
                    blockView.Block.BlockSolidity = (solidity.SelectedIndex << 4);
                    break;

                case 4:
                    interaction.Items.AddRange(stringController.GetStringList("solid interaction").ToArray());
                    blockView.Block.BlockSolidity = (solidity.SelectedIndex << 4);
                    break;

                case 5:
                    blockView.Block.BlockSolidity = 0x80;
                    interaction.Items.AddRange(stringController.GetStringList("item box").ToArray());
                    break;

                case 6:
                    blockView.Block.BlockSolidity = 0xC0;
                    interaction.Items.AddRange(stringController.GetStringList("solid interaction").ToArray());
                    break;


                case 7:
                    blockView.Block.BlockSolidity = 0xF0;
                    interaction.Items.AddRange(stringController.GetStringList("item box").ToArray());
                    break;
            }

            interaction.SelectedIndex = blockView.Block.BlockInteraction & 0x0F;
        }

        private Block[] currentBlocks
        {
            get
            {
                if ((string)levelTypes.SelectedItem != "Overlays")
                {
                    return LocalLevelTypes[levelTypes.SelectedIndex].Blocks;
                }

                return Overlays;
            }
        }

        private void blockList_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            if (m.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int col = m.X / 16;
                int row = m.Y / 16;
                Block block = currentBlocks[(col % 16) + (row * 16)];
                block.UpperLeft = blockView.Block.UpperLeft;
                block.UpperRight = blockView.Block.UpperRight;
                block.LowerLeft = blockView.Block.LowerLeft;
                block.LowerRight = blockView.Block.LowerRight;
                block.BlockInteraction = blockView.Block.BlockInteraction;
                block.BlockSolidity = blockView.Block.BlockSolidity;
                blockList.UpdateBlock(col, row);
            }
        }

        private void interaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            blockView.Block.BlockInteraction = interaction.SelectedIndex;
        }

        private void interactionOverlay_CheckedChanged(object sender, EventArgs e)
        {
            blockList.ShowInteractionOverlays = interactionOverlay.Checked;
            blockList.UpdateAll();
        }

        private void solidityOverlay_CheckedChanged(object sender, EventArgs e)
        {
            blockList.ShowSolidityOverlays = solidityOverlay.Checked;
            blockList.UpdateAll();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void goCodeTag_Click(object sender, EventArgs e)
        {
            if (Main.ASMEditor == null)
            {
                Main.ASMEditor = new ASMEditor();
                Main.ASMEditor.Initialize(localProjectController);
                Main.ASMEditor.Show();
            }
            else
            {
                Main.ASMEditor.Focus();
            }
        }
    }
}
