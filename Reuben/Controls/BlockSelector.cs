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

                    BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                    BlockProperty bpHi = bp & BlockProperty.MaskHi;
                    BlockProperty bpLow = bp & BlockProperty.MaskLow;
                    if (_ShowBlockProperties)
                    {
                        if (bpHi != BlockProperty.MaskHi)
                        {
                            switch (bpHi)
                            {
                                case BlockProperty.SolidTop:

                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16, 6, data, .75);
                                    break;


                                case BlockProperty.SolidBottom:

                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16 + 8, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16 + 8, 6, data, .75);
                                    break;

                                case BlockProperty.SolidAll:
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16, i * 16 + 8, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFF], j * 16 + 8, i * 16 + 8, 6, data, .75);
                                    break;

                                case BlockProperty.Water:

                                    RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16, i * 16 + 8, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16 + 8, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16 + 8, i * 16 + 8, 6, data, .75);
                                    break;

                                case BlockProperty.Foreground:

                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 6, data, .5);
                                    break;

                                case BlockProperty.Water | BlockProperty.Foreground:
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16, i * 16 + 8, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0xFD], j * 16 + 8, i * 16 + 8, 6, data, .5);
                                    RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16, i * 16 + 8, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x98], j * 16 + 8, i * 16, 6, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x99], j * 16 + 8, i * 16 + 8, 6, data, .75);
                                    break;
                            }

                            switch (bpLow | bpHi)
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

                                case BlockProperty.PSwitch:
                                    RenderSpecialTileAlpha(_SpecialTable[0x26], j * 16, i * 16, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x36], j * 16, i * 16 + 8, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x27], j * 16 + 8, i * 16, 4, data, .75);
                                    RenderSpecialTileAlpha(_SpecialTable[0x37], j * 16 + 8, i * 16 + 8, 4, data, .75);
                                    break;
                            }
                        }
                    }

                    if (_ShowSpecialBlocks)
                    {
                        switch (bpHi | bpLow)
                        {
                            case BlockProperty.FireFlower:
                                RenderSpecialTileAlpha(_SpecialTable[0x00], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x10], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x01], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x11], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.IceFlower:
                                RenderSpecialTileAlpha(_SpecialTable[0xE0], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF0], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xE1], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF1], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.SuperLeaf:
                                RenderSpecialTileAlpha(_SpecialTable[0x02], j * 16, i * 16, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x12], j * 16, i * 16 + 8, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x03], j * 16 + 8, i * 16, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x13], j * 16 + 8, i * 16 + 8, 3, data, .75);
                                break;

                            case BlockProperty.FireFoxSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0x0E], j * 16, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1E], j * 16, i * 16 + 8, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x0F], j * 16 + 8, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1F], j * 16 + 8, i * 16 + 8, 2, data, .75);
                                break;

                            case BlockProperty.FrogSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0x20], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x30], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x21], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x31], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.KoopaSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE4], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF4], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xE5], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF5], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.BooSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE8], j * 16, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF8], j * 16, i * 16 + 8, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xE9], j * 16 + 8, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF9], j * 16 + 8, i * 16 + 8, 2, data, .75);
                                break;

                            case BlockProperty.SledgeSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE6], j * 16, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF6], j * 16, i * 16 + 8, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xE7], j * 16 + 8, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF7], j * 16 + 8, i * 16 + 8, 2, data, .75);
                                break;

                            case BlockProperty.NinjaSuit:
                                RenderSpecialTileAlpha(_SpecialTable[0xE8], j * 16, i * 16, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF8], j * 16, i * 16 + 8, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xE9], j * 16 + 8, i * 16, 3, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xF9], j * 16 + 8, i * 16 + 8, 3, data, .75);
                                break;

                            case BlockProperty.Starman:
                                RenderSpecialTileAlpha(_SpecialTable[0x06], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x16], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x07], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x17], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.CoinBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x0A], j * 16, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1A], j * 16, i * 16 + 8, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x0B], j * 16 + 8, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1B], j * 16 + 8, i * 16 + 8, 2, data, .75);
                                break;

                            case BlockProperty.Vine:
                                RenderSpecialTileAlpha(_SpecialTable[0x24], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x34], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x25], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x35], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.PSwitchBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x0C], j * 16, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1C], j * 16, i * 16 + 8, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x0D], j * 16 + 8, i * 16, 1, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x1D], j * 16 + 8, i * 16 + 8, 1, data, .75);
                                break;

                            case BlockProperty.NoteBlock:
                                RenderSpecialTileAlpha(_SpecialTable[0x04], j * 16, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x14], j * 16, i * 16 + 8, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x05], j * 16 + 8, i * 16, 0, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x15], j * 16 + 8, i * 16 + 8, 0, data, .75);
                                break;

                            case BlockProperty.Brick:
                                RenderSpecialTileAlpha(_SpecialTable[0xDC], j * 16, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xEC], j * 16, i * 16 + 8, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xDD], j * 16 + 8, i * 16, 2, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xED], j * 16 + 8, i * 16 + 8, 2, data, .75);
                                break;
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


                BlockProperty bp = CurrentDefiniton[tileValue].BlockProperty;
                BlockProperty bpHi = bp & BlockProperty.MaskHi;
                BlockProperty bpLow = bp & BlockProperty.MaskLow;

                if (_ShowBlockProperties)
                {

                    if (bpHi != BlockProperty.MaskHi)
                    {
                        switch (bpHi)
                        {
                            case BlockProperty.SolidTop:

                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 0, 6, data, .75);
                                break;

                            case BlockProperty.SolidBottom:

                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 8, 6, data, .75);
                                break;

                            case BlockProperty.SolidAll:
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 0, 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFF], 8, 8, 6, data, .75);
                                break;

                            case BlockProperty.Water:

                                RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x98], 0, 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x99], 8, 8, 6, data, .75);
                                break;

                            case BlockProperty.Foreground:

                                RenderSpecialTileAlpha(_SpecialTable[0xFE], 0, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], 0, 8, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], 8, 0, 6, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0xFE], 8, 8, 6, data, .75);
                                break;
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

                            case BlockProperty.PSwitch:
                                RenderSpecialTileAlpha(_SpecialTable[0x26], 0, 0, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x36], 0, 8, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x27], 8, 0, 4, data, .75);
                                RenderSpecialTileAlpha(_SpecialTable[0x37], 8, 8, 4, data, .75);
                                break;
                        }
                    }
                }

                if (_ShowSpecialBlocks)
                {
                    switch (bpHi | bpLow)
                    {
                        case BlockProperty.FireFlower:
                            RenderSpecialTileAlpha(_SpecialTable[0x00], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x10], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x01], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x11], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.IceFlower:
                            RenderSpecialTileAlpha(_SpecialTable[0xE0], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF0], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE1], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF1], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.SuperLeaf:
                            RenderSpecialTileAlpha(_SpecialTable[0x02], 0, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x12], 0, 8, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x03], 8, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x13], 8, 8, 3, data, .75);
                            break;

                        case BlockProperty.FireFoxSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0x0E], 0, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1E], 0, 8, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x0F], 8, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1F], 8, 8, 2, data, .75);
                            break;

                        case BlockProperty.FrogSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0x20], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x30], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x21], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x31], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.KoopaSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0xE4], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF4], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE5], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF5], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.BooSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0xE8], 0, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF8], 0, 8, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE9], 8, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF9], 8, 8, 2, data, .75);
                            break;

                        case BlockProperty.SledgeSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0xE6], 0, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF6], 0, 8, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE7], 8, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF7], 8, 8, 3, data, .75);
                            break;

                        case BlockProperty.NinjaSuit:
                            RenderSpecialTileAlpha(_SpecialTable[0xE8], 0, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF8], 0, 8, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xE9], 8, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xF9], 8, 8, 3, data, .75);
                            break;

                        case BlockProperty.Starman:
                            RenderSpecialTileAlpha(_SpecialTable[0x06], 0, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x16], 0, 8, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x07], 8, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x17], 8, 8, 3, data, .75);
                            break;

                        case BlockProperty.CoinBlock:
                            RenderSpecialTileAlpha(_SpecialTable[0x0A], 0, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1A], 0, 8, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x0B], 8, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1B], 8, 8, 2, data, .75);
                            break;

                        case BlockProperty.Vine:
                            RenderSpecialTileAlpha(_SpecialTable[0x24], 0, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x34], 0, 8, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x25], 8, 0, 0, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x35], 8, 8, 0, data, .75);
                            break;

                        case BlockProperty.PSwitchBlock:
                            RenderSpecialTileAlpha(_SpecialTable[0x0C], 0, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1C], 0, 8, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x0D], 8, 0, 1, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x1D], 8, 8, 1, data, .75);
                            break;

                        case BlockProperty.NoteBlock:
                            RenderSpecialTileAlpha(_SpecialTable[0x04], 0, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x14], 0, 8, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x05], 8, 0, 3, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0x15], 8, 8, 3, data, .75);
                            break;

                        case BlockProperty.Brick:
                            RenderSpecialTileAlpha(_SpecialTable[0xDC], 0, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xEC], 0, 8, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xDD], 8, 0, 2, data, .75);
                            RenderSpecialTileAlpha(_SpecialTable[0xED], 8, 8, 2, data, .75);
                            break;
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
