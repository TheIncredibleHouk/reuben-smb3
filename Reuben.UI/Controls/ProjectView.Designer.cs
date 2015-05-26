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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.savebutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.projectName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.defaultsButton = new System.Windows.Forms.Button();
            this.textButton = new System.Windows.Forms.Button();
            this.asmButton = new System.Windows.Forms.Button();
            this.spritesButton = new System.Windows.Forms.Button();
            this.blocksButton = new System.Windows.Forms.Button();
            this.palettesButton = new System.Windows.Forms.Button();
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
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.levelContext.SuspendLayout();
            this.worldContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectTree
            // 
            this.projectTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTree.Location = new System.Drawing.Point(0, 0);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(263, 257);
            this.projectTree.TabIndex = 0;
            this.projectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectTree_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 111);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(263, 111);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.savebutton);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.projectName);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(255, 85);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // savebutton
            // 
            this.savebutton.Enabled = false;
            this.savebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebutton.Location = new System.Drawing.Point(172, 56);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(75, 23);
            this.savebutton.TabIndex = 3;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(91, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 4);
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
            this.projectName.Location = new System.Drawing.Point(10, 29);
            this.projectName.Margin = new System.Windows.Forms.Padding(4);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(237, 20);
            this.projectName.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.defaultsButton);
            this.tabPage2.Controls.Add(this.textButton);
            this.tabPage2.Controls.Add(this.asmButton);
            this.tabPage2.Controls.Add(this.spritesButton);
            this.tabPage2.Controls.Add(this.blocksButton);
            this.tabPage2.Controls.Add(this.palettesButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(255, 85);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // defaultsButton
            // 
            this.defaultsButton.Enabled = false;
            this.defaultsButton.Location = new System.Drawing.Point(173, 37);
            this.defaultsButton.Margin = new System.Windows.Forms.Padding(4);
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.Size = new System.Drawing.Size(75, 23);
            this.defaultsButton.TabIndex = 5;
            this.defaultsButton.Text = "Defaults";
            this.defaultsButton.UseVisualStyleBackColor = true;
            // 
            // textButton
            // 
            this.textButton.Enabled = false;
            this.textButton.Location = new System.Drawing.Point(90, 37);
            this.textButton.Margin = new System.Windows.Forms.Padding(4);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(75, 23);
            this.textButton.TabIndex = 4;
            this.textButton.Text = "Text";
            this.textButton.UseVisualStyleBackColor = true;
            this.textButton.Click += new System.EventHandler(this.textButton_Click);
            // 
            // asmButton
            // 
            this.asmButton.Enabled = false;
            this.asmButton.Location = new System.Drawing.Point(7, 37);
            this.asmButton.Margin = new System.Windows.Forms.Padding(4);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(75, 23);
            this.asmButton.TabIndex = 3;
            this.asmButton.Tag = "";
            this.asmButton.Text = "ASM";
            this.asmButton.UseVisualStyleBackColor = true;
            this.asmButton.Click += new System.EventHandler(this.asmButton_Click);
            // 
            // spritesButton
            // 
            this.spritesButton.Enabled = false;
            this.spritesButton.Location = new System.Drawing.Point(173, 7);
            this.spritesButton.Margin = new System.Windows.Forms.Padding(4);
            this.spritesButton.Name = "spritesButton";
            this.spritesButton.Size = new System.Drawing.Size(75, 23);
            this.spritesButton.TabIndex = 2;
            this.spritesButton.Text = "Sprites";
            this.spritesButton.UseVisualStyleBackColor = true;
            this.spritesButton.Click += new System.EventHandler(this.spritesButton_Click);
            // 
            // blocksButton
            // 
            this.blocksButton.Enabled = false;
            this.blocksButton.Location = new System.Drawing.Point(90, 7);
            this.blocksButton.Margin = new System.Windows.Forms.Padding(4);
            this.blocksButton.Name = "blocksButton";
            this.blocksButton.Size = new System.Drawing.Size(75, 23);
            this.blocksButton.TabIndex = 1;
            this.blocksButton.Text = "Blocks";
            this.blocksButton.UseVisualStyleBackColor = true;
            this.blocksButton.Click += new System.EventHandler(this.blocksButton_Click);
            // 
            // palettesButton
            // 
            this.palettesButton.Enabled = false;
            this.palettesButton.Location = new System.Drawing.Point(7, 7);
            this.palettesButton.Margin = new System.Windows.Forms.Padding(4);
            this.palettesButton.Name = "palettesButton";
            this.palettesButton.Size = new System.Drawing.Size(75, 23);
            this.palettesButton.TabIndex = 0;
            this.palettesButton.Text = "Palettes";
            this.palettesButton.UseVisualStyleBackColor = true;
            this.palettesButton.Click += new System.EventHandler(this.button2_Click);
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
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectTree);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(263, 368);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.levelContext.ResumeLayout(false);
            this.worldContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button defaultsButton;
        private System.Windows.Forms.Button textButton;
        private System.Windows.Forms.Button asmButton;
        private System.Windows.Forms.Button spritesButton;
        private System.Windows.Forms.Button blocksButton;
        private System.Windows.Forms.Button palettesButton;
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
    }
}
