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
using Reuben.NESGraphics;

namespace Reuben.UI.Forms
{
    public partial class SpriteEditor : Form
    {
        public SpriteEditor()
        {
            InitializeComponent();
        }

        public void Initialize(GraphicsController graphicsController, SpriteController spriteController)
        {
            spriteSelector.Initialize(graphicsController, spriteController, graphicsController.GraphicsData.Colors, null);
        }
    }
}
