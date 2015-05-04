using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reuben.UI
{
    public partial class FixedPanel : Panel
    {
        public FixedPanel() : base()
        {
            
        }

        protected override System.Drawing.Point  ScrollToControl(Control activeControl)
        {
            return DisplayRectangle.Location;
        }
    }
}
