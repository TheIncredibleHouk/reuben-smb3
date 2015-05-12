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
    public partial class SpriteSelector : Form
    {
        public SpriteSelector()
        {
            InitializeComponent();
        }


        public Color[] ColorReference
        {
            get { return sprites.ColorReference; }
            set { sprites.ColorReference = value; }
        }

        public Palette Palette
        {
            get { return sprites.Palette; }
            set { sprites.Palette = value; }
        }

        public SpriteController Sprites
        {
            get { return sprites.Sprites; }
            set { sprites.Sprites = value; }
        }

        public GraphicsController Graphics
        {
            get { return sprites.Graphics; }
            set { sprites.Graphics = value; }
        }

        public void UpdateGraphics()
        {
            sprites.UpdateGraphics();
        }

        public LevelEditor Editor { get; set; }

        private void SpriteSelector_MouseDown(object sender, MouseEventArgs e)
        {
            Editor.EditMode = EditMode.Sprites;
        }

        public bool Snapped { get; set; }

        private void SpriteSelector_SizeChanged_1(object sender, EventArgs e)
        {
            if (Editor != null && this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Snapped = true;
                Editor.MoveSelectors();
            }
        }

        private void SpriteSelector_Move(object sender, EventArgs e)
        {
            Snapped = false;

        }
    }
}
