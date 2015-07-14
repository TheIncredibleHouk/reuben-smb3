using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class ItemDisplay : UserControl
    {
        public ItemDisplay()
        {
            InitializeComponent();
        }

        public void UpdateTiles(string headerText, IEnumerable<LevelInfo> levels)
        {
            hostPanel.Controls.Clear();
            header.Text = headerText;

            foreach (LevelInfo info in levels)
            {
                MetroTile tile = new MetroTile();

                tile.Text = info.Name;
                tile.Height = 436 / 2;
                tile.Width = 436 / 2;
                tile.Margin = new System.Windows.Forms.Padding(10);

                string filePath = Controllers.Project.ProjectData.ProjectDirectory + @"\cache\" + info.Name;
                if (File.Exists(filePath))
                {
                    PictureBox box = new PictureBox();
                    box.Image = Image.FromFile(filePath);
                    tile.Controls.Add(box);
                    box.Width = 416;
                    box.Height = 416;
                    box.Location = new Point(10, 10);
                }

                hostPanel.Controls.Add(tile);
            }
        }
    }
}
