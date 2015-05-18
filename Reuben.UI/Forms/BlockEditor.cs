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
        public List<LevelType> LocalLevelTypes { get; private set; }

        public void Initialize(LevelController levels, GraphicsController graphics, int selectedLevelType = 1)
        {
            LocalLevelTypes = levels.LevelData.Types.MakeCopy();
            gfxController = graphics;

            delayUpdate = true;
            for (int i = 0; i < 256; i++)
            {
                graphics1.Items.Add(i.ToString("X2"));
                graphics2.Items.Add(i.ToString("X2"));
            }
            levelTypes.Items.AddRange(LocalLevelTypes.Select(l => l.Name ?? "").ToArray());
            paletteList.Palettes = graphics.GraphicsData.Palettes.OrderBy(p => p.Name.ToLower()).ToList();
            patternTable.ColorReference = blockList.ColorReference = blockView.ColorReference = paletteList.ColorReference = graphics.GraphicsData.Colors;
            paletteList.UpdateList();
            delayUpdate = false;
            levelTypes.SelectedIndex = selectedLevelType;
            blockView.Block = LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock];
            blockView.UpdateGraphics();
            this.blockList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blockList_MouseDown);
        }


        private bool delayUpdate;
        private void graphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!delayUpdate && graphics1.SelectedIndex != -1 && graphics2.SelectedIndex != -1)
            {
                blockView.Palette = blockList.Palette = patternTable.Palette = paletteList.SelectedPalette;
                blockView.PatternTable = blockList.PatternTable = patternTable.PatternTable = gfxController.MakePatternTable(new List<int>() { graphics1.SelectedIndex, graphics1.SelectedIndex + 1, graphics2.SelectedIndex, graphics2.SelectedIndex + 1 });
                blockList.UpdateGraphics();
                patternTable.UpdateGraphics();
            }
        }

        private void levelTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            delayUpdate = true;
            typeName.Text = LocalLevelTypes[levelTypes.SelectedIndex].Name;
            blockList.BlockList = LocalLevelTypes[levelTypes.SelectedIndex].Blocks;
            graphics1.SelectedIndex = LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID;
            graphics2.SelectedIndex = LocalLevelTypes[levelTypes.SelectedIndex].DefaultGraphicsID2;
            delayUpdate = false;
            paletteList.SelectedPalette = gfxController.GraphicsData.Palettes.Where(p => p.ID == LocalLevelTypes[levelTypes.SelectedIndex].DefaultPaletteID).FirstOrDefault();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LocalLevelTypes[levelTypes.SelectedIndex].Name = typeName.Text;
            int oldIndex = levelTypes.SelectedIndex;
            delayUpdate= true;
            levelTypes.Items[levelTypes.SelectedIndex] = typeName.Text;
            levelTypes.SelectedIndex = oldIndex;
            delayUpdate = false;
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

        private void blockList_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void blockList_SelectedBlockChanged(object sender, EventArgs e)
        {
            blockView.Block = LocalLevelTypes[levelTypes.SelectedIndex].Blocks[blockList.SelectedBlock];
            patternTable.PaletteIndex = blockView.PaletteIndex = (blockList.SelectedBlock & 0xC0) >> 6;
            patternTable.UpdateGraphics();
            blockView.UpdateGraphics();
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
    }
}
