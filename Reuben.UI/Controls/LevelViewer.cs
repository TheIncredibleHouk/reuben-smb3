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
using Reuben.Controllers;
using Reuben.NESGraphics;

namespace Reuben.UI
{
    public unsafe class LevelViewer : Control
    {
        private Bitmap bgBuffer;
        private Bitmap spriteBuffer;
        private const int levelRows = 0x1B;
        private const int levelCols = 16 * 15;
        private const int levelBitmapWidth = 16 * 16 * levelCols;
        private const int levelBitmapHeight = 16 * levelRows;

        public LevelViewer()
        {
            bgBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format24bppRgb);
            spriteBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format32bppArgb);
            this.Width = levelBitmapWidth;
            this.Height = levelBitmapHeight;
        }

        public Level Level { get; set; }
        public LevelType LevelType { get; set; }

        private Palette palette;
        public Palette Palette
        {
            get { return palette; }
            set
            {
                palette = value;
                UpdateColorReferences();
            }
        }

        private Color[] colorRefrence;
        public Color[] ColorReference
        {
            get { return colorRefrence; }
            set
            {
                colorRefrence = value;
                UpdateColorReferences();
            }
        }
        public PatternTable Graphics { get; set; }

        private void UpdateColorReferences()
        {
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
        public void UpdateArea(int row, int column, int width, int height)
        {
            UpdateBGArea(row, column, width, height);
            Invalidate(new Rectangle(row * 16, column * 16, width * 16, height * 16));
        }

        private Color[][] quickBGReference;

        private void UpdateBGArea(int row, int column, int width, int height)
        {
            BitmapData bitmap = bgBuffer.LockBits(new Rectangle(0, 0, bgBuffer.Width, bgBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            for (var x = column; x < width; x++)
            {
                for (var y = row; y < height; y++)
                {
                    DrawBGBlock(x, y, bitmap);
                }
            }

            bgBuffer.UnlockBits(bitmap);
        }

        private void DrawBGBlock(int row, int column, BitmapData bitmap)
        {
            if (quickBGReference == null)
            {
                return;
            }
            int blockValue = Level.Data[row, column];

            Block block = LevelType.Blocks[blockValue];
            int paletteIndex = (blockValue & 0xC0) >> 6;
            DrawBGTile(Graphics.GetTileByIndex(block.UpperLeft), row * 16, column * 16, quickBGReference[paletteIndex], bitmap);
            DrawBGTile(Graphics.GetTileByIndex(block.UpperRight), row * 16, column * 16 + 8, quickBGReference[paletteIndex], bitmap);
            DrawBGTile(Graphics.GetTileByIndex(block.LowerLeft), row * 16 + 8, column * 16, quickBGReference[paletteIndex], bitmap);
            DrawBGTile(Graphics.GetTileByIndex(block.LowerRight), row * 16 + 8, column * 16 + 8, quickBGReference[paletteIndex], bitmap);
        }

        private void DrawBGTile(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 3) + (x * 3));
                    Color c = reference[tile.Pixels[col, row]];

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Level == null || Graphics == null || ColorReference == null)
            {
                e.Graphics.Clear(Color.Black);
            }
            else
            {
                e.Graphics.DrawImage(bgBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
