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
            panel1.MouseWheel += panel1_MouseWheel;
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            mouseCap.Location = new Point(mouseCap.Location.X, 2);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color[] ColorReference
        {
            get { return sprites.ColorReference; }
            set { sprites.ColorReference = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get { return sprites.Palette; }
            set { sprites.Palette = value; }
        }

        private Sprite spriteList = new Sprite();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteController Sprites
        {
            get { return sprites.Sprites; }
            set { sprites.Sprites = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsController Graphics
        {
            get { return sprites.Graphics; }
            set { sprites.Graphics = value; }
        }

        public void UpdateGraphics()
        {
            sprites.UpdateGraphics();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LevelEditor Editor { get; set; }

        private void SpriteSelector_MouseDown(object sender, MouseEventArgs e)
        {
            Editor.EditMode = EditMode.Sprites;
            SelectedSprite = sprites.SpriteDrawBoundsCache.Where(r => r.Item2.Contains(e.X, e.Y)).Select(r => r.Item1).FirstOrDefault();
        }

        private Sprite selectedSprite;
        public Sprite SelectedSprite
        {
            get
            {
                return selectedSprite;
            }
            set
            {
                selectedSprite = value;
                if (value != null)
                {
                    sprites.SelectionRectangle = Sprites.GetClipBounds(selectedSprite);
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
