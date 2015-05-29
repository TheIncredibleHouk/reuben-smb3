using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reuben.UI.Controls
{
    public partial class DefinitionSpriteEditor : UserControl
    {
        public DefinitionSpriteEditor()
        {
            InitializeComponent();
        }

        public bool Selected
        {
            get { return selected.Checked; }
            set { selected.Checked = value; }
        }

        public string Value
        {
            get { return spriteValue.Text; }
            set
            {
                spriteValue.Text = value;
            }
        }
    }
}
