using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Controllers;
using FastColoredTextBoxNS;

namespace Reuben.UI
{
    public partial class ASMEditor : Form
    {

        public ASMEditor()
        {
            InitializeComponent();
            ColumnHeader h = new ColumnHeader();
            h.Width = asmFiles.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            asmFiles.Columns.Add(h);
        }

        private ASMController localASMController;

        public void Initialize(ASMController controller)
        {
            localASMController = controller;

            asmFiles.Items.AddRange(localASMController.GetFileList().Select(l => new ListViewItem(l)).ToArray());
            localASMController.CodeChanged += localASMController_CodeChanged;
        }

        private void localASMController_CodeChanged(object sender, EventArgs e)
        {
            TextLocation loc = (TextLocation)sender;
            if (filesOpenedSoFar.ContainsKey(loc.File))
            {
                ASMFastColoredTextBox textbox = (ASMFastColoredTextBox)filesOpenedSoFar[loc.File].Tag;
                textbox.UpdateLine(loc.LineNumber, loc.Text);
                //((LinesAccessor) textbox.Lines).
                //textbox.Lin
                //Range r = textbox.GetRange(new Place(0, loc.LineNumber), new Place(textbox.GetLineLength(loc.LineNumber), loc.LineNumber));
                //textbox.TextSource.InsertLine(loc.LineNumber, newLine);
            }
        }


        Dictionary<string, TabPage> filesOpenedSoFar = new Dictionary<string, TabPage>();
        public void OpenFile(string file)
        {
            if (filesOpenedSoFar.ContainsKey(file))
            {
                filesOpened.SelectedTab = filesOpenedSoFar[file];
                return;
            }

            TabPage page = new TabPage();
            page.Text = Path.GetFileName(file);

            ASMFastColoredTextBox textBox = new ASMFastColoredTextBox();
            page.Controls.Add(textBox);
            page.Tag = textBox;
            textBox.Dock = DockStyle.Fill;
            filesOpened.TabPages.Add(page);
            filesOpenedSoFar[file] = page;
            textBox.Initiliaze(localASMController, file);

            filesOpened.SelectedTab = page;
            textBox.TextChanged += textBox_TextChanged;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filesOpened.SelectedTab.Text = filesOpened.SelectedTab.Text + "*";

            ASMFastColoredTextBox textBox = ((ASMFastColoredTextBox)filesOpened.SelectedTab.Tag);
            textBox.TextChanged -= textBox_TextChanged;

        }

        private void asmFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (asmFiles.SelectedItems.Count > 0)
            {
                OpenFile(asmFiles.SelectedItems[0].Text);
                saveButton.Enabled = closeButton.Enabled = true;
            }
        }

        private void filesOpened_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesOpened.SelectedTab != null)
            {
                ((ASMFastColoredTextBox)filesOpened.SelectedTab.Tag).Focus();
            }

            saveButton.Enabled = closeButton.Enabled = filesOpened.TabPages.Count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (filesOpened.SelectedTab != null)
            {
                filesOpenedSoFar.Remove(filesOpenedSoFar.Where(k => k.Value == filesOpened.SelectedTab).Select(k => k.Key).FirstOrDefault());
                filesOpened.TabPages.Remove(filesOpened.SelectedTab);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (filesOpened.SelectedTab != null)
            {
                filesOpened.SelectedTab.Text = filesOpened.SelectedTab.Text.Trim('*');

                ((ASMFastColoredTextBox)filesOpened.SelectedTab.Tag).Save();
                ((ASMFastColoredTextBox)filesOpened.SelectedTab.Tag).TextChanged += textBox_TextChanged;
            }
        }

        public void GoToTag(string file, string tag)
        {
            foreach (string f in file.Split('|'))
            {
                OpenFile(f);

                tag = ((ASMFastColoredTextBox)filesOpened.SelectedTab.Tag).GoToTag(tag);
                if (tag == null)
                {
                    break;
                }
            }
        }

        private void ASMEditor_Deactivate(object sender, EventArgs e)
        {
            foreach (TabPage page in filesOpenedSoFar.Values)
            {
                if (page.Text.EndsWith("*"))
                {
                    ASMFastColoredTextBox textbox = ((ASMFastColoredTextBox)page.Tag);
                    localASMController.MarkAsDirty(textbox.File, textbox.Lines.ToArray());
                }
            }
        }

        private void goToLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
