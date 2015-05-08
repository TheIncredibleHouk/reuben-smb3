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
    public partial class Prompt : Form
    {
        public static string GetText(string label)
        {
            Prompt p = new Prompt();
            p.SetText(label);
            if (p.ShowDialog() == DialogResult.OK)
            {
                return p.TextValue;
            }

            return null;
        }

        public void SetText(string text)
        {
            label.Text = text;
        }

        public string TextValue { get { return textValue.Text; } }

        public Prompt()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            textValue.Focus();
            base.OnShown(e);
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
