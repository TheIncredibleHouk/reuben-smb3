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
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            bgBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format24bppRgb);
            spriteBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format32bppArgb);
            compositeBuffer = new Bitmap(levelBitmapWidth, levelBitmapHeight, PixelFormat.Format32bppArgb);
            SelectedSprites = new List<Sprite>();
            this.Width = levelBitmapWidth;
            this.Height = levelBitmapHeight;
        }

        public void Initialize(Level level, LevelType levelType, Palette levelPalette, Palette overlayPalette, Color[] colors, Block[] overlayBlocks, PatternTable patternTable, PatternTable overlayTable)
        {
            localLevel = level;
            localLevelType = levelType;
            localPalette = levelPalette;
            localColors = colors;
            localOverlayPalette = overlayPalette;
            localPatternTable = patternTable;
            localOverlayTable = overlayTable;
            localOverlayBlocks = overlayBlocks;

            if (localPalette != null && localColors != null)
            {
                quickBGReference = new Color[4][];
                quickSpriteReference = new Color[4][];
                quickBGOverlayReference = new Color[4][];
                quickSpriteOverlayReference = new Color[4][];

                quickBGReference[0] = new Color[4];
                quickBGReference[1] = new Color[4];
                quickBGReference[2] = new Color[4];
                quickBGReference[3] = new Color[4];

                quickSpriteReference[0] = new Color[4];
                quickSpriteReference[1] = new Color[4];
                quickSpriteReference[2] = new Color[4];
                quickSpriteReference[3] = new Color[4];

                quickBGOverlayReference[0] = new Color[4];
                quickBGOverlayReference[1] = new Color[4];
                quickBGOverlayReference[2] = new Color[4];
                quickBGOverlayReference[3] = new Color[4];

                quickSpriteOverlayReference[0] = new Color[4];
                quickSpriteOverlayReference[1] = new Color[4];
                quickSpriteOverlayReference[2] = new Color[4];
                quickSpriteOverlayReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickBGReference[i / 4][i % 4] = localColors[localPalette.BackgroundValues[i]];
                    quickSpriteReference[i / 4][i % 4] = localColors[localPalette.SpriteValues[i]];
                    quickBGOverlayReference[i / 4][i % 4] = localColors[localOverlayPalette.BackgroundValues[i]];
                    quickSpriteOverlayReference[i / 4][i % 4] = localColors[localOverlayPalette.SpriteValues[i]];
                }

                quickBGOverlayReference[0][0] =
                quickBGOverlayReference[1][0] =
                quickBGOverlayReference[2][0] =
                quickBGOverlayReference[3][0] =
                quickSpriteReference[0][0] =
                quickSpriteReference[1][0] =
                quickSpriteReference[2][0] =
                quickSpriteReference[3][0] =
                quickSpriteOverlayReference[0][0] =
                quickSpriteOverlayReference[1][0] =
                quickSpriteOverlayReference[2][0] =
                quickSpriteOverlayReference[3][0] = Color.Transparent;
            }
        }

        public void Update(Level level = null, LevelType levelType = null, Palette levelPalette = null, Palette overlayPalette = null, Color[] colors = null, Block[] overlayBlocks = null, PatternTable patternTable = null, PatternTable overlayTable = null)
        {
            localLevel = level ?? localLevel;
            localLevelType = levelType ?? localLevelType;
            localPalette = levelPalette ?? localPalette;
            localColors = colors ?? localColors;
            localOverlayPalette = overlayPalette ?? localOverlayPalette;
            localPatternTable = patternTable ?? localPatternTable;
            localOverlayTable = overlayTable ?? localOverlayTable;
            localOverlayBlocks = overlayBlocks ?? localOverlayBlocks;

            if (localPalette != null && localColors != null)
            {
                quickBGReference = new Color[4][];
                quickSpriteReference = new Color[4][];
                quickBGOverlayReference = new Color[4][];
                quickSpriteOverlayReference = new Color[4][];

                quickBGReference[0] = new Color[4];
                quickBGReference[1] = new Color[4];
                quickBGReference[2] = new Color[4];
                quickBGReference[3] = new Color[4];

                quickSpriteReference[0] = new Color[4];
                quickSpriteReference[1] = new Color[4];
                quickSpriteReference[2] = new Color[4];
                quickSpriteReference[3] = new Color[4];

                quickBGOverlayReference[0] = new Color[4];
                quickBGOverlayReference[1] = new Color[4];
                quickBGOverlayReference[2] = new Color[4];
                quickBGOverlayReference[3] = new Color[4];

                quickSpriteOverlayReference[0] = new Color[4];
                quickSpriteOverlayReference[1] = new Color[4];
                quickSpriteOverlayReference[2] = new Color[4];
                quickSpriteOverlayReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickBGReference[i / 4][i % 4] = localColors[localPalette.BackgroundValues[i]];
                    quickSpriteReference[i / 4][i % 4] = localColors[localPalette.SpriteValues[i]];
                    quickBGOverlayReference[i / 4][i % 4] = localColors[localOverlayPalette.BackgroundValues[i]];
                    quickSpriteOverlayReference[i / 4][i % 4] = localColors[localOverlayPalette.SpriteValues[i]];
                }

                quickBGOverlayReference[0][0] =
                quickBGOverlayReference[1][0] =
                quickBGOverlayReference[2][0] =
                quickBGOverlayReference[3][0] =
                quickSpriteReference[0][0] =
                quickSpriteReference[1][0] =
                quickSpriteReference[2][0] =
                quickSpriteReference[3][0] =
                quickSpriteOverlayReference[0][0] =
                quickSpriteOverlayReference[1][0] =
                quickSpriteOverlayReference[2][0] =
                quickSpriteOverlayReference[3][0] = Color.Transparent;
            }

            UpdateBlockDisplay(0, 0, 0xF0, 0x1B);
        }

        private Level localLevel;
        private LevelType localLevelType;
        private Palette localPalette;
        private Palette localOverlayPalette;
        private Color[] localColors;
        private Block[] localOverlayBlocks;
        private PatternTable localOverlayTable;
        private PatternTable localPatternTable;

        private Color[][] quickBGReference;
        private Color[][] quickSpriteReference;
        private Color[][] quickBGOverlayReference;
        private Color[][] quickSpriteOverlayReference;

        public bool ShowInteractionOverlays { get; set; }
        public bool ShowSolidityOverlays { get; set; }
        public bool ShowSpriteOverlays { get; set; }

        public void UpdateBlockDisplay(int column, int row, int width, int height)
        {

            if (quickBGReference == null || localPalette == null || localPatternTable == null || localPatternTable == null)
            {
                return;
            }

            UpdateBGArea(column, row, width, height);
            Rectangle area = new Rectangle(column * 16, row * 16, width * 16, height * 16);
            UpdateLayers(area);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Sprite> SelectedSprites { get; set; }


        public void UpdateSprites(IEnumerable<Sprite> sprites)
        {
            UpdateSprites(sprites, null);
        }

        public void UpdateSprites(IEnumerable<Sprite> sprites, IEnumerable<Rectangle> clearAreas)
        {
            List<Tuple<Sprite, Rectangle>> spriteBounds = Controllers.Sprites.GetBounds(sprites, ShowSpriteOverlays).ToList();
            Rectangle area = spriteBounds.Select(a => a.Item2).Combine();// generate all bound areas

            BitmapData bitmap = spriteBuffer.LockBits(new Rectangle(0, 0, spriteBuffer.Width, spriteBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            if (clearAreas != null)
            {
                area = Rectangle.Union(area, clearAreas.Combine());
                foreach (var r in clearAreas)
                {
                    Drawer.FillArea(new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1), Color.Transparent, bitmap);
                }
            }

            List<Tuple<Sprite, Rectangle>> affectedSprites = Controllers.Sprites.GetBounds(localLevel.Sprites, ShowSpriteOverlays).Where(r => r.Item2.IntersectsWith(area)).ToList(); // find the ones that are affected by the update
            area = Rectangle.Union(affectedSprites.Select(a => a.Item2).Combine(), area);

            foreach (Sprite sprite in affectedSprites.Select(s => s.Item1))
            {
                DrawSprite(sprite, bitmap);
            }


            spriteBuffer.UnlockBits(bitmap);
            UpdateLayers(area);
        }

        public void UpdatePointers(IEnumerable<LevelPointer> pointers, IEnumerable<Rectangle> clearAreas)
        {
            Rectangle area = pointers.Select(p => new Rectangle(p.X * 16, p.Y * 16, 32, 32)).Combine();// generate all bound areas

            BitmapData bitmap = spriteBuffer.LockBits(new Rectangle(0, 0, spriteBuffer.Width, spriteBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            if (clearAreas != null)
            {
                area = Rectangle.Union(area, clearAreas.Combine());
                foreach (var r in clearAreas)
                {
                    Drawer.FillArea(new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1), Color.Transparent, bitmap);
                }
            }

            List<Tuple<Sprite, Rectangle>> affectedSprites = Controllers.Sprites.GetBounds(localLevel.Sprites, ShowSpriteOverlays).Where(r => r.Item2.IntersectsWith(area)).ToList(); // find the ones that are affected by the update
            area = Rectangle.Union(affectedSprites.Select(a => a.Item2).Combine(), area);

            foreach (Sprite sprite in affectedSprites.Select(s => s.Item1))
            {
                DrawSprite(sprite, bitmap);
            }


            spriteBuffer.UnlockBits(bitmap);
            UpdateLayers(area);
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

            int blockValue = localLevel.Data[column, row];

            Block block = localLevelType.Blocks[blockValue];
            int paletteIndex = (blockValue & 0xC0) >> 6;

            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperLeft), column * 16, row * 16, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.UpperRight), column * 16 + 8, row * 16, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerLeft), column * 16, row * 16 + 8, quickBGReference[paletteIndex], bitmap);
            Drawer.DrawTileNoAlpha(localPatternTable.GetTileByIndex(block.LowerRight), column * 16 + 8, row * 16 + 8, quickBGReference[paletteIndex], bitmap);


            if (ShowInteractionOverlays)
            {
                if (block.BlockSolidity == 0x80 || block.BlockSolidity == 0xF0 || block.BlockInteraction > 0x00)
                {
                    Block overlayBlock = null;
                    int index = 0;
                    for (; index < 0x100; index++)
                    {
                        if (localOverlayBlocks[index].BlockInteraction == block.BlockInteraction &&
                            localOverlayBlocks[index].BlockSolidity == block.BlockSolidity)
                        {
                            overlayBlock = localOverlayBlocks[index];
                            break;
                        }
                    }

                    if (overlayBlock != null)
                    {
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.UpperLeft), column * 16, row * 16, quickBGOverlayReference[index / 0x40], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.UpperRight), column * 16 + 8, row * 16, quickBGOverlayReference[index / 0x40], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.LowerLeft), column * 16, row * 16 + 8, quickBGOverlayReference[index / 0x40], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.LowerRight), column * 16 + 8, row * 16 + 8, quickBGOverlayReference[index / 0x40], .75, bitmap);
                    }
                }
            }

            if (ShowSolidityOverlays)
            {
                switch (block.BlockSolidity)
                {
                    case 0x10:
                        Drawer.FillTileWithAlpha(column * 16, row * 16 + 8, Color.White, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16, row * 16, Color.White, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16, Color.White, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16 + 8, Color.White, .5, bitmap);
                        break;

                    case 0x20:
                        Drawer.FillTileWithAlpha(column * 16, row * 16 + 8, Color.Blue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16, row * 16, Color.Blue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16, Color.Blue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16 + 8, Color.Blue, .5, bitmap);
                        break;

                    case 0x30:
                        Drawer.FillTileWithAlpha(column * 16, row * 16 + 8, Color.LightBlue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16, row * 16, Color.LightBlue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16, Color.LightBlue, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16 + 8, Color.LightBlue, .5, bitmap);
                        break;

                    case 0x40:
                        Drawer.FillTileWithAlpha(column * 16, row * 16, Color.Brown, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16, Color.Brown, .5, bitmap);
                        break;

                    case 0xC0:
                    case 0xF0:
                        Drawer.FillTileWithAlpha(column * 16, row * 16 + 8, Color.Brown, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16, row * 16, Color.Brown, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16, Color.Brown, .5, bitmap);
                        Drawer.FillTileWithAlpha(column * 16 + 8, row * 16 + 8, Color.Brown, .5, bitmap);
                        break;
                }
            }
        }


        private void DrawSprite(Sprite sprite, BitmapData bitmap)
        {
            int x = sprite.X * 16;
            int y = sprite.Y * 16;

            SpriteDefinition def = Controllers.Sprites.GetDefinition(sprite.ObjectID);
            bool forceOverlay = def.SpriteInfo.Where(s => !s.Overlay).Count() == 0;
            foreach (var info in def.SpriteInfo)
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
                if (info.Overlay && !ShowSpriteOverlays && !forceOverlay)
                {
                    continue;
                }

                Color[][] colorReference;
                Tile tile1 = null;
                Tile tile2 = null;

                if (info.Overlay)
                {
                    tile1 = Controllers.Graphics.GetExtraTileByBankIndex(info.Table, info.Value);
                    tile2 = Controllers.Graphics.GetExtraTileByBankIndex(info.Table, info.Value + 1);
                    colorReference = quickSpriteOverlayReference;
                }
                else
                {
                    tile1 = Controllers.Graphics.GetTileByBankIndex(info.Table, info.Value);
                    tile2 = Controllers.Graphics.GetTileByBankIndex(info.Table, info.Value + 1);
                    colorReference = quickSpriteReference;
                }

                if (info.HorizontalFlip && info.VerticalFlip)
                {
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                }
                else if (info.HorizontalFlip)
                {
                    Drawer.DrawTileHorizontalFlipAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalFlipAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                }
                else if (info.VerticalFlip)
                {
                    Drawer.DrawTileVerticalFlipAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileVerticalFlipAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                }
                else
                {
                    Drawer.DrawTileAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
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

            Invalidate(area);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (localLevel == null || localPatternTable == null || localColors == null || compositeBuffer == null)
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
                        Rectangle drawRectangle = Controllers.Sprites.GetClipBounds(s);
                        e.Graphics.DrawRectangle(Pens.White, drawRectangle);
                        e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(drawRectangle.X + 1, drawRectangle.Y + 1, drawRectangle.Width - 2, drawRectangle.Height - 2));
                    }
                }
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
                Invalidate(new Rectangle[] { oldRectangle, selectionRectangle }.Combine());
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        public Bitmap GetThumbnail()
        {
            Bitmap thumbnail = new Bitmap(256, 256, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(thumbnail);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(compositeBuffer, new RectangleF(0, 0, 256, 256), new RectangleF(0, 0, 432, 432), GraphicsUnit.Pixel);
            g.Dispose();
            return thumbnail;
        }

        protected override void Dispose(bool disposing)
        {
            compositeBuffer.Dispose();
            spriteBuffer.Dispose();
            bgBuffer.Dispose();
            base.Dispose(disposing);
        }
    }
}
