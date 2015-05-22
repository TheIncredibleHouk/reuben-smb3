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

        public void Initialize(Level level, LevelType levelType, Palette levelPalette, Palette overlayPalette, Color[] colors, PatternTable patternTable, PatternTable overlayTable, SpriteController spriteController, LevelController levelController, GraphicsController grahicsController)
        {
            localLevel = level;
            localLevelType = levelType;
            localPalette = levelPalette;
            localColors = colors;
            localOverlayPalette = overlayPalette;
            localPatternTable = patternTable;
            localOverlayTable = overlayTable;
            localSpriteController = spriteController;
            localGraphicsController = grahicsController;
            localLevelController = levelController;

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

        public void Update(Level level = null, LevelType levelType = null, Palette levelPalette = null, Palette overlayPalette = null, Color[] colors = null, PatternTable patternTable = null, PatternTable overlayTable = null)
        {
            localLevel = level ?? localLevel;
            localLevelType = levelType ?? localLevelType;
            localPalette = levelPalette ?? localPalette;
            localColors = colors;
            localOverlayPalette = overlayPalette ?? localOverlayPalette;
            localPatternTable = patternTable ?? localPatternTable;
            localOverlayTable = overlayTable ?? localOverlayTable;

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
                    quickBGOverlayReference[i / 4][i % 4] = localColors[overlayPalette.BackgroundValues[i]];
                    quickSpriteOverlayReference[i / 4][i % 4] = localColors[overlayPalette.SpriteValues[i]];
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

            UpdateBGArea(0, 0, 0xF0, 0x1B);
        }

        private Level localLevel;
        private LevelType localLevelType;
        private Palette localPalette;
        private Palette localOverlayPalette;
        private Color[] localColors;
        private PatternTable localOverlayTable;
        private PatternTable localPatternTable;
        private SpriteController localSpriteController;
        private LevelController localLevelController;
        private GraphicsController localGraphicsController;

        private Color[][] quickBGReference;
        private Color[][] quickSpriteReference;
        private Color[][] quickBGOverlayReference;
        private Color[][] quickSpriteOverlayReference;

        public bool ShowInteractionOverlays { get; set; }
        public bool ShowSolidityOverlays { get; set; }

        private bool blockUpdating;
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
            List<Tuple<Sprite, Rectangle>> spriteBounds = localSpriteController.GetBounds(sprites).ToList();
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

            List<Tuple<Sprite, Rectangle>> affectedSprites = localSpriteController.GetBounds(localLevel.Sprites).Where(r => r.Item2.IntersectsWith(area)).ToList(); // find the ones that are affected by the update
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

            if (ShowSolidityOverlays)
            {

            }

            if (ShowInteractionOverlays)
            {
                if (block.BlockSolidity == 0x80 || block.BlockSolidity == 0xF0 || block.BlockInteraction > 0x00)
                {
                    Block overlayBlock = localLevelController.GetOverlay(block);
                    if (overlayBlock != null)
                    {
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.UpperLeft), column * 16, row * 16, quickBGOverlayReference[paletteIndex], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.UpperRight), column * 16 + 8, row * 16, quickBGOverlayReference[paletteIndex], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.LowerLeft), column * 16, row * 16 + 8, quickBGOverlayReference[paletteIndex], .75, bitmap);
                        Drawer.DrawTileAsAlpha(localOverlayTable.GetTileByIndex(overlayBlock.LowerRight), column * 16 + 8, row * 16 + 8, quickBGOverlayReference[paletteIndex], .75, bitmap);
                    }
                }
            }
        }


        private void DrawSprite(Sprite sprite, BitmapData bitmap)
        {
            int x = sprite.X * 16;
            int y = sprite.Y * 16;

            foreach (var info in localSpriteController.GetDefinition(sprite.ObjectID).SpriteInfo)
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

                Tile tile1 = localGraphicsController.GetTileByBankIndex(info.Table, info.Value);
                Tile tile2 = localGraphicsController.GetTileByBankIndex(info.Table, info.Value + 1);
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
                //gfx.DrawImage(bgBuffer, area, area, GraphicsUnit.Pixel);
                gfx.DrawImage(spriteBuffer, area, area, GraphicsUnit.Pixel);
            }

            Invalidate(area);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (localLevel == null || localPatternTable == null || localColors == null)
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
                        Rectangle drawRectangle = localSpriteController.GetClipBounds(s);
                        e.Graphics.DrawRectangle(Pens.White, drawRectangle);
                        e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(drawRectangle.X + 1, drawRectangle.Y + 1, drawRectangle.Width - 2, drawRectangle.Height - 2));
                    }
                }

                blockUpdating = false;
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
    }
}
