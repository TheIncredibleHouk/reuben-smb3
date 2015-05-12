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
using Reuben.Controllers;

namespace Reuben.UI
{
    public class BlocksViewer : Control
    {
        private Bitmap buffer;

        public BlocksViewer()
        {
            drawBoundCache = new List<Tuple<Sprite, Rectangle>>();
            this.Width = 128;
            this.Height = 256;
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

        private GraphicsController gfx;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsController Graphics
        {
            get
            {
                return gfx;
            }
            set
            {
                gfx = value;
            }
        }

        private SpriteController sprites;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteController Sprites
        {
            get
            {
                return sprites;
            }
            set
            {
                sprites = value;
                drawBoundCache.Clear();
                int lastY = 0;
                foreach (SpriteDefinition def in Sprites.SpriteData.Definitions)
                {
                    Sprite s = new Sprite();
                    s.X = 0;
                    s.Y = lastY;
                    s.ObjectID = def.GameID;
                    Rectangle drawArea = Sprites.GetClipBounds(s);
                    lastY = drawArea.Bottom + 16;
                    drawBoundCache.Add(new Tuple<Sprite, Rectangle>(s, drawArea));
                }

                buffer = new Bitmap(128, lastY, PixelFormat.Format32bppArgb);
            }
        }


        private List<Tuple<Sprite, Rectangle>> drawBoundCache;

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
                    quickSpriteReference = new Color[4][];

                    quickSpriteReference[0] = new Color[4];
                    quickSpriteReference[1] = new Color[4];
                    quickSpriteReference[2] = new Color[4];
                    quickSpriteReference[3] = new Color[4];

                    for (int i = 0; i < 16; i++)
                    {
                        quickSpriteReference[i / 4][i % 4] = ColorReference[Palette.BackgroundValues[i]];
                    }
                }
            }
        }

        private Color[][] quickSpriteReference;

        public void UpdateGraphics()
        {
            if (colors == null || graphics == null || blocks == null || palette == null)
            {
                using (Graphics gfx2 = System.Drawing.Graphics.FromImage(buffer))
                {
                    gfx2.Clear(Color.Black);
                }

                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            foreach (var item in drawBoundCache)
            {
                DrawSprite(item.Item1, data);
            }

            buffer.UnlockBits(data);
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
                    xOffset >= buffer.Width - 8 ||
                    yOffset >= buffer.Height - 8)
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
