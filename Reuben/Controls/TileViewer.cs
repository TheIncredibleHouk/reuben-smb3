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
    public unsafe class TileViewer : Control
    {
        public event EventHandler TileChanged;
        private bool _ShowGrid;
        public bool ShowGrid
        {
            get { return _ShowGrid; }
            set
            {
                if (_ShowGrid == value) return;
                _ShowGrid = value;
                FullRender();
            }
        }

        private Tile[] CurrentTiles;

        public TileViewer()
        {
            BackBuffer = new Bitmap(256, 256, PixelFormat.Format24bppRgb);
            BackBuffer.SetResolution(96, 96);
            QuickColorLookup = new Color[4];
            this.Width = this.Height = 256;
            CurrentTiles = new Tile[4];
            FullRender();

            this.MouseMove += new MouseEventHandler(TileViewer_MouseMove);
            this.MouseDown += new MouseEventHandler(TileViewer_MouseDown);
            this.MouseUp += new MouseEventHandler(TileViewer_MouseUp);
        }

        void TileViewer_MouseUp(object sender, MouseEventArgs e)
        {
            DrawMode = false;
        }

        private bool DrawMode;

        void TileViewer_MouseDown(object sender, MouseEventArgs e)
        {
            UpdatePixel(e.X, e.Y);
        }

        void TileViewer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            if (DrawMode)
            {
                if (MouseButtons == MouseButtons.Left)
                {
                    UpdatePixel(e.X, e.Y);
                }
                else
                {
                    DrawMode = false;
                }
            }
        }

        void UpdatePixel(int x, int y)
        {
            if (x < 0 || y < 0 || x > 255 || y > 255) return;
            int tileNumber = x / 128 + ((y / 128) * 2);
            int pixelX = (x % 128) / 16;
            int pixelY = (y % 128) / 16;

            CurrentTiles[tileNumber][pixelX, pixelY] = SelectedOffset;

            Graphics g = Graphics.FromImage(BackBuffer);
            Brush b = new SolidBrush(QuickColorLookup[SelectedOffset]);
            if (ShowGrid)
            {
                g.FillRectangle(b, new Rectangle((x / 16) * 16 + 1, (y / 16) * 16 + 1, 15, 15));
            }
            else
            {
                g.FillRectangle(b, new Rectangle((x / 16) * 16, (y / 16) * 16, 16, 16));
            }
            g.Dispose();

            DrawMode = true;
            if (TileChanged != null)
            {
                TileChanged(null, null);
            }

            Invalidate(new Rectangle((x / 16) * 16 , (y / 16) * 16, 16, 16));
        }

        public void UpdateTile(int tileNumber, Tile tile)
        {
            CurrentTiles[tileNumber] = tile;
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

        public byte SelectedOffset { get; set; }

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
            if (CurrentTiles[0] == null || CurrentTiles[1] == null ||
                CurrentTiles[2] == null || CurrentTiles[3] == null || _CurrentPalette == null)
            {
                Graphics.FromImage(BackBuffer).Clear(Color.Black);
                return;
            }

            Graphics g = Graphics.FromImage(BackBuffer);

            if (first)
            {
                RenderTile(CurrentTiles[0], 0, 0, g);
            }

            if (second)
            {
                RenderTile(CurrentTiles[1], 8, 0, g);
            }

            if (third)
            {
                RenderTile(CurrentTiles[2], 0, 8, g);
            }

            if (fourth)
            {
                RenderTile(CurrentTiles[3], 8, 8, g);
            }

            if (ShowGrid)
            {
                for (int i = 0; i < 16; i++)
                {
                    g.DrawLine(Pens.White, i * 16, 0, i * 16, 256);
                    g.DrawLine(Pens.White, 0, i * 16, 256, i * 16);
                }
            }
            
            g.Dispose();
            Invalidate();
        }

        private void RenderTile(Tile tile, int x, int y, Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color c = QuickColorLookup[tile[j, i]];
                    Brush b = new SolidBrush(Color.FromArgb(c.R, c.G, c.B));
                    g.FillRectangle(b, new Rectangle((x * 16) + (j * 16), (y * 16) + (i * 16), 16, 16));
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(BackBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
