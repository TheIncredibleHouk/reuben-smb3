using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.Model;

namespace Reuben.UI.Controls
{
    public class PaletteView : Control
    {
        public Palette Palette { get; private set; }
        public Color[] ColorReference { get; set; }

        private Bitmap buffer;

        public PaletteView()
        {
            this.Size = new Size(256, 32);
            buffer = new Bitmap(256, 32);
        }

        public PaletteView(Palette palette, Color[] colors)
        {
            this.Size = new Size(256, 32);
            buffer = new Bitmap(256, 32);
            Palette = palette;
            ColorReference = colors;
            UpdateAll();
        }

        public void SetPalette(Palette palette)
        {
            Palette = palette;
            UpdateAll();
            Invalidate();
        }
        

        public void UpdateAll()
        {
            using (Graphics gfx = Graphics.FromImage(buffer))
            {
                if (ColorReference == null || Palette == null)
                {
                    gfx.Clear(Color.Black);
                    return;
                }

                for (int i = 0; i < 16; i++)
                {
                    DrawColor(gfx, ColorReference[Palette.BackgroundValues[i]], i, 0);
                    DrawColor(gfx, ColorReference[Palette.SpriteValues[i]], i, 1);
                }
            }
        }

        private void DrawColor(Graphics gfx, Color color, int column, int row)
        {
            Brush brush = new SolidBrush(color);
            Rectangle rect = new Rectangle(column * 16,
                                           row * 16,
                                           16,
                                           16);
            gfx.FillRectangle(brush, rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffer, 0, 0);
        }

        public Bitmap GetImage()
        {
            return buffer;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(buffer != null)
                {
                    buffer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}