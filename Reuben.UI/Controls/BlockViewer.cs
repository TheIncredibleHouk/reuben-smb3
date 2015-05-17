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

    public class BlockViewer : Control
    {
        private Bitmap buffer;
        private Bitmap displayBuffer;

        public BlockViewer()
        {
            buffer = new Bitmap(16, 16, PixelFormat.Format24bppRgb);
            displayBuffer = new Bitmap(64, 64, PixelFormat.Format24bppRgb);
            this.Width = displayBuffer.Width;
            this.Height = displayBuffer.Height;
        }


        private Block block;
        public Block Block
        {
            get
            {
                return block;
            }
            set
            {
                block = value;
            }
        }

        private int paletteIndex;
        public int PaletteIndex
        {
            get
            {
                return paletteIndex;
            }
            set
            {
                paletteIndex = value;
            }
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
            if (block == null || colors == null || graphics == null || palette == null)
            {
                using (Graphics gfx = Graphics.FromImage(displayBuffer))
                {
                    gfx.Clear(Color.Black);
                }

                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);


            Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperLeft), 0, 0, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperLeft), 8, 0, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperLeft), 0, 8, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(graphics.GetTileByIndex(block.UpperLeft), 8, 8, quickBGReference[paletteIndex], data);

            using (Graphics gfx = Graphics.FromImage(displayBuffer))
            {
                gfx.DrawImage(buffer, new Rectangle(0, 0, displayBuffer.Width, displayBuffer.Height), new Rectangle(0, 0, buffer.Width, buffer.Height), GraphicsUnit.Pixel);
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(displayBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);

        }
    }
}
