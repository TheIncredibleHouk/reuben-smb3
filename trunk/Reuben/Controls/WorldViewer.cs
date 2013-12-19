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
    public unsafe class  WorldViewer : Control
    {

        public WorldViewer()
        {
            QuickColorLookup = new Color[8, 4];
            SpecialColors = new Color[8, 4];
            CurrentDefiniton = null;
            Redraw();
            HasSelection = false;
            Zoom = 1;
        }

        public Bitmap BackBuffer { get; private set; }
        private Bitmap CompositeBuffer;
        private Bitmap SpriteBuffer;

        private World _CurrentWorld;
        public World CurrentWorld
        {
            get { return _CurrentWorld; }
            set
            {
                _CurrentWorld = value;
                if (_CurrentWorld != null)
                {
                    _CurrentWorld.TileChanged += new EventHandler<TEventArgs<Point>>(_CurrentWorld_TileChanged);
                    _CurrentWorld.SpriteAdded += new EventHandler<TEventArgs<Sprite>>(_CurrentLevel_SpriteAdded);
                    _CurrentWorld.SpriteRemoved += new EventHandler<TEventArgs<Sprite>>(_CurrentWorld_SpriteRemoved);

                    BackBuffer = new Bitmap(_CurrentWorld.Width * 16, _CurrentWorld.Height * 16, PixelFormat.Format32bppArgb);
                    SpriteBuffer = new Bitmap(_CurrentWorld.Width * 16, _CurrentWorld.Height * 16, PixelFormat.Format32bppArgb);
                    CompositeBuffer = new Bitmap(_CurrentWorld.Width * 16, _CurrentWorld.Height * 16, PixelFormat.Format32bppArgb);
                    this.Width = _CurrentWorld.Width * 16; ;
                    this.Height = _CurrentWorld.Height * 16;
                    if (!DelayDrawing)
                    {
                        FullRender();
                        FullSpriteRender();
                        Redraw();
                    }
                }
            }
        }

        private void _CurrentWorld_TileChanged(object sender, TEventArgs<Point> e)
        {
            UpdateBlock(e.Data.X, e.Data.Y);
        }

        private void _CurrentWorld_SpriteRemoved(object sender, TEventArgs<Sprite> e)
        {

            FullSpriteRender();
            Redraw();
        }

        private void _CurrentLevel_SpriteAdded(object sender, TEventArgs<Sprite> e)
        {
            SpriteDefinition sp = ProjectController.SpriteManager.GetMapDefinition(e.Data.InGameID);
            Rectangle r = new Rectangle(e.Data.X * 16 + sp.MaxLeftX, e.Data.Y * 16 + sp.MaxTopY, sp.MaxRightX - sp.MaxLeftX, sp.MaxBottomY - sp.MaxTopY);
            FullSpriteRender(r);
            Redraw();
        }

        private PatternTable _SpecialTable;
        public PatternTable SpecialTable
        {
            get { return _SpecialTable; }
            set
            {
                _SpecialTable = value;

                if (!DelayDrawing)
                {
                    FullRender();
                    Redraw();
                }
            }
        }

        private PatternTable _CurrentTable;
        public PatternTable CurrentTable
        {
            get
            {
                return _CurrentTable;
            }
            set
            {
                if (_CurrentTable != null)
                {
                    _CurrentTable.GraphicsChanged -= _CurrentTable_GraphicsChanged;
                }

                _CurrentTable = value;

                if (_CurrentTable != null)
                {
                    _CurrentTable.GraphicsChanged += new EventHandler<TEventArgs<int>>(_CurrentTable_GraphicsChanged);
                }

                if (!DelayDrawing)
                {
                    FullRender();
                    Redraw();
                }
            }
        }

        void _CurrentTable_GraphicsChanged(object sender, TEventArgs<int> e)
        {
            if (!DelayDrawing)
            {
                FullRender();
                Redraw();
            }
        }

        private BlockDefinition _CurrentDefiniton;
        public BlockDefinition CurrentDefiniton
        {
            get { return _CurrentDefiniton; }
            set
            {
                _CurrentDefiniton = value;
                if (!DelayDrawing)
                {
                    FullRender();
                    Redraw();
                }
            }
        }

        private PaletteInfo _CurrentPalette;
        public PaletteInfo CurrentPalette
        {
            set
            {
                _CurrentPalette = value;
                UpdateColors();
                if (!DelayDrawing)
                {
                    FullRender();
                    FullSpriteRender();
                    Redraw();
                }
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

        private void FullRender()
        {
            if (BackBuffer == null) return;

            if (_CurrentTable == null || _CurrentPalette == null || _CurrentDefiniton == null)
            {
                Graphics.FromImage(BackBuffer).Clear(Color.Black);
                return;
            }

            BitmapData data = BackBuffer.LockBits(new Rectangle(0, 0, BackBuffer.Width, BackBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            for (int i = 0; i < _CurrentWorld.Height; i++)
            {
                for (int j = 0; j < _CurrentWorld.Width; j++)
                {
                    int tileValue = CurrentWorld.LevelData[j, i];
                    int PaletteIndex = tileValue / 0x40;
                    Block b = CurrentDefiniton[tileValue];
                    RenderTile(_CurrentTable[b[0, 0]], j * 16, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[0, 1]], j * 16, i * 16 + 8, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 0]], j * 16 + 8, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 1]], j * 16 + 8, i * 16 + 8, PaletteIndex, data);

                    if (_ShowPointers)
                    {
                        WorldPointer p = CurrentWorld.Pointers.Find(pt => (pt.X == j && pt.Y == i));
                        if (p != null)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0xA2], j * 16, i * 16, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xB2], j * 16, i * 16 + 8, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xA3], j * 16 + 8, i * 16, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xB3], j * 16 + 8, i * 16 + 8, 5, data, 1.0);
                        }
                    }
                }
            }
            BackBuffer.UnlockBits(data);
        }

        private void FullSpriteRender()
        {
            if (BackBuffer == null) return;
            FullSpriteRender(new Rectangle(0, 0, SpriteBuffer.Width, SpriteBuffer.Height));
        }

        private void FullSpriteRender(Rectangle rect)
        {
            if (rect.X < 0) rect.X = 0;
            if (rect.Y < 0) rect.Y = 0;
            if (rect.X > SpriteBuffer.Width) rect.X = SpriteBuffer.Width;
            if (rect.Y > SpriteBuffer.Height) rect.Y = SpriteBuffer.Height;
            if ((rect.X + rect.Width) > SpriteBuffer.Width) rect.Width = SpriteBuffer.Width - rect.X;
            if ((rect.Y + rect.Height) > SpriteBuffer.Height) rect.Height = SpriteBuffer.Height - rect.Y;

            if (_CurrentPalette == null)
                return;

            BitmapData data = SpriteBuffer.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            ClearAreaWithTransparentcolor(rect.Width, rect.Height, data);

            int definiteX, definiteY;

            foreach (var s in CurrentWorld.SpriteData)
            {
                SpriteDefinition def = ProjectController.SpriteManager.GetMapDefinition(s.InGameID);
                if (def == null) continue;
                foreach (var sp in def.Sprites)
                {
                    if (sp.Table < 0) continue;
                    definiteX = s.X * 16 + sp.X;
                    definiteY = s.Y * 16 + sp.Y;

                    if ((definiteX + 8) < rect.X || definiteX - 8 > rect.X + rect.Width) continue;
                    if ((definiteY + 16) < rect.Y || definiteY - 16 > rect.Y + rect.Height) continue;

                    definiteX = definiteX - rect.X;
                    definiteY = definiteY - rect.Y;
                    if (!sp.HorizontalFlip && !sp.VerticalFlip)
                    {
                        RenderSprite(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), definiteX, definiteY, sp.Palette, data);
                        RenderSprite(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), definiteX, (definiteY) + 8, sp.Palette, data);
                    }
                    else if (sp.HorizontalFlip && !sp.VerticalFlip)
                    {
                        RenderSpriteHorizontalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), definiteX, definiteY, sp.Palette, data);
                        RenderSpriteHorizontalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), definiteX, (definiteY) + 8, sp.Palette, data);
                    }
                    else if (!sp.HorizontalFlip && sp.VerticalFlip)
                    {
                        RenderSpriteVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), definiteX, definiteY, sp.Palette, data);
                        RenderSpriteVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), definiteX, (definiteY) + 8, sp.Palette, data);
                    }
                    else
                    {
                        RenderSpriteHorizontalVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value + 1), definiteX, definiteY, sp.Palette, data);
                        RenderSpriteHorizontalVerticalFlip(ProjectController.GraphicsManager.QuickTileGrab(sp.Table, sp.Value), definiteX, (definiteY) + 8, sp.Palette, data);
                    }
                }
            }

            SpriteBuffer.UnlockBits(data);
        }

        private void RenderSprite(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tile[j, i] == 0) continue;
                    if (x + j < 0 || x + j >= data.Width) continue;
                    if (y + i < 0 || y + i >= data.Height) continue;
                    long offset = (data.Stride * (y + i)) + (x * 4);
                    long xOffset = (j * 4) + offset;
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
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }


        private void RenderSpriteHorizontalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (tile[j, i] == 0) continue;
                    if (x + (7 - j) < 0 || x + (7 - j) >= data.Width) continue;
                    if (y + i < 0 || y + i >= data.Height) continue;
                    long offset = (data.Stride * (y + i)) + (x * 4);
                    long xOffset = ((7 - j) * 4) + offset;
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
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }


        private void RenderSpriteVerticalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tile[j, i] == 0) continue;
                    if (x + j < 0 || x + j >= data.Width) continue;
                    if (y + (7 - i) < 0 || y + (7 - i) >= data.Height) continue;
                    long offset = (data.Stride * (y + (7 - i))) + (x * 4);
                    long xOffset = (j * 4) + offset;
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
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }


        private void RenderSpriteHorizontalVerticalFlip(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (tile[j, i] == 0) continue;
                    if (x + (7 - j) < 0 || x + (7 - j) >= data.Width) continue;
                    if (y + (7 - i) < 0 || y + (7 - i) >= data.Height) continue;
                    long offset = (data.Stride * (y + (7 - i))) + (x * 4);
                    long xOffset = ((7 - j) * 4) + offset;
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
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }

        private void ClearAreaWithTransparentcolor(int width, int height, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    long offset = data.Stride * i;
                    long xOffset = (j * 4) + offset;

                    *(dataPointer + xOffset) = 0;
                    *(dataPointer + xOffset + 1) = 0;
                    *(dataPointer + xOffset + 2) = 0;
                    *(dataPointer + xOffset + 3) = 0;
                }
            }
        }

        private void UpdateBlock(int x, int y)
        {
            BitmapData data = BackBuffer.LockBits(new Rectangle(x*16, y*16, 16, 16), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int tileValue = CurrentWorld.LevelData[x, y];
            int PaletteIndex = tileValue / 0x40;
            Block b = CurrentDefiniton[tileValue];
            RenderTile(_CurrentTable[b[0, 0]], 0, 0, PaletteIndex, data);
            RenderTile(_CurrentTable[b[0, 1]], 0, 8, PaletteIndex, data);
            RenderTile(_CurrentTable[b[1, 0]], 8, 0, PaletteIndex, data);
            RenderTile(_CurrentTable[b[1, 1]], 8, 8, PaletteIndex, data);

            WorldPointer p = CurrentWorld.Pointers.Find(pt => pt.X == x && pt.Y == y);
            if (p != null)
            {
                RenderSpecialTileAlpha(_SpecialTable[0xA2], 0, 0, 5, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xB2], 0, 8, 5, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xA3], 8, 0, 5, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xB3], 8, 8, 5, data, 1.0);
            }

            BackBuffer.UnlockBits(data);

            if(!DelayDrawing)
                Redraw(new Rectangle(x * 16, y * 16, 16, 16));
        }

        private void RenderTile(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 4);
                    long xOffset = (j * 4) + offset;
                    Color c = QuickColorLookup[PaletteIndex, tile[j, i]];
                    if (_ShowGrid)
                    {
                        if ((j == 0 && x % 16 == 0) || (i == 0 && y % 16 == 0))
                        {
                            if (((j + i) % 2) > 0)
                                c = Color.FromArgb(255, 255, 255);
                            else
                                c = Color.FromArgb(0, 0, 0);
                        }
                    }
                    
                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }

        private void RenderSpecialTileAlpha(Tile tile, int x, int y, int PaletteIndex, BitmapData data, double alpha)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 4);
                    long xOffset = (j * 4) + offset;
                    Color c = SpecialColors[PaletteIndex, tile[j, i]];
                    if(c == Color.Empty) continue;

                    if (_ShowGrid)
                    {
                        if ((j == 0 && x % 16 == 0) || (i == 0 && y % 16 == 0))
                            c = Color.FromArgb(255, 255, 255);
                    }

                    *(dataPointer + xOffset) = (byte) ((1 - alpha) * (*(dataPointer + xOffset)) + (alpha * c.B));
                    *(dataPointer + xOffset + 1) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 1)) + (alpha * c.G));
                    *(dataPointer + xOffset + 2) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 2)) + (alpha * c.R));
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(DelayDrawing) return;
            if (BackBuffer == null) return;

            Rectangle destRect = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);
            Rectangle sourceRect = new Rectangle(e.ClipRectangle.X / Zoom, e.ClipRectangle.Y / Zoom, e.ClipRectangle.Width / Zoom, e.ClipRectangle.Height / Zoom);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            if (Zoom > 1)
            {
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            }
            Graphics g = Graphics.FromImage(CompositeBuffer);
            g.DrawImage(BackBuffer, sourceRect, sourceRect, GraphicsUnit.Pixel);
            g.DrawImage(SpriteBuffer, sourceRect, sourceRect, GraphicsUnit.Pixel);

            if (HasSelection)
            {
                g.DrawRectangle(Pens.White, new Rectangle(_SelectionRectangle.X * 16, _SelectionRectangle.Y * 16, (_SelectionRectangle.Width * 16) - 1, (_SelectionRectangle.Height * 16) - 1));
                g.DrawRectangle(Pens.Red, new Rectangle((_SelectionRectangle.X * 16) + 1, (_SelectionRectangle.Y * 16) + 1, (_SelectionRectangle.Width * 16) - 3, (_SelectionRectangle.Height * 16) - 3));
            }
            if (HasSelectionLine)
            {
                g.DrawLine(Pens.White, _SelectionLine.Start.X * 16 + 8, _SelectionLine.Start.Y * 16 + 8, _SelectionLine.End.X * 16 + 8, _SelectionLine.End.Y * 16 + 8);
                g.DrawLine(Pens.Black, _SelectionLine.Start.X * 16 + 9, _SelectionLine.Start.Y * 16 + 9, _SelectionLine.End.X * 16 + 9, _SelectionLine.End.Y * 16 + 9);
            }


            e.Graphics.DrawImage(CompositeBuffer, destRect, sourceRect, GraphicsUnit.Pixel);
            g.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private bool _ShowGrid;
        public bool ShowGrid
        {
            get { return _ShowGrid; }
            set
            {
                if (_ShowGrid == value) return;
                _ShowGrid = value;
                if (!DelayDrawing)
                {
                    FullRender();
                    Redraw();
                }
            }
        }

        public void ClearSelection()
        {
            HasSelection = false;
            UpdateArea(_SelectionRectangle);
            _SelectionRectangle = new Rectangle(0, 0, 0, 0);
            
        }

        public void ClearLine()
        {
            HasSelectionLine = false;
            if (SelectionLine == null) return;
            Rectangle area = new Rectangle();
            int lowestX, lowestY, highestX, highestY;
            lowestX = _SelectionLine.Start.X < _SelectionLine.End.X ? _SelectionLine.Start.X : _SelectionLine.End.X;
            lowestY = _SelectionLine.Start.Y < _SelectionLine.End.Y ? _SelectionLine.Start.Y : _SelectionLine.End.Y;
            highestX = _SelectionLine.Start.X > _SelectionLine.End.X ? _SelectionLine.Start.X : _SelectionLine.End.X;
            highestY = _SelectionLine.Start.Y > _SelectionLine.End.Y ? _SelectionLine.Start.Y : _SelectionLine.End.Y;
            area.X = lowestX;
            area.Y = lowestY;
            area.Width = highestX - lowestX + 1;
            area.Height = highestY - lowestY + 1;
            UpdateArea(area);
        }

        private Rectangle _SelectionRectangle;
        public Rectangle SelectionRectangle
        {
            get { return _SelectionRectangle; }
            set
            {
                Rectangle updateArea = new Rectangle();

                if (HasSelection)
                {
                    updateArea.X = value.X < _SelectionRectangle.X ? value.X : _SelectionRectangle.X;
                    updateArea.Y = value.Y < _SelectionRectangle.Y ? value.Y : _SelectionRectangle.Y;
                    updateArea.Width = value.X + value.Width > _SelectionRectangle.X + _SelectionRectangle.Width ? value.X + value.Width - updateArea.X : _SelectionRectangle.X + _SelectionRectangle.Width - updateArea.X;
                    updateArea.Height = value.Y + value.Height > _SelectionRectangle.Y + _SelectionRectangle.Height ? value.Y + value.Height - updateArea.Y : _SelectionRectangle.Y + _SelectionRectangle.Height - updateArea.Y;
                }
                else
                {
                    updateArea = value;
                }

                if (_SelectionRectangle == value) return;
                _SelectionRectangle = value;
                HasSelection = true;
                UpdateArea(updateArea);
            }
        }

        public bool DelayDrawing { get; set; }

        public void UpdateArea()
        {
            HasSelection = false;
            Redraw(new Rectangle(_SelectionRectangle.X * 16, _SelectionRectangle.Y * 16, _SelectionRectangle.Width * 16, _SelectionRectangle.Height * 16));
        }

        public void UpdateArea(Rectangle rect)
        {
            
            Redraw(new Rectangle(rect.X * 16, rect.Y * 16, rect.Width * 16, rect.Height * 16));
        }

        public void UpdateSprites()
        {
            FullSpriteRender();
            Redraw();
        }

        public void UpdateSprites(Rectangle r)
        {
            FullSpriteRender(r);
            Redraw(r);
        }

        public void Redraw()
        {
            if (!DelayDrawing)
            {
                if (BackBuffer == null) return;
                Redraw(new Rectangle(0, 0, BackBuffer.Width * Zoom, BackBuffer.Height * Zoom));
            }
        }

        public void Redraw(Rectangle rect)
        {
            if (!DelayDrawing)
            {
                if (BackBuffer == null) return;
                Invalidate(new Rectangle(rect.X * Zoom, rect.Y * Zoom, rect.Width * Zoom, rect.Height * Zoom));
            }
        }

        public void UpdatePoint(int x, int y)
        {
            UpdateBlock(x, y);
        }

        private Line _SelectionLine;
        public Line SelectionLine
        {
            get
            {
                return _SelectionLine;
            }
            set
            {
                if (value == null) return;
                Rectangle area = new Rectangle();
                int lowestX, lowestY, highestX, highestY;
                lowestX = value.Start.X < value.End.X ? value.Start.X : value.End.X;
                lowestY = value.Start.Y < value.End.Y ? value.Start.Y : value.End.Y;
                highestX = value.Start.X > value.End.X ? value.Start.X : value.End.X;
                highestY = value.Start.Y > value.End.Y ? value.Start.Y : value.End.Y;
                if (_SelectionLine != null)
                {
                    int lowestX2, lowestY2, highestX2, highestY2;
                    lowestX2 = _SelectionLine.Start.X < _SelectionLine.End.X ? _SelectionLine.Start.X : _SelectionLine.End.X;
                    lowestY2 = _SelectionLine.Start.Y < _SelectionLine.End.Y ? _SelectionLine.Start.Y : _SelectionLine.End.Y;
                    highestX2 = _SelectionLine.Start.X > _SelectionLine.End.X ? _SelectionLine.Start.X : _SelectionLine.End.X;
                    highestY2 = _SelectionLine.Start.Y > _SelectionLine.End.Y ? _SelectionLine.Start.Y : _SelectionLine.End.Y;

                    lowestX = lowestX2 < lowestX ? lowestX2 : lowestX;
                    lowestY = lowestY2 < lowestY ? lowestY2 : lowestY;
                    highestX = highestX2 > highestX ? highestX2 : highestX;
                    highestY = highestY2 > highestY ? highestY2 : highestY;
                }

                area.X = lowestX;
                area.Y = lowestY;
                area.Width = highestX - lowestX + 1;
                area.Height = highestY - lowestY + 2;
                _SelectionLine = value;
                HasSelectionLine = true;
                UpdateArea(area);
            }
        }

        private int _Zoom;
        public int Zoom
        {
            get { return _Zoom; }
            set
            {
                _Zoom = value;
                if (_CurrentWorld == null) return;
                this.Width = _CurrentWorld.Width * 16 * Zoom;
                this.Height = _CurrentWorld.Height * 16 * Zoom;
                Redraw(new Rectangle(0, 0, BackBuffer.Width, BackBuffer.Height));
            }
        }

        private bool _ShowPointers;
        public bool ShowPointers
        {
            get { return _ShowPointers; }
            set
            {
                _ShowPointers = value;
                FullRender();
                Redraw();
            }
        }

        public bool HasSelection { get; private set; }
        public bool HasSelectionLine { get; private set; }


        public void FullUpdate()
        {
            if (!DelayDrawing)
            {
                FullRender();
                FullSpriteRender();
                Redraw();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _CurrentWorld.TileChanged -=  _CurrentWorld_TileChanged;
            _CurrentWorld.SpriteAdded -=  _CurrentLevel_SpriteAdded;
            _CurrentWorld.SpriteRemoved -=  _CurrentWorld_SpriteRemoved;
            _CurrentTable.GraphicsChanged -= _CurrentTable_GraphicsChanged;
            base.Dispose(disposing);
        }
    }
}
