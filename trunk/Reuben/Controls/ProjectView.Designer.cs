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
            this.TrvProjectView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblGuid = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LblModified = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblLayout = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnChangeName = new System.Windows.Forms.Button();
            this.LblName = new System.Windows.Forms.Label();
            this.LblType = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CtxLevels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxWorlds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newLevelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.CtxLevels.SuspendLayout();
            this.CtxWorlds.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrvProjectView
            // 
            this.TrvProjectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvProjectView.Location = new System.Drawing.Point(0, 0);
            this.TrvProjectView.Name = "TrvProjectView";
            this.TrvProjectView.Size = new System.Drawing.Size(319, 162);
            this.TrvProjectView.TabIndex = 0;
            this.TrvProjectView.DoubleClick += new System.EventHandler(this.TrvProjectView_DoubleClick);
            this.TrvProjectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvProjectView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.BtnEdit);
            this.panel1.Controls.Add(this.LblGuid);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.LblModified);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.LblSize);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LblLayout);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnChangeName);
            this.panel1.Controls.Add(this.LblName);
            this.panel1.Controls.Add(this.LblType);
            this.panel1.Controls.Add(this.TxtName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 183);
            this.panel1.TabIndex = 1;
            // 
            // LblGuid
            // 
            this.LblGuid.AutoSize = true;
            this.LblGuid.Location = new System.Drawing.Point(100, 159);
            this.LblGuid.Margin = new System.Windows.Forms.Padding(3);
            this.LblGuid.Name = "LblGuid";
            this.LblGuid.Padding = new System.Windows.Forms.Padding(2);
            this.LblGuid.Size = new System.Drawing.Size(212, 17);
            this.LblGuid.TabIndex = 13;
            this.LblGuid.Text = "00000000-00000000-00000000-00000000";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 158);
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
            this.LblModified.Location = new System.Drawing.Point(100, 136);
            this.LblModified.Margin = new System.Windows.Forms.Padding(3);
            this.LblModified.Name = "LblModified";
            this.LblModified.Padding = new System.Windows.Forms.Padding(2);
            this.LblModified.Size = new System.Drawing.Size(39, 17);
            this.LblModified.TabIndex = 11;
            this.LblModified.Text = "Name";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 135);
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
            this.LblSize.Location = new System.Drawing.Point(100, 113);
            this.LblSize.Margin = new System.Windows.Forms.Padding(3);
            this.LblSize.Name = "LblSize";
            this.LblSize.Padding = new System.Windows.Forms.Padding(2);
            this.LblSize.Size = new System.Drawing.Size(39, 17);
            this.LblSize.TabIndex = 9;
            this.LblSize.Text = "Name";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 112);
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
            this.LblLayout.Location = new System.Drawing.Point(100, 90);
            this.LblLayout.Margin = new System.Windows.Forms.Padding(3);
            this.LblLayout.Name = "LblLayout";
            this.LblLayout.Padding = new System.Windows.Forms.Padding(2);
            this.LblLayout.Size = new System.Drawing.Size(39, 17);
            this.LblLayout.TabIndex = 7;
            this.LblLayout.Text = "Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Layout";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BtnChangeName
            // 
            this.BtnChangeName.Location = new System.Drawing.Point(177, 6);
            this.BtnChangeName.Name = "BtnChangeName";
            this.BtnChangeName.Size = new System.Drawing.Size(94, 23);
            this.BtnChangeName.TabIndex = 3;
            this.BtnChangeName.Text = "Change Name";
            this.BtnChangeName.UseVisualStyleBackColor = true;
            this.BtnChangeName.Click += new System.EventHandler(this.BtnChangeName_Click);
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(61, 43);
            this.LblName.Margin = new System.Windows.Forms.Padding(3);
            this.LblName.Name = "LblName";
            this.LblName.Padding = new System.Windows.Forms.Padding(2);
            this.LblName.Size = new System.Drawing.Size(39, 17);
            this.LblName.TabIndex = 0;
            this.LblName.Text = "Name";
            this.LblName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblType
            // 
            this.LblType.AutoSize = true;
            this.LblType.Location = new System.Drawing.Point(100, 67);
            this.LblType.Margin = new System.Windows.Forms.Padding(3);
            this.LblType.Name = "LblType";
            this.LblType.Padding = new System.Windows.Forms.Padding(2);
            this.LblType.Size = new System.Drawing.Size(39, 17);
            this.LblType.TabIndex = 5;
            this.LblType.Text = "Name";
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(106, 43);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(206, 20);
            this.TxtName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 66);
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
            this.newLevelToolStripMenuItem,
            this.MnuMoveTo,
            this.deleteLevelToolStripMenuItem});
            this.CtxLevels.Name = "CtxLevels";
            this.CtxLevels.Size = new System.Drawing.Size(138, 70);
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
            this.newLevelToolStripMenuItem1,
            this.editWorldToolStripMenuItem});
            this.CtxWorlds.Name = "CtxWorlds";
            this.CtxWorlds.Size = new System.Drawing.Size(130, 48);
            // 
            // newLevelToolStripMenuItem1
            // 
            this.newLevelToolStripMenuItem1.Name = "newLevelToolStripMenuItem1";
            this.newLevelToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.newLevelToolStripMenuItem1.Text = "New Level";
            this.newLevelToolStripMenuItem1.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // editWorldToolStripMenuItem
            // 
            this.editWorldToolStripMenuItem.Name = "editWorldToolStripMenuItem";
            this.editWorldToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.editWorldToolStripMenuItem.Text = "Edit World";
            this.editWorldToolStripMenuItem.Click += new System.EventHandler(this.editWorldToolStripMenuItem_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(15, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 23);
            this.BtnEdit.TabIndex = 14;
            this.BtnEdit.Text = "Edit";
            this.BtnEdit.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
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
            this.CtxLevels.ResumeLayout(false);
            this.CtxWorlds.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TrvProjectView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Button BtnChangeName;
        private System.Windows.Forms.ContextMenuStrip CtxLevels;
        private System.Windows.Forms.Label LblType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuMoveTo;
        private System.Windows.Forms.ContextMenuStrip CtxWorlds;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem1;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnEdit;
    }
}
