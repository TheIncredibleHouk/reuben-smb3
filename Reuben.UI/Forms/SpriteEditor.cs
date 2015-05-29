using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;
using Reuben.NESGraphics;

namespace Reuben.UI
{
    public partial class SpriteEditor : Form
    {
        public SpriteEditor()
        {
            InitializeComponent();
        }

        private SpriteController localSpriteController;
        public void Initialize(GraphicsController graphicsController, SpriteController spriteController, LevelController levelController)
        {
            localSpriteController = spriteController;
            spriteSelector.Initialize(graphicsController, spriteController, graphicsController.GraphicsData.Colors, graphicsController.GraphicsData.Palettes[0]);
            spriteViewer.Initialize(graphicsController, spriteController, graphicsController.GraphicsData.Colors, graphicsController.GraphicsData.Palettes[0], levelController.LevelData.OverlayPalette);
            paletteList.Palettes = graphicsController.GraphicsData.Palettes;
            paletteList.ColorReference = graphicsController.GraphicsData.Colors;
            paletteList.UpdateList();
            paletteList.SelectedIndexChanged += paletteList_SelectedIndexChanged;
            spriteSelector.SelectedSpriteChanged += spriteSelector_SelectedSpriteChanged;
        }

        void spriteSelector_SelectedSpriteChanged(object sender, EventArgs e)
        {
            if (spriteSelector.SelectedSprite != null)
            {
                spriteViewer.CurrentDefinition = localSpriteController.GetDefinition(spriteSelector.SelectedSprite.ObjectID);
                editorPropertyList.Text = String.Join("\r\n", spriteViewer.CurrentDefinition.PropertyDescriptions);
                displayProperty.Items.Clear();
                displayProperty.Items.AddRange(spriteViewer.CurrentDefinition.PropertyDescriptions.ToArray());
                if (displayProperty.Items.Count > 0)
                {
                    displayProperty.SelectedIndex = 0;
                }
            }
        }

        void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteSelector.Update(palette: paletteList.SelectedPalette);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            spriteViewer.DisplaySpecialTiles = checkBox1.Checked;
            spriteViewer.UpdateGraphics();
        }

        private void displayProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteViewer.Property = displayProperty.SelectedIndex;
            spriteViewer.UpdateGraphics();
        }

        private void editorPropertyList_TextChanged(object sender, EventArgs e)
        {
            spriteViewer.CurrentDefinition.PropertyDescriptions = editorPropertyList.Text.Split('\n').Select(j => j.Trim()).ToList();
            int oldSelection = displayProperty.SelectedIndex;
            displayProperty.Items.Clear();
            displayProperty.Items.AddRange(spriteViewer.CurrentDefinition.PropertyDescriptions.ToArray());
            if(oldSelection >= -1 && displayProperty.Items.Count > 0)
            {
                if (oldSelection < displayProperty.Items.Count - 1)
                {
                    displayProperty.SelectedIndex = oldSelection;
                }
                else
                {
                    displayProperty.SelectedIndex = 0;
                }
            }
            else if(displayProperty.Items.Count > 0)
            {
                displayProperty.SelectedIndex = 0;
            }
        }
    }
}
