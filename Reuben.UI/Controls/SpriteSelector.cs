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

namespace Reuben.UI
{
    public partial class SpriteSelector : UserControl
    {
        public SpriteSelector()
        {
            InitializeComponent();
            scrollPanel.MouseWheel += panel1_MouseWheel;
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            mouseCap.Location = new Point(mouseCap.Location.X, 2);
        }

        public void Initialize(GraphicsController graphicsController, SpriteController spriteController, Color[] colors, Palette palette)
        {
            localColorReference = colors;
            localGraphicsController = graphicsController;
            localSpriteController = spriteController;
            localPalette = palette;

            sprites.ColorReference = localColorReference;
            sprites.Graphics = localGraphicsController;
            sprites.Sprites = localSpriteController;
            sprites.Palette = localPalette;
            sprites.UpdateGraphics();
        }

        public void Update(Color[] colors = null, Palette palette = null)
        {
            localColorReference = colors ?? localColorReference;
            localPalette = palette ?? localPalette;

            sprites.ColorReference = localColorReference;
            sprites.Graphics = localGraphicsController;
            sprites.Sprites = localSpriteController;
            sprites.Palette = localPalette;
            sprites.UpdateGraphics();


            if (selectedSprite != null)
            {
                var newSprite = sprites.SpriteDrawBoundsCache.Where(t => t.Item1.ObjectID == selectedSprite.ObjectID).Select(t => t.Item1).FirstOrDefault();
                if (newSprite != null)
                {
                    SelectedSprite = newSprite;
                    scrollPanel.VerticalScroll.Value = selectedSprite.Y * 16 - 16;
                    mouseCap.Location = new Point(mouseCap.Location.X, 2);
                }
                else
                {
                    sprites.SelectionRectangle = Rectangle.Empty;
                }
            }
        }

        private Color[] localColorReference;
        private Palette localPalette;
        private SpriteController localSpriteController;
        private GraphicsController localGraphicsController;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LevelEditor Editor { get; set; }

        public event EventHandler SelectedSpriteChanged;
        private void SpriteSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (Editor != null)
            {
                Editor.EditMode = EditMode.Sprites;
            }
            SelectedSprite = sprites.SpriteDrawBoundsCache.Where(r => r.Item2.Contains(e.X, e.Y)).Select(r => r.Item1).FirstOrDefault();
            if (SelectedSpriteChanged != null)
            {
                SelectedSpriteChanged(this, null);
            }
        }

        private Sprite selectedSprite;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Sprite SelectedSprite
        {
            get
            {
                return selectedSprite;
            }
            set
            {
                if (value != null)
                {
                    selectedSprite = sprites.SpriteDrawBoundsCache.Where(s => s.Item1.ObjectID == value.ObjectID).Select(s => s.Item1).FirstOrDefault();
                    sprites.SelectionRectangle = localSpriteController.GetClipBounds(selectedSprite);
                }
                else
                {
                    sprites.SelectionRectangle = Rectangle.Empty;
                }
            }
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            sprites.FilterSprites(filter.Text);
            sprites.UpdateGraphics();

            if (selectedSprite != null)
            {
                var newSprite = sprites.SpriteDrawBoundsCache.Where(t => t.Item1.ObjectID == selectedSprite.ObjectID).Select(t => t.Item1).FirstOrDefault();
                if (newSprite != null)
                {
                    SelectedSprite = newSprite;
                    scrollPanel.VerticalScroll.Value = selectedSprite.Y * 16 - 16;
                    mouseCap.Location = new Point(mouseCap.Location.X, 2);
                }
                else
                {
                    sprites.SelectionRectangle = Rectangle.Empty;
                }
            }
        }

        private void sprites_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCap.Focus();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            mouseCap.Location = new Point(mouseCap.Location.X, 2);
        }
    }
}
