using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;
using Reuben.UI.Controls;

namespace Reuben.UI
{
    public partial class PaletteManager : Form
    {
        public PaletteManager()
        {
            InitializeComponent();
        }

        private GraphicsController graphics;
        private List<Palette> localPalettes;
        public void SetGraphicsController(GraphicsController controller)
        {
            graphics = controller;
            localPalettes = graphics.GraphicsData.Palettes.OrderBy(p => p.Name).ToList();
            paletteList.Palettes = localPalettes;
            selectedPalette.ColorReference = paletteList.ColorReference = graphics.GraphicsData.Colors;
            colorView.SetColorReference(paletteList.ColorReference);
            paletteList.UpdateList();
        }

        private void allPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paletteList.SelectedIndex > -1)
            {
                selectedPalette.SetPalette(paletteList.SelectedPalette);
                paletteName.Text = paletteList.SelectedPalette.Name;
            }
        }

        private void paletteName_TextChanged_1(object sender, EventArgs e)
        {
            if (paletteList.SelectedPalette != null)
            {
                paletteList.SelectedPalette.Name = paletteName.Text;
                paletteName.Enabled = true;
                paletteList.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graphics.GraphicsData.Palettes = localPalettes;
            graphics.SavePalettes();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = Prompt.GetText("Palette name.");
            if (text != null)
            {
                Palette p = new Palette();
                p.Name = text;
                localPalettes.Add(p);
                paletteList.UpdateList();
                paletteList.SelectedPalette = p;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Confirm.GetConfirmation("Are you sure you want to remove this palette?"))
            {
                localPalettes.Remove(paletteList.SelectedPalette);
                paletteName.Text = "";
                paletteName.Enabled = false;
                paletteList.UpdateList();
            }
        }

        private int selectedColorIndex;
        private void colorView_MouseClick(object sender, MouseEventArgs e)
        {
            int column = e.X / 16;
            int row = e.Y / 16;
            colorView.SelectionPoint = new Point(column * 16, row * 16);
            selectedColorIndex = row * 16 + column;
        }

        private void selectedPalette_MouseClick(object sender, MouseEventArgs e)
        {
            int column = e.X / 16;
            int row = e.Y / 16;
            if(paletteList.SelectedPalette != null)
            {
                if (column % 4 == 0)
                {
                    paletteList.SelectedPalette.BackgroundValues[0] = selectedColorIndex;
                    paletteList.SelectedPalette.BackgroundValues[4] = selectedColorIndex;
                    paletteList.SelectedPalette.BackgroundValues[8] = selectedColorIndex;
                    paletteList.SelectedPalette.BackgroundValues[12] = selectedColorIndex;
                    paletteList.SelectedPalette.SpriteValues[0] = selectedColorIndex;
                    paletteList.SelectedPalette.SpriteValues[4] = selectedColorIndex;
                    paletteList.SelectedPalette.SpriteValues[8] = selectedColorIndex;
                    paletteList.SelectedPalette.SpriteValues[12] = selectedColorIndex;
                }
                else
                {
                    if (row == 0)
                    {
                        paletteList.SelectedPalette.BackgroundValues[column] = selectedColorIndex;
                    }
                    else if (row == 1)
                    {
                        paletteList.SelectedPalette.SpriteValues[column] = selectedColorIndex;
                    }
                }

                selectedPalette.UpdateAll();
                selectedPalette.Invalidate();
                paletteList.Invalidate();
            }
        }
    }
}
