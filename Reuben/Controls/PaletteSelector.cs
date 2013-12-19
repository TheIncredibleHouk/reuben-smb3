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
    public unsafe class PaletteSelector : Control
    {
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedOffsetChanged;

        public bool SelectablePaletteMode { get; set; }

        public PaletteSelector()
        {
            BackBuffer = new Bitmap(256, 32);
            _CurrentPalette = null;
            this.Size = new Size(256, 32);
            this.MouseDown += new MouseEventHandler(PaletteSelector_MouseDown);
        }

        private PaletteInfo _CurrentPalette;
        public PaletteInfo CurrentPalette
        {
            get
            {
                return _CurrentPalette;
            }
            set
            {
                if (_CurrentPalette != value)
                {
                    if (_CurrentPalette != null)
                    {
                        _CurrentPalette.PaletteChanged -= _CurrentPalette_PaletteChanged;
                    }

                    _CurrentPalette = value;

                    if (value != null)
                    {
                        _CurrentPalette.PaletteChanged += new EventHandler<TEventArgs<DoubleValue<int, int>>>(_CurrentPalette_PaletteChanged);
                    }
                }
                RenderFull();
            }
        }

        void _CurrentPalette_PaletteChanged(object sender, TEventArgs<DoubleValue<int, int>> e)
        {
            if (!DelayedRender)
            {
                Graphics g = Graphics.FromImage(BackBuffer);
                UpdateColor(g, e.Data.Value1, e.Data.Value2);
                g.Dispose();
                Invalidate();
            }
        }

        private bool DelayedRender = false;
        Bitmap BackBuffer;

        public void BeginRender()
        {
            DelayedRender = true;
        }

        public void EndRender()
        {
            DelayedRender = false;
            RenderFull();
        }

        private void RenderFull()
        {
            Graphics g = Graphics.FromImage(BackBuffer);
            if (CurrentPalette == null)
            {
                g.Clear(Color.Black);
            }
            else
            {
                g.Clear(ProjectController.ColorManager.Colors[CurrentPalette.Background]);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        UpdateColor(g, i, j);
                    }
                }
            }
            g.Dispose();
            Invalidate();
        }


        private void UpdateColor(Graphics g, int index, int offset)
        {
            Color c = ProjectController.ColorManager.Colors[_CurrentPalette[index, offset]];
            bool isTransparent = false;
            if(c == Color.Empty)
            {
                c = Color.Black;
                isTransparent = true;
            }

            Brush brush = new SolidBrush(c);
            Rectangle rect = new Rectangle(((index % 4) * 64) + (offset * 16),
                                           (index / 4) * 16,
                                           16, 16);
            g.FillRectangle(brush, rect);

            if (isTransparent)
            {
                g.FillRectangle(Brushes.White, rect.X + 4, rect.Y + 4, rect.Width / 2, rect.Height / 2);
            }

            if (SelectablePaletteMode && SelectedIndex == index && SelectedOffset == offset)
            {
                rect.Width -= 1;
                rect.Height -= 1;
                g.DrawRectangle(Pens.White, rect);
                rect.X += 1;
                rect.Y += 1;
                rect.Width -= 2;
                rect.Height -= 2;
                g.DrawRectangle(Pens.Red, rect);
            }

            brush.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(BackBuffer, 0, 0);
        }

        protected override void  OnPaintBackground(PaintEventArgs pevent)
        {
             	 
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PaletteSelector
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaletteSelector_MouseDown);
            this.ResumeLayout(false);

        }

        public int SelectedIndex { get; private set; }
        public int SelectedOffset { get; private set; }

        private void PaletteSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if(!SelectablePaletteMode)
                return;
            int offset = (e.X % 64) / 16;
            int index = ((e.Y / 16) * 4) + (e.X / 64);

            if (offset == SelectedOffset && index == SelectedIndex) return;

            int oldIndex = SelectedIndex;
            int oldOffset = SelectedOffset;

            SelectablePaletteMode = false;
            Graphics g = Graphics.FromImage(BackBuffer);
            UpdateColor(g, SelectedIndex, SelectedOffset);
            SelectablePaletteMode = true;
            SelectedOffset = offset;
            SelectedIndex = index;
            UpdateColor(g, SelectedIndex, SelectedOffset);

            if (oldIndex != SelectedIndex)
            {
                if (SelectedIndexChanged != null)
                {
                    SelectedIndexChanged(this, null);
                }
            }

            if (oldOffset != SelectedOffset)
            {
                if (SelectedOffsetChanged != null)
                {
                    SelectedOffsetChanged(this, null);
                }
            }

            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            _CurrentPalette.PaletteChanged -= _CurrentPalette_PaletteChanged;
            base.Dispose(disposing);
        }
    }
}
