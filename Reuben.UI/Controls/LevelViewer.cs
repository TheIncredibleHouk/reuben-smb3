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
        private Bitmap compositeBuffer;
        private const int levelRows = 0x1B;
        private const int levelCols = 16 * 15;
        private const int levelBitmapWidth = 16 * 16 * levelCols;
        private const int levelBitmapHeight = 16 * levelRows;

        public LevelViewer()
        {
            bgBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format24bppRgb);
            spriteBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format32bppArgb);
            compositeBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format32bppArgb);
            SelectedSprites = new List<Sprite>();
            this.Width = levelBitmapWidth;
            this.Height = levelBitmapHeight;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Level Level { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LevelType LevelType { get; set; }

        private Palette palette;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color[] ColorReference
        {
            get { return colorRefrence; }
            set
            {
                colorRefrence = value;
                UpdateColorReferences();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatternTable PatternTable { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsController Graphics { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteController Sprites { get; set; }

        private Color[][] quickBGReference;
        private Color[][] quickSpriteReference;
        private void UpdateColorReferences()
        {
            if (Palette != null && ColorReference != null)
            {
                quickBGReference = new Color[4][];
                quickSpriteReference = new Color[4][];

                quickBGReference[0] = new Color[4];
                quickBGReference[1] = new Color[4];
                quickBGReference[2] = new Color[4];
                quickBGReference[3] = new Color[4];

                quickSpriteReference[0] = new Color[4];
                quickSpriteReference[1] = new Color[4];
                quickSpriteReference[2] = new Color[4];
                quickSpriteReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickBGReference[i / 4][i % 4] = ColorReference[Palette.BackgroundValues[i]];
                    quickSpriteReference[i / 4][i % 4] = ColorReference[Palette.SpriteValues[i]];
                }

                quickSpriteReference[0][0] =
                quickSpriteReference[1][0] =
                quickSpriteReference[2][0] =
                quickSpriteReference[3][0] = Color.Transparent;
            }

        }

        private bool blockUpdating;
        public void UpdateBlockDisplay(int column, int row, int width, int height)
        {
            if (blockUpdating)
            {
                return;
            }

            if (quickBGReference == null || palette == null || PatternTable == null || Graphics == null)
            {
                return;
            }

            UpdateBGArea(column, row, width, height);
            Rectangle area = new Rectangle(column * 16, row * 16, width * 16, height * 16);
            UpdateLayers(area);
            Invalidate(area);
            blockUpdating = true;
        }

        private bool spriteUpdating;
        public void UpdateSpriteDisplay(int column, int row, int width, int height)
        {
            if (spriteUpdating)
            {
                return;
            }

            if (quickBGReference == null || palette == null || PatternTable == null || Graphics == null)
            {
                return;
            }

            UpdateSpriteArea(column, row, width, height);
            Rectangle area = new Rectangle(row * 16, column * 16, width * 16, height * 16);
            UpdateLayers(area);
            Invalidate(area);
            spriteUpdating = true;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Sprite> SelectedSprites { get; set; }
        private void UpdateSpriteArea(int row, int column, int width, int height)
        {
            Rectangle area = new Rectangle(column * 16, row * 16, width * 16, height * 16);
            List<Tuple<Sprite, Rectangle>> spriteBounds = Sprites.GetBounds(Level.Sprites).ToList(); // generate all bound areas
            List<Tuple<Sprite, Rectangle>> affectedSprites = spriteBounds.Where(r => r.Item2.IntersectsWith(area)).ToList(); // find the ones that are affected by the update
            int minX = Math.Max(spriteBounds.Min(r => r.Item2.X), 0);
            int maxX = Math.Min(spriteBounds.Max(r => r.Item2.X), levelBitmapWidth);
            int minY = Math.Max(spriteBounds.Min(r => r.Item2.Y), 0);
            int maxY = Math.Max(spriteBounds.Max(r => r.Item2.Y), levelBitmapHeight);

            minX = minX / 16;
            maxX = maxX / 16;
            minY = minY / 16;
            maxY = maxY / 16;

            Rectangle updateArea = new Rectangle(minX, minY, maxX - minX, maxY - minY);
            using (var gfx = System.Drawing.Graphics.FromImage(spriteBuffer))
            {
                gfx.FillRectangle(Brushes.Transparent, area);
            }


            BitmapData bitmap = spriteBuffer.LockBits(new Rectangle(0, 0, spriteBuffer.Width, spriteBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            foreach (Sprite sprite in affectedSprites.Select(s => s.Item1))
            {
                DrawSprite(sprite, bitmap);
            }

            spriteBuffer.UnlockBits(bitmap);
        }

        private void UpdateBGArea(int column, int row, int width, int height)
        {
            BitmapData bitmap = bgBuffer.LockBits(new Rectangle(0, 0, bgBuffer.Width, bgBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            for (int x = 0, i = column; x < width; i++, x++)
            {
                for (int y = 0, j = row; y < height; j++, y++)
                {
                    DrawBGBlock(i, j, bitmap);
                }
            }

            bgBuffer.UnlockBits(bitmap);
        }

        private void DrawBGBlock(int column, int row, BitmapData bitmap)
        {

            int blockValue = Level.Data[column, row];

            Block block = LevelType.Blocks[blockValue];
            int paletteIndex = (blockValue & 0xC0) >> 6;
            Drawer.DrawTileNoAlpha(PatternTable.GetTileByIndex(block.UpperLeft), column * 16, row * 16, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(PatternTable.GetTileByIndex(block.UpperRight), column * 16 + 8, row * 16, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(PatternTable.GetTileByIndex(block.LowerLeft), column * 16, row * 16 + 8, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(PatternTable.GetTileByIndex(block.LowerRight), column * 16 + 8, row * 16 + 8, quickBGReference[paletteIndex], bitmap);
        }


        private void DrawSprite(Sprite sprite, BitmapData bitmap)
        {
            int x = sprite.X * 16;
            int y = sprite.Y * 16;

            foreach (var info in Sprites.GetDefinition(sprite.ObjectID).SpriteInfo)
            {
                if (info.Properties.Count > 0 && !info.Properties.Contains(sprite.Property))
                {
                    // if the info is property specific, only sprites with that sprite draw that tile
                    return;
                }


                int paletteIndex = info.Palette;
                int xOffset = x + info.X;
                int yOffset = y + info.Y;
                if (xOffset < 0 || y < 0 ||
                    xOffset >= levelBitmapWidth - 8 ||
                    yOffset >= levelBitmapHeight - 8)
                {
                    // prevent overflow drawing
                    return;
                }
                if (info.Table == -1)
                {
                    continue;
                }

                Tile tile1 = Graphics.GetTileByBankIndex(info.Table, info.Value);
                Tile tile2 = Graphics.GetTileByBankIndex(info.Table, info.Value + 1);
                if (info.HorizontalFlip && info.VerticalFlip)
                {
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
                else if (info.HorizontalFlip)
                {
                    Drawer.DrawTileHorizontalFlipAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalFlipAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
                else if (info.VerticalFlip)
                {
                    Drawer.DrawTileVerticalFlipAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileVerticalFlipAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
                else
                {
                    Drawer.DrawTileAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
            }
        }


        private void UpdateLayers(Rectangle area)
        {
            using (var gfx = System.Drawing.Graphics.FromImage(compositeBuffer))
            {
                gfx.DrawImage(bgBuffer, area, area, GraphicsUnit.Pixel);
                gfx.DrawImage(spriteBuffer, area, area, GraphicsUnit.Pixel);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Level == null || PatternTable == null || ColorReference == null)
            {
                e.Graphics.Clear(Color.Black);
            }
            else
            {
                e.Graphics.DrawImage(compositeBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
                if (EditMode == UI.EditMode.Blocks)
                {
                    if (SelectionRectangle != Rectangle.Empty)
                    {
                        Pen pen = new Pen(Brushes.White);
                        pen.DashStyle = SelectionType == UI.SelectionType.Draw ? System.Drawing.Drawing2D.DashStyle.Solid : System.Drawing.Drawing2D.DashStyle.Dot;
                        e.Graphics.DrawRectangle(pen, SelectionRectangle);
                        pen.Color = Color.Red;
                        e.Graphics.DrawRectangle(pen, new Rectangle(SelectionRectangle.X + 1, SelectionRectangle.Y + 1, SelectionRectangle.Width - 2, SelectionRectangle.Height - 2));
                    }
                }

                if (EditMode == UI.EditMode.Sprites)
                {
                    if (SelectionRectangle != Rectangle.Empty)
                    {
                        Pen pen = new Pen(Brushes.White);
                        pen.DashStyle = SelectionType == UI.SelectionType.Draw ? System.Drawing.Drawing2D.DashStyle.Solid : System.Drawing.Drawing2D.DashStyle.Dot;
                        e.Graphics.DrawRectangle(pen, SelectionRectangle);
                        pen.Color = Color.Blue;
                        e.Graphics.DrawRectangle(pen, new Rectangle(SelectionRectangle.X + 1, SelectionRectangle.Y + 1, SelectionRectangle.Width - 2, SelectionRectangle.Height - 2));
                    }

                    foreach (Sprite s in SelectedSprites)
                    {
                        Rectangle drawRectangle = Sprites.GetClipBounds(s);
                        e.Graphics.DrawRectangle(Pens.White, drawRectangle);
                        e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(drawRectangle.X + 1, drawRectangle.Y + 1, drawRectangle.Width - 2, drawRectangle.Height - 2));
                    }
                }

                blockUpdating = spriteUpdating = false;
            }
        }


        public SelectionType SelectionType { get; set; }
        public EditMode EditMode { get; set; }

        private Rectangle selectionRectangle;
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
                Invalidate(new Rectangle[] { oldRectangle, selectionRectangle}.Combine());
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
