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

        public void Initialize(PatternTable patternTable, PatternTable overlayTable, Block[] blockList, Block[] overlayBlocks, Palette palette, Palette overlayPalette, Color[] colors)
        {
            blocks.Initialize(patternTable, overlayTable, blockList, overlayBlocks, palette, overlayPalette, colors);
        }

        public void Update(PatternTable patternTable = null, PatternTable overlayTable = null, Block[] blockList = null, Block[] overlayBlocks = null, Palette palette = null, Palette overlayPalette = null, Color[] colors = null)
        {
            blocks.Update(patternTable, overlayTable, blockList, overlayBlocks, palette, overlayPalette, colors);
        }

        public void SetSelectionRectangle(Rectangle r)
        {
            blocks.SetSelectionRectangle(r);
        }

        public bool ShowInteractionOverlays
        {
            get { return blocks.ShowInteractionOverlays; }
            set { blocks.ShowInteractionOverlays = value; }
        }

        public bool ShowSolidityOverlays
        {
            get { return blocks.ShowSolidityOverlays; }
            set { blocks.ShowSolidityOverlays = value; }
        }

        public void UpdateAll()
        {
            blocks.UpdateAll();
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
        public event EventHandler DoubleClicked;

        private void blocks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DoubleClicked != null)
            {
                DoubleClicked(sender, e);
            }
        }
    }
}
