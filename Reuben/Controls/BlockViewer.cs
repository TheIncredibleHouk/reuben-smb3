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
    public unsafe class BlockViewer : Control
    {
        public BlockViewer()
        {
            BackBuffer = new Bitmap(16, 16);
            QuickColorLookup = new Color[4, 4];
            CurrentBlock = null;
            this.Width = this.Height = 32;
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

        private Block _CurrentBlock;
        public Block CurrentBlock
        {
            get
            {
                return _CurrentBlock;
            }
            set
            {
                if (_CurrentBlock == value) return;
                _CurrentBlock = value;
                FullRender();
            }
        }

        private int _PaletteIndex;
        public int PaletteIndex
        {
            set
            {
                _PaletteIndex = value;
            }
        }

        Bitmap BackBuffer;

        private Color[,] QuickColorLookup;

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

        private void FullRender()
        {
            if (_CurrentTable == null || _CurrentPalette == null || _CurrentBlock == null)
            {
                Graphics.FromImage(BackBuffer).Clear(Color.Black);
                return;
            }

            BitmapData data = BackBuffer.LockBits(new Rectangle(0, 0, 16, 16), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            RenderTile(_CurrentTable[_CurrentBlock[0, 0]], 0, 0, _PaletteIndex, data);
            RenderTile(_CurrentTable[_CurrentBlock[0, 1]], 0, 8, _PaletteIndex, data);
            RenderTile(_CurrentTable[_CurrentBlock[1, 0]], 8, 0, _PaletteIndex, data);
            RenderTile(_CurrentTable[_CurrentBlock[1, 1]], 8, 8, _PaletteIndex, data);

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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(BackBuffer, new Rectangle(0, 0, 33, 33), new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        public void SetTile(int x, int y, byte value)
        {
            _CurrentBlock[x, y] = value;
            FullRender();
        }

        protected override void Dispose(bool disposing)
        {
            if (_CurrentTable != null)
            {
                _CurrentTable.GraphicsChanged -= _CurrentTable_GraphicsChanged;
            }
            base.Dispose(disposing);
        }
    }
}
