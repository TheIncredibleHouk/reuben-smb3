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
        Block[] overlays;

        public List<LevelType> LocalLevelTypes { get; private set; }
        
        public void Initialize(LevelController levels, GraphicsController graphics, StringController strings, int selectedLevelType = 1)
        {
            LocalLevelTypes = levels.LevelData.Types.MakeCopy();
            gfxController = graphics;
            overlays = levels.LevelData.Overlays.MakeCopy();
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
            patternTable.ColorReference = blockList.ColorReference = blockView.ColorReference = paletteList.ColorReference = graphics.GraphicsData.Colors;
            paletteList.UpdateList();
            delayUpdate = false;
            levelTypes.SelectedIndex = selectedLevelType;
            blockView.Block = LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock];
            blockView.UpdateGraphics();
            blockList.SelectedBlock = 0;
        }


        private bool delayUpdate;
        private void graphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!delayUpdate && graphics1.SelectedIndex != -1 && graphics2.SelectedIndex != -1 && graphics1.Enabled && graphics2.Enabled)
            {
                blockView.Palette = blockList.Palette = patternTable.Palette = paletteList.SelectedPalette;
                blockView.PatternTable = blockList.PatternTable = patternTable.PatternTable = gfxController.MakePatternTable(new List<int>() { graphics1.SelectedIndex, graphics1.SelectedIndex + 1, graphics2.SelectedIndex, graphics2.SelectedIndex + 1 });
                blockList.UpdateGraphics();
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

                typeName.Text = "Overlays";
                blockList.BlockList = overlays;

                delayUpdate = false;
                patternTable.Palette = blockView.Palette = blockList.Palette = overlayPalette;
                patternTable.PatternTable = blockView.PatternTable = blockList.PatternTable = gfxController.MakeExtraPatternTable(new List<int>() { 4, 5, 6, 7 });
                patternTable.UpdateGraphics();
                blockView.Update();
                blockList.UpdateGraphics();
            }
            else
            {
                typeName.Enabled = setDefaultButton.Enabled = paletteList.Enabled = graphics1.Enabled = graphics2.Enabled = true;

                typeName.Text = LocalLevelTypes[levelTypes.SelectedIndex].Name;
                blockList.BlockList = LocalLevelTypes[levelTypes.SelectedIndex].Blocks;
                
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
                blockView.Block = LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock];
            }
            else
            {
                blockView.Block = overlays[blockList.SelectedBlock];
            }

            patternTable.PaletteIndex = blockView.PaletteIndex = (blockList.SelectedBlock & 0xC0) >> 6;
            patternTable.UpdateGraphics();
            blockView.UpdateGraphics();

            switch (blockView.Block.BlockSolidity)
            {
                case 0x00:
                case 0x10:
                case 0x20:
                case 0x30:
                    solidity.SelectedIndex = (blockView.Block.BlockSolidity & 0xF0) >> 4;
                    interaction.Items.Clear();
                    interaction.Items.AddRange(stringController.GetStringList("interaction").ToArray());
                    break;

                case 0x40:
                    solidity.SelectedIndex = 4;
                    interaction.Items.Clear();
                    interaction.Items.AddRange(stringController.GetStringList("solid interaction").ToArray());
                    break;

                case 0x80:
                    solidity.SelectedIndex = 5;
                    interaction.Items.Clear();
                    interaction.Items.AddRange(stringController.GetStringList("item box").ToArray());
                    break;

                case 0xC0:
                    solidity.SelectedIndex = 6;
                    interaction.Items.Clear();
                    interaction.Items.AddRange(stringController.GetStringList("solid interaction").ToArray());
                    break;


                case 0xF0:
                    solidity.SelectedIndex = 7;
                    interaction.Items.Clear();
                    interaction.Items.AddRange(stringController.GetStringList("item box").ToArray());
                    break;
            }

            interaction.SelectedIndex = blockView.Block.BlockInteraction & 0x0F;
        }

        private int selectedTile;
        private void patternTable_MouseDown(object sender, MouseEventArgs e)
        {
            int col = e.X / 16;
            var row = e.Y / 16;

            patternTable.SelectionRectangle = new Rectangle(col * 16, row * 16, 15, 15);
            selectedTile = col + (row * 16);
        }

        private void blockView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int col = e.X / 32;
                int row = e.Y / 32;
                if (col == 0 && row == 0)
                {
                    LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock].UpperLeft = selectedTile;
                }

                else if (col == 1 && row == 0)
                {
                    LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock].UpperRight = selectedTile;
                }

                else if (col == 0 && row == 1)
                {
                    LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock].LowerLeft = selectedTile;
                }

                else if (col == 1 && row == 1)
                {
                    LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock].LowerRight = selectedTile;
                }

                blockView.UpdateGraphics();
            }
        }

        private void solidity_SelectedIndexChanged(object sender, EventArgs e)
        {
            blockView.Block.BlockInteraction = interaction.SelectedIndex;

            switch (solidity.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    blockView.Block.BlockSolidity = (solidity.SelectedIndex << 4);
                    break;

                case 5:
                    blockView.Block.BlockSolidity = 0x80;
                    break;

                case 6:
                    blockView.Block.BlockSolidity = 0xC0;
                    break;


                case 7:
                    blockView.Block.BlockSolidity = 0xF0;
                    break;
            }
        }
    }
}
