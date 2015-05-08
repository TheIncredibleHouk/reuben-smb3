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
        public void SetGraphicsController(GraphicsController controller)
        {
            graphics = controller;
            allPalettes.Palettes = graphics.GraphicsData.Palettes;
            selectedPalette.ColorReference = allPalettes.ColorReference = graphics.GraphicsData.Colors;
            

            colorView.SetColorReference(allPalettes.ColorReference);
            allPalettes.UpdateList();
        }

        private void allPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allPalettes.SelectedIndex > -1)
            {
                selectedPalette.SetPalette(graphics.GraphicsData.Palettes[allPalettes.SelectedIndex]);
            }
        }
    }
}
