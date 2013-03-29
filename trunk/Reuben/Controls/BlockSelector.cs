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
    public unsafe class BlockSelector : Control
    {
        public event EventHandler<TEventArgs<MouseButtons>> SelectionChanged;

        public BlockSelector()
        {
            BackBuffer = new Bitmap(256, 256);
            QuickColorLookup = new Color[4, 4];
            SpecialColors = new Color[8, 4];
            CurrentDefiniton = null;
            this.Width = this.Height = 256;
            this.MouseDown += new MouseEventHandler(PatternTableViewer_MouseDown);
            FullRender();
        }

        private PatternTable _SpecialTable;
        public PatternTable SpecialTable
        {
            get { return _SpecialTable; }
            set
            {
                _SpecialTable = value;
                FullRender();
            }
        }

        private BlockLayout _BlockLayout;
        public BlockLayout BlockLayout
        {
            get { return _BlockLayout; }
            set
            {
                _BlockLayout = value;
                FullRender();
            }
        }

        private bool _ShowSpecialBlocks;
        public bool ShowSpecialBlocks
        {
            get { return _ShowSpecialBlocks; }
            set
            {
                _ShowSpecialBlocks = value;
                FullRender();
            }
        }


        private PatternTable _CurrentTable;
        public PatternTable CurrentTable
        {
            set
            {
                if (_CurrentTable != null)
                {
                    _CurrentTable.GraphicsChanged -= _CurrentTable_GraphicsChanged;
                }

                if (_CurrentTable != value)
                {
                    _CurrentTable = value;

                    if (_CurrentTable != null)
                    {
                        _CurrentTable.GraphicsChanged += new EventHandler<TEventArgs<int>>(_CurrentTable_GraphicsChanged);
                    }

                    FullRender();
                }
            }
        }

        void _CurrentTable_GraphicsChanged(object sender, TEventArgs<int> e)
        {
            FullRender();
        }

        private int DefinitionIndex;
        private BlockDefinition _CurrentDefiniton;
        public BlockDefinition CurrentDefiniton
        {
            get { return _CurrentDefiniton; }
            set
            {
                if (_CurrentDefiniton == value) return;
                _CurrentDefiniton = value;
                DefinitionIndex = ProjectController.BlockManager.AllDefinitions.IndexOf(value);
                FullRender();
            }
        }

        private BlockDefinition _SpecialDefinitions;
        public BlockDefinition SpecialDefnitions
        {
            get { return _SpecialDefinitions; }
            set
            {
                _SpecialDefinitions = value;
                FullRender();
            }
        }

        private PaletteInfo _CurrentPalette;
        public PaletteInfo CurrentPalette
        {
            set
            {
                _CurrentPalette = value;
                UpdateColors();
                FullRender();
            }
        }

        Bitmap BackBuffer;

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
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        QuickColorLookup[j, i] = ProjectController.ColorManager.Colors[_CurrentPalette[j, i]];
                    }
                }
            }
        }

        public bool HaltRendering { get; set; }

        private void FullRender()
        {
            if (HaltRendering || _CurrentTable == null || _CurrentPalette == null || _CurrentDefiniton == null || BlockLayout == null)
            {
                Graphics.FromImage(BackBuffer).Clear(Color.Black);
                return;
            }

            BitmapData data = BackBuffer.LockBits(new Rectangle(0, 0, 256, 256), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    int tileValue = _BlockLayout.Layout[i * 16 + j];
                    int PaletteIndex = tileValue / 0x40;
                    if (tileValue < 0)
                    {
                        RenderBlank(j * 16, i * 16, data);
                        RenderBlank(j * 16, i * 16 + 8, data);
                        RenderBlank(j * 16 + 8, i * 16, data);
                        RenderBlank(j * 16 + 8, i * 16 + 8, data);
                        continue;
                    }

                    Block b = CurrentDefiniton[tileValue];

                    RenderTile(_CurrentTable[b[0, 0]], j * 16, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[0, 1]], j * 16, i * 16 + 8, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 0]], j * 16 + 8, i * 16, PaletteIndex, data);
                    RenderTile(_CurrentTable[b[1, 1]], j * 16 + 8, i * 16 + 8, PaletteIndex, data);

                    if (_ShowBlockProperties)
                    {
                        BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                        BlockProperty bp78Bits = bp & BlockProperty.Mask78Bits;
                        BlockProperty bp56bits = bp & BlockProperty.Mask56Bits;
                        BlockProperty bpLow = bp & BlockProperty.MaskLow;



                        if ((bp78Bits & BlockProperty.SolidTop) > 0)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16, 6, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16, 6, data, .75);
                        }

                        if ((bp78Bits & BlockProperty.SolidBottom) > 0)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16 + 8, 6, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16 + 8, 6, data, .75);
                        }

                        if ((bp56bits & BlockProperty.Water) > 0)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16, i * 16, 6, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16, i * 16 + 8, 6, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16 + 8, i * 16, 6, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16 + 8, i * 16 + 8, 6, data, .75);
                        }

                        if ((bp56bits & BlockProperty.Foreground) > 0)
                        {
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16, 6, data, .5);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 6, data, .5);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16, 6, data, .5);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 6, data, .5);
                        }

                        switch (bpLow)
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

                            case BlockProperty.MoveLeft:
                                RenderSpecialTileAlpha(_SpecialTable[0x28], j * 16, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x38], j * 16, i * 16 + 8, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x28], j * 16 + 8, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x38], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                break;

                            case BlockProperty.MoveRight:
                                RenderSpecialTileAlpha(_SpecialTable[0x29], j * 16, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x39], j * 16, i * 16 + 8, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x29], j * 16 + 8, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x39], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                break;

                            case BlockProperty.MoveUp:
                                RenderSpecialTileAlpha(_SpecialTable[0x2A], j * 16, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x2A], j * 16, i * 16 + 8, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x2B], j * 16 + 8, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x2B], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                break;

                            case BlockProperty.MoveDown:
                                RenderSpecialTileAlpha(_SpecialTable[0x3A], j * 16, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x3A], j * 16, i * 16 + 8, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x3B], j * 16 + 8, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x3B], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                break;

                            case BlockProperty.VerticalPipeLeft:
                                RenderSpecialTileAlpha(_SpecialTable[0x44], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x54], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x45], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x55], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.VerticalPipeRight:
                                RenderSpecialTileAlpha(_SpecialTable[0x46], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x56], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x47], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x57], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.HorizontalPipeBottom:
                                RenderSpecialTileAlpha(_SpecialTable[0x48], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x58], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x49], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x59], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.Unstable:
                                RenderSpecialTileAlpha(_SpecialTable[0x60], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x70], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x61], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x71], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.Climbable:
                                RenderSpecialTileAlpha(_SpecialTable[0x2E], j * 16, i * 16, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x3E], j * 16, i * 16 + 8, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x2F], j * 16 + 8, i * 16, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x3F], j * 16 + 8, i * 16 + 8, 4, data, .75);
                                break;
                        }
                    }
                    if (_ShowSpecialBlocks)
                    {
                        SpecialBlock sb = (SpecialBlock)SpecialDefnitions[tileValue];
                        if (sb != null)
                        {
                            int SpecialPaletteIndex = sb.Palette;
                            RenderSpecialTileAlpha(_SpecialTable[sb[0, 0]], j * 16, i * 16, SpecialPaletteIndex, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[sb[0, 1]], j * 16, i * 16 + 8, SpecialPaletteIndex, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[sb[1, 0]], j * 16 + 8, i * 16, SpecialPaletteIndex, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[sb[1, 1]], j * 16 + 8, i * 16 + 8, SpecialPaletteIndex, data, .75);
                        }
                    }
                }
            }
            BackBuffer.UnlockBits(data);
            Invalidate();
        }

        private void RenderTile(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c = QuickColorLookup[PaletteIndex, tile[j, i]];
                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }

        private void RenderBlank(int x, int y, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c;
                    if ((i + j) % 2 == 0)
                    {
                        c = Color.Black;
                    }
                    else
                    {
                        c = Color.Red;
                    }

                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
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
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c = SpecialColors[PaletteIndex, tile[j, i]];
                    if (c == Color.Empty) continue;

                    *(dataPointer + xOffset) = (byte)((1 - alpha) * (*(dataPointer + xOffset)) + (alpha * c.B));
                    *(dataPointer + xOffset + 1) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 1)) + (alpha * c.G));
                    *(dataPointer + xOffset + 2) = (byte)((1 - alpha) * (*(dataPointer + xOffset + 2)) + (alpha * c.R));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(BackBuffer, 0, 0);
            Rectangle rect = new Rectangle((SelectedIndex % 16) * 16, (SelectedIndex / 16) * 16, 15, 15);
            e.Graphics.DrawRectangle(Pens.White, rect);
            rect.X += 1;
            rect.Y += 1;
            rect.Width -= 2;
            rect.Height -= 2;
            e.Graphics.DrawRectangle(Pens.Red, rect);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        public void UpdateSelection()
        {
            Rectangle rect = new Rectangle((SelectedIndex % 16) * 16, (SelectedIndex / 16) * 16, 16, 16);
            BitmapData data = BackBuffer.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int tileValue = BlockLayout.Layout[SelectedIndex];
            if (tileValue >= 0)
            {
                int PaletteIndex = tileValue / 0x40;
                Block b = CurrentDefiniton[tileValue];


                RenderTile(_CurrentTable[b[0, 0]], 0, 0, PaletteIndex, data);
                RenderTile(_CurrentTable[b[0, 1]], 0, 8, PaletteIndex, data);
                RenderTile(_CurrentTable[b[1, 0]], 8, 0, PaletteIndex, data);
                RenderTile(_CurrentTable[b[1, 1]], 8, 8, PaletteIndex, data);

                if (_ShowBlockProperties)
                {
                    BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                    BlockProperty bp78bits = bp & BlockProperty.Mask78Bits;
                    BlockProperty bp56bits = bp & BlockProperty.Mask56Bits;
                    BlockProperty bpLow = bp & BlockProperty.MaskLow;



                    if ((bp78bits & BlockProperty.SolidTop) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 0, 6, data, .75);
                    }

                    if ((bp78bits & BlockProperty.SolidBottom) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 8, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 8, 6, data, .75);
                    }

                    if ((bp78bits & BlockProperty.Water) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 8, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 0, 6, data, .75);
                        RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 8, 6, data, .75);
                    }

                    if ((bp78bits & BlockProperty.Foreground) > 0)
                    {
                        RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 0, 6, data, .5);
                        RenderSpecialTileAlpha(_SpecialTable[0xFD], 0, 8, 6, data, .5);
                        RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 0, 6, data, .5);
                        RenderSpecialTileAlpha(_SpecialTable[0xFD], 8, 8, 6, data, .5);
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

                        case BlockProperty.MoveLeft:
                            RenderSpecialTileAlpha(_SpecialTable[0x28], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x38], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x28], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x38], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.MoveRight:
                            RenderSpecialTileAlpha(_SpecialTable[0x29], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x39], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x29], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x39], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.MoveUp:
                            RenderSpecialTileAlpha(_SpecialTable[0x2A], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x2A], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x2B], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x2B], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.MoveDown:
                            RenderSpecialTileAlpha(_SpecialTable[0x3A], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3A], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3B], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3B], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.VerticalPipeLeft:
                            RenderSpecialTileAlpha(_SpecialTable[0x44], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x54], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x45], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x55], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.VerticalPipeRight:
                            RenderSpecialTileAlpha(_SpecialTable[0x46], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x56], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x47], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x57], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.HorizontalPipeBottom:
                            RenderSpecialTileAlpha(_SpecialTable[0x48], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x58], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x49], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x59], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.Unstable:
                            RenderSpecialTileAlpha(_SpecialTable[0x60], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x70], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x61], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x71], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.Climbable:
                            RenderSpecialTileAlpha(_SpecialTable[0x2E], 0, 0, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3E], 0, 8, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x2F], 8, 0, 4, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x3F], 8, 8, 4, data, .75);
                            break;
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
            }
            else
            {
                RenderBlank(0, 0, data);
                RenderBlank(0, 8, data);
                RenderBlank(8, 0, data);
                RenderBlank(8, 8, data);
            }
            BackBuffer.UnlockBits(data);
            Invalidate(rect);
        }

        public int SelectedTileIndex
        {
            get
            {
                if (BlockLayout == null || BlockLayout.Layout == null) return 0;
                if (BlockLayout.Layout[SelectedIndex] < 0) return 0;
                return BlockLayout.Layout[SelectedIndex];
            }
            set
            {
                if (BlockLayout == null || BlockLayout.Layout == null) return;
                bool found = false;
                for (int i = 0; i < 256; i++)
                {
                    if (BlockLayout.Layout[i] == value)
                    {
                        SelectedIndex = i;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];
                    SelectedIndex = value;
                }

                SelectedBlock = _CurrentDefiniton[BlockLayout.Layout[SelectedIndex]];

                if (SelectionChanged != null)
                {
                    SelectionChanged(this, null);
                }

                Invalidate();
            }
        }

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
            }
        }
        public Block SelectedBlock { get; private set; }

        private void PatternTableViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (BlockLayout == null || CurrentDefiniton == null)
            {
                MessageBox.Show("There is no selected layout to edit. Click add to add a new layout before editing");
                return;
            }

            if (SelectedBlock != null)
            {
                SelectedBlock.DefinitionChanged -= SelectedBlock_DefinitionChanged;
            }

            SelectedIndex = e.X / 16 + ((e.Y / 16) * 16);
            if (BlockLayout.Layout[SelectedIndex] == -1)
            {
                Invalidate();
                return;
            }

            SelectedBlock = CurrentDefiniton[BlockLayout.Layout[SelectedIndex]];
            if (SelectionChanged != null)
            {
                SelectionChanged(this, new TEventArgs<MouseButtons>(e.Button));
            }

            SelectedBlock.DefinitionChanged += new EventHandler(SelectedBlock_DefinitionChanged);
            Invalidate();
        }

        void SelectedBlock_DefinitionChanged(object sender, EventArgs e)
        {
            UpdateSelection();
        }

        public void Redraw()
        {
            UpdateColors();
            FullRender();
            Invalidate();
        }

        private bool _ShowBlockProperties;
        public bool ShowBlockProperties
        {
            get { return _ShowBlockProperties; }
            set
            {
                _ShowBlockProperties = value;
                FullRender();
                Invalidate();
            }
        }
    }
}
