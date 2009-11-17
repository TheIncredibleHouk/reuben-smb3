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
    public partial class NewLevelForm : Form
    {
        public NewLevelForm()
        {
            InitializeComponent();
            CmbWorlds.DisplayMember = "Name";
            CmbType.DisplayMember = "Name";
            CmbType.DataSource = from l in ProjectController.LevelManager.LevelTypes where l.InGameID != 0 select l;
            CmbWorlds.DataSource = ProjectController.WorldManager.Worlds;
            CmbLayout.SelectedIndex = 0;
        }

        public DialogResult ShowDialog(WorldInfo world)
        {
            CmbWorlds.SelectedItem = world;
            return this.ShowDialog();
        }

        public WorldInfo SelectedWorld
        {
            get { return CmbWorlds.SelectedItem as WorldInfo; }
        }

        public string LevelName
        {
            get { return TxtName.Text; }
        }

        public LevelType LevelType
        {
            get { return CmbType.SelectedItem as LevelType; }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public LevelLayout LevelLayout
        {
            get
            {
                switch (CmbLayout.SelectedIndex)
                {
                    case 0:
                        return LevelLayout.Horizontal;

                    case 1:
                        return LevelLayout.Vertical;
                }

                return LevelLayout.Horizontal;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
