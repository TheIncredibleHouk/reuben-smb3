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
    public class ColorView : Control
    {
        private Color[] colorReference;
        private Bitmap buffer;

        public ColorView()
        {
            this.Size = new Size(16 * 16, 16 * 4);
            buffer = new Bitmap(16 *16, 16 * 4);
        }

        public void SetColorReference(Color[] colors)
        {
            colorReference = colors;
            UpdateAll();
        }

        private void UpdateAll()
        {
            using (Graphics gfx = Graphics.FromImage(buffer))
            {
                if (colorReference == null)
                {
                    gfx.Clear(Color.Black);
                    return;
                }

                for (int i = 0; i < 0x40; i++)
                {
                    DrawColor(gfx, colorReference[i], (int) i % 0x10, (int) i / 0x10);
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
    }
}
