using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;
namespace Reuben.UI.Controls
{
    public class PaletteView : Control
    {
        private Bitmap buffer;
        public PaletteView()
        {
            this.Size = new Size(256, 32);
            buffer = new Bitmap(256, 32);
        }

        private GraphicsController graphics;
        public void SetGraphicsControler(GraphicsController controller)
        {
            graphics = controller;
        }

        private Palette currentPalette;
        public void SetPalette(Palette palette)
        {
            currentPalette = palette;
            UpdateAll();
        }

        private void UpdateAll()
        {
            using(Graphics gfx = Graphics.FromImage(buffer))
            {
            for (int i = 0; i < 32; i++)
            {
                DrawColor(gfx, graphics.GraphicsData.Colors[currentPalette.GetColorIndex )
            }
        }

        private void DrawColor(Graphics gfx, Color color, int column, int row)
        {
            Brush brush = new SolidBrush(color);
            Rectangle rect = new Rectangle(column * 16,
                                           row * 16,
                                           16,
                                           16);
            gfx.FillRectangle(brush, rect);
        }
    }
}
