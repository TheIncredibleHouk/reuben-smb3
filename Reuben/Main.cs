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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ReubenController.MainWindow = this;
        }

        public void HideProjectview()
        {
            BtnShowHide_Click(null, null);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.CreateNewProject();
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
            ReubenController.OpenProject();
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

        private int previousProjectSize = 0;
        private void BtnShowHide_Click(object sender, EventArgs e)
        {
            if (PrvProject.Visible)
            {
                previousProjectSize = PnlRightSide.Width;
                PnlRightSide.Width = 35;
                PrvProject.Visible = false;
                BtnShowHide.Text = "<<";
            }
            else
            {
                PnlRightSide.Width = previousProjectSize;
                PrvProject.Visible = true;
                BtnShowHide.Text = ">>";
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

        private void graphicsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReubenController.ImportGraphics();
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
            ReubenController.ReloadDefaults();
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

    }
}
