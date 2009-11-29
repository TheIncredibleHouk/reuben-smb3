namespace Daiz.NES.Reuben
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.currentLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.map16EditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.compileROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpRawLevelToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PnlRightSide = new System.Windows.Forms.Panel();
            this.PrvProject = new Daiz.NES.Reuben.ProjectView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnShowHide = new System.Windows.Forms.Button();
            this.rOMWithGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMWoGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.PnlRightSide.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.editorToolStripMenuItem,
            this.MnuWindows,
            this.projectToolStripMenuItem2,
            this.debugToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.MdiWindowListItem = this.MnuWindows;
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(795, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.MnuNewLevel});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.projectToolStripMenuItem.Text = "Project";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // MnuNewLevel
            // 
            this.MnuNewLevel.Name = "MnuNewLevel";
            this.MnuNewLevel.Size = new System.Drawing.Size(111, 22);
            this.MnuNewLevel.Text = "Level";
            this.MnuNewLevel.Click += new System.EventHandler(this.MnuNewLevel_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem1});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // projectToolStripMenuItem1
            // 
            this.projectToolStripMenuItem1.Name = "projectToolStripMenuItem1";
            this.projectToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.projectToolStripMenuItem1.Text = "Project";
            this.projectToolStripMenuItem1.Click += new System.EventHandler(this.projectToolStripMenuItem1_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphicsToolStripMenuItem1,
            this.currentLevelToolStripMenuItem});
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            // 
            // graphicsToolStripMenuItem1
            // 
            this.graphicsToolStripMenuItem1.Name = "graphicsToolStripMenuItem1";
            this.graphicsToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.graphicsToolStripMenuItem1.Text = "Graphics";
            this.graphicsToolStripMenuItem1.Click += new System.EventHandler(this.graphicsToolStripMenuItem_Click_1);
            // 
            // currentLevelToolStripMenuItem
            // 
            this.currentLevelToolStripMenuItem.Name = "currentLevelToolStripMenuItem";
            this.currentLevelToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.currentLevelToolStripMenuItem.Text = "Current Level";
            this.currentLevelToolStripMenuItem.Click += new System.EventHandler(this.currentLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphicsToolStripMenuItem,
            this.dToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // graphicsToolStripMenuItem
            // 
            this.graphicsToolStripMenuItem.Name = "graphicsToolStripMenuItem";
            this.graphicsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.graphicsToolStripMenuItem.Text = "Graphics";
            this.graphicsToolStripMenuItem.Click += new System.EventHandler(this.graphicsToolStripMenuItem_Click_1);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dToolStripMenuItem.Text = "Existing Level";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelToPNGToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // levelToPNGToolStripMenuItem
            // 
            this.levelToPNGToolStripMenuItem.Name = "levelToPNGToolStripMenuItem";
            this.levelToPNGToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.levelToPNGToolStripMenuItem.Text = "Level to PNG";
            this.levelToPNGToolStripMenuItem.Click += new System.EventHandler(this.levelToPNGToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(107, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paletteManagerToolStripMenuItem,
            this.graphicsEditorToolStripMenuItem,
            this.map16EditorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // paletteManagerToolStripMenuItem
            // 
            this.paletteManagerToolStripMenuItem.Name = "paletteManagerToolStripMenuItem";
            this.paletteManagerToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.paletteManagerToolStripMenuItem.Text = "Palette Editor";
            this.paletteManagerToolStripMenuItem.Click += new System.EventHandler(this.paletteManagerToolStripMenuItem_Click);
            // 
            // graphicsEditorToolStripMenuItem
            // 
            this.graphicsEditorToolStripMenuItem.Name = "graphicsEditorToolStripMenuItem";
            this.graphicsEditorToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.graphicsEditorToolStripMenuItem.Text = "Graphics Editor";
            this.graphicsEditorToolStripMenuItem.Click += new System.EventHandler(this.graphicsEditorToolStripMenuItem_Click);
            // 
            // map16EditorToolStripMenuItem
            // 
            this.map16EditorToolStripMenuItem.Name = "map16EditorToolStripMenuItem";
            this.map16EditorToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.map16EditorToolStripMenuItem.Text = "TSA Editor";
            this.map16EditorToolStripMenuItem.Click += new System.EventHandler(this.map16EditorToolStripMenuItem_Click);
            // 
            // editorToolStripMenuItem
            // 
            this.editorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutManagerToolStripMenuItem});
            this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            this.editorToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.editorToolStripMenuItem.Text = "Editor";
            // 
            // layoutManagerToolStripMenuItem
            // 
            this.layoutManagerToolStripMenuItem.Name = "layoutManagerToolStripMenuItem";
            this.layoutManagerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.layoutManagerToolStripMenuItem.Text = "Layout Editor";
            this.layoutManagerToolStripMenuItem.Click += new System.EventHandler(this.layoutManagerToolStripMenuItem_Click);
            // 
            // MnuWindows
            // 
            this.MnuWindows.Name = "MnuWindows";
            this.MnuWindows.Size = new System.Drawing.Size(68, 20);
            this.MnuWindows.Text = "Windows";
            // 
            // projectToolStripMenuItem2
            // 
            this.projectToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultToolStripMenuItem,
            this.toolStripSeparator4,
            this.compileROMToolStripMenuItem});
            this.projectToolStripMenuItem2.Name = "projectToolStripMenuItem2";
            this.projectToolStripMenuItem2.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem2.Text = "Project";
            // 
            // setDefaultToolStripMenuItem
            // 
            this.setDefaultToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultsToolStripMenuItem});
            this.setDefaultToolStripMenuItem.Name = "setDefaultToolStripMenuItem";
            this.setDefaultToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setDefaultToolStripMenuItem.Text = "Set Default";
            // 
            // defaultsToolStripMenuItem
            // 
            this.defaultsToolStripMenuItem.Name = "defaultsToolStripMenuItem";
            this.defaultsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.defaultsToolStripMenuItem.Text = "Sprite Definitions";
            this.defaultsToolStripMenuItem.Click += new System.EventHandler(this.defaultsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // compileROMToolStripMenuItem
            // 
            this.compileROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rOMWithGraphicsToolStripMenuItem,
            this.rOMWoGraphicsToolStripMenuItem});
            this.compileROMToolStripMenuItem.Name = "compileROMToolStripMenuItem";
            this.compileROMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compileROMToolStripMenuItem.Text = "Compile";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpRawLevelToFileToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // dumpRawLevelToFileToolStripMenuItem
            // 
            this.dumpRawLevelToFileToolStripMenuItem.Name = "dumpRawLevelToFileToolStripMenuItem";
            this.dumpRawLevelToFileToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.dumpRawLevelToFileToolStripMenuItem.Text = "Dump Raw Level To File";
            this.dumpRawLevelToFileToolStripMenuItem.Click += new System.EventHandler(this.dumpRawLevelToFileToolStripMenuItem_Click);
            // 
            // PnlRightSide
            // 
            this.PnlRightSide.Controls.Add(this.PrvProject);
            this.PnlRightSide.Controls.Add(this.panel2);
            this.PnlRightSide.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlRightSide.Location = new System.Drawing.Point(543, 24);
            this.PnlRightSide.Name = "PnlRightSide";
            this.PnlRightSide.Size = new System.Drawing.Size(252, 432);
            this.PnlRightSide.TabIndex = 3;
            // 
            // PrvProject
            // 
            this.PrvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrvProject.Location = new System.Drawing.Point(0, 31);
            this.PrvProject.Name = "PrvProject";
            this.PrvProject.Size = new System.Drawing.Size(252, 401);
            this.PrvProject.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnShowHide);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 31);
            this.panel2.TabIndex = 2;
            // 
            // BtnShowHide
            // 
            this.BtnShowHide.Location = new System.Drawing.Point(3, 3);
            this.BtnShowHide.Name = "BtnShowHide";
            this.BtnShowHide.Size = new System.Drawing.Size(29, 23);
            this.BtnShowHide.TabIndex = 0;
            this.BtnShowHide.Text = ">>";
            this.BtnShowHide.UseVisualStyleBackColor = true;
            this.BtnShowHide.Click += new System.EventHandler(this.BtnShowHide_Click);
            // 
            // rOMWithGraphicsToolStripMenuItem
            // 
            this.rOMWithGraphicsToolStripMenuItem.Name = "rOMWithGraphicsToolStripMenuItem";
            this.rOMWithGraphicsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.rOMWithGraphicsToolStripMenuItem.Text = "ROM w/ Graphics";
            this.rOMWithGraphicsToolStripMenuItem.Click += new System.EventHandler(this.compileROMToolStripMenuItem_Click);
            // 
            // rOMWoGraphicsToolStripMenuItem
            // 
            this.rOMWoGraphicsToolStripMenuItem.Name = "rOMWoGraphicsToolStripMenuItem";
            this.rOMWoGraphicsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.rOMWoGraphicsToolStripMenuItem.Text = "ROM w/o Graphics";
            this.rOMWoGraphicsToolStripMenuItem.Click += new System.EventHandler(this.rOMWoGraphicsToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 456);
            this.Controls.Add(this.PnlRightSide);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Main";
            this.Text = "Main";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.PnlRightSide.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MnuNewLevel;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paletteManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem map16EditorToolStripMenuItem;
        private System.Windows.Forms.Panel PnlRightSide;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnShowHide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuWindows;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem setDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem compileROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpRawLevelToFileToolStripMenuItem;
        private ProjectView PrvProject;
        private System.Windows.Forms.ToolStripMenuItem rOMWithGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOMWoGraphicsToolStripMenuItem;


    }
}