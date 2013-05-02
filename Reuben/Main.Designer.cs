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
            this.projectToolStripMenuFromValue = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReload = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsToolStripMenuFromValue = new System.Windows.Forms.ToolStripMenuItem();
            this.currentLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuToValue = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMWithGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blockPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFromValue = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.allEditorDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPaletteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.map16EditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpRawLevelToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PnlRightSide = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnShowHide = new System.Windows.Forms.Button();
            this.PrvProject = new Daiz.NES.Reuben.ProjectView();
            this.MainMenu.SuspendLayout();
            this.PnlRightSide.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.MnuTools,
            this.MnuEditor,
            this.MnuWindows,
            this.MnuDebug});
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
            this.MnuReload,
            this.compileROMToolStripMenuItem,
            this.toolStripSeparator1,
            this.MnuImport,
            this.MnuExport,
            this.toolStripSeparator2,
            this.MnuProject,
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.MnuNewLevel.Enabled = false;
            this.MnuNewLevel.Name = "MnuNewLevel";
            this.MnuNewLevel.Size = new System.Drawing.Size(111, 22);
            this.MnuNewLevel.Text = "Level";
            this.MnuNewLevel.Click += new System.EventHandler(this.MnuNewLevel_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuFromValue});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // projectToolStripMenuFromValue
            // 
            this.projectToolStripMenuFromValue.Name = "projectToolStripMenuFromValue";
            this.projectToolStripMenuFromValue.Size = new System.Drawing.Size(111, 22);
            this.projectToolStripMenuFromValue.Text = "Project";
            this.projectToolStripMenuFromValue.Click += new System.EventHandler(this.projectToolStripMenuFromValue_Click);
            // 
            // MnuReload
            // 
            this.MnuReload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphicsToolStripMenuFromValue,
            this.currentLevelToolStripMenuItem});
            this.MnuReload.Enabled = false;
            this.MnuReload.Name = "MnuReload";
            this.MnuReload.Size = new System.Drawing.Size(152, 22);
            this.MnuReload.Text = "Reload";
            // 
            // graphicsToolStripMenuFromValue
            // 
            this.graphicsToolStripMenuFromValue.Name = "graphicsToolStripMenuFromValue";
            this.graphicsToolStripMenuFromValue.Size = new System.Drawing.Size(144, 22);
            this.graphicsToolStripMenuFromValue.Text = "Graphics";
            this.graphicsToolStripMenuFromValue.Click += new System.EventHandler(this.graphicsToolStripMenuFromValue_Click);
            // 
            // currentLevelToolStripMenuItem
            // 
            this.currentLevelToolStripMenuItem.Name = "currentLevelToolStripMenuItem";
            this.currentLevelToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.currentLevelToolStripMenuItem.Text = "Current Level";
            this.currentLevelToolStripMenuItem.Click += new System.EventHandler(this.currentLevelToolStripMenuItem_Click);
            // 
            // compileROMToolStripMenuItem
            // 
            this.compileROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuToValue,
            this.rOMWithGraphicsToolStripMenuItem});
            this.compileROMToolStripMenuItem.Name = "compileROMToolStripMenuItem";
            this.compileROMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compileROMToolStripMenuItem.Text = "Compile";
            // 
            // toolStripMenuToValue
            // 
            this.toolStripMenuToValue.Name = "toolStripMenuToValue";
            this.toolStripMenuToValue.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuToValue.Text = "Save to ROM";
            this.toolStripMenuToValue.Click += new System.EventHandler(this.toolStripMenuToValue_Click);
            // 
            // rOMWithGraphicsToolStripMenuItem
            // 
            this.rOMWithGraphicsToolStripMenuItem.Name = "rOMWithGraphicsToolStripMenuItem";
            this.rOMWithGraphicsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rOMWithGraphicsToolStripMenuItem.Text = "Save As...";
            this.rOMWithGraphicsToolStripMenuItem.Click += new System.EventHandler(this.compileROMToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuImport
            // 
            this.MnuImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphicsToolStripMenuItem,
            this.dToolStripMenuItem});
            this.MnuImport.Enabled = false;
            this.MnuImport.Name = "MnuImport";
            this.MnuImport.Size = new System.Drawing.Size(152, 22);
            this.MnuImport.Text = "Import";
            // 
            // graphicsToolStripMenuItem
            // 
            this.graphicsToolStripMenuItem.Name = "graphicsToolStripMenuItem";
            this.graphicsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.graphicsToolStripMenuItem.Text = "Graphics";
            this.graphicsToolStripMenuItem.Click += new System.EventHandler(this.graphicsToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dToolStripMenuItem.Text = "Existing Level";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // MnuExport
            // 
            this.MnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelToPNGToolStripMenuItem});
            this.MnuExport.Enabled = false;
            this.MnuExport.Name = "MnuExport";
            this.MnuExport.Size = new System.Drawing.Size(152, 22);
            this.MnuExport.Text = "Export";
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
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // MnuProject
            // 
            this.MnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultToolStripMenuItem,
            this.setPaletteFileToolStripMenuItem,
            this.toolStripSeparator4});
            this.MnuProject.Enabled = false;
            this.MnuProject.Name = "MnuProject";
            this.MnuProject.Size = new System.Drawing.Size(152, 22);
            this.MnuProject.Text = "Defaults";
            // 
            // setDefaultToolStripMenuItem
            // 
            this.setDefaultToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultsToolStripMenuItem,
            this.blockPropertiesToolStripMenuItem,
            this.specialGraphicsToolStripMenuItem,
            this.toolStripMenuFromValue,
            this.paletteFileToolStripMenuItem,
            this.toolStripSeparator3,
            this.allEditorDefinitionsToolStripMenuItem});
            this.setDefaultToolStripMenuItem.Name = "setDefaultToolStripMenuItem";
            this.setDefaultToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setDefaultToolStripMenuItem.Text = "Set Default";
            // 
            // defaultsToolStripMenuItem
            // 
            this.defaultsToolStripMenuItem.Name = "defaultsToolStripMenuItem";
            this.defaultsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.defaultsToolStripMenuItem.Text = "Sprite Definitions";
            // 
            // blockPropertiesToolStripMenuItem
            // 
            this.blockPropertiesToolStripMenuItem.Name = "blockPropertiesToolStripMenuItem";
            this.blockPropertiesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.blockPropertiesToolStripMenuItem.Text = "Block Properties";
            // 
            // specialGraphicsToolStripMenuItem
            // 
            this.specialGraphicsToolStripMenuItem.Name = "specialGraphicsToolStripMenuItem";
            this.specialGraphicsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.specialGraphicsToolStripMenuItem.Text = "Special Graphics";
            // 
            // toolStripMenuFromValue
            // 
            this.toolStripMenuFromValue.Name = "toolStripMenuFromValue";
            this.toolStripMenuFromValue.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuFromValue.Text = "Music List";
            // 
            // paletteFileToolStripMenuItem
            // 
            this.paletteFileToolStripMenuItem.Name = "paletteFileToolStripMenuItem";
            this.paletteFileToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.paletteFileToolStripMenuItem.Text = "Palette File";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // allEditorDefinitionsToolStripMenuItem
            // 
            this.allEditorDefinitionsToolStripMenuItem.Name = "allEditorDefinitionsToolStripMenuItem";
            this.allEditorDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.allEditorDefinitionsToolStripMenuItem.Text = "All Editor Definitions";
            // 
            // setPaletteFileToolStripMenuItem
            // 
            this.setPaletteFileToolStripMenuItem.Name = "setPaletteFileToolStripMenuItem";
            this.setPaletteFileToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setPaletteFileToolStripMenuItem.Text = "Set Palette File";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(147, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MnuTools
            // 
            this.MnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paletteManagerToolStripMenuItem,
            this.graphicsEditorToolStripMenuItem,
            this.map16EditorToolStripMenuItem});
            this.MnuTools.Enabled = false;
            this.MnuTools.Name = "MnuTools";
            this.MnuTools.Size = new System.Drawing.Size(48, 20);
            this.MnuTools.Text = "Tools";
            // 
            // paletteManagerToolStripMenuItem
            // 
            this.paletteManagerToolStripMenuItem.Name = "paletteManagerToolStripMenuItem";
            this.paletteManagerToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.paletteManagerToolStripMenuItem.Text = "Palette Editor";
            this.paletteManagerToolStripMenuItem.Click += new System.EventHandler(this.paletteManagerToolStripMenuItem_Click);
            // 
            // graphicsEditorToolStripMenuItem
            // 
            this.graphicsEditorToolStripMenuItem.Name = "graphicsEditorToolStripMenuItem";
            this.graphicsEditorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.graphicsEditorToolStripMenuItem.Text = "Graphics Editor";
            this.graphicsEditorToolStripMenuItem.Click += new System.EventHandler(this.graphicsEditorToolStripMenuItem_Click);
            // 
            // map16EditorToolStripMenuItem
            // 
            this.map16EditorToolStripMenuItem.Name = "map16EditorToolStripMenuItem";
            this.map16EditorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.map16EditorToolStripMenuItem.Text = "Block Definitions Editor";
            this.map16EditorToolStripMenuItem.Click += new System.EventHandler(this.map16EditorToolStripMenuItem_Click);
            // 
            // MnuEditor
            // 
            this.MnuEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutManagerToolStripMenuItem});
            this.MnuEditor.Enabled = false;
            this.MnuEditor.Name = "MnuEditor";
            this.MnuEditor.Size = new System.Drawing.Size(50, 20);
            this.MnuEditor.Text = "Editor";
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
            this.MnuWindows.Enabled = false;
            this.MnuWindows.Name = "MnuWindows";
            this.MnuWindows.Size = new System.Drawing.Size(68, 20);
            this.MnuWindows.Text = "Windows";
            // 
            // MnuDebug
            // 
            this.MnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpRawLevelToFileToolStripMenuItem});
            this.MnuDebug.Enabled = false;
            this.MnuDebug.Name = "MnuDebug";
            this.MnuDebug.Size = new System.Drawing.Size(54, 20);
            this.MnuDebug.Text = "Debug";
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
            this.PnlRightSide.Location = new System.Drawing.Point(470, 24);
            this.PnlRightSide.Name = "PnlRightSide";
            this.PnlRightSide.Size = new System.Drawing.Size(325, 432);
            this.PnlRightSide.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnShowHide);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 31);
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
            // PrvProject
            // 
            this.PrvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrvProject.Location = new System.Drawing.Point(0, 31);
            this.PrvProject.Name = "PrvProject";
            this.PrvProject.Size = new System.Drawing.Size(325, 401);
            this.PrvProject.TabIndex = 3;
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
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuFromValue;
        private System.Windows.Forms.ToolStripMenuItem MnuNewLevel;
        private System.Windows.Forms.ToolStripMenuItem MnuTools;
        private System.Windows.Forms.ToolStripMenuItem paletteManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem map16EditorToolStripMenuItem;
        private System.Windows.Forms.Panel PnlRightSide;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnShowHide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MnuImport;
        private System.Windows.Forms.ToolStripMenuItem graphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuEditor;
        private System.Windows.Forms.ToolStripMenuItem layoutManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuWindows;
        private System.Windows.Forms.ToolStripMenuItem MnuExport;
        private System.Windows.Forms.ToolStripMenuItem levelToPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuReload;
        private System.Windows.Forms.ToolStripMenuItem currentLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsToolStripMenuFromValue;
        private System.Windows.Forms.ToolStripMenuItem MnuDebug;
        private System.Windows.Forms.ToolStripMenuItem dumpRawLevelToFileToolStripMenuItem;
        private ProjectView PrvProject;
        private System.Windows.Forms.ToolStripMenuItem compileROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuToValue;
        private System.Windows.Forms.ToolStripMenuItem rOMWithGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuProject;
        private System.Windows.Forms.ToolStripMenuItem setDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blockPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFromValue;
        private System.Windows.Forms.ToolStripMenuItem paletteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem allEditorDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPaletteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;


    }
}