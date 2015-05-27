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
            }
        }

        void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {

            spriteSelector.Update(palette: paletteList.SelectedPalette);
        }
    }
}
