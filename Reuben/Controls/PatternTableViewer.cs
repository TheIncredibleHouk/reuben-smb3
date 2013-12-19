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
    public unsafe class PatternTableViewer : Control
    {
        private bool _ShowGrid;
        public bool ShowGrid
        {
            get { return _ShowGrid; }
            set
            {
                if (_ShowGrid == value) return;
                _ShowGrid = value;
                Invalidate();
            }
        }
        private Tile[,] TileMap;
        private int[,] IndexMap;

        public event EventHandler SelectionChanged;
        public Tile[] SelectedTiles { get; private set; }

        private TileSelectionMode _SelectionMode;
        public TileSelectionMode TileSelectionMode
        {
            get { return _SelectionMode; }
            set
            {
                if (_SelectionMode == value) return;
                _SelectionMode = value;
            }
        }

        private ArrangementMode _ArrangementMode;
        public ArrangementMode ArrangementMode
        {
            set
            {
                if (value == _ArrangementMode) return;
                _ArrangementMode = value;
                FullRender();
            }
        }
        public PatternTableViewer()
        {
            TileMap = new Tile[16, 16];
            BackBuffer = new Bitmap(128, 128);
            IndexMap = new int[16, 16];
            QuickColorLookup = new Color[4];
            this.Width = this.Height = 256;
            SelectedTiles = new Tile[4];
            this.MouseDown += new MouseEventHandler(PatternTableViewer_MouseDown);
            FullRender();
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

        private int _PaletteIndex;
        public int PaletteIndex
        {
            set
            {
                _PaletteIndex = value;
                UpdateColors();
                FullRender();
            }
        }

        Bitmap BackBuffer;

        private Color[] QuickColorLookup;

        private void UpdateColors()
        {
            if (_CurrentPalette != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    QuickColorLookup[i] = ProjectController.ColorManager.Colors[_CurrentPalette[_PaletteIndex, i]];
                }
            }
        }

        private void FullRender()
        {
            FullRender(true, true, true, true);
        }

        private void FullRender(bool first, bool second, bool third, bool fourth)
        {
            if (_CurrentTable == null || _CurrentPalette == null)
            {
                Graphics.FromImage(BackBuffer).Clear(Color.Black);
                return;
            }

            BitmapData data = BackBuffer.LockBits(new Rectangle(0, 0, 128, 128), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            if (first)
            {
                switch (_ArrangementMode)
                {
                    case ArrangementMode.Normal:
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                RenderTile(_CurrentTable[j, i], j * 8, i * 8, data);
                                TileMap[j, i] = _CurrentTable[j, i];
                                IndexMap[j, i] = i * 16 + j;
                            }
                        }
                        break;

                    case ArrangementMode.Map16:

                        for (int y = 0; y < 4; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            {
                                RenderTile(_CurrentTable[x, y], ((x / 2) * 8) + ((y % 2) * 64), (x % 2) * 8 + ((y / 2) * 16), data);
                                TileMap[x, y] = _CurrentTable[(x % 8) * 2 + (y % 2), (x / 8) + ((y / 2) * 2)];
                                IndexMap[x, y] = ((x % 8) * 2 + (y % 2)) + (((x / 8) + ((y / 2) * 2)) * 16);
                            }
                        }
                        break;
                }
            }

            if (second)
            {
                switch (_ArrangementMode)
                {
                    case ArrangementMode.Normal:
                        for (int i = 4; i < 8; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                RenderTile(_CurrentTable[j, i], j * 8, i * 8, data);
                                TileMap[j, i] = _CurrentTable[j, i];
                                IndexMap[j, i] = i * 16 + j;
                            }
                        }
                        break;

                    case ArrangementMode.Map16:

                        for (int y = 4; y < 8; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            {
                                RenderTile(_CurrentTable[x, y], ((x / 2) * 8) + ((y % 2) * 64), (x % 2) * 8 + ((y / 2) * 16), data);
                                TileMap[x, y] = _CurrentTable[(x % 8) * 2 + (y % 2), (x / 8) + ((y / 2) * 2)];
                                IndexMap[x, y] = ((x % 8) * 2 + (y % 2)) + (((x / 8) + ((y / 2) * 2)) * 16);
                            }
                        }
                        break;
                }
            }

            if (third)
            {
                switch (_ArrangementMode)
                {
                    case ArrangementMode.Normal:
                        for (int i = 8; i < 12; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                RenderTile(_CurrentTable[j, i], j * 8, i * 8, data);
                                TileMap[j, i] = _CurrentTable[j, i];
                                IndexMap[j, i] = i * 16 + j;
                            }
                        }
                        break;

                    case ArrangementMode.Map16:

                        for (int y = 8; y < 12; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            {
                                RenderTile(_CurrentTable[x, y], ((x / 2) * 8) + ((y % 2) * 64), (x % 2) * 8 + ((y / 2) * 16), data);
                                TileMap[x, y] = _CurrentTable[(x % 8) * 2 + (y % 2), (x / 8) + ((y / 2) * 2)];
                                IndexMap[x, y] = ((x % 8) * 2 + (y % 2)) + (((x / 8) + ((y / 2) * 2)) * 16);
                            }
                        }
                        break;
                }
            }

            if (fourth)
            {
                switch (_ArrangementMode)
                {
                    case ArrangementMode.Normal:
                        for (int i = 12; i < 16; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                RenderTile(_CurrentTable[j, i], j * 8, i * 8, data);
                                TileMap[j, i] = _CurrentTable[j, i];
                                IndexMap[j, i] = i * 16 + j;
                            }
                        }
                        break;

                    case ArrangementMode.Map16:

                        for (int y = 12; y < 16; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            {
                                RenderTile(_CurrentTable[x, y], ((x / 2) * 8) + ((y % 2) * 64), (x % 2) * 8 + ((y / 2) * 16), data);
                                TileMap[x, y] = _CurrentTable[(x % 8) * 2 + (y % 2), (x / 8) + ((y / 2) * 2)];
                                IndexMap[x, y] = ((x % 8) * 2 + (y % 2)) + (((x / 8) + ((y / 2) * 2)) * 16);
                            }
                        }
                        break;
                }
            }

            BackBuffer.UnlockBits(data);
            Invalidate();
        }

        private void RenderTile(Tile tile, int x, int y, BitmapData data)
        {
            byte* dataPointer = (byte*)data.Scan0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    long offset = (data.Stride * (y + i)) + (x * 3);
                    long xOffset = (j * 3) + offset;
                    Color c = QuickColorLookup[tile[j, i]];
                    *(dataPointer + xOffset) = c.B;
                    *(dataPointer + xOffset + 1) = c.G;
                    *(dataPointer + xOffset + 2) = c.R;
                }
            }
        }

        public int SelectedX { get; private set; }
        public int SelectedY { get; private set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(BackBuffer, new Rectangle(0, 0, 257, 257), new Rectangle(0, 0, 128, 128), GraphicsUnit.Pixel);
            if (ShowGrid)
            {
                for (int i = 0; i < 16; i++)
                {
                    e.Graphics.DrawLine(Pens.White, i * 16, 0, i * 16, 256);
                    e.Graphics.DrawLine(Pens.White, 0, i * 16, 256, i * 16);
                }
            }
            switch (_SelectionMode)
            {
                case TileSelectionMode.SingleTile:
                    e.Graphics.DrawRectangle(Pens.White, new Rectangle(SelectedX * 16, SelectedY * 16, 15, 15));
                    e.Graphics.DrawRectangle(Pens.Red, new Rectangle(SelectedX * 16 + 1, SelectedY * 16 + 1, 13, 13));
                    break;

                case TileSelectionMode.TileBlock:
                    e.Graphics.DrawRectangle(Pens.White, new Rectangle(SelectedX * 16, SelectedY * 16, 31, 31));
                    e.Graphics.DrawRectangle(Pens.Red, new Rectangle(SelectedX * 16 + 1, SelectedY * 16 + 1, 29, 29));
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        public void UpdateSelection()
        {
            PatternTableViewer_MouseDown(null, new MouseEventArgs(MouseButtons.None, 1, SelectedX * 16, SelectedY * 16, 0));
        }

        private void PatternTableViewer_MouseDown(object sender, MouseEventArgs e)
        {
            int xIndex = e.X / 16;
            int yIndex = e.Y / 16;

            foreach (var t in SelectedTiles)
            {
                if (t != null)
                    t.PixelsChanged -= t_PixelsChanged;
            }

            switch (_SelectionMode)
            {
                case TileSelectionMode.SingleTile:
                    SelectedTiles[0] = TileMap[xIndex, yIndex];
                    if (SelectionChanged != null)
                        SelectionChanged(this, null);
                    SelectedX = xIndex;
                    SelectedY = yIndex;

                    Invalidate();
                    break;

                case TileSelectionMode.TileBlock:
                    if (xIndex >= 15) xIndex = 14;
                    if (yIndex >= 15) yIndex = 14;
                    SelectedTiles[0] = TileMap[xIndex, yIndex];
                    SelectedTiles[1] = TileMap[xIndex + 1, yIndex];
                    SelectedTiles[2] = TileMap[xIndex, yIndex + 1];
                    SelectedTiles[3] = TileMap[xIndex + 1, yIndex + 1];
                    if (SelectionChanged != null)
                        SelectionChanged(this, null);
                    SelectedX = xIndex;
                    SelectedY = yIndex;
                    Invalidate();
                    break;
            }

            SelectedIndex = IndexMap[SelectedX, SelectedY];

            foreach (var t in SelectedTiles)
            {
                if (t != null)
                {
                    t.PixelsChanged += new EventHandler(t_PixelsChanged);
                }
            }
        }

        void t_PixelsChanged(object sender, EventArgs e)
        {
            FullRender();
        }

        public int SelectedIndex { get; private set; }

        protected override void Dispose(bool disposing)
        {
            _CurrentTable.GraphicsChanged -= _CurrentTable_GraphicsChanged;
            base.Dispose(disposing);
        }
    }

    public enum ArrangementMode
    {
        Normal,
        Map16
    }

    public enum TileSelectionMode
    {
        SingleTile,
        TileBlock
    }
}
