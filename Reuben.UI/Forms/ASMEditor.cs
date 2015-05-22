using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace Reuben.UI
{
    public partial class ASMEditor : Form
    {
        public ASMEditor()
        {
            InitializeComponent();
        }
        public void Initialize(string lexerFile)
        {
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();
            //scintilla1.Styles[Style.ASM6502.CommentLine].BackColor
        }
    }
}
