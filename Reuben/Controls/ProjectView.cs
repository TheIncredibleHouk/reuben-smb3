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
            ProjectController.WorldManager.WorldAdded += new EventHandler<TEventArgs<WorldInfo>>(WorldManager_WorldAdded);
        }

        void WorldManager_WorldAdded(object sender, TEventArgs<WorldInfo> e)
        {
            AddWorld(e.Data);
        }


        void LevelManager_LevelAdded(object sender, TEventArgs<LevelInfo> e)
        {
            AddLevel(e.Data);
        }

        void ProjectManager_ProjectLoaded(object sender, TEventArgs<Project> e)
        {
            RefreshProject();
        }

        TreeNode projectNode = new TreeNode();
        private void RefreshProject()
        {
            WorldToNodes.Clear();
            LevelToNodes.Clear();
            NodesToWorlds.Clear();
            NodesToLevels.Clear();
            TrvProjectView.Nodes.Clear();
            projectNode.Nodes.Clear();
            projectNode.Tag = ProjectController.ProjectManager.CurrentProject;
            projectNode.Text = ProjectController.ProjectManager.CurrentProject.Name + " (Project)";

            foreach(var w in from w in ProjectController.WorldManager.Worlds orderby w.Ordinal select w)
            {
                TreeNode nextNode = new TreeNode();
                nextNode.Text = w.Name;
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
            projectNode.Expand();
            TlsEdit.Enabled = false;
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
            worldNode.Expand();
        }

        private void AddWorld(WorldInfo wi)
        {
            TreeNode nextNode = new TreeNode();
            nextNode.Text = wi.Name;
            nextNode.Tag = wi;
            WorldToNodes.Add(wi, nextNode);
            NodesToWorlds.Add(nextNode, wi);
            projectNode.Nodes.Insert(projectNode.Nodes.Count - 1, nextNode);
            ToolStripMenuItem nextMenu = new ToolStripMenuItem(wi.Name);
            nextMenu.Tag = wi;
            MnuMoveTo.DropDownItems.Add(nextMenu);
            nextMenu.Click += new EventHandler(MoveLevelTo_Clicked);
        }

        private void TrvProjectView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedLevel = null;
            if (e.Node == null) return;
            TreeNode node = e.Node;
            if (NodesToWorlds.ContainsKey(node))
            {
                SelectionType = SelectionType.World;
                SelectedWorld = NodesToWorlds[node];

                LblGuid.Visible = LblLayout.Visible = LblModified.Visible = LblSize.Visible = !SelectedWorld.IsNoWorld;
                LblName.Text = SelectedWorld.Name;
                if (!SelectedWorld.IsNoWorld)
                {
                    LblType.Text = "World Map - " + (SelectedWorld.Ordinal + 1);
                    LblLayout.Text = "Map";
                    TrvProjectView.ContextMenuStrip = CtxWorlds;
                    LblGuid.Text = SelectedWorld.WorldGuid.ToString();
                    LblModified.Text = SelectedWorld.LastModified.ToString();
                    LblSize.Text = SelectedWorld.LastCompressionSize + " bytes";
                }
                else
                {
                    LblType.Text = "Grouping";
                    LblLayout.Text = "";
                    TrvProjectView.ContextMenuStrip = null;
                    LblGuid.Text = "";
                    LblModified.Text = "";
                    LblSize.Text = "";
                }

                TlsEdit.Enabled = !SelectedWorld.IsNoWorld;
            }
            else if (NodesToLevels.ContainsKey(node))
            {
                SelectionType = SelectionType.Level;
                SelectedLevel = NodesToLevels[node];
                SelectedWorld = ProjectController.WorldManager.GetWorldInfo(SelectedLevel.WorldGuid);
                LblGuid.Visible = LblLayout.Visible = LblModified.Visible = LblSize.Visible = true;
                LblName.Text = SelectedLevel.Name;
                TrvProjectView.ContextMenuStrip = CtxLevels;
                LblType.Text = ProjectController.LevelManager.LevelTypes[SelectedLevel.LevelType].Name;
                LblGuid.Text = SelectedLevel.LevelGuid.ToString();
                LblLayout.Text = SelectedLevel.Layout.ToString();
                LblModified.Text = SelectedLevel.LastModified.ToString();
                LblSize.Text = SelectedLevel.LastCompressionSize + " bytes";
                TlsEdit.Enabled = true;
            }
            else
            {
                SelectionType = SelectionType.Project;
                LblName.Text = ((Project)e.Node.Tag).Name;
                LblType.Text = "Project";
                LblGuid.Visible = LblLayout.Visible = LblModified.Visible = LblSize.Visible = false;
                TrvProjectView.ContextMenuStrip = CtxWorlds;
                TlsEdit.Enabled = false;
            }
        }

        private void BtnChangeName_Click(object sender, EventArgs e)
        {
            if (TrvProjectView.SelectedNode == null) return;

            if (NodesToLevels.ContainsKey(TrvProjectView.SelectedNode))
            {
                SelectedLevel.Name = LblName.Text;
                TrvProjectView.SelectedNode.Text = LblName.Text;
            }
            else if (NodesToWorlds.ContainsKey(TrvProjectView.SelectedNode))
            {
                SelectedWorld.Name = LblName.Text;
                TrvProjectView.SelectedNode.Text = LblName.Text;
            }
            else
            {
                ProjectController.ProjectManager.CurrentProject.Name = LblName.Text;
                ProjectController.ProjectName = LblName.Text;
                TrvProjectView.SelectedNode.Text = LblName.Text + " (Project)";
            }
        }

        private void TrvProjectView_DoubleClick(object sender, EventArgs e)
        {
            if (SelectionType == SelectionType.Level)
            {
                Open();
            }
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.Level;
            New();
            SelectionType = prevType;
        }

        private LevelInfo SelectedLevel;
        private WorldInfo SelectedWorld;

        private void deleteLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.Level;
            Delete();
            SelectionType = prevType;
        }

        private void editWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.World;
            Open();
            SelectionType = prevType;
        }

        private void New()
        {
            switch (SelectionType)
            {
                case SelectionType.Level:
                    ReubenController.CreateNewLevel(SelectedWorld);
                    break;


                case SelectionType.World:
                    ReubenController.CreateNewWorld();
                    break;
            }
        }

        private void Open()
        {
            switch (SelectionType)
            {
                case SelectionType.Level:

                    ReubenController.EditLevel(SelectedLevel);
                    break;

                case SelectionType.World:
                    ReubenController.EditWorld(SelectedWorld);
                    break;
            }
        }

        private void Delete()
        {
            ConfirmForm cForm = new ConfirmForm();

            cForm.StartPosition = FormStartPosition.CenterParent;
            cForm.Owner = ReubenController.MainWindow;

            switch (SelectionType)
            {
                case SelectionType.Level:
                    if (cForm.Confirm("Are you sure you want to permanently remove this level?"))
                    {
                        ReubenController.CloseLevelEditor(SelectedLevel);
                        LevelInfo li = SelectedLevel;
                        WorldToNodes[SelectedWorld].Nodes.Remove(LevelToNodes[li]);
                        LevelToNodes.Remove(li);
                        ProjectController.LevelManager.RemoveLevel(li);
                    }
                    break;

                case SelectionType.World:

                    if (cForm.Confirm("Are you sure you want to permanently remove this world? All levels will be moved to No World."))
                    {
                        ReubenController.CloseWorldEditor(SelectedWorld);
                        ProjectController.WorldManager.RemoveWorld(SelectedWorld);
                        WorldInfo noWorld = ProjectController.WorldManager.GetNoWorld();
                        WorldInfo thisWorld = SelectedWorld;

                        foreach (TreeNode node in WorldToNodes[SelectedWorld].Nodes)
                        {
                            LevelInfo li = NodesToLevels[node];
                            TreeNode oldWorldNode = WorldToNodes[SelectedWorld];
                            TreeNode newWorldNode = WorldToNodes[noWorld];
                            TreeNode lvlNode = LevelToNodes[li];
                            li.WorldGuid = noWorld.WorldGuid;
                            oldWorldNode.Nodes.Remove(lvlNode);
                            newWorldNode.Nodes.Add(lvlNode);
                        }

                        projectNode.Nodes.Remove(WorldToNodes[thisWorld]);
                        NodesToWorlds.Remove(WorldToNodes[thisWorld]);
                        WorldToNodes.Remove(thisWorld);
                        ToolStripMenuItem removeThis = null;
                        foreach (ToolStripMenuItem menu in MnuMoveTo.DropDownItems)
                        {
                            if (menu.Tag == thisWorld)
                            {
                                removeThis = menu;
                                break;
                            }
                        }

                        if (removeThis != null)
                        {
                            MnuMoveTo.DropDownItems.Remove(removeThis);
                        }
                    }
                    break;
            }
        }
                    
        SelectionType SelectionType;

        private void deleteWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.World;
            Delete();
            SelectionType = prevType;
        }

        private void TsbNewLevel_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.Level;
            New();
            SelectionType = prevType;
            
        }

        private void TsbNewWorld_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.World;
            New();
            SelectionType = prevType;
        }

        private void newWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionType prevType = SelectionType;
            SelectionType = SelectionType.World;
            New();
            SelectionType = prevType;
        }

        private void TsbOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void TsbRename_Click(object sender, EventArgs e)
        {
            InputForm iForm = new InputForm();
            iForm.StartPosition = FormStartPosition.CenterParent;
            iForm.Owner = ReubenController.MainWindow;

            string name = iForm.GetInput("Please enter a new name");
            if (name != null)
            {
                switch (SelectionType)
                {
                    case SelectionType.Level:
                        SelectedLevel.Name = name;
                        LevelToNodes[SelectedLevel].Text = name;
                        break;

                    case SelectionType.World:
                        SelectedWorld.Name = name;
                        WorldToNodes[SelectedWorld].Text = name;
                        break;
                }
            }
        }

        private void TsbDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }

    public enum SelectionType
    {
        Project,
        World,
        Level
    }
}
