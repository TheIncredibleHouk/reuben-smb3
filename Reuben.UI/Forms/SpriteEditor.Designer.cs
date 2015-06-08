namespace Reuben.UI
{
    partial class SpriteEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.spriteName = new System.Windows.Forms.TextBox();
            this.panel41 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.patTable = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gfxBank = new System.Windows.Forms.ComboBox();
            this.squashState = new System.Windows.Forms.CheckBox();
            this.tailImmune = new System.Windows.Forms.CheckBox();
            this.harmfulStomp = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gameHalt = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.collisionBox = new System.Windows.Forms.ComboBox();
            this.stompApathy = new System.Windows.Forms.CheckBox();
            this.shellStomp = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.clipWidth = new System.Windows.Forms.ComboBox();
            this.clipHeight = new System.Windows.Forms.ComboBox();
            this.gamePalette = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.codeTags = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.syntaxError = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.definitionCode = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.displayProperty = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.killAction = new System.Windows.Forms.ComboBox();
            this.attrGroups = new System.Windows.Forms.GroupBox();
            this.boundBox = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.bounceOthers = new System.Windows.Forms.CheckBox();
            this.immuneIce = new System.Windows.Forms.CheckBox();
            this.immuneFire = new System.Windows.Forms.CheckBox();
            this.immuneNinjaHammers = new System.Windows.Forms.CheckBox();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.spriteViewer = new Reuben.UI.SpriteViewer();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.panel41.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.attrGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 531);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Editor Name";
            // 
            // spriteName
            // 
            this.spriteName.Location = new System.Drawing.Point(21, 551);
            this.spriteName.Name = "spriteName";
            this.spriteName.Size = new System.Drawing.Size(174, 20);
            this.spriteName.TabIndex = 3;
            this.spriteName.TextChanged += new System.EventHandler(this.spriteName_TextChanged);
            // 
            // panel41
            // 
            this.panel41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel41.Controls.Add(this.attrGroups);
            this.panel41.Controls.Add(this.button3);
            this.panel41.Controls.Add(this.syntaxError);
            this.panel41.Controls.Add(this.label2);
            this.panel41.Controls.Add(this.definitionCode);
            this.panel41.Controls.Add(this.button9);
            this.panel41.Controls.Add(this.label1);
            this.panel41.Controls.Add(this.displayProperty);
            this.panel41.Controls.Add(this.paletteList);
            this.panel41.Controls.Add(this.spriteName);
            this.panel41.Controls.Add(this.label3);
            this.panel41.Controls.Add(this.label7);
            this.panel41.Controls.Add(this.button2);
            this.panel41.Controls.Add(this.button1);
            this.panel41.Controls.Add(this.checkBox1);
            this.panel41.Controls.Add(this.spriteViewer);
            this.panel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel41.Location = new System.Drawing.Point(277, 0);
            this.panel41.Name = "panel41";
            this.panel41.Size = new System.Drawing.Size(810, 843);
            this.panel41.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(125, 244);
            this.label12.Margin = new System.Windows.Forms.Padding(4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 45;
            this.label12.Text = "Table\r\n";
            // 
            // patTable
            // 
            this.patTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.patTable.FormattingEnabled = true;
            this.patTable.Items.AddRange(new object[] {
            "No Change",
            "Bank 3",
            "Bank 4"});
            this.patTable.Location = new System.Drawing.Point(125, 265);
            this.patTable.Margin = new System.Windows.Forms.Padding(4);
            this.patTable.Name = "patTable";
            this.patTable.Size = new System.Drawing.Size(66, 21);
            this.patTable.TabIndex = 44;
            this.patTable.SelectedIndexChanged += new System.EventHandler(this.patTable_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 244);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Gfx Bank";
            // 
            // gfxBank
            // 
            this.gfxBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gfxBank.FormattingEnabled = true;
            this.gfxBank.Items.AddRange(new object[] {
            "No Change",
            "Bank 3",
            "Bank 4"});
            this.gfxBank.Location = new System.Drawing.Point(7, 265);
            this.gfxBank.Margin = new System.Windows.Forms.Padding(4);
            this.gfxBank.Name = "gfxBank";
            this.gfxBank.Size = new System.Drawing.Size(110, 21);
            this.gfxBank.TabIndex = 42;
            this.gfxBank.SelectedIndexChanged += new System.EventHandler(this.gfxBank_SelectedIndexChanged);
            // 
            // squashState
            // 
            this.squashState.AutoSize = true;
            this.squashState.Location = new System.Drawing.Point(7, 120);
            this.squashState.Margin = new System.Windows.Forms.Padding(4);
            this.squashState.Name = "squashState";
            this.squashState.Size = new System.Drawing.Size(200, 17);
            this.squashState.TabIndex = 41;
            this.squashState.Text = "Squashed when stomped (Goombas)";
            this.squashState.UseVisualStyleBackColor = true;
            this.squashState.CheckedChanged += new System.EventHandler(this.squashState_CheckedChanged);
            // 
            // tailImmune
            // 
            this.tailImmune.AutoSize = true;
            this.tailImmune.Location = new System.Drawing.Point(7, 70);
            this.tailImmune.Margin = new System.Windows.Forms.Padding(4);
            this.tailImmune.Name = "tailImmune";
            this.tailImmune.Size = new System.Drawing.Size(129, 17);
            this.tailImmune.TabIndex = 40;
            this.tailImmune.Text = "Immune to tail attacks";
            this.tailImmune.UseVisualStyleBackColor = true;
            this.tailImmune.CheckedChanged += new System.EventHandler(this.tailImmune_CheckedChanged);
            // 
            // harmfulStomp
            // 
            this.harmfulStomp.AutoSize = true;
            this.harmfulStomp.Location = new System.Drawing.Point(7, 95);
            this.harmfulStomp.Margin = new System.Windows.Forms.Padding(4);
            this.harmfulStomp.Name = "harmfulStomp";
            this.harmfulStomp.Size = new System.Drawing.Size(117, 17);
            this.harmfulStomp.TabIndex = 39;
            this.harmfulStomp.Text = "Stomping is harmful";
            this.harmfulStomp.UseVisualStyleBackColor = true;
            this.harmfulStomp.CheckedChanged += new System.EventHandler(this.harmfulStomp_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 344);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Game Halt Action";
            // 
            // gameHalt
            // 
            this.gameHalt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameHalt.FormattingEnabled = true;
            this.gameHalt.Items.AddRange(new object[] {
            "Do Nothing",
            "Game Loop",
            "Draw 16 x 16",
            "Draw 16 x 16 Mirrored",
            "Draw 16 x 32",
            "Draw 48 x 16"});
            this.gameHalt.Location = new System.Drawing.Point(7, 365);
            this.gameHalt.Margin = new System.Windows.Forms.Padding(4);
            this.gameHalt.Name = "gameHalt";
            this.gameHalt.Size = new System.Drawing.Size(236, 21);
            this.gameHalt.TabIndex = 37;
            this.gameHalt.SelectedIndexChanged += new System.EventHandler(this.gameHalt_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 394);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Tile Detection Hit Box";
            // 
            // collisionBox
            // 
            this.collisionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collisionBox.FormattingEnabled = true;
            this.collisionBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.collisionBox.Location = new System.Drawing.Point(7, 415);
            this.collisionBox.Margin = new System.Windows.Forms.Padding(4);
            this.collisionBox.Name = "collisionBox";
            this.collisionBox.Size = new System.Drawing.Size(236, 21);
            this.collisionBox.TabIndex = 35;
            this.collisionBox.SelectedIndexChanged += new System.EventHandler(this.collisionBox_SelectedIndexChanged);
            // 
            // stompApathy
            // 
            this.stompApathy.AutoSize = true;
            this.stompApathy.Location = new System.Drawing.Point(7, 20);
            this.stompApathy.Margin = new System.Windows.Forms.Padding(4);
            this.stompApathy.Name = "stompApathy";
            this.stompApathy.Size = new System.Drawing.Size(155, 17);
            this.stompApathy.TabIndex = 34;
            this.stompApathy.Text = "Apathetic to being stomped";
            this.stompApathy.UseVisualStyleBackColor = true;
            this.stompApathy.CheckedChanged += new System.EventHandler(this.stompApathy_CheckedChanged);
            // 
            // shellStomp
            // 
            this.shellStomp.AutoSize = true;
            this.shellStomp.Location = new System.Drawing.Point(7, 45);
            this.shellStomp.Margin = new System.Windows.Forms.Padding(4);
            this.shellStomp.Name = "shellStomp";
            this.shellStomp.Size = new System.Drawing.Size(95, 17);
            this.shellStomp.TabIndex = 32;
            this.shellStomp.Text = "Has shell state";
            this.shellStomp.UseVisualStyleBackColor = true;
            this.shellStomp.CheckedChanged += new System.EventHandler(this.shellStomp_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 494);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Clip Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 494);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Clip Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 494);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Palette";
            // 
            // clipWidth
            // 
            this.clipWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clipWidth.FormattingEnabled = true;
            this.clipWidth.Items.AddRange(new object[] {
            "8",
            "16",
            "24",
            "32",
            "40",
            "48",
            "64"});
            this.clipWidth.Location = new System.Drawing.Point(157, 515);
            this.clipWidth.Margin = new System.Windows.Forms.Padding(4);
            this.clipWidth.Name = "clipWidth";
            this.clipWidth.Size = new System.Drawing.Size(70, 21);
            this.clipWidth.TabIndex = 28;
            this.clipWidth.SelectedIndexChanged += new System.EventHandler(this.clipWidth_SelectedIndexChanged);
            // 
            // clipHeight
            // 
            this.clipHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clipHeight.FormattingEnabled = true;
            this.clipHeight.Items.AddRange(new object[] {
            "16",
            "32",
            "48",
            "64"});
            this.clipHeight.Location = new System.Drawing.Point(81, 515);
            this.clipHeight.Margin = new System.Windows.Forms.Padding(4);
            this.clipHeight.Name = "clipHeight";
            this.clipHeight.Size = new System.Drawing.Size(68, 21);
            this.clipHeight.TabIndex = 27;
            this.clipHeight.SelectedIndexChanged += new System.EventHandler(this.clipHeight_SelectedIndexChanged);
            // 
            // gamePalette
            // 
            this.gamePalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gamePalette.FormattingEnabled = true;
            this.gamePalette.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.gamePalette.Location = new System.Drawing.Point(7, 515);
            this.gamePalette.Margin = new System.Windows.Forms.Padding(4);
            this.gamePalette.Name = "gamePalette";
            this.gamePalette.Size = new System.Drawing.Size(66, 21);
            this.gamePalette.TabIndex = 26;
            this.gamePalette.SelectedIndexChanged += new System.EventHandler(this.gamePalette_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 544);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Code Tags";
            // 
            // codeTags
            // 
            this.codeTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeTags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.codeTags.LabelWrap = false;
            this.codeTags.Location = new System.Drawing.Point(7, 565);
            this.codeTags.Margin = new System.Windows.Forms.Padding(4);
            this.codeTags.Name = "codeTags";
            this.codeTags.Size = new System.Drawing.Size(241, 76);
            this.codeTags.TabIndex = 24;
            this.codeTags.UseCompatibleStateImageBehavior = false;
            this.codeTags.View = System.Windows.Forms.View.Details;
            this.codeTags.DoubleClick += new System.EventHandler(this.codeTags_DoubleClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 805);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // syntaxError
            // 
            this.syntaxError.AutoSize = true;
            this.syntaxError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxError.ForeColor = System.Drawing.Color.Red;
            this.syntaxError.Location = new System.Drawing.Point(104, 810);
            this.syntaxError.Margin = new System.Windows.Forms.Padding(4);
            this.syntaxError.Name = "syntaxError";
            this.syntaxError.Size = new System.Drawing.Size(149, 13);
            this.syntaxError.TabIndex = 22;
            this.syntaxError.Text = "Syntax error in definition.";
            this.syntaxError.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 652);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Editor Drawing Code";
            // 
            // definitionCode
            // 
            this.definitionCode.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.definitionCode.Location = new System.Drawing.Point(21, 672);
            this.definitionCode.Multiline = true;
            this.definitionCode.Name = "definitionCode";
            this.definitionCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.definitionCode.Size = new System.Drawing.Size(774, 125);
            this.definitionCode.TabIndex = 20;
            this.definitionCode.WordWrap = false;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(386, 598);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "Modify";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // displayProperty
            // 
            this.displayProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayProperty.FormattingEnabled = true;
            this.displayProperty.Location = new System.Drawing.Point(204, 600);
            this.displayProperty.Margin = new System.Windows.Forms.Padding(4);
            this.displayProperty.Name = "displayProperty";
            this.displayProperty.Size = new System.Drawing.Size(174, 21);
            this.displayProperty.TabIndex = 14;
            this.displayProperty.SelectedIndexChanged += new System.EventHandler(this.displayProperty_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 531);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Display Palette";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 579);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Display Property";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(720, 805);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(637, 805);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(21, 578);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Display Overlays";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.spriteSelector);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(277, 843);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 808);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 31);
            this.panel3.TabIndex = 1;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(113, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 1;
            this.button8.Text = "Add";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(194, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Delete";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 294);
            this.label13.Margin = new System.Windows.Forms.Padding(4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Death Action";
            // 
            // killAction
            // 
            this.killAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.killAction.FormattingEnabled = true;
            this.killAction.Items.AddRange(new object[] {
            "Do halt action",
            "Draw 16 x 16 ",
            "Draw 16 x 16 Mirrored",
            "Draw 16 x 32",
            "Draw 16 x 32 Horizontally Flipped",
            "Game loop & death sequence",
            "Draw giant",
            "Poof",
            "Draw unless game halted",
            "Game loop"});
            this.killAction.Location = new System.Drawing.Point(7, 315);
            this.killAction.Margin = new System.Windows.Forms.Padding(4);
            this.killAction.Name = "killAction";
            this.killAction.Size = new System.Drawing.Size(236, 21);
            this.killAction.TabIndex = 46;
            this.killAction.SelectedIndexChanged += new System.EventHandler(this.killAction_SelectedIndexChanged);
            // 
            // attrGroups
            // 
            this.attrGroups.Controls.Add(this.immuneNinjaHammers);
            this.attrGroups.Controls.Add(this.immuneFire);
            this.attrGroups.Controls.Add(this.immuneIce);
            this.attrGroups.Controls.Add(this.bounceOthers);
            this.attrGroups.Controls.Add(this.boundBox);
            this.attrGroups.Controls.Add(this.label14);
            this.attrGroups.Controls.Add(this.stompApathy);
            this.attrGroups.Controls.Add(this.label13);
            this.attrGroups.Controls.Add(this.codeTags);
            this.attrGroups.Controls.Add(this.killAction);
            this.attrGroups.Controls.Add(this.label4);
            this.attrGroups.Controls.Add(this.label12);
            this.attrGroups.Controls.Add(this.gamePalette);
            this.attrGroups.Controls.Add(this.patTable);
            this.attrGroups.Controls.Add(this.clipHeight);
            this.attrGroups.Controls.Add(this.label11);
            this.attrGroups.Controls.Add(this.clipWidth);
            this.attrGroups.Controls.Add(this.gfxBank);
            this.attrGroups.Controls.Add(this.label5);
            this.attrGroups.Controls.Add(this.squashState);
            this.attrGroups.Controls.Add(this.label6);
            this.attrGroups.Controls.Add(this.tailImmune);
            this.attrGroups.Controls.Add(this.label8);
            this.attrGroups.Controls.Add(this.harmfulStomp);
            this.attrGroups.Controls.Add(this.shellStomp);
            this.attrGroups.Controls.Add(this.label10);
            this.attrGroups.Controls.Add(this.collisionBox);
            this.attrGroups.Controls.Add(this.gameHalt);
            this.attrGroups.Controls.Add(this.label9);
            this.attrGroups.Location = new System.Drawing.Point(539, 11);
            this.attrGroups.Name = "attrGroups";
            this.attrGroups.Size = new System.Drawing.Size(255, 654);
            this.attrGroups.TabIndex = 48;
            this.attrGroups.TabStop = false;
            this.attrGroups.Text = "Attributes";
            // 
            // boundBox
            // 
            this.boundBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boundBox.FormattingEnabled = true;
            this.boundBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.boundBox.Location = new System.Drawing.Point(7, 465);
            this.boundBox.Margin = new System.Windows.Forms.Padding(4);
            this.boundBox.Name = "boundBox";
            this.boundBox.Size = new System.Drawing.Size(236, 21);
            this.boundBox.TabIndex = 48;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 444);
            this.label14.Margin = new System.Windows.Forms.Padding(4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 49;
            this.label14.Text = "Object Collision Box";
            // 
            // bounceOthers
            // 
            this.bounceOthers.AutoSize = true;
            this.bounceOthers.Location = new System.Drawing.Point(7, 145);
            this.bounceOthers.Margin = new System.Windows.Forms.Padding(4);
            this.bounceOthers.Name = "bounceOthers";
            this.bounceOthers.Size = new System.Drawing.Size(147, 17);
            this.bounceOthers.TabIndex = 50;
            this.bounceOthers.Text = "Bounces off other objects";
            this.bounceOthers.UseVisualStyleBackColor = true;
            // 
            // immuneIce
            // 
            this.immuneIce.AutoSize = true;
            this.immuneIce.Location = new System.Drawing.Point(7, 170);
            this.immuneIce.Margin = new System.Windows.Forms.Padding(4);
            this.immuneIce.Name = "immuneIce";
            this.immuneIce.Size = new System.Drawing.Size(116, 17);
            this.immuneIce.TabIndex = 51;
            this.immuneIce.Text = "Immune to ice balls";
            this.immuneIce.UseVisualStyleBackColor = true;
            // 
            // immuneFire
            // 
            this.immuneFire.AutoSize = true;
            this.immuneFire.Location = new System.Drawing.Point(7, 195);
            this.immuneFire.Margin = new System.Windows.Forms.Padding(4);
            this.immuneFire.Name = "immuneFire";
            this.immuneFire.Size = new System.Drawing.Size(116, 17);
            this.immuneFire.TabIndex = 52;
            this.immuneFire.Text = "Immune to fire balls";
            this.immuneFire.UseVisualStyleBackColor = true;
            // 
            // immuneNinjaHammers
            // 
            this.immuneNinjaHammers.AutoSize = true;
            this.immuneNinjaHammers.Location = new System.Drawing.Point(7, 220);
            this.immuneNinjaHammers.Margin = new System.Windows.Forms.Padding(4);
            this.immuneNinjaHammers.Name = "immuneNinjaHammers";
            this.immuneNinjaHammers.Size = new System.Drawing.Size(223, 17);
            this.immuneNinjaHammers.TabIndex = 53;
            this.immuneNinjaHammers.Text = "Immune to ninja stars, hammers and shells";
            this.immuneNinjaHammers.UseVisualStyleBackColor = true;
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(204, 551);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(174, 21);
            this.paletteList.TabIndex = 5;
            // 
            // spriteViewer
            // 
            this.spriteViewer.Location = new System.Drawing.Point(20, 11);
            this.spriteViewer.Margin = new System.Windows.Forms.Padding(4);
            this.spriteViewer.Name = "spriteViewer";
            this.spriteViewer.Size = new System.Drawing.Size(512, 512);
            this.spriteViewer.TabIndex = 0;
            this.spriteViewer.Text = "spriteViewer1";
            // 
            // spriteSelector
            // 
            this.spriteSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSelector.Location = new System.Drawing.Point(0, 0);
            this.spriteSelector.Name = "spriteSelector";
            this.spriteSelector.Size = new System.Drawing.Size(273, 808);
            this.spriteSelector.TabIndex = 0;
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 843);
            this.Controls.Add(this.panel41);
            this.Controls.Add(this.panel2);
            this.Name = "SpriteEditor";
            this.Text = "SpriteEditor";
            this.Activated += new System.EventHandler(this.SpriteEditor_Activated);
            this.panel41.ResumeLayout(false);
            this.panel41.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.attrGroups.ResumeLayout(false);
            this.attrGroups.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteViewer spriteViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox spriteName;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.Panel panel2;
        private SpriteSelector spriteSelector;
        private System.Windows.Forms.Label label3;
        private Controls.PaletteList paletteList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox displayProperty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox definitionCode;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label syntaxError;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView codeTags;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox clipWidth;
        private System.Windows.Forms.ComboBox clipHeight;
        private System.Windows.Forms.ComboBox gamePalette;
        private System.Windows.Forms.CheckBox stompApathy;
        private System.Windows.Forms.CheckBox shellStomp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox collisionBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox gameHalt;
        private System.Windows.Forms.CheckBox harmfulStomp;
        private System.Windows.Forms.CheckBox tailImmune;
        private System.Windows.Forms.CheckBox squashState;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox gfxBank;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox patTable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox killAction;
        private System.Windows.Forms.GroupBox attrGroups;
        private System.Windows.Forms.CheckBox immuneNinjaHammers;
        private System.Windows.Forms.CheckBox immuneFire;
        private System.Windows.Forms.CheckBox immuneIce;
        private System.Windows.Forms.CheckBox bounceOthers;
        private System.Windows.Forms.ComboBox boundBox;
        private System.Windows.Forms.Label label14;

    }
}