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

namespace Reuben.UI
{
    public partial class BlockSelector : UserControl
    {
        public BlockSelector()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color[] ColorReference
        {
            get { return blocks.ColorReference; }
            set { blocks.ColorReference = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatternTable PatternTable
        {
            get { return blocks.PatternTable; }
            set { blocks.PatternTable = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Block[] BlockList
        {
            get { return blocks.BlockList; }
            set { blocks.BlockList = value; }
        }

        public void UpdateGraphics()
        {
            blocks.UpdateGraphics();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get { return blocks.Palette; }
            set { blocks.Palette = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle SelectionRectangle
        {
            get { return blocks.SelectionRectangle; }
            set { blocks.SelectionRectangle = value; }
        }

        private int selectedBlock;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedBlock
        {
            get { return selectedBlock; }
            set
            {
                selectedBlock = value;
                blocks.SelectionRectangle = new Rectangle((value % 16) * 16, (value / 16) * 16, 15, 15);
            }
        }
        private void blocks_MouseDown(object sender, MouseEventArgs e)
        {
            int col = (e.X / 16) * 16;
            int row = (e.Y / 16) * 16;
            blocks.SelectionRectangle = new Rectangle(col, row, 15, 15);
            selectedBlock = e.X / 16 + ((e.Y / 16) * 16);
            Editor.EditMode = EditMode.Blocks;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LevelEditor Editor { get; set; }
    }
}
