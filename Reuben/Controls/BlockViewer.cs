using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.Controllers;
using Reuben.NESGraphics;
using Reuben.Model;

namespace Reuben.UI
{
    public unsafe class BlockViewer : Control
    {
        private Bitmap backBuffer;
        private PatternTable currentTable;
        private Palette currentPalette;
        private int paletteIndex;
        private Block currentBlock;
        private GraphicsController graphicsController;

        public BlockViewer(GraphicsController gfxController)
        {
            backBuffer = new Bitmap(16, 16);
            currentBlock = null;
            graphicsController = gfxController;
            this.Width = this.Height = 32;
            FullRender();
        }

        public void SetPatternTable(PatternTable table)
        {
            currentTable = table;
            FullRender();
        }

        public void SetPallete(Palette palette)
        {
            currentPalette = palette;
            UpdateColors();
            FullRender();
        }

        public void SetCurrentBlock(Block block)
        {
            currentBlock = block;
            FullRender();
        }

        private Color[,] QuickColorLookup;

        private void UpdateColors()
        {
            if (currentPalette != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        QuickColorLookup[j, i] = graphicsController.ColorReference[currentPalette.GetColorIndex(j, i)];
                    }
                }
            }
        }

        private void FullRender()
        {
            if (currentTable == null || currentPalette == null || currentBlock == null)
            {
                Graphics.FromImage(backBuffer).Clear(Color.Black);
                return;
            }

            BitmapData data = backBuffer.LockBits(new Rectangle(0, 0, 16, 16), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            RenderTile(currentTable.GetTileByIndex(currentBlock.UpperLeft), 0, 0, paletteIndex, data);
            RenderTile(currentTable.GetTileByIndex(currentBlock.UpperRight), 0, 8, paletteIndex, data);
            RenderTile(currentTable.GetTileByIndex(currentBlock.LowerLeft), 8, 0, paletteIndex, data);
            RenderTile(currentTable.GetTileByIndex(currentBlock.LowerRight), 8, 8, paletteIndex, data);

            backBuffer.UnlockBits(data);
            Invalidate();
        }

        private void RenderTile(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c = QuickColorLookup[PaletteIndex, tile.Pixels[j, i]];
                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(backBuffer, new Rectangle(0, 0, 33, 33), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
        }

        public void SetTile(int x, int y, byte value)
        {
            currentBlock.SetTileByPoint(x, y, value);
            FullRender();
        }
    }
}
