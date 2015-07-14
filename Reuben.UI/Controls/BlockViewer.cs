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

        public void Initialize(PatternTable patternTable, Color[] colors, Palette palette)
        {
            localPatternTable = patternTable;
            localColors = colors;
            localPalette = palette;
            if (localPalette != null && localColors != null)
            {
                quickBGReference = new Color[4][];

                quickBGReference[0] = new Color[4];
                quickBGReference[1] = new Color[4];
                quickBGReference[2] = new Color[4];
                quickBGReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickBGReference[i / 4][i % 4] = localColors[localPalette.BackgroundValues[i]];
                }
            }
        }

        public void Update(PatternTable patternTable = null, Color[] colors = null, Palette palette = null)
        {
            localPatternTable = patternTable ?? localPatternTable;
            localColors = colors ?? localColors;
            localPalette = palette ?? localPalette;
            if (localPalette != null && localColors != null)
            {
                quickBGReference = new Color[4][];

                quickBGReference[0] = new Color[4];
                quickBGReference[1] = new Color[4];
                quickBGReference[2] = new Color[4];
                quickBGReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickBGReference[i / 4][i % 4] = localColors[localPalette.BackgroundValues[i]];
                }
            }
        }

        private PatternTable localPatternTable;
        private Color[] localColors;
        private Palette localPalette;
        private Color[][] quickBGReference;

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


        

        public void UpdateGraphics()
        {
            if (block == null || localColors == null || localPatternTable == null || localPalette == null)
            {
                using (Graphics gfx = Graphics.FromImage(displayBuffer))
                {
                    gfx.Clear(Color.White);
                }

                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);


            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperLeft), 0, 0, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperRight), 8, 0, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerLeft), 0, 8, quickBGReference[paletteIndex], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerRight), 8, 8, quickBGReference[paletteIndex], data);

            buffer.UnlockBits(data);
            using (Graphics gfx = Graphics.FromImage(displayBuffer))
            {
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gfx.DrawImage(buffer, new Rectangle(0, 0, displayBuffer.Width, displayBuffer.Height), new Rectangle(0, 0, buffer.Width, buffer.Height), GraphicsUnit.Pixel);
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(displayBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        protected override void Dispose(bool disposing)
        {
            buffer.Dispose();
            displayBuffer.Dispose();
            base.Dispose(disposing);
        }
    }
}
