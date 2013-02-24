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
    public unsafe class LevelViewer : Control
    {

        public LevelViewer()
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

        private Level _CurrentLevel;
        public Level CurrentLevel
        {
            get { return _CurrentLevel; }
            set
            {
                _CurrentLevel = value;
                if (_CurrentLevel != null)
                {
                    _CurrentLevel.TileChanged += new EventHandler<TEventArgs<Point>>(_CurrentLevel_TileChanged);
                    _CurrentLevel.SpriteAdded += new EventHandler<TEventArgs<Sprite>>(_CurrentLevel_SpriteAdded);
                    _CurrentLevel.SpriteRemoved += new EventHandler<TEventArgs<Sprite>>(_CurrentLevel_SpriteRemoved);
                    BackBuffer = new Bitmap(_CurrentLevel.Width * 16, _CurrentLevel.Height * 16, PixelFormat.Format32bppArgb);
                    SpriteBuffer = new Bitmap(_CurrentLevel.Width * 16, _CurrentLevel.Height * 16, PixelFormat.Format32bppArgb);
                    CompositeBuffer = new Bitmap(_CurrentLevel.Width * 16, _CurrentLevel.Height * 16, PixelFormat.Format32bppArgb);
                    this.Width = _CurrentLevel.Width * 16; ;
                    this.Height = _CurrentLevel.Height * 16;
                    if (!DelayDrawing)
                    {
                        FullRender();
                        FullSpriteRender();
                        Redraw();
                    }
                }
            }
        }

        private void _CurrentLevel_TileChanged(object sender, TEventArgs<Point> e)
        {
            UpdateBlock(e.Data.X, e.Data.Y);
        }

        private void _CurrentLevel_SpriteRemoved(object sender, TEventArgs<Sprite> e)
        {
            FullSpriteRender();
            Redraw();
        }

        private void _CurrentLevel_SpriteAdded(object sender, TEventArgs<Sprite> e)
        {
            SpriteDefinition sp = ProjectController.SpriteManager.GetDefinition(e.Data.InGameID);
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

        private bool _ShowSpecialBlocks;
        public bool ShowSpecialBlocks
        {
            get { return _ShowSpecialBlocks; }
            set
            {
                if (BackBuffer == null) return;
                _ShowSpecialBlocks = value;
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
            FullRender();
            Redraw();
        }

        public BlockDefinition SpecialDefnitions { private get; set; }

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
                Graphics.FromImage(BackBuffer).Clear(QuickColorLookup[0, 0]);
                return;
            }

            BitmapData data = BackBuffer.LockBits(new Rectangle(0, 0, BackBuffer.Width, BackBuffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            double transSpecial = CurrentLevel.Settings.ItemTransparency;

            for (int i = 0; i < _CurrentLevel.Height; i++)
            {
                for (int j = 0; j < _CurrentLevel.Width; j++)
                {
                    int tileValue = CurrentLevel.LevelData[j, i];
                    int PaletteIndex = tileValue / 0x40;
                    Block b = CurrentDefiniton[tileValue];

                    RenderTile(_CurrentTable[b[0, 0]], j * 16, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[0, 1]], j * 16, i * 16 + 8, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 0]], j * 16 + 8, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 1]], j * 16 + 8, i * 16 + 8, PaletteIndex, data);

                    if (_ShowBlockProperties)
                    {
                        BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                        BlockProperty bpHigh = bp & BlockProperty.MaskHi;
                        BlockProperty bpLow = bp & BlockProperty.MaskLow;

                        if ((bpHigh & BlockProperty.Alternative) == BlockProperty.Background)
                        {

                            if ((bpHigh & BlockProperty.Solid) > 0)
                            {
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16 + 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16 + 8, 6, data, .75);
                            }

                            else if ((bpHigh & BlockProperty.Water) > 0)
                            {
                                RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16, i * 16 + 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16 + 8, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16 + 8, i * 16 + 8, 6, data, .75);
                            }

                            else if ((bpHigh & BlockProperty.Foreground) > 0)
                            {
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], j * 16, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], j * 16, i * 16 + 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], j * 16 + 8, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], j * 16 + 8, i * 16 + 8, 6, data, .75);
                            }

                            switch (bp)
                            {
                                case BlockProperty.Harmful:
                                    RenderSpecialTileAlpha(_SpecialTable[0x40], j * 16, i * 16, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x50], j * 16, i * 16 + 8, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x41], j * 16 + 8, i * 16, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x51], j * 16 + 8, i * 16 + 8, 4, data, .75);
                                    break;

                                case BlockProperty.Slick:
                                    RenderSpecialTileAlpha(_SpecialTable[0xD2], j * 16, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE2], j * 16, i * 16 + 8, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xD3], j * 16 + 8, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE3], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                    break;

                                case BlockProperty.ConveyorLeft:
                                    RenderSpecialTileAlpha(_SpecialTable[0x28], j * 16, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x28], j * 16, i * 16 + 8, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x38], j * 16 + 8, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x38], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                    break;

                                case BlockProperty.ConveyorRight:
                                    RenderSpecialTileAlpha(_SpecialTable[0x29], j * 16, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x29], j * 16, i * 16 + 8, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x39], j * 16 + 8, i * 16, 1, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x39], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                    break;


                                case BlockProperty.VerticalPipeLeft:
                                case BlockProperty.VerticalPipeRight:
                                    RenderSpecialTileAlpha(_SpecialTable[0x2A], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3A], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x2B], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3B], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.Unstable:
                                    RenderSpecialTileAlpha(_SpecialTable[0x60], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x70], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x61], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x71], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.HighGravity:
                                    RenderSpecialTileAlpha(_SpecialTable[0x3A], j * 16, i * 16, 7, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3A], j * 16, i * 16 + 8, 7, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3B], j * 16 + 8, i * 16, 7, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3B], j * 16 + 8, i * 16 + 8, 7, data, .75);
                                    break;
                            }
                        }
                        else
                        {
                            if ((bpHigh & BlockProperty.Solid) > BlockProperty.Background)
                            {
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16, 6, data, .75);
                            }

                            switch (bp)
                            {
                                case BlockProperty.SlopeFiller:
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeBottomLeft30:
                                    RenderSpecialTileAlpha(_SpecialTable[0x46], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x47], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeTopLeft30:
                                    RenderSpecialTileAlpha(_SpecialTable[0x46], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x47], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeBottomRight30:
                                    RenderSpecialTileAlpha(_SpecialTable[0x48], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x49], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeTopRight30:
                                    RenderSpecialTileAlpha(_SpecialTable[0x48], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x49], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeLeft45:
                                    RenderSpecialTileAlpha(_SpecialTable[0x44], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x44], j * 16 + 8, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeRight45:
                                    RenderSpecialTileAlpha(_SpecialTable[0x45], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x45], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                    break;

                                case BlockProperty.SlopeTop:
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16, 0, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16, 0, data, .75);
                                    break;
                            }
                        }
                    }
                    if (_ShowSpecialBlocks)
                    {
                        SpecialBlock sb = (SpecialBlock)SpecialDefnitions[tileValue];
                        if (sb != null)
                        {
                            int SpecialPaletteIndex = sb.Palette;
                            RenderSpecialTileAlpha(_SpecialTable[sb[0, 0]], j * 16, i * 16, SpecialPaletteIndex, data, transSpecial);
                            RenderSpecialTileAlpha(_SpecialTable[sb[0, 1]], j * 16, i * 16 + 8, SpecialPaletteIndex, data, transSpecial);
                            RenderSpecialTileAlpha(_SpecialTable[sb[1, 0]], j * 16 + 8, i * 16, SpecialPaletteIndex, data, transSpecial);
                            RenderSpecialTileAlpha(_SpecialTable[sb[1, 1]], j * 16 + 8, i * 16 + 8, SpecialPaletteIndex, data, transSpecial);
                        }
                    }

                    if (_ShowPointers)
                    {
                        LevelPointer p = CurrentLevel.Pointers.Find(pt => (pt.XEnter == j || pt.XEnter + 1 == j) && (pt.YEnter == i || pt.YEnter + 1 == i));
                        if (p != null)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0xA2], j * 16, i * 16, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xB2], j * 16, i * 16 + 8, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xA3], j * 16 + 8, i * 16, 5, data, 1.0);
                            RenderSpecialTileAlpha(_SpecialTable[0xB3], j * 16 + 8, i * 16 + 8, 5, data, 1.0);
                        }
                    }

                    if (_DisplayStartingPosition && ((j == CurrentLevel.XStart && i == CurrentLevel.YStart) || (j == CurrentLevel.XAltStart && i == CurrentLevel.YAltStart)))
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xA0], j * 16, i * 16, 4, data, 1.0);
                        RenderSpecialTileAlpha(_SpecialTable[0xB0], j * 16, i * 16 + 8, 4, data, 1.0);
                        RenderSpecialTileAlpha(_SpecialTable[0xA1], j * 16 + 8, i * 16, 4, data, 1.0);
                        RenderSpecialTileAlpha(_SpecialTable[0xB1], j * 16 + 8, i * 16 + 8, 4, data, 1.0);
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

            foreach (var s in CurrentLevel.SpriteData)
            {
                SpriteDefinition def = ProjectController.SpriteManager.GetDefinition(s.InGameID);
                if (def == null) continue;
                foreach (var sp in def.Sprites)
                {
                    if (sp.Table < 0 && !_ShowSpecial) continue;
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
            BitmapData data = BackBuffer.LockBits(new Rectangle(x * 16, y * 16, 16, 16), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int tileValue = CurrentLevel.LevelData[x, y];
            int PaletteIndex = tileValue / 0x40;
            Block b = CurrentDefiniton[tileValue];

            RenderTile(_CurrentTable[b[0, 0]], 0, 0, PaletteIndex, data);
            RenderTile(_CurrentTable[b[0, 1]], 0, 8, PaletteIndex, data);
            RenderTile(_CurrentTable[b[1, 0]], 8, 0, PaletteIndex, data);
            RenderTile(_CurrentTable[b[1, 1]], 8, 8, PaletteIndex, data);

            if (_ShowBlockProperties)
            {
                BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                BlockProperty bpHigh = bp & BlockProperty.MaskHi;
                BlockProperty bpLow = bp & BlockProperty.MaskLow;

                if ((bpHigh & BlockProperty.Alternative) == BlockProperty.Background)
                {

                    if ((bpHigh & BlockProperty.Solid) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 8, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 8, 6, data, .75);
                    }

                    else if ((bpHigh & BlockProperty.Water) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 8, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 8, 6, data, .75);
                    }

                    else if ((bpHigh & BlockProperty.Foreground) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFE], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFE], 0, 8, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFE], 8, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFE], 8, 8, 6, data, .75);
                    }

                    switch (bpLow)
                    {
                        case BlockProperty.Harmful:
                            RenderSpecialTileAlpha(_SpecialTable[0x40], 0, 0, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x50], 0, 8, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x41], 8, 0, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x51], 8, 8, 4, data, .75);
                            break;

                        case BlockProperty.Slick:
                            RenderSpecialTileAlpha(_SpecialTable[0xD2], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE2], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xD3], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE3], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.ConveyorLeft:
                            RenderSpecialTileAlpha(_SpecialTable[0x28], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x28], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x38], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x38], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.ConveyorRight:
                            RenderSpecialTileAlpha(_SpecialTable[0x29], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x29], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x39], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x39], 8, 8, 1, data, .75);
                            break;


                        case BlockProperty.VerticalPipeLeft:
                        case BlockProperty.VerticalPipeRight:
                            RenderSpecialTileAlpha(_SpecialTable[0x2A], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3A], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x2B], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3B], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.Unstable:
                            RenderSpecialTileAlpha(_SpecialTable[0x60], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x70], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x61], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x71], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.HighGravity:
                            RenderSpecialTileAlpha(_SpecialTable[0x3A], 0, 0, 7, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3A], 0, 8, 7, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3B], 8, 0, 7, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3B], 8, 8, 7, data, .75);
                            break;
                    }
                }
                else
                {
                    if ((bpHigh & BlockProperty.Solid) > BlockProperty.Background)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 0, 6, data, .75);
                    }

                    switch (bp)
                    {
                        case BlockProperty.SlopeFiller:
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeBottomLeft30:
                            RenderSpecialTileAlpha(_SpecialTable[0x46], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x47], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeTopLeft30:
                            RenderSpecialTileAlpha(_SpecialTable[0x46], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x47], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeBottomRight30:
                            RenderSpecialTileAlpha(_SpecialTable[0x48], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x49], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeTopRight30:
                            RenderSpecialTileAlpha(_SpecialTable[0x48], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x49], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeLeft45:
                            RenderSpecialTileAlpha(_SpecialTable[0x44], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x44], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeRight45:
                            RenderSpecialTileAlpha(_SpecialTable[0x45], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x45], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SlopeTop:
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 0, 0, data, .75);
                            break;
                    }
                }
            }
            if (_ShowSpecialBlocks)
            {
                SpecialBlock sb = (SpecialBlock)SpecialDefnitions[tileValue];
                if (sb != null)
                {
                    int SpecialPaletteIndex = sb.Palette;
                    RenderSpecialTileAlpha(_SpecialTable[sb[0, 0]], 0, 0, SpecialPaletteIndex, data, .75);
                    RenderSpecialTileAlpha(_SpecialTable[sb[0, 1]], 0, 8, SpecialPaletteIndex, data, .75);
                    RenderSpecialTileAlpha(_SpecialTable[sb[1, 0]], 8, 0, SpecialPaletteIndex, data, .75);
                    RenderSpecialTileAlpha(_SpecialTable[sb[1, 1]], 8, 8, SpecialPaletteIndex, data, .75);
                }
            }

            if (_ShowPointers)
            {
                LevelPointer p = CurrentLevel.Pointers.Find(pt => (pt.XEnter == x || pt.XEnter + 1 == x) && (pt.YEnter == y || pt.YEnter + 1 == y));
                if (p != null)
                {
                    RenderSpecialTileAlpha(_SpecialTable[0xA2], 0, 0, 5, data, 1.0);
                    RenderSpecialTileAlpha(_SpecialTable[0xB2], 0, 8, 5, data, 1.0);
                    RenderSpecialTileAlpha(_SpecialTable[0xA3], 8, 0, 5, data, 1.0);
                    RenderSpecialTileAlpha(_SpecialTable[0xB3], 8, 8, 5, data, 1.0);
                }
            }

            if (_DisplayStartingPosition && ((x == CurrentLevel.XStart && y == CurrentLevel.YStart) || (x == CurrentLevel.XAltStart && y == CurrentLevel.YAltStart)))
            {
                RenderSpecialTileAlpha(_SpecialTable[0xA0], 0, 0, 4, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xB0], 0, 8, 4, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xA1], 8, 0, 4, data, 1.0);
                RenderSpecialTileAlpha(_SpecialTable[0xB1], 8, 8, 4, data, 1.0);
            }

            BackBuffer.UnlockBits(data);

            if (!DelayDrawing)
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
                    if (c == Color.Empty) continue;

                    if (_ShowGrid)
                    {
                        if ((j == 0 && x % 16 == 0) || (i == 0 && y % 16 == 0))
                            c = Color.FromArgb(255, 255, 255);
                    }

                    *(dataPointer + xOffset) = (byte)((1 - alpha) * (*(dataPointer + xOffset)) + (alpha * c.B));
                    *(dataPointer + xOffset + 1) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 1)) + (alpha * c.G));
                    *(dataPointer + xOffset + 2) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 2)) + (alpha * c.R));
                    *(dataPointer + xOffset + 3) = 255;
                }
            }
        }

        public Guide VerticalGuide1 { get; set; }
        public Guide VerticalGuide2 { get; set; }
        public Guide HorizontalGuide1 { get; set; }
        public Guide HorizontalGuide2 { get; set; }

        int vG1, vG2, hG1, hG2;

        public void UpdateGuide(Orientation orientation, int guideNumber)
        {
            Rectangle rect = new Rectangle();
            switch (orientation)
            {
                case Orientation.Horizontal:
                    if (guideNumber == 1)
                    {
                        rect.X = HorizontalGuide1.Position < hG1 ? HorizontalGuide1.Position : hG1;
                        rect.Y = 0;
                        rect.Width = Math.Abs(HorizontalGuide1.Position - hG1) + 1;
                        rect.Height = BackBuffer.Height;
                        hG1 = HorizontalGuide1.Position;
                    }
                    if (guideNumber == 2)
                    {
                        rect.X = HorizontalGuide2.Position < hG2 ? HorizontalGuide2.Position : hG2;
                        rect.Y = 0;
                        rect.Width = Math.Abs(HorizontalGuide2.Position - hG2) + 1;
                        rect.Height = BackBuffer.Height;
                        hG2 = HorizontalGuide2.Position;
                    }

                    break;

                case Orientation.Vertical:
                    if (guideNumber == 1)
                    {
                        rect.X = 0;
                        rect.Y = VerticalGuide1.Position < vG1 ? VerticalGuide1.Position : vG1;
                        rect.Width = BackBuffer.Width;
                        rect.Height = Math.Abs(VerticalGuide1.Position - vG1) + 1;
                        vG1 = VerticalGuide1.Position;
                    }
                    if (guideNumber == 2)
                    {
                        rect.X = 0;
                        rect.Y = VerticalGuide2.Position < vG2 ? VerticalGuide2.Position : vG2;
                        rect.Width = BackBuffer.Width;
                        rect.Height = Math.Abs(VerticalGuide2.Position - vG2) + 1;
                        vG2 = VerticalGuide2.Position;
                    }

                    break;
            }

            Invalidate(rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DelayDrawing) return;
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

            Pen hPen = new Pen(HorizontalGuide1.Color);
            Pen vPen = new Pen(VerticalGuide1.Color);

            if (VerticalGuide1.Visible)
            {
                g.DrawLine(vPen, 0, VerticalGuide1.Position, BackBuffer.Width, VerticalGuide1.Position);
            }

            if (VerticalGuide2.Visible)
            {
                g.DrawLine(vPen, 0, VerticalGuide2.Position, BackBuffer.Width, VerticalGuide2.Position);
            }

            if (HorizontalGuide1.Visible)
            {
                g.DrawLine(hPen, HorizontalGuide1.Position, 0, HorizontalGuide1.Position, BackBuffer.Height);
            }

            if (HorizontalGuide2.Visible)
            {
                g.DrawLine(hPen, HorizontalGuide2.Position, 0, HorizontalGuide2.Position, BackBuffer.Height);
            }

            e.Graphics.DrawImage(CompositeBuffer, destRect, sourceRect, GraphicsUnit.Pixel);
            g.Dispose();
            hPen.Dispose();
            vPen.Dispose();
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

        private bool _ShowSpecial;
        public bool ShowSpecialSprites
        {
            get { return _ShowSpecial; }
            set
            {
                _ShowSpecial = value;
                if (!DelayDrawing)
                {
                    FullSpriteRender();
                    Redraw();
                }
            }
        }

        private bool _ShowBlockProperties;
        public bool ShowBlockProperties
        {
            get { return _ShowBlockProperties; }
            set
            {
                _ShowBlockProperties = value;
                if (!DelayDrawing)
                {
                    FullRender();
                    Redraw();
                }
            }
        }

        private bool _DisplayStartingPosition;
        public bool DisplayStartingPosition
        {
            get { return _DisplayStartingPosition; }
            set
            {
                _DisplayStartingPosition = value;
                if (CurrentLevel != null)
                {
                    UpdateBlock(CurrentLevel.XStart, CurrentLevel.YStart);
                    UpdateBlock(CurrentLevel.XAltStart, CurrentLevel.YAltStart);
                }
            }
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
                if (_CurrentLevel == null) return;
                this.Width = _CurrentLevel.Width * 16 * Zoom;
                this.Height = _CurrentLevel.Height * 16 * Zoom;
                Redraw(new Rectangle(0, 0, BackBuffer.Width, BackBuffer.Height));
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
    }
}
