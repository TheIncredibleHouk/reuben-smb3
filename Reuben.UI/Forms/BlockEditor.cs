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

        public void Initialize(LevelController levels, GraphicsController graphics)
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
            paletteList.Palettes = graphics.GraphicsData.Palettes;
            patternTable.ColorReference = blockList.ColorReference = blockView.ColorReference = paletteList.ColorReference = graphics.GraphicsData.Colors;
            paletteList.UpdateList();
            delayUpdate = false;
        }


        private bool delayUpdate;
        private void graphics1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!delayUpdate)
            {
                blockList.Palette = patternTable.Palette = paletteList.SelectedPalette;
                blockList.PatternTable = patternTable.PatternTable = gfxController.MakePatternTable(new List<int>() { graphics1.SelectedIndex, graphics1.SelectedIndex + 1, graphics2.SelectedIndex, graphics2.SelectedIndex + 1 });
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
            graphics2.SelectedIndex = 0xC0;
            delayUpdate = false;
            paletteList.SelectedPalette = gfxController.GraphicsData.Palettes.Where(p => p.ID == LocalLevelTypes[levelTypes.SelectedIndex].DefaultPaletteID).FirstOrDefault();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LocalLevelTypes[levelTypes.SelectedIndex].Name = typeName.Text;
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
            LocalLevelTypes[levelTypes.SelectedIndex].DefaultPaletteID = paletteList.SelectedPalette.ID;
        }
    }
}
