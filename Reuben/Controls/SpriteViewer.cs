using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Daiz.Library;
using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public unsafe class SpriteViewer : Control
    {
        public event EventHandler<TEventArgs<Sprite>> SelectionChanged;

        public SpriteViewer(int SpriteCount)
        {
            SpriteBuffer = new Bitmap(SpriteCount * 64, 48);
            QuickColorLookup = new Color[8, 4];
            SpecialColors = new Color[8, 4];
            this.Width = SpriteCount * 64;
            this.Height = 48;
            Invalidate();
        }

        private Bitmap SpriteBuffer;

        private PaletteInfo _CurrentPalette;
        public PaletteInfo CurrentPalette
        {
            set
            {
                _CurrentPalette = value;
                UpdateColors();
                FullSpriteRender();
                Invalidate();
            }
        }
        private Color[,] QuickColorLookup;
        private Color[,] SpecialColors;

        public PaletteInfo SpecialPalette
        {
            set
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        SpecialColors[j, i] = ProjectController.ColorManager.Colors[value[j, i]];
                    }
                }
            }
        }

        private void UpdateColors()
        {
            if (_CurrentPalette != null)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        QuickColorLookup[j, i] = ProjectController.ColorManager.Colors[_CurrentPalette[j, i]];
                    }
                }
            }
        }

        public List<Sprite> SpriteList { get; set; }
        private void FullSpriteRender()
        {
            Graphics g = Graphics.FromImage(SpriteBuffer);
            g.Clear(QuickColorLookup[0, 0]);
            g.Dispose();

            if (_CurrentPalette == null)
                return;

            BitmapData data = SpriteBuffer.LockBits(new Rectangle(0, 0, SpriteBuffer.Width, 48), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            foreach (var s in SpriteList)
            {
                SpriteDefinition def = IsViewingMapSprites ?  ProjectController.SpriteManager.GetMapDefinition(s.InGameID) : ProjectController.SpriteManager.GetDefinition(s.InGameID);
                foreach (var sp in def.Sprites)
                {
                    if (sp.Table < 0) continue;
                    if (!sp.HorizontalFlip && !sp.VerticalFlip)
                    {
                        RenderSprite(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), s.X * 16 + sp.X, s.Y * 16 + sp.Y, sp.Palette, data);
                        RenderSprite(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), s.X * 16 + sp.X, (s.Y * 16 + sp.Y) + 8, sp.Palette, data);
                    }
                    else if (sp.HorizontalFlip && !sp.VerticalFlip)
                    {
                        RenderSpriteHorizontalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), s.X * 16 + sp.X, s.Y * 16 + sp.Y, sp.Palette, data);
                        RenderSpriteHorizontalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), s.X * 16 + sp.X, (s.Y * 16 + sp.Y) + 8, sp.Palette, data);
                    }
                    else if (!sp.HorizontalFlip && sp.VerticalFlip)
                    {
                        RenderSpriteVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), s.X * 16 + sp.X, s.Y * 16 + sp.Y, sp.Palette, data);
                        RenderSpriteVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), s.X * 16 + sp.X, (s.Y * 16 + sp.Y) + 8, sp.Palette, data);
                    }
                    else
                    {
                        RenderSpriteHorizontalVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), s.X * 16 + sp.X, s.Y * 16 + sp.Y, sp.Palette, data);
                        RenderSpriteHorizontalVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), s.X * 16 + sp.X, (s.Y * 16 + sp.Y) + 8, sp.Palette, data);
                    }
                }
            }

            SpriteBuffer.UnlockBits(data);
        }

        private void RenderSprite(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            if (x < 0 || y < 0 || x >= SpriteBuffer.Width || (y + 8) > SpriteBuffer.Height) return;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tile[j, i] == 0) continue;
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c;
                    if (PaletteIndex > 0)
                    {
                        c = QuickColorLookup[PaletteIndex + 4, tile[j, i]];
                    }
                    else
                    {
                        c = SpecialColors[PaletteIndex * -1, tile[j, i]];
                    }

                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }


        private void RenderSpriteHorizontalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            if (x < 0 || y < 0 || x >= SpriteBuffer.Width || (y + 8) > SpriteBuffer.Height) return;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (tile[j, i] == 0) continue;
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = ((7 - j) * 3) + offset;
                    Color c;
                    if (PaletteIndex > 0)
                    {
                        c = QuickColorLookup[PaletteIndex + 4, tile[j, i]];
                    }
                    else
                    {
                        c = SpecialColors[PaletteIndex * -1, tile[j, i]];
                    }

                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }


        private void RenderSpriteVerticalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            if (x < 0 || y < 0 || x >= SpriteBuffer.Width || (y + 8) > SpriteBuffer.Height) return;
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tile[j, i] == 0) continue;
                    long offset = (data.Stride * (y + (7 - i))) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c;
                    if (PaletteIndex > 0)
                    {
                        c = QuickColorLookup[PaletteIndex + 4, tile[j, i]];
                    }
                    else
                    {
                        c = SpecialColors[PaletteIndex * -1, tile[j, i]];
                    }

                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }


        private void RenderSpriteHorizontalVerticalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            if (x < 0 || y < 0 || x >= SpriteBuffer.Width || (y + 8) > SpriteBuffer.Height) return;
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (tile[j, i] == 0) continue;
                    long offset = (data.Stride * (y + (7 - i))) + (x * 3);
                    long xOffset = ((7 - j) * 3) + offset;
                    Color c;
                    if (PaletteIndex > 0)
                    {
                        c = QuickColorLookup[PaletteIndex + 4, tile[j, i]];
                    }
                    else
                    {
                        c = SpecialColors[PaletteIndex * -1, tile[j, i]];
                    }

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
            e.Graphics.DrawImage(SpriteBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            if (SelectionSet)
            {
                e.Graphics.DrawRectangle(Pens.White, new Rectangle(_SelectionRectangle.X * 16, _SelectionRectangle.Y * 16, (_SelectionRectangle.Width * 16) - 1, (_SelectionRectangle.Height * 16) - 1));
                e.Graphics.DrawRectangle(Pens.Red, new Rectangle((_SelectionRectangle.X * 16) + 1, (_SelectionRectangle.Y * 16) + 1, (_SelectionRectangle.Width * 16) - 3, (_SelectionRectangle.Height * 16) - 3));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private bool SelectionSet = false;

        public void ClearSelection()
        {
            SelectionSet = false;
            _SelectionRectangle = new Rectangle(0, 0, 0, 0);
        }

        private Rectangle _SelectionRectangle;
        public Rectangle SelectionionRectangle
        {
            get { return _SelectionRectangle; }
            set
            {
                if (_SelectionRectangle == value) return;
                _SelectionRectangle = value;
                SelectionSet = true;
                Invalidate(new Rectangle(0, 0, 0, 0));
            }
        }

        public void UpdateSprites()
        {
            FullSpriteRender();
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;

            SelectedSprite = SelectSprite(x, y);
        }

        private Sprite SelectSprite(int x, int y)
        {
            var possibleSprites = (from s in SpriteList
                                   where x >= s.X && x <= s.X + (s.Width - 1) &&
                                         y >= s.Y && y <= s.Y + (s.Height - 1)
                                   select s).FirstOrDefault();
            if (possibleSprites == null)
            {
                return SelectedSprite;
            }

            return possibleSprites;
        }

        private Sprite _SelectedSprite;
        public Sprite SelectedSprite
        {
            get { return _SelectedSprite; }
            set
            {
                _SelectedSprite = value;
                if (_SelectedSprite != null)
                {
                    SelectionionRectangle = new Rectangle(SelectedSprite.X, SelectedSprite.Y, SelectedSprite.Width, SelectedSprite.Height);
                    if (SelectionChanged != null)
                    {
                        SelectionChanged(this, new TEventArgs<Sprite>(value));
                    }
                }
                else
                {
                    ClearSelection();
                }

                FullSpriteRender();
                Invalidate();
            }
        }

        public void SetSelectedSprite(Sprite sprite)
        {
            _SelectedSprite = SpriteList.Find(s => s.InGameID == sprite.InGameID);
            if (_SelectedSprite != null)
            {
                SelectionionRectangle = new Rectangle(SelectedSprite.X, SelectedSprite.Y, SelectedSprite.Width, SelectedSprite.Height);
            }
            else
            {
                ClearSelection();
            }

            FullSpriteRender();
            Invalidate();
        }

        public bool IsViewingMapSprites { get; set; }
    }
}
