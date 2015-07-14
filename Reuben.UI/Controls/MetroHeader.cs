using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace Reuben.UI
{
    public class MetroHeader : MetroLabel
    {
        public MetroHeader()
            : base()
        {
            this.AutoSize = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(this.ForeColor), new Point(0, this.Height - 1), new Point(this.Width - 1, this.Height - 1));
        }
    }
}
