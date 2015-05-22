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
    public unsafe class BlockListViewer : Control
    {
        private Bitmap buffer;

        public BlockListViewer()
        {
            buffer = new Bitmap(256, 256, PixelFormat.Format24bppRgb);
            this.Width = buffer.Width;
            this.Height = buffer.Height;
        }

        public void Initialize(PatternTable patternTable, Block[] blocks, Palette palette, Color[] colors)
        {
            localPatternTable = patternTable;
            localBlockList = blocks;
            localPalette = palette;
            localColors = colors;

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

            UpdateGraphics();
        }

        public void Update(PatternTable patternTable = null, Block[] blocks = null, Palette palette = null, Color[] colors = null)
        {
            localPatternTable = patternTable ?? localPatternTable;
            localBlockList = blocks ?? localBlockList;
            localPalette = palette ?? localPalette;
            localColors = colors ?? localColors;

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
            UpdateGraphics();
        }

        private PatternTable localPatternTable;
        private Block[] localBlockList;
        private Color[] localColors;
        private Palette localPalette;
        private Color[][] quickBGReference;


        private void UpdateGraphics()
        {
            if (localColors == null || localPatternTable == null || localBlockList == null || localPalette == null)
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
                    Block block = localBlockList[row * 16 + col];
                    int x = col * 16;
                    int y = row * 16;
                    Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperLeft), x, y, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperRight), x + 8, y, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerLeft), x, y + 8, quickBGReference[row / 4], data);
                    Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerRight), x + 8, y + 8, quickBGReference[row / 4], data);
                }
            }
            buffer.UnlockBits(data);
            Invalidate();
        }

        public void UpdateBlock(int col, int row)
        {
            if (localColors == null || localPatternTable == null || localBlockList == null || localPalette == null)
            {
                using (Graphics gfx = Graphics.FromImage(buffer))
                {
                    gfx.Clear(Color.Black);
                }

                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            Block block = localBlockList[row * 16 + col];
            int x = col * 16;
            int y = row * 16;
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperLeft), x, y, quickBGReference[row / 4], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperRight), x + 8, y, quickBGReference[row / 4], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerLeft), x, y + 8, quickBGReference[row / 4], data);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerRight), x + 8, y + 8, quickBGReference[row / 4], data);

            buffer.UnlockBits(data);
            Invalidate(new Rectangle(col * 16, row * 16, 17, 17));
        }

        private Rectangle selectionRectangle;

        public void SetSelectionRectangle(Rectangle r)
        {
            if (selectionRectangle == r)
            {
                return;
            }

            var oldRectangle = selectionRectangle;
            selectionRectangle = r;
            var minX = Math.Min(oldRectangle.Left, selectionRectangle.Left);
            var minY = Math.Min(oldRectangle.Top, selectionRectangle.Top);
            var maxX = Math.Max(oldRectangle.Right, selectionRectangle.Right);
            var maxY = Math.Max(oldRectangle.Bottom, selectionRectangle.Bottom);
            Invalidate(new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            if (selectionRectangle != null)
            {
                e.Graphics.DrawRectangle(Pens.White, selectionRectangle);
                e.Graphics.DrawRectangle(Pens.Red, new Rectangle(selectionRectangle.X + 1, selectionRectangle.Y + 1, selectionRectangle.Width - 2, selectionRectangle.Height - 2));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
