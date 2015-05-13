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

namespace Reuben.UI.Controls
{
    public class SpritesViewer : Control
    {
        private Bitmap buffer;

        public SpritesViewer()
        {
            drawBoundCache = new List<Tuple<Sprite, Rectangle>>();
            this.Width = 256;
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

        private GraphicsController graphics;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsController Graphics
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
                int lastY = 0, targetY =0;
                foreach (SpriteDefinition def in Sprites.SpriteData.Definitions)
                {
                    Sprite s = new Sprite();
                    s.X = 1;
                    targetY = s.Y = lastY / 16 + 1;
                    s.ObjectID = def.GameID;
                    Rectangle drawArea = Sprites.GetClipBounds(s);
                    while (drawArea.X < 0)
                    {
                        s.X++;
                        drawArea = Sprites.GetClipBounds(s);
                    }
                    while (drawArea.Y < targetY * 16)
                    {
                        s.Y++;
                        drawArea = Sprites.GetClipBounds(s);
                    }

                    lastY = drawArea.Bottom + 16;
                    drawBoundCache.Add(new Tuple<Sprite, Rectangle>(s, drawArea));
                }

                buffer = new Bitmap(256, lastY, PixelFormat.Format32bppArgb);
                this.Height = lastY;
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
                        quickSpriteReference[i / 4][i % 4] = ColorReference[Palette.SpriteValues[i]];
                    }

                    quickSpriteReference[0][1] = Color.Black;
                    quickSpriteReference[0][2] = Color.White;
                    quickSpriteReference[0][3] = Color.White;

                    using (Graphics gfx = System.Drawing.Graphics.FromImage(buffer))
                    {
                        gfx.Clear(quickSpriteReference[1][0]);
                    }
                }
            }
        }

        private Color[][] quickSpriteReference;

        public void UpdateGraphics()
        {
            if (colors == null || graphics == null || sprites == null || palette == null)
            {
                return;
            }

            BitmapData data = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

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

            SpriteDefinition definition = Sprites.GetDefinition(sprite.ObjectID);
            foreach (var info in definition.SpriteInfo)
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

                Tile tile1 = Graphics.GetTileByBankIndex(info.Table, info.Value % 0x40);
                Tile tile2 = Graphics.GetTileByBankIndex(info.Table, (info.Value % 0x40) + 1);
                if (info.HorizontalFlip && info.VerticalFlip)
                {
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile1, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile2, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                }
                else if (info.HorizontalFlip)
                {
                    Drawer.DrawTileHorizontalFlipAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalFlipAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
                else if (info.VerticalFlip)
                {
                    Drawer.DrawTileVerticalFlipAlpha(tile1, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileVerticalFlipAlpha(tile2, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                }
                else
                {
                    Drawer.DrawTileAlpha(tile1, xOffset, yOffset, quickSpriteReference[paletteIndex], bitmap);
                    Drawer.DrawTileAlpha(tile2, xOffset, yOffset + 8, quickSpriteReference[paletteIndex], bitmap);
                }
            }

            string safeName = definition.Name.ToUpper();
            for (int i = 0; i < safeName.Length; i++)
            {
                if (i >= 32)
                {
                    break;
                }

                int tile = safeName[i] - 'A';
                if (tile < 0)
                {
                    continue;
                }
                Drawer.DrawTileAlpha(Graphics.GetExtraTileByBankIndex(0, tile), i * 8, y - 8, quickSpriteReference[0], bitmap);
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
            if (buffer == null)
            {
                e.Graphics.Clear(Color.Gray);
            }
            else
            {
                e.Graphics.DrawImage(buffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
                if (SelectionRectangle != null)
                {
                    e.Graphics.DrawRectangle(Pens.White, SelectionRectangle);
                    e.Graphics.DrawRectangle(Pens.Red, new Rectangle(SelectionRectangle.X + 1, SelectionRectangle.Y + 1, SelectionRectangle.Width - 2, SelectionRectangle.Height - 2));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }
    }
}
