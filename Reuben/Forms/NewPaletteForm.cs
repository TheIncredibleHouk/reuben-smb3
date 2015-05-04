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
    public partial class NewPaletteForm : Form
    {
        public NewPaletteForm()
        {
            InitializeComponent();
        }

        public string GetInput(string Message)
        {
            LblMessage.Text = Message;
            if (this.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return TxtInput.Text;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public bool UseCurentPalette
        {
            get { return ChkUseCurrent.Checked; }
        }
    }
}
