using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Daiz.Library;
using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public partial class LevelPointerEditor : UserControl
    {
        public LevelPointerEditor()
        {
            InitializeComponent();
            foreach(WorldInfo wi in from w in ProjectController.WorldManager.Worlds orderby w.Ordinal select w)
            {
                CmbWorldExit.Items.Add(wi.Name);
            }
        }

        public Level CurrentLevel { get; set; }

        private LevelPointer _CurrentPointer;
        public LevelPointer CurrentPointer
        {
            get
            {
                return _CurrentPointer;
            }
            set
            {
                if (value == null)
                {
                    this.Enabled = false;
                }
                else
                {
                    this.Enabled = true;
                    _CurrentPointer = value;
                    if (value.LevelGuid != Guid.Empty)
                    {
                        LevelInfo li = ProjectController.LevelManager.GetLevelInfo(value.LevelGuid);
                        LblPointsToWorld.Text = "World: " + ProjectController.WorldManager.GetWorldInfo(li.WorldGuid).Name;
                        LblPointsToLevel.Text = "Level: " + li.Name;
                    }
                    else
                    {
                        LblPointsToLevel.Text = "No level set.";
                    }

                    CmbWorldExit.Enabled = value.ExitsLevel;
                    CmbWorldExit.SelectedIndex = value.World;
                    CmbActions.SelectedIndex = value.ExitType;
                    NumXExit.Value = value.XExit;
                    NumYExit.Value = value.YExit;
                    LblXEnter.Text = "X Entrance: None";
                    LblYEnter.Text = "Y Entrance: None";
                    ChkExitsLevel.Checked = value.ExitsLevel;
                    ChkRedraw.Checked = value.RedrawLevel;
                    ChkKeepObjects.Checked = value.KeepObjects;
                    ChkDisableWeather.Checked = value.DisableWeather;
                    BtnChange.Enabled = CmbActions.Enabled = !ChkExitsLevel.Checked;
                    UpdatePosition();
                }
            }
        }

        private void NumXExit_ValueChanged(object sender, EventArgs e)
        {
            _CurrentPointer.XExit = (int) NumXExit.Value;
        }

        private void NumYExit_ValueChanged(object sender, EventArgs e)
        {
            _CurrentPointer.YExit = (int)NumYExit.Value;
        }

        private void CmbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CurrentPointer.ExitType = CmbActions.SelectedIndex;
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            LevelSelect lSelect = new LevelSelect();
            lSelect.StartPosition = FormStartPosition.CenterParent;
            lSelect.Owner = ReubenController.MainWindow;

            DialogResult dr = lSelect.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (lSelect.SelectedLevel != null)
                {
                    _CurrentPointer.LevelGuid = lSelect.SelectedLevel.LevelGuid;
                    ChkRedraw.Checked = _CurrentPointer.LevelGuid != CurrentLevel.Guid;
                    LblPointsToWorld.Text = "World: " + ProjectController.WorldManager.GetWorldInfo(lSelect.SelectedLevel.WorldGuid).Name;
                    LblPointsToLevel.Text = " Level: " + lSelect.SelectedLevel.Name;
                }
            }
        }

        private void ChkExitsLevel_CheckedChanged(object sender, EventArgs e)
        {
            BtnChange.Enabled = CmbActions.Enabled = ChkRedraw.Enabled = !ChkExitsLevel.Checked;
            CmbActions.Enabled = !ChkExitsLevel.Checked;
            CmbWorldExit.Enabled = ChkExitsLevel.Checked;
            _CurrentPointer.ExitsLevel = ChkExitsLevel.Checked;
        }

        public void UpdatePosition()
        {
            LblXEnter.Text = "X Entrance: " + CurrentPointer.XEnter.ToHexString() + "-" + (CurrentPointer.XEnter + 1).ToHexString();
            LblYEnter.Text = "Y Entrance: " + CurrentPointer.YEnter.ToHexString() + "-" + (CurrentPointer.YEnter + 1).ToHexString();
        }

        private void BtnOpenLevel_Click(object sender, EventArgs e)
        {
            LevelInfo li = ProjectController.LevelManager.GetLevelInfo(CurrentPointer.LevelGuid);
            if (li == null)
            {
                MessageBox.Show("The level could no longer be found in the project");
            }
            else
            {
                ReubenController.EditLevel(li);
            }
        }

        private void CmbWorldExit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPointer.World = CmbWorldExit.SelectedIndex;
        }

        private void ChkRedraw_CheckedChanged(object sender, EventArgs e)
        {
            _CurrentPointer.RedrawLevel = ChkRedraw.Checked;
        }

        private void ChkKeepObjects_CheckedChanged(object sender, EventArgs e)
        {
            _CurrentPointer.KeepObjects = ChkKeepObjects.Checked;
        }

        private void ChkDisableWeather_CheckedChanged(object sender, EventArgs e)
        {
            _CurrentPointer.DisableWeather = ChkDisableWeather.Checked;
        }
    }
}
