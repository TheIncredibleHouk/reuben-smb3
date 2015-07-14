using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Controllers;
using Reuben.Model;

namespace Reuben.UI
{
    public partial class StringManager : Form
    {
        public StringManager()
        {
            InitializeComponent();
            ColumnHeader h = new ColumnHeader();
            h.Width = resourceDisplay.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            resourceDisplay.Columns.Add(h);
        }

        private Dictionary<string, List<string>> localResources;
        public void Initalize()
        {
            localResources = Controllers.Strings.Resource.ResourceLists.MakeCopy();
            FilterResources();
        }

        public string SelectedResource
        {
            get
            {
                if (resourceDisplay.SelectedItems.Count == 0)
                {
                    return null;
                }

                return resourceDisplay.SelectedItems[0].Text;
            }
            set
            {
                resourceDisplay.SelectedItems.Clear();
                var index = filteredList.IndexOf(value);
                if (index > -1)
                {
                    resourceDisplay.SelectedIndices.Add(index);
                }
            }
        }

        private List<string> filteredList;
        public void FilterResources()
        {
            string selected = SelectedResource;
            string text = filter.Text.ToLower().Trim();
            filteredList = localResources.Keys.Where(k => k.ToLower().Contains(text)).Select(k => k).ToList();
            resourceDisplay.Items.Clear();
            resourceDisplay.Items.AddRange(filteredList.ToArray().Select(l => new ListViewItem(l)).ToArray());
            SelectedResource = selected;
        }

        private string previousResource;
        private void resourceDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (previousResource != null)
            {
                SetResourceValues(previousResource);
            }

            previousResource = SelectedResource;
            if (SelectedResource != null)
            {
                values.Text = string.Join("\r\n", localResources[SelectedResource]);
            }
            else
            {
                values.Text = "";
            }

        }

        private void SetResourceValues(string resourceName)
        {
            if (resourceName != null)
            {
                localResources[resourceName] = values.Text.Split("\n".ToCharArray()).Select(t => t.Trim()).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetResourceValues(previousResource);
            Controllers.Strings.Resource.ResourceLists = localResources;
            Controllers.Strings.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string value = Prompt.GetText("List name.");
            if (value != null)
            {
                localResources.Add(value, new List<string>());
            }
            FilterResources();
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            FilterResources();
        }
    }
}
