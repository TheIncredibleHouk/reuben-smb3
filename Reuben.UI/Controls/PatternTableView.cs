using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.UI;
using Reuben.NESGraphics;
using Reuben.Model;

namespace Reuben.UI
{
    public class PatternTableView : Control
    {
        private Bitmap buffer;
        private Bitmap displayBuffer;

        public PatternTableView()
        {
            buffer = new Bitmap(128, 128, PixelFormat.Format24bppRgb);
            displayBuffer = new Bitmap(256, 256, PixelFormat.Format24bppRgb);
            this.Width = displayBuffer.Width;
            this.Height = displayBuffer.Height;
        }

        private PatternTable graphics;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatternTable PatternTable
        {
            get
            {
                return graphics;
            }
            set
            {
                graphics = value;
            }
        }

        private Color[] colors;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color[] ColorReference
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
            }
        }

        public int PaletteIndex { get; set; }

        private Palette palette;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get
            {
                return palette;
            }
            set
            {
                palette = value;
                if (Palette != null && ColorReference != null)
                {
                    quickBGReference = new Color[4][];

                    quickBGReference[0] = new Color[4];
                    quickBGReference[1] = new Color[4];
                    quickBGReference[2] = new Color[4];
                    quickBGReference[3] = new Color[4];

                    for (int i = 0; i < 16; i++)
                    {
                        quickBGReference[i / 4][i % 4] = ColorReference[Palette.BackgroundValues[i]];
                    }
                }
            }
        }

        private Color[][] quickBGReference;

        public void UpdateGraphics()
        {
            if (colors == null || graphics == null || palette == null)
            {
                using (Graphics gfx = Graphics.FromImage(displayBuffer))
                {
                    gfx.Clear(Color.Black);
                }

                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (var row = 0; row < 16; row++)
            {
                for (var col = 0; col < 16; col++)
                {
                    int x = col * 8;
                    int y = row * 8;
                    Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(col + (row * 16)), x, y, quickBGReference[PaletteIndex], data);
                }
            }
            buffer.UnlockBits(data);
            using (Graphics gfx = Graphics.FromImage(displayBuffer))
            {
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gfx.DrawImage(buffer, new Rectangle(0, 0, displayBuffer.Width, displayBuffer.Height), new Rectangle(0, 0, buffer.Width, buffer.Height), GraphicsUnit.Pixel);
            }
            Invalidate();
        }

        private Rectangle selectionRectangle;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle SelectionRectangle
        {
            get
            {
                return selectionRectangle;
            }
            set
            {
                if (selectionRectangle == value)
                {
                    return;
                }

                var oldRectangle = selectionRectangle;
                selectionRectangle = value;
                var minX = Math.Min(oldRectangle.Left, selectionRectangle.Left);
                var minY = Math.Min(oldRectangle.Top, selectionRectangle.Top);
                var maxX = Math.Max(oldRectangle.Right, selectionRectangle.Right);
                var maxY = Math.Max(oldRectangle.Bottom, selectionRectangle.Bottom);
                Invalidate(new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(displayBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            if (SelectionRectangle != null)
            {
                e.Graphics.DrawRectangle(Pens.White, SelectionRectangle);
                e.Graphics.DrawRectangle(Pens.Red, new Rectangle(SelectionRectangle.X + 1, SelectionRectangle.Y + 1, SelectionRectangle.Width - 2, SelectionRectangle.Height - 2));
            }
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

                if(displayBuffer != null)
                {
                    displayBuffer.Dispose();
                }
            }
            
            base.Dispose(disposing);
        }
    }
}
