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

namespace Reuben.UI.Controls
{
    public unsafe class BlocksViewer : Control
    {
        private Bitmap buffer;

        public BlocksViewer()
        {
            buffer = new Bitmap(256, 256, PixelFormat.Format24bppRgb);
            this.Width = buffer.Width;
            this.Height = buffer.Height;
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

        private Block[] blocks;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Block[] BlockList
        {
            get
            {
                return blocks;
            }
            set
            {
                blocks = value;
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
            if (colors == null || graphics == null || blocks == null || palette == null)
            {
                using (Graphics gfx = Graphics.FromImage(buffer))
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
                    Block block = blocks[row * 16 + col];
                    int x = col * 16;
                    int y = row * 16;
                    Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperLeft), x, y, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperRight), x + 8, y, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.LowerLeft), x, y + 8, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.LowerRight), x + 8, y + 8, quickBGReference[row / 4], data);
                }
            }
            buffer.UnlockBits(data);
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
            e.Graphics.DrawImage(buffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            if (SelectionRectangle != null)
            {
                e.Graphics.DrawRectangle(Pens.White, SelectionRectangle);
                e.Graphics.DrawRectangle(Pens.Red, new Rectangle(SelectionRectangle.X + 1, SelectionRectangle.Y + 1, SelectionRectangle.Width - 2, SelectionRectangle.Height - 2));
            }
        }
    }
}
