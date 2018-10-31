using System;
using System.Windows.Forms;

namespace Reuben.Controls.Liquid
{
    public class LiquidButton : Button
    {
        public LiquidButton() : base()
        {
            this.ParentChanged += LiquidButton_ParentChanged;
        }

        private void LiquidButton_ParentChanged(object sender, EventArgs e)
        {
            this.Parent.Resize += Parent_Resize;
            this.Adjust();
        }

        private void Parent_Resize(object sender, EventArgs e)
        {
            this.Adjust();
        }

        private void Adjust()
        {
            this.Left = 0;
            this.Width = this.Parent.Width;
        }
    }
}
