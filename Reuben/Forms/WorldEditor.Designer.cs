namespace Daiz.NES.Reuben
{
    partial class WorldEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldEditor));
            this.PnlInfo = new System.Windows.Forms.Panel();
            this.TabLevelInfo = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.CmbMusic = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CmbLength = new System.Windows.Forms.ComboBox();
            this.tabPageEX1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbGraphics = new System.Windows.Forms.ComboBox();
            this.CmbPalettes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LblHexGraphics = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LblSprite = new System.Windows.Forms.Label();
            this.LblPositition = new System.Windows.Forms.Label();
            this.LblRightClickMode = new System.Windows.Forms.Label();
            this.PnlDrawing = new System.Windows.Forms.Panel();
            this.TabEditSelector = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BlvRight = new Daiz.NES.Reuben.BlockViewer();
            this.LblSelectorHover = new System.Windows.Forms.Label();
            this.BlvLeft = new Daiz.NES.Reuben.BlockViewer();
            this.BlsSelector = new Daiz.NES.Reuben.BlockSelector();
            this.CmbLayouts = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LblSpriteSelected = new System.Windows.Forms.Label();
            this.TabClass1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BtnDeletePointer = new System.Windows.Forms.Button();
            this.BtnAddPointer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PntEditor = new Daiz.NES.Reuben.WorldPointerEditor();
            this.TlsDrawing = new System.Windows.Forms.ToolStrip();
            this.TsbPencil = new System.Windows.Forms.ToolStripButton();
            this.TsbLine = new System.Windows.Forms.ToolStripButton();
            this.TsbRectangle = new System.Windows.Forms.ToolStripButton();
            this.TsbOutline = new System.Windows.Forms.ToolStripButton();
            this.TsbBucket = new System.Windows.Forms.ToolStripButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TlsTileCommands = new System.Windows.Forms.ToolStrip();
            this.TsbCut = new System.Windows.Forms.ToolStripButton();
            this.TsbCopy = new System.Windows.Forms.ToolStripButton();
            this.TsbPaste = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbGrid = new System.Windows.Forms.ToolStripButton();
            this.TsbPointers = new System.Windows.Forms.ToolStripButton();
            this.LevelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PnlView = new Daiz.NES.Reuben.FixedPanel();
            this.PnlLengthControl = new System.Windows.Forms.Panel();
            this.WldView = new Daiz.NES.Reuben.WorldViewer();
            this.PnlInfo.SuspendLayout();
            this.TabLevelInfo.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPageEX1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.PnlDrawing.SuspendLayout();
            this.TabEditSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TlsDrawing.SuspendLayout();
            this.panel2.SuspendLayout();
            this.TlsTileCommands.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.PnlView.SuspendLayout();
            this.PnlLengthControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlInfo
            // 
            this.PnlInfo.AutoScroll = true;
            this.PnlInfo.Controls.Add(this.TabLevelInfo);
            this.PnlInfo.Controls.Add(this.panel4);
            this.PnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlInfo.Location = new System.Drawing.Point(281, 373);
            this.PnlInfo.Name = "PnlInfo";
            this.PnlInfo.Size = new System.Drawing.Size(495, 122);
            this.PnlInfo.TabIndex = 1;
            // 
            // TabLevelInfo
            // 
            this.TabLevelInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabLevelInfo.Controls.Add(this.tabPage4);
            this.TabLevelInfo.Controls.Add(this.tabPageEX1);
            this.TabLevelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabLevelInfo.Location = new System.Drawing.Point(0, 29);
            this.TabLevelInfo.Name = "TabLevelInfo";
            this.TabLevelInfo.SelectedIndex = 1;
            this.TabLevelInfo.Size = new System.Drawing.Size(495, 93);
            this.TabLevelInfo.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabLevelInfo.TabIndex = 20;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage4.Controls.Add(this.CmbMusic);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.CmbLength);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(487, 67);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Level";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // CmbMusic
            // 
            this.CmbMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMusic.DropDownWidth = 150;
            this.CmbMusic.FormattingEnabled = true;
            this.CmbMusic.Location = new System.Drawing.Point(6, 22);
            this.CmbMusic.Name = "CmbMusic";
            this.CmbMusic.Size = new System.Drawing.Size(129, 21);
            this.CmbMusic.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Music";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Length";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CmbLength
            // 
            this.CmbLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLength.FormattingEnabled = true;
            this.CmbLength.Location = new System.Drawing.Point(141, 22);
            this.CmbLength.Name = "CmbLength";
            this.CmbLength.Size = new System.Drawing.Size(58, 21);
            this.CmbLength.TabIndex = 21;
            this.CmbLength.SelectedIndexChanged += new System.EventHandler(this.CmbLength_SelectedIndexChanged);
            // 
            // tabPageEX1
            // 
            this.tabPageEX1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPageEX1.Controls.Add(this.label1);
            this.tabPageEX1.Controls.Add(this.CmbGraphics);
            this.tabPageEX1.Controls.Add(this.CmbPalettes);
            this.tabPageEX1.Controls.Add(this.label5);
            this.tabPageEX1.Controls.Add(this.LblHexGraphics);
            this.tabPageEX1.Location = new System.Drawing.Point(4, 4);
            this.tabPageEX1.Name = "tabPageEX1";
            this.tabPageEX1.Size = new System.Drawing.Size(487, 67);
            this.tabPageEX1.TabIndex = 4;
            this.tabPageEX1.Text = "Graphics";
            this.tabPageEX1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Graphics";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CmbGraphics
            // 
            this.CmbGraphics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics.DropDownWidth = 200;
            this.CmbGraphics.FormattingEnabled = true;
            this.CmbGraphics.Location = new System.Drawing.Point(11, 19);
            this.CmbGraphics.Name = "CmbGraphics";
            this.CmbGraphics.Size = new System.Drawing.Size(129, 21);
            this.CmbGraphics.TabIndex = 24;
            this.CmbGraphics.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics_SelectedIndexChanged);
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(149, 19);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(129, 21);
            this.CmbPalettes.TabIndex = 28;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Palette";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblHexGraphics
            // 
            this.LblHexGraphics.AutoSize = true;
            this.LblHexGraphics.Location = new System.Drawing.Point(99, 3);
            this.LblHexGraphics.Name = "LblHexGraphics";
            this.LblHexGraphics.Size = new System.Drawing.Size(41, 13);
            this.LblHexGraphics.TabIndex = 31;
            this.LblHexGraphics.Text = "label10";
            this.LblHexGraphics.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.LblSprite);
            this.panel4.Controls.Add(this.LblPositition);
            this.panel4.Controls.Add(this.LblRightClickMode);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(495, 29);
            this.panel4.TabIndex = 23;
            // 
            // LblSprite
            // 
            this.LblSprite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblSprite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSprite.Location = new System.Drawing.Point(217, 0);
            this.LblSprite.Name = "LblSprite";
            this.LblSprite.Size = new System.Drawing.Size(278, 29);
            this.LblSprite.TabIndex = 21;
            this.LblSprite.Text = "Sprite";
            this.LblSprite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPositition
            // 
            this.LblPositition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblPositition.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblPositition.Location = new System.Drawing.Point(148, 0);
            this.LblPositition.Margin = new System.Windows.Forms.Padding(3);
            this.LblPositition.Name = "LblPositition";
            this.LblPositition.Size = new System.Drawing.Size(69, 29);
            this.LblPositition.TabIndex = 24;
            this.LblPositition.Text = "X: 00 Y: 00";
            this.LblPositition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblRightClickMode
            // 
            this.LblRightClickMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblRightClickMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblRightClickMode.Location = new System.Drawing.Point(0, 0);
            this.LblRightClickMode.Margin = new System.Windows.Forms.Padding(3);
            this.LblRightClickMode.Name = "LblRightClickMode";
            this.LblRightClickMode.Size = new System.Drawing.Size(148, 29);
            this.LblRightClickMode.TabIndex = 23;
            this.LblRightClickMode.Text = "Right Click Mode: Selector";
            this.LblRightClickMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LevelToolTip.SetToolTip(this.LblRightClickMode, "Click to change modes.");
            this.LblRightClickMode.Click += new System.EventHandler(this.LblRightClickMode_Click);
            // 
            // PnlDrawing
            // 
            this.PnlDrawing.AutoScroll = true;
            this.PnlDrawing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlDrawing.Controls.Add(this.TabEditSelector);
            this.PnlDrawing.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlDrawing.Location = new System.Drawing.Point(0, 0);
            this.PnlDrawing.Name = "PnlDrawing";
            this.PnlDrawing.Size = new System.Drawing.Size(281, 495);
            this.PnlDrawing.TabIndex = 21;
            // 
            // TabEditSelector
            // 
            this.TabEditSelector.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabEditSelector.Controls.Add(this.tabPage1);
            this.TabEditSelector.Controls.Add(this.tabPage2);
            this.TabEditSelector.Controls.Add(this.tabPage3);
            this.TabEditSelector.Location = new System.Drawing.Point(0, 8);
            this.TabEditSelector.Name = "TabEditSelector";
            this.TabEditSelector.SelectedIndex = 2;
            this.TabEditSelector.Size = new System.Drawing.Size(277, 361);
            this.TabEditSelector.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabEditSelector.TabIndex = 17;
            this.TabEditSelector.SelectedIndexChanged += new System.EventHandler(this.TabEditSelector_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.BlvRight);
            this.tabPage1.Controls.Add(this.LblSelectorHover);
            this.tabPage1.Controls.Add(this.BlvLeft);
            this.tabPage1.Controls.Add(this.BlsSelector);
            this.tabPage1.Controls.Add(this.CmbLayouts);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(269, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tiles";
            this.tabPage1.ToolTipText = "Shortcut Q";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabPage1_MouseMove);
            // 
            // BlvRight
            // 
            this.BlvRight.CurrentBlock = null;
            this.BlvRight.Location = new System.Drawing.Point(229, 293);
            this.BlvRight.Name = "BlvRight";
            this.BlvRight.Size = new System.Drawing.Size(32, 32);
            this.BlvRight.TabIndex = 21;
            this.BlvRight.Text = "blockViewer1";
            // 
            // LblSelectorHover
            // 
            this.LblSelectorHover.AutoSize = true;
            this.LblSelectorHover.Location = new System.Drawing.Point(11, 296);
            this.LblSelectorHover.Name = "LblSelectorHover";
            this.LblSelectorHover.Size = new System.Drawing.Size(37, 13);
            this.LblSelectorHover.TabIndex = 20;
            this.LblSelectorHover.Text = "Block:";
            // 
            // BlvLeft
            // 
            this.BlvLeft.CurrentBlock = null;
            this.BlvLeft.Location = new System.Drawing.Point(191, 293);
            this.BlvLeft.Name = "BlvLeft";
            this.BlvLeft.Size = new System.Drawing.Size(32, 32);
            this.BlvLeft.TabIndex = 19;
            this.BlvLeft.Text = "blockViewer1";
            // 
            // BlsSelector
            // 
            this.BlsSelector.BlockLayout = null;
            this.BlsSelector.CurrentDefiniton = null;
            this.BlsSelector.HaltRendering = false;
            this.BlsSelector.Location = new System.Drawing.Point(6, 31);
            this.BlsSelector.Margin = new System.Windows.Forms.Padding(0);
            this.BlsSelector.Name = "BlsSelector";
            this.BlsSelector.SelectedIndex = 0;
            this.BlsSelector.SelectedTileIndex = 0;
            this.BlsSelector.ShowBlockSolidity = false;
            this.BlsSelector.ShowSpecialBlocks = false;
            this.BlsSelector.ShowTileInteractions = false;
            this.BlsSelector.Size = new System.Drawing.Size(256, 256);
            this.BlsSelector.SpecialDefnitions = null;
            this.BlsSelector.SpecialTable = null;
            this.BlsSelector.TabIndex = 0;
            this.BlsSelector.Text = "blockSelector1";
            this.BlsSelector.DoubleClick += new System.EventHandler(this.BlsSelector_DoubleClick);
            this.BlsSelector.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsSelector_MouseMove);
            // 
            // CmbLayouts
            // 
            this.CmbLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLayouts.FormattingEnabled = true;
            this.CmbLayouts.Location = new System.Drawing.Point(6, 5);
            this.CmbLayouts.Name = "CmbLayouts";
            this.CmbLayouts.Size = new System.Drawing.Size(256, 21);
            this.CmbLayouts.TabIndex = 16;
            this.CmbLayouts.SelectedIndexChanged += new System.EventHandler(this.CmbLayouts_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage2.Controls.Add(this.LblSpriteSelected);
            this.tabPage2.Controls.Add(this.TabClass1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(269, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sprites";
            this.tabPage2.ToolTipText = "Shortcut W";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabPage3_MouseMove);
            // 
            // LblSpriteSelected
            // 
            this.LblSpriteSelected.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblSpriteSelected.Location = new System.Drawing.Point(3, 308);
            this.LblSpriteSelected.Margin = new System.Windows.Forms.Padding(0);
            this.LblSpriteSelected.Name = "LblSpriteSelected";
            this.LblSpriteSelected.Padding = new System.Windows.Forms.Padding(4);
            this.LblSpriteSelected.Size = new System.Drawing.Size(262, 22);
            this.LblSpriteSelected.TabIndex = 3;
            this.LblSpriteSelected.Text = "None";
            // 
            // TabClass1
            // 
            this.TabClass1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabClass1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabClass1.Location = new System.Drawing.Point(2, 203);
            this.TabClass1.Margin = new System.Windows.Forms.Padding(0);
            this.TabClass1.Name = "TabClass1";
            this.TabClass1.Padding = new System.Drawing.Point(0, 0);
            this.TabClass1.SelectedIndex = 0;
            this.TabClass1.Size = new System.Drawing.Size(265, 100);
            this.TabClass1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage3.Controls.Add(this.BtnDeletePointer);
            this.tabPage3.Controls.Add(this.BtnAddPointer);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(269, 335);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pointers";
            this.tabPage3.ToolTipText = "Shortcut E";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabPage3_MouseMove);
            // 
            // BtnDeletePointer
            // 
            this.BtnDeletePointer.Location = new System.Drawing.Point(184, 304);
            this.BtnDeletePointer.Name = "BtnDeletePointer";
            this.BtnDeletePointer.Size = new System.Drawing.Size(75, 23);
            this.BtnDeletePointer.TabIndex = 3;
            this.BtnDeletePointer.Text = "Delete";
            this.BtnDeletePointer.UseVisualStyleBackColor = true;
            this.BtnDeletePointer.Click += new System.EventHandler(this.BtnDeletePointer_Click);
            // 
            // BtnAddPointer
            // 
            this.BtnAddPointer.Location = new System.Drawing.Point(103, 304);
            this.BtnAddPointer.Name = "BtnAddPointer";
            this.BtnAddPointer.Size = new System.Drawing.Size(75, 23);
            this.BtnAddPointer.TabIndex = 2;
            this.BtnAddPointer.Text = "Add";
            this.BtnAddPointer.UseVisualStyleBackColor = true;
            this.BtnAddPointer.Click += new System.EventHandler(this.BtnAddPointer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PntEditor);
            this.groupBox1.Location = new System.Drawing.Point(8, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 205);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pointer Info";
            // 
            // PntEditor
            // 
            this.PntEditor.CurrentPointer = null;
            this.PntEditor.Location = new System.Drawing.Point(6, 19);
            this.PntEditor.Name = "PntEditor";
            this.PntEditor.Padding = new System.Windows.Forms.Padding(4);
            this.PntEditor.Size = new System.Drawing.Size(239, 180);
            this.PntEditor.TabIndex = 0;
            // 
            // TlsDrawing
            // 
            this.TlsDrawing.Dock = System.Windows.Forms.DockStyle.Left;
            this.TlsDrawing.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TlsDrawing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbPencil,
            this.TsbLine,
            this.TsbRectangle,
            this.TsbOutline,
            this.TsbBucket});
            this.TlsDrawing.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.TlsDrawing.Location = new System.Drawing.Point(78, 0);
            this.TlsDrawing.Name = "TlsDrawing";
            this.TlsDrawing.Padding = new System.Windows.Forms.Padding(0);
            this.TlsDrawing.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TlsDrawing.Size = new System.Drawing.Size(117, 25);
            this.TlsDrawing.TabIndex = 15;
            this.TlsDrawing.Text = "toolStrip1";
            // 
            // TsbPencil
            // 
            this.TsbPencil.CheckOnClick = true;
            this.TsbPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbPencil.Image = ((System.Drawing.Image)(resources.GetObject("TsbPencil.Image")));
            this.TsbPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPencil.Name = "TsbPencil";
            this.TsbPencil.Size = new System.Drawing.Size(23, 22);
            this.TsbPencil.Text = "toolStripButton2";
            this.TsbPencil.ToolTipText = "Pencl";
            this.TsbPencil.Click += new System.EventHandler(this.TsbPencil_Click);
            // 
            // TsbLine
            // 
            this.TsbLine.CheckOnClick = true;
            this.TsbLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbLine.Image = ((System.Drawing.Image)(resources.GetObject("TsbLine.Image")));
            this.TsbLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbLine.Name = "TsbLine";
            this.TsbLine.Size = new System.Drawing.Size(23, 22);
            this.TsbLine.Text = "toolStripButton1";
            this.TsbLine.ToolTipText = "Diagonal Line";
            this.TsbLine.Click += new System.EventHandler(this.TsbLine_Click);
            // 
            // TsbRectangle
            // 
            this.TsbRectangle.CheckOnClick = true;
            this.TsbRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbRectangle.Image = ((System.Drawing.Image)(resources.GetObject("TsbRectangle.Image")));
            this.TsbRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbRectangle.Name = "TsbRectangle";
            this.TsbRectangle.Size = new System.Drawing.Size(23, 22);
            this.TsbRectangle.Text = "toolStripButton1";
            this.TsbRectangle.ToolTipText = "Rectangle Fill";
            this.TsbRectangle.Click += new System.EventHandler(this.TsbRectangle_Click);
            // 
            // TsbOutline
            // 
            this.TsbOutline.CheckOnClick = true;
            this.TsbOutline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbOutline.Image = ((System.Drawing.Image)(resources.GetObject("TsbOutline.Image")));
            this.TsbOutline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbOutline.Name = "TsbOutline";
            this.TsbOutline.Size = new System.Drawing.Size(23, 22);
            this.TsbOutline.Text = "toolStripButton1";
            this.TsbOutline.ToolTipText = "Rectangle Outline";
            this.TsbOutline.Click += new System.EventHandler(this.TsbOutline_Click);
            // 
            // TsbBucket
            // 
            this.TsbBucket.CheckOnClick = true;
            this.TsbBucket.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbBucket.Image = ((System.Drawing.Image)(resources.GetObject("TsbBucket.Image")));
            this.TsbBucket.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbBucket.Name = "TsbBucket";
            this.TsbBucket.Size = new System.Drawing.Size(23, 22);
            this.TsbBucket.Text = "toolStripButton1";
            this.TsbBucket.ToolTipText = "Flood Fill";
            this.TsbBucket.Click += new System.EventHandler(this.TsbBucket_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(253, 97);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "tabPage3";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(192, 71);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "tabPage4";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 25);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(234, 99);
            this.tabPage9.TabIndex = 0;
            this.tabPage9.Text = "tabPage7";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 25);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(234, 137);
            this.tabPage10.TabIndex = 1;
            this.tabPage10.Text = "tabPage8";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 25);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(234, 99);
            this.tabPage11.TabIndex = 0;
            this.tabPage11.Text = "tabPage7";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabPage12
            // 
            this.tabPage12.Location = new System.Drawing.Point(4, 25);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(234, 137);
            this.tabPage12.TabIndex = 1;
            this.tabPage12.Text = "tabPage8";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TlsTileCommands);
            this.panel2.Controls.Add(this.TlsDrawing);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(281, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 27);
            this.panel2.TabIndex = 23;
            // 
            // TlsTileCommands
            // 
            this.TlsTileCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TlsTileCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbCut,
            this.TsbCopy,
            this.TsbPaste,
            this.TsbDelete});
            this.TlsTileCommands.Location = new System.Drawing.Point(195, 0);
            this.TlsTileCommands.Name = "TlsTileCommands";
            this.TlsTileCommands.Size = new System.Drawing.Size(298, 25);
            this.TlsTileCommands.TabIndex = 23;
            this.TlsTileCommands.Text = "toolStrip1";
            // 
            // TsbCut
            // 
            this.TsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbCut.Image = global::Daiz.NES.Reuben.Properties.Resources.cut;
            this.TsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbCut.Name = "TsbCut";
            this.TsbCut.Size = new System.Drawing.Size(23, 22);
            this.TsbCut.Text = "Cut";
            this.TsbCut.ToolTipText = "Cut\r\n(Ctrl + X)";
            this.TsbCut.Click += new System.EventHandler(this.TsbCut_Click);
            // 
            // TsbCopy
            // 
            this.TsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbCopy.Image = global::Daiz.NES.Reuben.Properties.Resources.copy;
            this.TsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbCopy.Name = "TsbCopy";
            this.TsbCopy.Size = new System.Drawing.Size(23, 22);
            this.TsbCopy.Text = "Copy";
            this.TsbCopy.ToolTipText = "Copy\r\n(Ctrl + C)";
            this.TsbCopy.Click += new System.EventHandler(this.TsbCopy_Click);
            // 
            // TsbPaste
            // 
            this.TsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbPaste.Image = global::Daiz.NES.Reuben.Properties.Resources.paste;
            this.TsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPaste.Name = "TsbPaste";
            this.TsbPaste.Size = new System.Drawing.Size(23, 22);
            this.TsbPaste.Text = "Paste";
            this.TsbPaste.ToolTipText = "Paste\r\n(Ctrl + V)";
            this.TsbPaste.Click += new System.EventHandler(this.TsbPaste_Click);
            // 
            // TsbDelete
            // 
            this.TsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbDelete.Image = global::Daiz.NES.Reuben.Properties.Resources.delete;
            this.TsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDelete.Name = "TsbDelete";
            this.TsbDelete.Size = new System.Drawing.Size(23, 22);
            this.TsbDelete.Text = "Delete";
            this.TsbDelete.ToolTipText = "Delete\r\n(Delete)";
            this.TsbDelete.Click += new System.EventHandler(this.TsbDelete_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbSave,
            this.toolStripSeparator2,
            this.TsbGrid,
            this.TsbPointers});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(78, 25);
            this.toolStrip2.TabIndex = 22;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // TsbSave
            // 
            this.TsbSave.AutoSize = false;
            this.TsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbSave.Image = global::Daiz.NES.Reuben.Properties.Resources.saveHS;
            this.TsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSave.Name = "TsbSave";
            this.TsbSave.Size = new System.Drawing.Size(23, 20);
            this.TsbSave.Text = "Save Changes";
            this.TsbSave.ToolTipText = "Save Changes\r\n(Ctrl + S)";
            this.TsbSave.Click += new System.EventHandler(this.TsbSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // TsbGrid
            // 
            this.TsbGrid.CheckOnClick = true;
            this.TsbGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbGrid.Image = global::Daiz.NES.Reuben.Properties.Resources.grid;
            this.TsbGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbGrid.Name = "TsbGrid";
            this.TsbGrid.Size = new System.Drawing.Size(23, 22);
            this.TsbGrid.Text = "Toggle Grid";
            this.TsbGrid.ToolTipText = "Toggle Grid\r\n(Ctrl + G)";
            this.TsbGrid.CheckStateChanged += new System.EventHandler(this.TsbGrid_CheckedChanged);
            // 
            // TsbPointers
            // 
            this.TsbPointers.CheckOnClick = true;
            this.TsbPointers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbPointers.Image = global::Daiz.NES.Reuben.Properties.Resources.pointers;
            this.TsbPointers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPointers.Name = "TsbPointers";
            this.TsbPointers.Size = new System.Drawing.Size(23, 22);
            this.TsbPointers.Text = "Toggle Pointers";
            this.TsbPointers.ToolTipText = "Toggle Pointers\r\n(Ctrl + P)";
            this.TsbPointers.CheckStateChanged += new System.EventHandler(this.TsbPointers_CheckedChanged);
            // 
            // PnlView
            // 
            this.PnlView.AutoScroll = true;
            this.PnlView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PnlView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlView.Controls.Add(this.PnlLengthControl);
            this.PnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlView.Location = new System.Drawing.Point(281, 27);
            this.PnlView.Name = "PnlView";
            this.PnlView.Size = new System.Drawing.Size(495, 346);
            this.PnlView.TabIndex = 0;
            this.PnlView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WldView_PreviewKeyDown);
            // 
            // PnlLengthControl
            // 
            this.PnlLengthControl.Controls.Add(this.WldView);
            this.PnlLengthControl.Location = new System.Drawing.Point(0, 0);
            this.PnlLengthControl.Name = "PnlLengthControl";
            this.PnlLengthControl.Size = new System.Drawing.Size(461, 307);
            this.PnlLengthControl.TabIndex = 0;
            // 
            // WldView
            // 
            this.WldView.CurrentDefiniton = null;
            this.WldView.CurrentTable = null;
            this.WldView.CurrentWorld = null;
            this.WldView.DelayDrawing = false;
            this.WldView.Location = new System.Drawing.Point(0, -272);
            this.WldView.Name = "WldView";
            this.WldView.SelectionLine = null;
            this.WldView.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.WldView.ShowGrid = false;
            this.WldView.ShowPointers = false;
            this.WldView.Size = new System.Drawing.Size(3577, 270);
            this.WldView.SpecialTable = null;
            this.WldView.TabIndex = 0;
            this.WldView.Text = "levelViewer1";
            this.WldView.Zoom = 0;
            this.WldView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WldView_MouseDown);
            this.WldView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WldView_MouseMove);
            this.WldView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WldView_MouseUp);
            // 
            // WorldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 495);
            this.Controls.Add(this.PnlView);
            this.Controls.Add(this.PnlInfo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PnlDrawing);
            this.Name = "WorldEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "WorldEditor";
            this.PnlInfo.ResumeLayout(false);
            this.TabLevelInfo.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPageEX1.ResumeLayout(false);
            this.tabPageEX1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.PnlDrawing.ResumeLayout(false);
            this.TabEditSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.TlsDrawing.ResumeLayout(false);
            this.TlsDrawing.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TlsTileCommands.ResumeLayout(false);
            this.TlsTileCommands.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.PnlView.ResumeLayout(false);
            this.PnlLengthControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Daiz.NES.Reuben.FixedPanel PnlView;
        private System.Windows.Forms.Panel PnlLengthControl;
        private WorldViewer WldView;
        private System.Windows.Forms.Panel PnlInfo;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.Panel PnlDrawing;
        private System.Windows.Forms.TabControl TabEditSelector;
        private System.Windows.Forms.TabPage tabPage1;
        private BlockSelector BlsSelector;
        private System.Windows.Forms.ComboBox CmbLayouts;
        private System.Windows.Forms.ToolStrip TlsDrawing;
        private System.Windows.Forms.ToolStripButton TsbPencil;
        private System.Windows.Forms.ToolStripButton TsbLine;
        private System.Windows.Forms.ToolStripButton TsbRectangle;
        private System.Windows.Forms.ToolStripButton TsbOutline;
        private System.Windows.Forms.ToolStripButton TsbBucket;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label LblSpriteSelected;
        private System.Windows.Forms.TabControl TabClass1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button BtnDeletePointer;
        private System.Windows.Forms.Button BtnAddPointer;
        private System.Windows.Forms.GroupBox groupBox1;
        private WorldPointerEditor PntEditor;
        private System.Windows.Forms.Panel panel2;
        private BlockViewer BlvLeft;
        private System.Windows.Forms.Label LblSelectorHover;
        private BlockViewer BlvRight;
        private System.Windows.Forms.ToolTip LevelToolTip;
        private System.Windows.Forms.ToolStrip TlsTileCommands;
        private System.Windows.Forms.ToolStripButton TsbCut;
        private System.Windows.Forms.ToolStripButton TsbCopy;
        private System.Windows.Forms.ToolStripButton TsbPaste;
        private System.Windows.Forms.ToolStripButton TsbDelete;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label LblSprite;
        private System.Windows.Forms.Label LblPositition;
        private System.Windows.Forms.Label LblRightClickMode;
        private System.Windows.Forms.TabControl TabLevelInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox CmbMusic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CmbLength;
        private System.Windows.Forms.TabPage tabPageEX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbGraphics;
        private System.Windows.Forms.ComboBox CmbPalettes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblHexGraphics;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton TsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TsbGrid;
        private System.Windows.Forms.ToolStripButton TsbPointers;
    }
}