using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using Reuben.NESGraphics;
using Reuben.Model;
using Reuben.Controllers;
using System.ComponentModel;

namespace Reuben.UI
{
    public class SpriteViewer : Control
    {
        Bitmap buffer;

        public SpriteViewer()
        {
            buffer = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
            HighlightedSpriteInfo = new List<SpriteInfo>();
        }

        GraphicsController localGraphicsController;
        SpriteController localSpriteController;
        public void Initialize(GraphicsController graphicsController, SpriteController spriteController, Color[] colors, Palette palette, Palette overlayPalette)
        {
            currentSprite = new Sprite() { X = 0, Y = 0 };
            localGraphicsController = graphicsController;
            localSpriteController = spriteController;
            

            localColorReference = colors;
            localOverlayPalette = overlayPalette;
            localPalette = palette;

            if (localPalette != null && localColorReference != null && localOverlayPalette != null)
            {
                quickSpriteReference = new Color[4][];
                quickOverlayReference = new Color[4][];

                quickSpriteReference[0] = new Color[4];
                quickSpriteReference[1] = new Color[4];
                quickSpriteReference[2] = new Color[4];
                quickSpriteReference[3] = new Color[4];

                quickOverlayReference[0] = new Color[4];
                quickOverlayReference[1] = new Color[4];
                quickOverlayReference[2] = new Color[4];
                quickOverlayReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickSpriteReference[i / 4][i % 4] = localColorReference[localPalette.SpriteValues[i]];
                    quickOverlayReference[i / 4][i % 4] = localColorReference[localOverlayPalette.SpriteValues[i]];
                }

                quickSpriteReference[0][1] = Color.Black;
                quickSpriteReference[0][2] = Color.White;
                quickSpriteReference[0][3] = Color.White;
            }

            this.Width = buffer.Width;
            this.Height = buffer.Height;
        }

        public void Update(Color[] colors = null, Palette palette = null, Palette overlayPalette = null)
        {
            localColorReference = colors ?? localColorReference;
            localPalette = palette ?? localPalette;
            localOverlayPalette = overlayPalette ?? localOverlayPalette;
            if (localPalette != null && localColorReference != null && localOverlayPalette != null)
            {
                quickSpriteReference = new Color[4][];
                quickOverlayReference = new Color[4][];

                quickSpriteReference[0] = new Color[4];
                quickSpriteReference[1] = new Color[4];
                quickSpriteReference[2] = new Color[4];
                quickSpriteReference[3] = new Color[4];

                quickOverlayReference[0] = new Color[4];
                quickOverlayReference[1] = new Color[4];
                quickOverlayReference[2] = new Color[4];
                quickOverlayReference[3] = new Color[4];

                for (int i = 0; i < 16; i++)
                {
                    quickSpriteReference[i / 4][i % 4] = localColorReference[localPalette.SpriteValues[i]];
                    quickOverlayReference[i / 4][i % 4] = localColorReference[localOverlayPalette.SpriteValues[i]];
                }

                quickSpriteReference[0][1] = Color.Black;
                quickSpriteReference[0][2] = Color.White;
                quickSpriteReference[0][3] = Color.White;
            }
            UpdateGraphics();
        }

        private SpriteDefinition definition;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteDefinition CurrentDefinition
        {
            get { return definition; }
            set
            {
                definition = value;
                if (definition != null)
                {
                    UpdateGraphics();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DisplaySpecialTiles { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Property { get; set; }

        private Color[] localColorReference;
        private Color[][] quickSpriteReference;
        private Color[][] quickOverlayReference;
        private Palette localPalette;
        private Palette localOverlayPalette;

        private Sprite currentSprite;
        public void UpdateGraphics()
        {
            if (CurrentDefinition == null || localPalette == null || localColorReference == null || localGraphicsController == null || localSpriteController == null || localOverlayPalette == null)
            {
                Graphics.FromImage(buffer).Clear(Color.Black);
                return;
            }

            currentSprite.ObjectID = CurrentDefinition.GameID;
            Rectangle bounds = localSpriteController.GetClipBounds(currentSprite);

            int x = (buffer.Width / 2) - bounds.Width / 2;
            int y = (buffer.Height / 2) - bounds.Height / 2;

            
            BitmapData bitmap = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Drawer.FillArea(new Rectangle(0, 0, buffer.Width, buffer.Height), quickSpriteReference[0][0], bitmap);
            foreach (var info in definition.SpriteInfo)
            {
                if (info.Properties.Count > 0 && !info.Properties.Contains(Property))
                {
                    // if the info is property specific, only sprites with that sprite draw that tile
                    continue;
                }


                int paletteIndex;
                int xOffset = x + info.X;
                int yOffset = y + info.Y;
                if (xOffset < 0 || y < 0 ||
                    xOffset >= buffer.Width - 8 ||
                    yOffset >= buffer.Height - 8)
                {
                    // prevent overflow drawing
                    continue;
                }

                Tile tile1, tile2;
                Color[][] colorReference;

                if (info.Overlay)
                {
                    if (!DisplaySpecialTiles)
                    {
                        continue;
                    }

                    tile1 = localGraphicsController.GetExtraTileByBankIndex(info.Table, info.Value);
                    tile2 = localGraphicsController.GetExtraTileByBankIndex(info.Table, info.Value + 1);
                    colorReference = quickOverlayReference;
                    
                }
                else
                {
                    tile1 = localGraphicsController.GetTileByBankIndex(info.Table, info.Value % 0x40);
                    tile2 = localGraphicsController.GetTileByBankIndex(info.Table, (info.Value % 0x40) + 1);
                    colorReference = quickSpriteReference;
                }

                paletteIndex = (info.Palette >= 0 ? info.Palette : info.Palette * -1) % 4;

                if (info.HorizontalFlip && info.VerticalFlip)
                {
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile1, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalVerticalFlipAlpha(tile2, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                }
                else if (info.HorizontalFlip)
                {
                    Drawer.DrawTileHorizontalFlipAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileHorizontalFlipAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                }
                else if (info.VerticalFlip)
                {
                    Drawer.DrawTileVerticalFlipAlpha(tile1, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileVerticalFlipAlpha(tile2, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                }
                else
                {
                    Drawer.DrawTileAlpha(tile1, xOffset, yOffset, colorReference[paletteIndex], bitmap);
                    Drawer.DrawTileAlpha(tile2, xOffset, yOffset + 8, colorReference[paletteIndex], bitmap);
                }

                if (HighlightedSpriteInfo.Contains(info))
                {
                    Drawer.FillTileAsAlpha(xOffset, yOffset, Color.White, .25, bitmap);
                    Drawer.FillTileAsAlpha(xOffset, yOffset + 8, Color.White, .25, bitmap);
                }
            }

            buffer.UnlockBits(bitmap);
            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<SpriteInfo> HighlightedSpriteInfo { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (localPalette == null || localColorReference == null || localGraphicsController == null || localSpriteController == null || localOverlayPalette == null)
            {
                e.Graphics.Clear(Color.Black);
            }
            else
            {
                e.Graphics.DrawImage(buffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }
    }
}
