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
using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public unsafe class PaletteSelector : Control
    {
        public bool SelectablePaletteMode { get; set; }

        private Palette currentPalette;
        private Bitmap backBuffer;
        private GraphicsController graphicsController;

        public PaletteSelector()
        {
            backBuffer = new Bitmap(256, 32);

            this.Size = new Size(256, 32);
            this.MouseDown += new MouseEventHandler(PaletteSelector_MouseDown);
        }

        public void SetGraphicsController(GraphicsController controller)
        {
            graphicsController = controller;
        }

        public void SetPalette(Palette palette)
        {
            currentPalette = palette;
            RenderFull();
        }


        public void RenderFull()
        {
            using (Graphics graphicsContext = System.Drawing.Graphics.FromImage(backBuffer))
            {
                if (currentPalette == null)
                {
                    graphicsContext.Clear(Color.Black);
                }
                else
                {
                    graphicsContext.Clear(graphicsController.ColorReference[currentPalette.BackgroundValues[0]]);
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            UpdateColor(graphicsContext, i, j, graphicsController.ColorReference[currentPalette.GetColorIndex(i, j)]);
                        }
                    }
                }
            }
            Invalidate();
        }


        private void UpdateColor(Graphics graphicsContext, int index, int offset, Color color)
        {
            bool isTransparent = false;

            Brush brush = new SolidBrush(color);
            Rectangle rect = new Rectangle(((index % 4) * 64) + (offset * 16),
                                           (index / 4) * 16,
                                           16, 16);
            graphicsContext.FillRectangle(brush, rect);

            if (isTransparent)
            {
                graphicsContext.FillRectangle(Brushes.White, rect.X + 4, rect.Y + 4, rect.Width / 2, rect.Height / 2);
            }

            if (SelectablePaletteMode && SelectedIndex == index && SelectedOffset == offset)
            {
                rect.Width -= 1;
                rect.Height -= 1;
                graphicsContext.DrawRectangle(Pens.White, rect);
                rect.X += 1;
                rect.Y += 1;
                rect.Width -= 2;
                rect.Height -= 2;
                graphicsContext.DrawRectangle(Pens.Red, rect);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(backBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
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
            if (!SelectablePaletteMode)
            {
                return;
            }

            int offset = (e.X % 64) / 16;
            int index = ((e.Y / 16) * 4) + (e.X / 64);

            if (offset == SelectedOffset && index == SelectedIndex) return;

            int oldIndex = SelectedIndex;
            int oldOffset = SelectedOffset;

            SelectablePaletteMode = false;
            using (Graphics graphicsContext = Graphics.FromImage(backBuffer))
            {
                UpdateColor(g, SelectedIndex, SelectedOffset);

                SelectablePaletteMode = true;
                SelectedOffset = offset;
                SelectedIndex = index;
                UpdateColor(g, SelectedIndex, SelectedOffset);
            }

            Invalidate();
        }

    }
}
