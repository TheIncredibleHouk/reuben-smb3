namespace Daiz.NES.Reuben
{
    partial class ProjectView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectView));
            this.TrvProjectView = new System.Windows.Forms.TreeView();
            this.CtxLevels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxWorlds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newLevelToolStripMenuFromValue = new System.Windows.Forms.ToolStripMenuItem();
            this.TlsEdit = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.TsbNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbNewWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbOpen = new System.Windows.Forms.ToolStripButton();
            this.TsbRename = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.CtxLevels.SuspendLayout();
            this.CtxWorlds.SuspendLayout();
            this.TlsEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrvProjectView
            // 
            this.TrvProjectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvProjectView.Location = new System.Drawing.Point(0, 25);
            this.TrvProjectView.Name = "TrvProjectView";
            this.TrvProjectView.Size = new System.Drawing.Size(319, 320);
            this.TrvProjectView.TabIndex = 0;
            this.TrvProjectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvProjectView_AfterSelect);
            this.TrvProjectView.DoubleClick += new System.EventHandler(this.TrvProjectView_DoubleClick);
            // 
            // CtxLevels
            // 
            this.CtxLevels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMoveUp,
            this.mnuMoveDown,
            this.MnuMoveTo,
            this.toolStripSeparator1,
            this.newLevelToolStripMenuItem,
            this.renameLevelToolStripMenuItem,
            this.deleteLevelToolStripMenuItem});
            this.CtxLevels.Name = "CtxLevels";
            this.CtxLevels.Size = new System.Drawing.Size(153, 164);
            // 
            // mnuMoveUp
            // 
            this.mnuMoveUp.Name = "mnuMoveUp";
            this.mnuMoveUp.Size = new System.Drawing.Size(152, 22);
            this.mnuMoveUp.Text = "Move Up";
            this.mnuMoveUp.Click += new System.EventHandler(this.mnuMoveUp_Click);
            // 
            // mnuMoveDown
            // 
            this.mnuMoveDown.Name = "mnuMoveDown";
            this.mnuMoveDown.Size = new System.Drawing.Size(152, 22);
            this.mnuMoveDown.Text = "Move Down";
            this.mnuMoveDown.Click += new System.EventHandler(this.mnuMoveDown_Click);
            // 
            // MnuMoveTo
            // 
            this.MnuMoveTo.Name = "MnuMoveTo";
            this.MnuMoveTo.Size = new System.Drawing.Size(152, 22);
            this.MnuMoveTo.Text = "Move To";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // renameLevelToolStripMenuItem
            // 
            this.renameLevelToolStripMenuItem.Name = "renameLevelToolStripMenuItem";
            this.renameLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameLevelToolStripMenuItem.Text = "Rename Level";
            this.renameLevelToolStripMenuItem.Click += new System.EventHandler(this.TsbRename_Click);
            // 
            // deleteLevelToolStripMenuItem
            // 
            this.deleteLevelToolStripMenuItem.Name = "deleteLevelToolStripMenuItem";
            this.deleteLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteLevelToolStripMenuItem.Text = "Delete Level";
            this.deleteLevelToolStripMenuItem.Click += new System.EventHandler(this.deleteLevelToolStripMenuItem_Click);
            // 
            // CtxWorlds
            // 
            this.CtxWorlds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWorldToolStripMenuItem,
            this.editWorldToolStripMenuItem,
            this.deleteWorldToolStripMenuItem,
            this.toolStripSeparator2,
            this.newLevelToolStripMenuFromValue});
            this.CtxWorlds.Name = "CtxWorlds";
            this.CtxWorlds.Size = new System.Drawing.Size(143, 98);
            // 
            // newWorldToolStripMenuItem
            // 
            this.newWorldToolStripMenuItem.Name = "newWorldToolStripMenuItem";
            this.newWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.newWorldToolStripMenuItem.Text = "New World";
            this.newWorldToolStripMenuItem.Click += new System.EventHandler(this.newWorldToolStripMenuItem_Click);
            // 
            // editWorldToolStripMenuItem
            // 
            this.editWorldToolStripMenuItem.Name = "editWorldToolStripMenuItem";
            this.editWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.editWorldToolStripMenuItem.Text = "Edit World";
            this.editWorldToolStripMenuItem.Click += new System.EventHandler(this.editWorldToolStripMenuItem_Click);
            // 
            // deleteWorldToolStripMenuItem
            // 
            this.deleteWorldToolStripMenuItem.Name = "deleteWorldToolStripMenuItem";
            this.deleteWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteWorldToolStripMenuItem.Text = "Delete World";
            this.deleteWorldToolStripMenuItem.Click += new System.EventHandler(this.deleteWorldToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // newLevelToolStripMenuFromValue
            // 
            this.newLevelToolStripMenuFromValue.Name = "newLevelToolStripMenuFromValue";
            this.newLevelToolStripMenuFromValue.Size = new System.Drawing.Size(142, 22);
            this.newLevelToolStripMenuFromValue.Text = "New Level";
            this.newLevelToolStripMenuFromValue.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // TlsEdit
            // 
            this.TlsEdit.AutoSize = false;
            this.TlsEdit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TlsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.TsbOpen,
            this.TsbRename,
            this.TsbDelete});
            this.TlsEdit.Location = new System.Drawing.Point(0, 0);
            this.TlsEdit.Name = "TlsEdit";
            this.TlsEdit.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TlsEdit.Size = new System.Drawing.Size(319, 25);
            this.TlsEdit.TabIndex = 15;
            this.TlsEdit.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbNewLevel,
            this.TsbNewWorld});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(47, 22);
            this.toolStripSplitButton1.Text = "New";
            this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // TsbNewLevel
            // 
            this.TsbNewLevel.Name = "TsbNewLevel";
            this.TsbNewLevel.Size = new System.Drawing.Size(106, 22);
            this.TsbNewLevel.Text = "Level";
            this.TsbNewLevel.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // TsbNewWorld
            // 
            this.TsbNewWorld.Name = "TsbNewWorld";
            this.TsbNewWorld.Size = new System.Drawing.Size(106, 22);
            this.TsbNewWorld.Text = "World";
            this.TsbNewWorld.Click += new System.EventHandler(this.newWorldToolStripMenuItem_Click);
            // 
            // TsbOpen
            // 
            this.TsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("TsbOpen.Image")));
            this.TsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbOpen.Name = "TsbOpen";
            this.TsbOpen.Size = new System.Drawing.Size(40, 22);
            this.TsbOpen.Text = "Open";
            this.TsbOpen.Click += new System.EventHandler(this.TsbOpen_Click);
            // 
            // TsbRename
            // 
            this.TsbRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbRename.Image = ((System.Drawing.Image)(resources.GetObject("TsbRename.Image")));
            this.TsbRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbRename.Name = "TsbRename";
            this.TsbRename.Size = new System.Drawing.Size(54, 22);
            this.TsbRename.Text = "Rename";
            this.TsbRename.Click += new System.EventHandler(this.TsbRename_Click);
            // 
            // TsbDelete
            // 
            this.TsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("TsbDelete.Image")));
            this.TsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDelete.Name = "TsbDelete";
            this.TsbDelete.Size = new System.Drawing.Size(44, 22);
            this.TsbDelete.Text = "Delete";
            this.TsbDelete.Click += new System.EventHandler(this.TsbDelete_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TrvProjectView);
            this.Controls.Add(this.TlsEdit);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(319, 345);
            this.CtxLevels.ResumeLayout(false);
            this.CtxWorlds.ResumeLayout(false);
            this.TlsEdit.ResumeLayout(false);
            this.TlsEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TrvProjectView;
        private System.Windows.Forms.ContextMenuStrip CtxLevels;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuMoveTo;
        private System.Windows.Forms.ContextMenuStrip CtxWorlds;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuFromValue;
        private System.Windows.Forms.ToolStripMenuItem editWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStrip TlsEdit;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem TsbNewLevel;
        private System.Windows.Forms.ToolStripMenuItem TsbNewWorld;
        private System.Windows.Forms.ToolStripButton TsbOpen;
        private System.Windows.Forms.ToolStripButton TsbRename;
        private System.Windows.Forms.ToolStripButton TsbDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuMoveUp;
        private System.Windows.Forms.ToolStripMenuItem mnuMoveDown;
    }
}
