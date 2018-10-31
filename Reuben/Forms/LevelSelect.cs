using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public partial class LevelSelect : Form
    {
        public LevelSelect()
        {
            InitializeComponent();
            LbxWorlds.DisplayMember = "Name";
            LbxWorlds.DataSource = ProjectController.WorldManager.Worlds;
            LbxLevels.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public LevelInfo SelectedLevel
        {
            get
            {
                if (LbxLevels.SelectedIndex >= 0)
                {
                    return LbxLevels.SelectedItem as LevelInfo;
                }

                return null;
            }
        }

        private void LbxWorlds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LbxWorlds.SelectedIndex >= 0)
            {
                WorldInfo wInfo = LbxWorlds.SelectedItem as WorldInfo;
                LbxLevels.DataSource = (from l in ProjectController.LevelManager.Levels
                                        where l.WorldGuid == wInfo.WorldGuid
                                        orderby l.Name.ToLower()
                                        select l).ToList();
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void LbxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {

            BtnSelect.Enabled = LbxLevels.SelectedIndex >= 0;
        }
    }
}
