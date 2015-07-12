namespace Reuben.UI
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
            this.projectTree = new System.Windows.Forms.TreeView();
            this.savebutton = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.projectName = new System.Windows.Forms.TextBox();
            this.defaultsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.textLinkLabel = new System.Windows.Forms.LinkLabel();
            this.asmLinkLabel = new System.Windows.Forms.LinkLabel();
            this.spritesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.blocksLinkLabel = new System.Windows.Forms.LinkLabel();
            this.palettesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.levelContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.levelContext.SuspendLayout();
            this.worldContext.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectTree
            // 
            this.projectTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.projectTree.Location = new System.Drawing.Point(0, 0);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(146, 268);
            this.projectTree.TabIndex = 0;
            this.projectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectTree_AfterSelect);
            // 
            // savebutton
            // 
            this.savebutton.Enabled = false;
            this.savebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebutton.Location = new System.Drawing.Point(168, 52);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(75, 23);
            this.savebutton.TabIndex = 3;
            this.savebutton.Text = "Save";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(87, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4);
            this.label1.Size = new System.Drawing.Size(43, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // projectName
            // 
            this.projectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectName.Enabled = false;
            this.projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectName.Location = new System.Drawing.Point(6, 25);
            this.projectName.Margin = new System.Windows.Forms.Padding(4);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(237, 20);
            this.projectName.TabIndex = 1;
            // 
            // defaultsLinkLabel
            // 
            this.defaultsLinkLabel.Enabled = false;
            this.defaultsLinkLabel.Location = new System.Drawing.Point(10, 127);
            this.defaultsLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.defaultsLinkLabel.Name = "defaultsLinkLabel";
            this.defaultsLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.defaultsLinkLabel.TabIndex = 5;
            this.defaultsLinkLabel.Text = "Defaults";
            // 
            // textLinkLabel
            // 
            this.textLinkLabel.Enabled = false;
            this.textLinkLabel.Location = new System.Drawing.Point(10, 65);
            this.textLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.textLinkLabel.Name = "textLinkLabel";
            this.textLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.textLinkLabel.TabIndex = 4;
            this.textLinkLabel.Text = "Text";
            this.textLinkLabel.Click += new System.EventHandler(this.textLinkLabel_Click);
            // 
            // asmLinkLabel
            // 
            this.asmLinkLabel.Enabled = false;
            this.asmLinkLabel.Location = new System.Drawing.Point(10, 34);
            this.asmLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.asmLinkLabel.Name = "asmLinkLabel";
            this.asmLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.asmLinkLabel.TabIndex = 3;
            this.asmLinkLabel.Tag = "";
            this.asmLinkLabel.Text = "ASM";
            this.asmLinkLabel.Click += new System.EventHandler(this.asmLinkLabel_Click);
            // 
            // spritesLinkLabel
            // 
            this.spritesLinkLabel.Enabled = false;
            this.spritesLinkLabel.Location = new System.Drawing.Point(10, 158);
            this.spritesLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.spritesLinkLabel.Name = "spritesLinkLabel";
            this.spritesLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.spritesLinkLabel.TabIndex = 2;
            this.spritesLinkLabel.Text = "Sprites";
            this.spritesLinkLabel.Click += new System.EventHandler(this.spritesLinkLabel_Click);
            // 
            // blocksLinkLabel
            // 
            this.blocksLinkLabel.Enabled = false;
            this.blocksLinkLabel.Location = new System.Drawing.Point(10, 96);
            this.blocksLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.blocksLinkLabel.Name = "blocksLinkLabel";
            this.blocksLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.blocksLinkLabel.TabIndex = 1;
            this.blocksLinkLabel.Text = "Blocks";
            this.blocksLinkLabel.Click += new System.EventHandler(this.blocksLinkLabel_Click);
            // 
            // palettesLinkLabel
            // 
            this.palettesLinkLabel.Enabled = false;
            this.palettesLinkLabel.Location = new System.Drawing.Point(10, 4);
            this.palettesLinkLabel.Margin = new System.Windows.Forms.Padding(4);
            this.palettesLinkLabel.Name = "palettesLinkLabel";
            this.palettesLinkLabel.Size = new System.Drawing.Size(75, 23);
            this.palettesLinkLabel.TabIndex = 0;
            this.palettesLinkLabel.Text = "Palettes";
            this.palettesLinkLabel.Click += new System.EventHandler(this.button2_Click);
            // 
            // levelContext
            // 
            this.levelContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLevelToolStripMenuItem,
            this.toolStripSeparator1,
            this.openToolStripMenuItem,
            this.deleteLevelToolStripMenuItem});
            this.levelContext.Name = "levelContext";
            this.levelContext.Size = new System.Drawing.Size(138, 76);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openLevelToolStripMenuItem.Text = "Open Level";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.openLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openToolStripMenuItem.Text = "New Level";
            // 
            // deleteLevelToolStripMenuItem
            // 
            this.deleteLevelToolStripMenuItem.Name = "deleteLevelToolStripMenuItem";
            this.deleteLevelToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.deleteLevelToolStripMenuItem.Text = "Delete Level";
            // 
            // worldContext
            // 
            this.worldContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWorldToolStripMenuItem,
            this.toolStripSeparator2,
            this.editWorldToolStripMenuItem,
            this.deleteWorldToolStripMenuItem});
            this.worldContext.Name = "worldContext";
            this.worldContext.Size = new System.Drawing.Size(143, 76);
            // 
            // openWorldToolStripMenuItem
            // 
            this.openWorldToolStripMenuItem.Name = "openWorldToolStripMenuItem";
            this.openWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openWorldToolStripMenuItem.Text = "Open World";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // editWorldToolStripMenuItem
            // 
            this.editWorldToolStripMenuItem.Name = "editWorldToolStripMenuItem";
            this.editWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.editWorldToolStripMenuItem.Text = "Edit World";
            // 
            // deleteWorldToolStripMenuItem
            // 
            this.deleteWorldToolStripMenuItem.Name = "deleteWorldToolStripMenuItem";
            this.deleteWorldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteWorldToolStripMenuItem.Text = "Delete World";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.savebutton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.projectName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 100);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.defaultsLinkLabel);
            this.panel3.Controls.Add(this.palettesLinkLabel);
            this.panel3.Controls.Add(this.textLinkLabel);
            this.panel3.Controls.Add(this.blocksLinkLabel);
            this.panel3.Controls.Add(this.asmLinkLabel);
            this.panel3.Controls.Add(this.spritesLinkLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(412, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(93, 268);
            this.panel3.TabIndex = 3;
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.projectTree);
            this.Controls.Add(this.panel2);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(505, 368);
            this.levelContext.ResumeLayout(false);
            this.worldContext.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectTree;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel savebutton;
        private System.Windows.Forms.LinkLabel button1;
        private System.Windows.Forms.LinkLabel defaultsLinkLabel;
        private System.Windows.Forms.LinkLabel textLinkLabel;
        private System.Windows.Forms.LinkLabel asmLinkLabel;
        private System.Windows.Forms.LinkLabel spritesLinkLabel;
        private System.Windows.Forms.LinkLabel blocksLinkLabel;
        private System.Windows.Forms.LinkLabel palettesLinkLabel;
        private System.Windows.Forms.ContextMenuStrip levelContext;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLevelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip worldContext;
        private System.Windows.Forms.ToolStripMenuItem openWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}
