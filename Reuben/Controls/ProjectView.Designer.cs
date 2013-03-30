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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblName = new System.Windows.Forms.Label();
            this.TlsEdit = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.TsbNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbNewWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.TsbOpen = new System.Windows.Forms.ToolStripButton();
            this.TsbRename = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.LblGuid = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LblModified = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblLayout = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LblType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CtxLevels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxWorlds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newLevelToolStripMenuFromValue = new System.Windows.Forms.ToolStripMenuItem();
            this.editWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.TlsEdit.SuspendLayout();
            this.CtxLevels.SuspendLayout();
            this.CtxWorlds.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrvProjectView
            // 
            this.TrvProjectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvProjectView.Location = new System.Drawing.Point(0, 0);
            this.TrvProjectView.Name = "TrvProjectView";
            this.TrvProjectView.Size = new System.Drawing.Size(319, 174);
            this.TrvProjectView.TabIndex = 0;
            this.TrvProjectView.DoubleClick += new System.EventHandler(this.TrvProjectView_DoubleClick);
            this.TrvProjectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvProjectView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LblName);
            this.panel1.Controls.Add(this.TlsEdit);
            this.panel1.Controls.Add(this.LblGuid);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.LblModified);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.LblSize);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LblLayout);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.LblType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 171);
            this.panel1.TabIndex = 1;
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(100, 32);
            this.LblName.Margin = new System.Windows.Forms.Padding(3);
            this.LblName.Name = "LblName";
            this.LblName.Padding = new System.Windows.Forms.Padding(2);
            this.LblName.Size = new System.Drawing.Size(39, 17);
            this.LblName.TabIndex = 15;
            this.LblName.Text = "Name";
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
            this.TlsEdit.TabIndex = 14;
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
            this.TsbNewLevel.Size = new System.Drawing.Size(152, 22);
            this.TsbNewLevel.Text = "Level";
            this.TsbNewLevel.Click += new System.EventHandler(this.TsbNewLevel_Click);
            // 
            // TsbNewWorld
            // 
            this.TsbNewWorld.Name = "TsbNewWorld";
            this.TsbNewWorld.Size = new System.Drawing.Size(152, 22);
            this.TsbNewWorld.Text = "World";
            this.TsbNewWorld.Click += new System.EventHandler(this.TsbNewWorld_Click);
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
            // LblGuid
            // 
            this.LblGuid.AutoSize = true;
            this.LblGuid.Location = new System.Drawing.Point(100, 147);
            this.LblGuid.Margin = new System.Windows.Forms.Padding(3);
            this.LblGuid.Name = "LblGuid";
            this.LblGuid.Padding = new System.Windows.Forms.Padding(2);
            this.LblGuid.Size = new System.Drawing.Size(212, 17);
            this.LblGuid.TabIndex = 13;
            this.LblGuid.Text = "00000000-00000000-00000000-00000000";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 146);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(2);
            this.label9.Size = new System.Drawing.Size(88, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblModified
            // 
            this.LblModified.AutoSize = true;
            this.LblModified.Location = new System.Drawing.Point(100, 124);
            this.LblModified.Margin = new System.Windows.Forms.Padding(3);
            this.LblModified.Name = "LblModified";
            this.LblModified.Padding = new System.Windows.Forms.Padding(2);
            this.LblModified.Size = new System.Drawing.Size(39, 17);
            this.LblModified.TabIndex = 11;
            this.LblModified.Text = "Name";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 123);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(2);
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Last Modified";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblSize
            // 
            this.LblSize.AutoSize = true;
            this.LblSize.Location = new System.Drawing.Point(100, 101);
            this.LblSize.Margin = new System.Windows.Forms.Padding(3);
            this.LblSize.Name = "LblSize";
            this.LblSize.Padding = new System.Windows.Forms.Padding(2);
            this.LblSize.Size = new System.Drawing.Size(39, 17);
            this.LblSize.TabIndex = 9;
            this.LblSize.Text = "Name";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(2);
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Calculated Size";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblLayout
            // 
            this.LblLayout.AutoSize = true;
            this.LblLayout.Location = new System.Drawing.Point(100, 78);
            this.LblLayout.Margin = new System.Windows.Forms.Padding(3);
            this.LblLayout.Name = "LblLayout";
            this.LblLayout.Padding = new System.Windows.Forms.Padding(2);
            this.LblLayout.Size = new System.Drawing.Size(39, 17);
            this.LblLayout.TabIndex = 7;
            this.LblLayout.Text = "Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Layout";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(61, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(2);
            this.label12.Size = new System.Drawing.Size(39, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Name";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblType
            // 
            this.LblType.AutoSize = true;
            this.LblType.Location = new System.Drawing.Point(100, 55);
            this.LblType.Margin = new System.Windows.Forms.Padding(3);
            this.LblType.Name = "LblType";
            this.LblType.Padding = new System.Windows.Forms.Padding(2);
            this.LblType.Size = new System.Drawing.Size(39, 17);
            this.LblType.TabIndex = 5;
            this.LblType.Text = "Name";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CtxLevels
            // 
            this.CtxLevels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuMoveTo,
            this.toolStripSeparator1,
            this.newLevelToolStripMenuItem,
            this.deleteLevelToolStripMenuItem});
            this.CtxLevels.Name = "CtxLevels";
            this.CtxLevels.Size = new System.Drawing.Size(138, 76);
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // MnuMoveTo
            // 
            this.MnuMoveTo.Name = "MnuMoveTo";
            this.MnuMoveTo.Size = new System.Drawing.Size(137, 22);
            this.MnuMoveTo.Text = "Move To";
            // 
            // deleteLevelToolStripMenuItem
            // 
            this.deleteLevelToolStripMenuItem.Name = "deleteLevelToolStripMenuItem";
            this.deleteLevelToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
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
            // newLevelToolStripMenuFromValue
            // 
            this.newLevelToolStripMenuFromValue.Name = "newLevelToolStripMenuFromValue";
            this.newLevelToolStripMenuFromValue.Size = new System.Drawing.Size(142, 22);
            this.newLevelToolStripMenuFromValue.Text = "New Level";
            this.newLevelToolStripMenuFromValue.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // newWorldToolStripMenuItem
            // 
            this.newWorldToolStripMenuItem.Name = "newWorldToolStripMenuItem";
            this.newWorldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newWorldToolStripMenuItem.Text = "New World";
            this.newWorldToolStripMenuItem.Click += new System.EventHandler(this.newWorldToolStripMenuItem_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TrvProjectView);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(319, 345);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TlsEdit.ResumeLayout(false);
            this.TlsEdit.PerformLayout();
            this.CtxLevels.ResumeLayout(false);
            this.CtxWorlds.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TrvProjectView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip CtxLevels;
        private System.Windows.Forms.Label LblType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuMoveTo;
        private System.Windows.Forms.ContextMenuStrip CtxWorlds;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuFromValue;
        private System.Windows.Forms.ToolStripMenuItem editWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLevelToolStripMenuItem;
        private System.Windows.Forms.Label LblLayout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblGuid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label LblModified;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip TlsEdit;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem TsbNewLevel;
        private System.Windows.Forms.ToolStripMenuItem TsbNewWorld;
        private System.Windows.Forms.ToolStripButton TsbOpen;
        private System.Windows.Forms.ToolStripButton TsbRename;
        private System.Windows.Forms.ToolStripButton TsbDelete;
        private System.Windows.Forms.ToolStripMenuItem deleteWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newWorldToolStripMenuItem;
    }
}
