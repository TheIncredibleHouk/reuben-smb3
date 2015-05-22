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

        public void Initialize(PatternTable patternTable, Block[] blockList, Palette palette, Color[] colors)
        {
            blocks.Initialize(patternTable, blockList, palette, colors);
        }

        public void Update(PatternTable patternTable = null, Block[] blockList = null, Palette palette = null, Color[] colors = null)
        {
            blocks.Update(patternTable, blockList, palette, colors);
        }

        public void SetSelectionRectangle(Rectangle r)
        {
        }

        private int selectedBlock;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedBlock
        {
            get { return selectedBlock; }
            set
            {
                selectedBlock = value;

                blocks.SetSelectionRectangle(new Rectangle((value % 16) * 16, (value / 16) * 16, 15, 15));
                if (SelectedBlockChanged != null)
                {
                    SelectedBlockChanged(null, null);
                }
            }
        }

        public void UpdateBlock(int col, int row)
        {
            blocks.UpdateBlock(col, row);
        }

        public event EventHandler BubbledMouseDown;
        private void blocks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int col = (e.X / 16) * 16;
                int row = (e.Y / 16) * 16;
                blocks.SetSelectionRectangle(new Rectangle(col, row, 15, 15));
                if (Editor != null)
                {
                    Editor.EditMode = EditMode.Blocks;
                }

                SelectedBlock = e.X / 16 + ((e.Y / 16) * 16);
            }
            else
            {
                if (BubbledMouseDown != null)
                {
                    BubbledMouseDown(sender, e);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LevelEditor Editor { get; set; }

        public event EventHandler SelectedBlockChanged;
    }
}
