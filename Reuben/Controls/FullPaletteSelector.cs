using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.UI.ProjectManagement;

namespace Reuben.UI
{
    public unsafe class FullPaletteSelector : Control
    {
        public event EventHandler SelectedPaletteChanged;

        public FullPaletteSelector()
        {
            BackBuffer = new Bitmap(256, 64);
            this.Size = new Size(256, 64);
            RenderFull();
            SelectedColor = 0;
        }

        Bitmap BackBuffer;

        private int _SelectedColor = 0x3F;

        public int SelectedColor
        {
            get { return _SelectedColor; }
            set
            {
                if (_SelectedColor == value) return;

                Rectangle rect = new Rectangle((_SelectedColor % 16) * 16,
                                               (_SelectedColor / 16) * 16,
                                               16,
                                               16);

                Graphics g = Graphics.FromImage(BackBuffer);
                UpdateColor(g, rect, ProjectController.ColorManager.Colors[_SelectedColor]);
                rect = new Rectangle((value % 16) * 16,
                                     (value / 16) * 16,
                                      15,
                                      15);

                g.DrawRectangle(Pens.White, rect);
                rect.X += 1;
                rect.Y += 1;
                rect.Width -= 2;
                rect.Height -= 2;
                g.DrawRectangle(Pens.Red, rect);
                g.Dispose();
                _SelectedColor = value;
                Invalidate();
                if (SelectedPaletteChanged != null) SelectedPaletteChanged(this, null);
            }
        }

        private void RenderFull()
        {
            Graphics g = Graphics.FromImage(BackBuffer);
            if (ProjectController.ColorManager.Colors == null)
            {
                g.Clear(Color.Black);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        UpdateColor(g, i, j, ProjectController.ColorManager.Colors[i * 16 + j]);
                    }
                }
            }
            g.Dispose();
        }

        private void UpdateColor(Graphics g, int index, int offset, Color color)
        {
            Rectangle rect = new Rectangle(offset * 16,
                                           index * 16,
                                           16, 16);
            UpdateColor(g, rect, color);
        }

        private void UpdateColor(Graphics g, Rectangle rect, Color color)
        {
            Brush brush = new SolidBrush(color);
            g.FillRectangle(brush, rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(BackBuffer, 0, 0);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int index = ((e.Y / 16) * 16) + (e.X / 16);
            if (index < 0x40)
                SelectedColor = index;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }
    }
}
