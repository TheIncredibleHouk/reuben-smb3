using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reuben.UI
{
    public partial class ConfirmForm : Form
    {
        public ConfirmForm()
        {
            InitializeComponent();
        }

        public bool Confirm(string text)
        {
            LblText.Text = text;
            return this.ShowDialog() == DialogResult.OK;
        }
    }
}
