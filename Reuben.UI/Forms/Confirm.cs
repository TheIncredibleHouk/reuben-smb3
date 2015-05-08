using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reuben.UI
{
    public partial class Confirm : Form
    {
        public static bool GetConfirmation(string label)
        {
            Confirm c = new Confirm();
            c.SetText(label);
            return c.ShowDialog() == DialogResult.OK;
        }

        public void SetText(string text)
        {
            label.Text = text;
        }

        public Confirm()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
