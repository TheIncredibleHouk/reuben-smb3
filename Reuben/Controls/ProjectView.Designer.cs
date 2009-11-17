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
            this.TrvProjectView.Size = new System.Drawing.Size(259, 250);
            this.TrvProjectView.TabIndex = 0;
            this.TrvProjectView.DoubleClick += new System.EventHandler(this.TrvProjectView_DoubleClick);
            this.TrvProjectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvProjectView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnChangeName);
            this.panel1.Controls.Add(this.LblName);
            this.panel1.Controls.Add(this.LblType);
            this.panel1.Controls.Add(this.TxtName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 250);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 95);
            this.panel1.TabIndex = 1;
            // 
            // BtnChangeName
            // 
            this.BtnChangeName.Location = new System.Drawing.Point(172, 4);
            this.BtnChangeName.Name = "BtnChangeName";
            this.BtnChangeName.Size = new System.Drawing.Size(75, 23);
            this.BtnChangeName.TabIndex = 3;
            this.BtnChangeName.Text = "Change";
            this.BtnChangeName.UseVisualStyleBackColor = true;
            this.BtnChangeName.Click += new System.EventHandler(this.BtnChangeName_Click);
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(8, 6);
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
            this.LblType.Location = new System.Drawing.Point(53, 32);
            this.LblType.Margin = new System.Windows.Forms.Padding(3);
            this.LblType.Name = "LblType";
            this.LblType.Padding = new System.Windows.Forms.Padding(2);
            this.LblType.Size = new System.Drawing.Size(39, 17);
            this.LblType.TabIndex = 5;
            this.LblType.Text = "Name";
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(53, 6);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(113, 20);
            this.TxtName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(35, 17);
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
            this.CtxWorlds.Size = new System.Drawing.Size(153, 70);
            // 
            // newLevelToolStripMenuItem1
            // 
            this.newLevelToolStripMenuItem1.Name = "newLevelToolStripMenuItem1";
            this.newLevelToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.newLevelToolStripMenuItem1.Text = "New Level";
            this.newLevelToolStripMenuItem1.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // editWorldToolStripMenuItem
            // 
            this.editWorldToolStripMenuItem.Name = "editWorldToolStripMenuItem";
            this.editWorldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editWorldToolStripMenuItem.Text = "Edit World";
            this.editWorldToolStripMenuItem.Click += new System.EventHandler(this.editWorldToolStripMenuItem_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TrvProjectView);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(259, 345);
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
    }
}
