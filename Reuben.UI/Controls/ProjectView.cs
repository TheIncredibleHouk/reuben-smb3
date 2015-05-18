using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI.Controls
{
    public partial class ProjectView : UserControl
    {
        ProjectController projectController;
        GraphicsController graphicsController;
        LevelController levelController;
        StringController resourceController;
        SpriteController spriteController;

        public ProjectView()
        {
            InitializeComponent();
        }

        public void SetProjectController(ProjectController controller)
        {
            projectController = controller;

            graphicsController = new GraphicsController();
            graphicsController.LoadGraphics(controller.Project.GraphicsFile);
            graphicsController.LoadExtraGraphics(controller.Project.ExtraGraphicsFile);
            graphicsController.LoadPalettes(controller.Project.PaletteFile);

            levelController = new LevelController();
            levelController.Load(controller.Project.LevelDataFile);

            resourceController = new StringController();
            resourceController.Load(controller.Project.StringDataFile);

            spriteController = new SpriteController();
            spriteController.Load(controller.Project.SpriteDataFile);

            savebutton.Enabled =
            palettesButton.Enabled =
            blocksButton.Enabled =
            spritesButton.Enabled =
            asmButton.Enabled = 
            textButton.Enabled = 
            defaultsButton.Enabled =
            projectName.Enabled = true;

            projectName.Text = controller.Project.Name;
        }

        public void RefreshTreeView()
        {
            projectTree.BeginUpdate();
            projectTree.Nodes.Clear();
            foreach (var n in projectController.Project.Structure.Nodes)
            {
                AddNode(projectTree.Nodes, n);
            }
            projectTree.EndUpdate();
        }

        public void AddNode(TreeNodeCollection nodes, ProjectNode currentNode)
        {
            var newNode = new TreeNode() { Text = currentNode.Name, Tag = currentNode };
            nodes.Add(newNode);
            foreach (var n in currentNode.Nodes)
            {
                AddNode(newNode.Nodes, n);
            }
        }

        ProjectController mainProject = new ProjectController();
        private void OpenFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON File (*.json)|*json|All Files|*";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!mainProject.Load(openDialog.FileName))
                {
                    MessageBox.Show("Unable to load " + openDialog.FileName + ". Please verify the file is a proper Reuben project file.");
                }
                else
                {
                    SetProjectController(mainProject);
                    RefreshTreeView();
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PaletteManager mgr = new PaletteManager();
            mgr.SetGraphicsController(graphicsController);
            mgr.ShowDialog();
        }

        private void projectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(projectTree.SelectedNode != null && ((ProjectNode)projectTree.SelectedNode.Tag).Type == NodeType.Level)
            {
                projectTree.ContextMenuStrip = levelContext;
            }
            else
            {
                projectTree.ContextMenuStrip = worldContext;
            }
        }

        private void openLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelInfo levelInfo = levelController.LevelData.Levels.Where(l => l.ID == ((ProjectNode) projectTree.SelectedNode.Tag).ID).FirstOrDefault();
            if (levelInfo != null)
            {
                levelInfo.File = projectController.Project.LevelsDirectory + "\\" + levelInfo.Name + ".json";
                LevelEditor editor = new LevelEditor();
                editor.Show();
                editor.LoadLevel(levelInfo, levelController, graphicsController, resourceController, spriteController);
            }
        }

        private void textButton_Click(object sender, EventArgs e)
        {
            StringManager mgr = new StringManager();
            mgr.Show();
            mgr.SetResources(resourceController);
        }

        private void blocksButton_Click(object sender, EventArgs e)
        {
            BlockEditor editor = new BlockEditor();
            editor.Initialize(levelController, graphicsController);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                levelController.LevelData.Types = editor.LocalLevelTypes;
                levelController.Save();
            }
        }
    }
}
