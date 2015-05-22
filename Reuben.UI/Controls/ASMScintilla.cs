using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using ScintillaNET;

namespace Reuben.UI
{
    public class ASMScintilla : Scintilla
    {
        private const int SYMBOLS = 0;
        public void Initiliaze()
        {
            

            //StyleNeeded += ASMScintilla_StyleNeeded;
        }

        private void ASMScintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            this.SetStyling(0, 0);  
        }
    }
}
