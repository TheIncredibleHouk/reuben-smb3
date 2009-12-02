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
    public partial class ProjectView : UserControl
    {
        private Dictionary<WorldInfo, TreeNode> WorldToNodes;
        private Dictionary<LevelInfo, TreeNode> LevelToNodes;
        private Dictionary<TreeNode, WorldInfo> NodesToWorlds;
        private Dictionary<TreeNode, LevelInfo> NodesToLevels;
        public ProjectView()
        {
            InitializeComponent();
            WorldToNodes = new Dictionary<WorldInfo, TreeNode>();
            LevelToNodes = new Dictionary<LevelInfo, TreeNode>();
            NodesToWorlds = new Dictionary<TreeNode, WorldInfo>();
            NodesToLevels = new Dictionary<TreeNode, LevelInfo>();

            ProjectController.ProjectManager.ProjectLoaded += new EventHandler<TEventArgs<Project>>(ProjectManager_ProjectLoaded);
            ProjectController.LevelManager.LevelAdded += new EventHandler<TEventArgs<LevelInfo>>(LevelManager_LevelAdded);
        }

        void LevelManager_LevelAdded(object sender, TEventArgs<LevelInfo> e)
        {
            AddLevel(e.Data);
        }

        void ProjectManager_ProjectLoaded(object sender, TEventArgs<Project> e)
        {
            RefreshProject();
        }

        private void RefreshProject()
        {
            WorldToNodes.Clear();
            LevelToNodes.Clear();
            NodesToWorlds.Clear();
            NodesToLevels.Clear();
            TrvProjectView.Nodes.Clear();
            TreeNode projectNode = new TreeNode();
            projectNode.Tag = ProjectController.ProjectManager.CurrentProject;
            projectNode.Text = ProjectController.ProjectManager.CurrentProject.Name + " (Project)";

            foreach(var w in ProjectController.WorldManager.Worlds)
            {
                TreeNode nextNode = new TreeNode();
                nextNode.Text = w.Name + (w.Ordinal > 0 ? " (" + w.Ordinal + ")" : "");
                nextNode.Tag = w;
                foreach(var l in from lvl in ProjectController.LevelManager.Levels where lvl.WorldGuid == w.WorldGuid select lvl)
                {
                    TreeNode nextNextNode = new TreeNode();
                    nextNextNode.Text = l.Name;
                    nextNode.Nodes.Add(nextNextNode);
                    LevelToNodes.Add(l, nextNextNode);
                    NodesToLevels.Add(nextNextNode, l);
                }

                WorldToNodes.Add(w, nextNode);
                NodesToWorlds.Add(nextNode, w);
                projectNode.Nodes.Add(nextNode);
                ToolStripMenuItem nextMenu = new ToolStripMenuItem(w.Name);
                nextMenu.Tag = w;
                MnuMoveTo.DropDownItems.Add(nextMenu);
                nextMenu.Click += new EventHandler(MoveLevelTo_Clicked);
            }

            TrvProjectView.Nodes.Add(projectNode);
            BtnChangeName.Enabled = false;

            
        }

        private void MoveLevelTo_Clicked(object sender, EventArgs e)
        {
            WorldInfo wi = (sender as ToolStripMenuItem).Tag as WorldInfo;
            TreeNode oldWorldNode = WorldToNodes[ProjectController.WorldManager.GetWorldInfo(SelectedLevel.WorldGuid)];
            TreeNode newWorldNode = WorldToNodes[wi];
            TreeNode lvlNode = LevelToNodes[SelectedLevel];
            SelectedLevel.WorldGuid = wi.WorldGuid;
            oldWorldNode.Nodes.Remove(lvlNode);
            newWorldNode.Nodes.Add(lvlNode);
            
        }

        private void AddLevel(LevelInfo info)
        {
            TreeNode worldNode = WorldToNodes[ProjectController.WorldManager.GetWorldInfo(info.WorldGuid)];
            TreeNode nextLevelNode = new TreeNode();
            nextLevelNode.Tag = info;
            nextLevelNode.Text = info.Name;
            NodesToLevels.Add(nextLevelNode, info);
            LevelToNodes.Add(info, nextLevelNode);
            worldNode.Nodes.Add(nextLevelNode);
        }

        private void TrvProjectView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedLevel = null;
            BtnChangeName.Enabled = e.Node != null;
            if (e.Node == null) return;
            TreeNode node = e.Node;
            if (NodesToWorlds.ContainsKey(node))
            {
                SelectedWorld = NodesToWorlds[node];
                TxtName.Text = SelectedWorld.Name;
                TrvProjectView.ContextMenuStrip = CtxWorlds;
                LblType.Text = "World Map";
                LblGuid.Text = SelectedWorld.WorldGuid.ToString();
                LblLayout.Text = "Map";
                LblModified.Text = SelectedWorld.LastModified.ToString();
                LblSize.Text = SelectedWorld.LastCompressionSize + " bytes";
                LblType.Text = "World Map";
            }
            else if (NodesToLevels.ContainsKey(node))
            {
                SelectedLevel = NodesToLevels[node];
                SelectedWorld = ProjectController.WorldManager.GetWorldInfo(SelectedLevel.WorldGuid);
                TxtName.Text = SelectedLevel.Name;
                LblType.Text = ProjectController.LevelManager.GetLevelType(SelectedLevel.LevelType).Name;
                TrvProjectView.ContextMenuStrip = CtxLevels;
            }
            else
            {
                TxtName.Text = (e.Node.Tag as Project).Name;
                LblType.Text = "Project";
                TrvProjectView.ContextMenuStrip = null;
            }
        }

        private void BtnChangeName_Click(object sender, EventArgs e)
        {
            if (TrvProjectView.SelectedNode == null) return;

            if (NodesToLevels.ContainsKey(TrvProjectView.SelectedNode))
            {
                SelectedLevel.Name = TxtName.Text;
                TrvProjectView.SelectedNode.Text = TxtName.Text;
            }
            else if (NodesToWorlds.ContainsKey(TrvProjectView.SelectedNode))
            {
                SelectedWorld.Name = TxtName.Text;
                TrvProjectView.SelectedNode.Text = TxtName.Text;
            }
            else
            {
                ProjectController.ProjectManager.CurrentProject.Name = TxtName.Text;
                ProjectController.ProjectName = TxtName.Text;
                TrvProjectView.SelectedNode.Text = TxtName.Text + " (Project)";
            }
        }

        private void TrvProjectView_DoubleClick(object sender, EventArgs e)
        {
            if (TrvProjectView.SelectedNode != null)
            {
                if (NodesToLevels.ContainsKey(TrvProjectView.SelectedNode))
                {
                    ReubenController.EditLevel(SelectedLevel);
                }
            }
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReubenController.CreateNewLevel(SelectedWorld);
        }

        private LevelInfo SelectedLevel;
        private WorldInfo SelectedWorld;

        private void deleteLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmForm cForm = new ConfirmForm();
            if (cForm.Confirm("Are you sure you want to permanently remove this level?"))
            {
                ProjectController.LevelManager.RemoveLevel(SelectedLevel);
                WorldToNodes[SelectedWorld].Nodes.Remove(LevelToNodes[SelectedLevel]);
            }
        }

        private void editWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = TrvProjectView.SelectedNode;
            WorldInfo world = (WorldInfo) node.Tag;
            ReubenController.EditWorld(world);
        }
    }
}
