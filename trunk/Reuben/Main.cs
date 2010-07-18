﻿using System;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ReubenController.MainWindow = this;
        }

        public void HideProjectview()
        {
            ProjectViewVisible = false;
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!MnuDebug.Enabled)
            {
                MnuDebug.Enabled = MnuEditor.Enabled = MnuExport.Enabled = MnuImport.Enabled = MnuNewLevel.Enabled = MnuProject.Enabled = MnuReload.Enabled = MnuTools.Enabled = MnuWindows.Enabled = ReubenController.CreateNewProject();
            }
            else
            {
                ReubenController.CreateNewProject();
            }
        }

        private void MnuNewLevel_Click(object sender, EventArgs e)
        {
            ReubenController.CreateNewLevel();
        }

        private void projectToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ProjectController.Save();
        }

        private void projectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MnuDebug.Enabled = MnuEditor.Enabled = MnuExport.Enabled = MnuImport.Enabled = MnuNewLevel.Enabled = MnuProject.Enabled = MnuReload.Enabled = MnuTools.Enabled = MnuWindows.Enabled = ReubenController.OpenProject();
        }

        private void paletteManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.OpenPaletteViewer();
        }

        private void graphicsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.OpenGraphicsEditor();
        }

        private void map16EditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                if (ActiveMdiChild is LevelEditor)
                {
                    Level l = ((LevelEditor)ActiveMdiChild).CurrentLevel;
                    ReubenController.OpenBlockEditor(l.Type, 0, l.GraphicsBank, l.AnimationBank, l.Palette);
                }
                else
                {
                    World w = ((WorldEditor)ActiveMdiChild).CurrentWorld;
                    ReubenController.OpenBlockEditor(w.Type, 0, w.GraphicsBank, w.AnimationBank, w.Palette);
                }
            }
            else
            {
                ReubenController.OpenBlockEditor();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (ProjectController.ProjectManager.CurrentProject != null)
                ProjectController.Save();
            base.OnClosed(e);
        }

        private int previousProjectSize = 256;
        private void BtnShowHide_Click(object sender, EventArgs e)
        {
            ProjectViewVisible = !ProjectViewVisible;
        }

        public bool ProjectViewVisible
        {
            get { return PrvProject.Visible;}
            set
            {
                if (value)
                {
                    PnlRightSide.Width = previousProjectSize;
                    PrvProject.Visible = true;
                    BtnShowHide.Text = ">>";
                }
                else
                {
                    previousProjectSize = PnlRightSide.Width;
                    PnlRightSide.Width = 35;
                    PrvProject.Visible = false;
                    BtnShowHide.Text = "<<";
                }
            }
        }

        private void layoutManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.OpenLayoutManager();
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.ImportLevel();
        }


        private void levelToPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.SaveCurrentLevelToBitmap();
        }

        private void graphicsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReubenController.ReloadGraphics();
        }

        private void currentLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.ReloadLevel();
        }

        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if(cForm.Confirm("Resetting the default sprite definitions requires all levels and worlds to be closed."))
            {
                ProjectController.SpriteManager.LoadDefaultSprites();
                ProjectController.Save();
            }
        }

        private void dumpRawLevelToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.DumpRawLevel();
        }

        private void compileROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.CompileRom(true);
        }

        private void rOMWoGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.CompileRom(false);
        }

        private void blockPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if(cForm.Confirm("Resetting the default block properties requires all levels and worlds to be closed."))
            {
                ProjectController.SpecialManager.LoadDefaultSpecials();
                ProjectController.Save();
            }
        }

        private void specialGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if(cForm.Confirm("Resetting the special graphics requires all levels and worlds to be closed."))
            {
                ProjectController.SpecialManager.LoadDefaultSpecialGraphics();
                ProjectController.Save();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if(cForm.Confirm("Resetting the music list requires all levels and worlds to be closed."))
            {
                ProjectController.MusicManager.LoadDefault();
                ProjectController.Save();
            }
        }

        private void allEditorDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            if(cForm.Confirm("Resetting the all editor definitions requires all levels and worlds to be closed."))
            {
                ProjectController.SpriteManager.LoadDefaultSprites();
                ProjectController.MusicManager.LoadDefault();
                ProjectController.SpecialManager.LoadDefaultSpecialGraphics();
                ProjectController.SpecialManager.LoadDefaultSpecials();
                ProjectController.Save();
            }
        }

        private void setPaletteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.GetPaletteFile();
        }

        private void paletteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.DefaulPaletteFile();
        }

    }
}