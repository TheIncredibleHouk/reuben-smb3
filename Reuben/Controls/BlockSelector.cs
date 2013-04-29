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
                    int x = j * 16, y = i * 16;
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


                    #region draw special overlays
                    BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                    BlockProperty bpHi = bp & BlockProperty.MaskHi;
                    BlockProperty bpLow = bp & BlockProperty.Cherry;
                    if (_ShowBlockSolidity)
                    {

                        switch (bpHi)
                        {
                            case BlockProperty.SolidTop:

                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y, 6, data);
                                break;


                            case BlockProperty.SolidBottom:

                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y + 8, 6, data);
                                break;

                            case BlockProperty.Water:

                                RenderSpecialTileAlpha(_SpecialTable[0x98], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x98], x + 8, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], x + 8, y + 8, 6, data);
                                break;

                            case BlockProperty.Foreground:

                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y + 8, 6, data);
                                break;

                            case BlockProperty.Water | BlockProperty.Foreground:
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x98], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x98], x + 8, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], x + 8, y + 8, 6, data);
                                break;

                            case BlockProperty.Background:
                                break;

                            default:
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y + 8, 6, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y + 8, 6, data);
                                break;
                        }
                    }

                    if (_ShowTileInteractions)
                    {
                        if (bpHi != BlockProperty.MaskHi)
                        {
                            switch (bpLow)
                            {
                                case BlockProperty.Harmful:
                                    RenderSpecialTileAlpha(_SpecialTable[0x40], x, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x50], x, y + 8, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x41], x + 8, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x51], x + 8, y + 8, 4, data);
                                    break;

                                case BlockProperty.Slick:
                                    RenderSpecialTileAlpha(_SpecialTable[0xD2], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE2], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xD3], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE3], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.MoveLeft:
                                    RenderSpecialTileAlpha(_SpecialTable[0x28], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x38], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x28], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x38], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.MoveRight:
                                    RenderSpecialTileAlpha(_SpecialTable[0x29], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x39], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x29], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x39], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.MoveUp:
                                    RenderSpecialTileAlpha(_SpecialTable[0x2A], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x2A], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x2B], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x2B], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.MoveDown:
                                    RenderSpecialTileAlpha(_SpecialTable[0x3A], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3A], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3B], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3B], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.VerticalPipeLeft:
                                    RenderSpecialTileAlpha(_SpecialTable[0x44], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x54], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x45], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x55], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.VerticalPipeRight:
                                    RenderSpecialTileAlpha(_SpecialTable[0x46], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x56], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x47], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x57], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.HorizontalPipeBottom:
                                    RenderSpecialTileAlpha(_SpecialTable[0x48], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x58], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x49], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x59], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.Unstable:
                                    RenderSpecialTileAlpha(_SpecialTable[0x60], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x70], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x61], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x71], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.Climbable:
                                    RenderSpecialTileAlpha(_SpecialTable[0x2E], x, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3E], x, y + 8, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x2F], x + 8, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x3F], x + 8, y + 8, 4, data);
                                    break;

                                case BlockProperty.PSwitch:
                                    RenderSpecialTileAlpha(_SpecialTable[0x26], x, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x36], x, y + 8, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x27], x + 8, y, 4, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x37], x + 8, y + 8, 4, data);
                                    break;
                            }
                        }
                    }
                    if (_ShowSpecialBlocks)
                    {
                        if (bpHi == BlockProperty.MaskHi)
                        {
                            switch (bpHi | bpLow)
                            {
                                case BlockProperty.FireFlower:
                                    RenderSpecialTileAlpha(_SpecialTable[0x00], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x10], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x01], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x11], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.IceFlower:
                                    RenderSpecialTileAlpha(_SpecialTable[0xE0], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF0], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE1], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF1], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.SuperLeaf:
                                    RenderSpecialTileAlpha(_SpecialTable[0x02], x, y, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x12], x, y + 8, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x03], x + 8, y, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x13], x + 8, y + 8, 3, data);
                                    break;

                                case BlockProperty.FireFoxSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0x0E], x, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1E], x, y + 8, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x0F], x + 8, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1F], x + 8, y + 8, 2, data);
                                    break;

                                case BlockProperty.FrogSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0x20], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x30], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x21], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x31], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.KoopaSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0xE4], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF4], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE5], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF5], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.BooSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0xE8], x, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF8], x, y + 8, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE9], x + 8, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF9], x + 8, y + 8, 2, data);
                                    break;

                                case BlockProperty.SledgeSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0xE6], x, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF6], x, y + 8, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE7], x + 8, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF7], x + 8, y + 8, 2, data);
                                    break;

                                case BlockProperty.NinjaSuit:
                                    RenderSpecialTileAlpha(_SpecialTable[0xE8], x, y, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF8], x, y + 8, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xE9], x + 8, y, 3, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xF9], x + 8, y + 8, 3, data);
                                    break;

                                case BlockProperty.Starman:
                                    RenderSpecialTileAlpha(_SpecialTable[0x06], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x16], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x07], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x17], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.CoinBlock:
                                    RenderSpecialTileAlpha(_SpecialTable[0x0A], x, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1A], x, y + 8, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x0B], x + 8, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1B], x + 8, y + 8, 2, data);
                                    break;

                                case BlockProperty.Vine:
                                    RenderSpecialTileAlpha(_SpecialTable[0x24], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x34], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x25], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x35], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.PSwitchBlock:
                                    RenderSpecialTileAlpha(_SpecialTable[0x0C], x, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1C], x, y + 8, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x0D], x + 8, y, 1, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x1D], x + 8, y + 8, 1, data);
                                    break;

                                case BlockProperty.NoteBlock:
                                    RenderSpecialTileAlpha(_SpecialTable[0x04], x, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x14], x, y + 8, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x05], x + 8, y, 0, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0x15], x + 8, y + 8, 0, data);
                                    break;

                                case BlockProperty.Brick:
                                    RenderSpecialTileAlpha(_SpecialTable[0xDC], x, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xEC], x, y + 8, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xDD], x + 8, y, 2, data);
                                    RenderSpecialTileAlpha(_SpecialTable[0xED], x + 8, y + 8, 2, data);
                                    break;
                            }
                        }
                    }
                    #endregion
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

        private void RenderSpecialTileAlpha(Tile tile, int x, int y, int PaletteIndex, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;
            double alpha = .5;
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

            int x = 0, y = 0;
            int tileValue = BlockLayout.Layout[SelectedIndex];
            if (tileValue >= 0)
            {
                int PaletteIndex = tileValue / 0x40;
                Block b = CurrentDefiniton[tileValue];


                RenderTile(_CurrentTable[b[0, 0]], 0, 0, PaletteIndex, data);
                RenderTile(_CurrentTable[b[0, 1]], 0, 8, PaletteIndex, data);
                RenderTile(_CurrentTable[b[1, 0]], 8, 0, PaletteIndex, data);
                RenderTile(_CurrentTable[b[1, 1]], 8, 8, PaletteIndex, data);

                #region draw special overlays
                BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                BlockProperty bpHi = bp & BlockProperty.MaskHi;
                BlockProperty bpLow = bp & BlockProperty.Cherry;
                if (_ShowBlockSolidity)
                {

                    switch (bpHi)
                    {
                        case BlockProperty.SolidTop:

                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y, 6, data);
                            break;


                        case BlockProperty.SolidBottom:

                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y + 8, 6, data);
                            break;

                        case BlockProperty.Water:

                            RenderSpecialTileAlpha(_SpecialTable[0x98], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x98], x + 8, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], x + 8, y + 8, 6, data);
                            break;

                        case BlockProperty.Foreground:

                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y + 8, 6, data);
                            break;

                        case BlockProperty.Water | BlockProperty.Foreground:
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFD], x + 8, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x98], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x98], x + 8, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0x99], x + 8, y + 8, 6, data);
                            break;

                        case BlockProperty.Background:
                            break;

                        default:
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x, y + 8, 6, data);
                            RenderSpecialTileAlpha(_SpecialTable[0xFF], x + 8, y + 8, 6, data);
                            break;
                    }
                }

                if (_ShowTileInteractions)
                {
                    if (bpHi != BlockProperty.MaskHi)
                    {
                        switch (bpLow)
                        {
                            case BlockProperty.Harmful:
                                RenderSpecialTileAlpha(_SpecialTable[0x40], x, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x50], x, y + 8, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x41], x + 8, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x51], x + 8, y + 8, 4, data);
                                break;

                            case BlockProperty.Slick:
                                RenderSpecialTileAlpha(_SpecialTable[0xD2], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE2], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xD3], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE3], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.MoveLeft:
                                RenderSpecialTileAlpha(_SpecialTable[0x28], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x38], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x28], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x38], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.MoveRight:
                                RenderSpecialTileAlpha(_SpecialTable[0x29], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x39], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x29], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x39], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.MoveUp:
                                RenderSpecialTileAlpha(_SpecialTable[0x2A], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x2A], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x2B], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x2B], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.MoveDown:
                                RenderSpecialTileAlpha(_SpecialTable[0x3A], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x3A], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x3B], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x3B], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.VerticalPipeLeft:
                                RenderSpecialTileAlpha(_SpecialTable[0x44], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x54], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x45], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x55], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.VerticalPipeRight:
                                RenderSpecialTileAlpha(_SpecialTable[0x46], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x56], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x47], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x57], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.HorizontalPipeBottom:
                                RenderSpecialTileAlpha(_SpecialTable[0x48], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x58], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x49], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x59], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.Unstable:
                                RenderSpecialTileAlpha(_SpecialTable[0x60], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x70], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x61], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x71], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.Climbable:
                                RenderSpecialTileAlpha(_SpecialTable[0x2E], x, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x3E], x, y + 8, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x2F], x + 8, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x3F], x + 8, y + 8, 4, data);
                                break;

                            case BlockProperty.PSwitch:
                                RenderSpecialTileAlpha(_SpecialTable[0x26], x, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x36], x, y + 8, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x27], x + 8, y, 4, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x37], x + 8, y + 8, 4, data);
                                break;
                        }
                    }
                }
                if (_ShowSpecialBlocks)
                {
                    if (bpHi == BlockProperty.MaskHi)
                    {
                        switch (bpHi | bpLow)
                        {
                            case BlockProperty.FireFlower:
                                RenderSpecialTileAlpha(_SpecialTable[0x00], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x10], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x01], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x11], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.IceFlower:
                                RenderSpecialTileAlpha(_SpecialTable[0xE0], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF0], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE1], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF1], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.SuperLeaf:
                                RenderSpecialTileAlpha(_SpecialTable[0x02], x, y, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x12], x, y + 8, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x03], x + 8, y, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x13], x + 8, y + 8, 3, data);
                                break;

                            case BlockProperty.FireFoxSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0x0E], x, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1E], x, y + 8, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x0F], x + 8, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1F], x + 8, y + 8, 2, data);
                                break;

                            case BlockProperty.FrogSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0x20], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x30], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x21], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x31], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.KoopaSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE4], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF4], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE5], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF5], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.BooSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE8], x, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF8], x, y + 8, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE9], x + 8, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF9], x + 8, y + 8, 2, data);
                                break;

                            case BlockProperty.SledgeSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE6], x, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF6], x, y + 8, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE7], x + 8, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF7], x + 8, y + 8, 2, data);
                                break;

                            case BlockProperty.NinjaSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE8], x, y, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF8], x, y + 8, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xE9], x + 8, y, 3, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xF9], x + 8, y + 8, 3, data);
                                break;

                            case BlockProperty.Starman:
                                RenderSpecialTileAlpha(_SpecialTable[0x06], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x16], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x07], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x17], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.CoinBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x0A], x, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1A], x, y + 8, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x0B], x + 8, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1B], x + 8, y + 8, 2, data);
                                break;

                            case BlockProperty.Vine:
                                RenderSpecialTileAlpha(_SpecialTable[0x24], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x34], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x25], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x35], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.PSwitchBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x0C], x, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1C], x, y + 8, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x0D], x + 8, y, 1, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x1D], x + 8, y + 8, 1, data);
                                break;

                            case BlockProperty.NoteBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x04], x, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x14], x, y + 8, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x05], x + 8, y, 0, data);
                                RenderSpecialTileAlpha(_SpecialTable[0x15], x + 8, y + 8, 0, data);
                                break;

                            case BlockProperty.Brick:
                                RenderSpecialTileAlpha(_SpecialTable[0xDC], x, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xEC], x, y + 8, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xDD], x + 8, y, 2, data);
                                RenderSpecialTileAlpha(_SpecialTable[0xED], x + 8, y + 8, 2, data);
                                break;
                        }
                    }
                }
                #endregion
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

        private bool _ShowBlockSolidity;
        public bool ShowBlockSolidity
        {
            get { return _ShowBlockSolidity; }
            set
            {
                _ShowBlockSolidity = value;
                FullRender();
                Invalidate();
            }
        }


        private bool _ShowTileInteractions;
        public bool ShowTileInteractions
        {
            get
            {
                return _ShowTileInteractions;
            }

            set
            {
                _ShowTileInteractions = value;
                FullRender();
            }
        }
    }
}
