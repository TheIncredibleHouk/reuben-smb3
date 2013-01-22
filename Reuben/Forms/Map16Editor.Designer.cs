namespace Daiz.NES.Reuben
{
    partial class Map16Editor
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
            this.CmbGraphics1 = new System.Windows.Forms.ComboBox();
            this.CmbGraphics2 = new System.Windows.Forms.ComboBox();
            this.CmbPalettes = new System.Windows.Forms.ComboBox();
            this.CmbDefinitions = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RdoMap16 = new System.Windows.Forms.RadioButton();
            this.RdoNormal = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnSaveClose = new System.Windows.Forms.Button();
            this.LblHexGraphics1 = new System.Windows.Forms.Label();
            this.LblHexGraphics2 = new System.Windows.Forms.Label();
            this.ChkShowSpecials = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LblBlockSelected = new System.Windows.Forms.Label();
            this.BtnApplyGlobally = new System.Windows.Forms.Button();
            this.TSAToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ChkBlockProperties = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BlockProp1 = new System.Windows.Forms.CheckBox();
            this.BlockProp2 = new System.Windows.Forms.CheckBox();
            this.BlockProp5 = new System.Windows.Forms.CheckBox();
            this.BlockProp3 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SpecialList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PlsView = new Daiz.NES.Reuben.PaletteSelector();
            this.BlvCurrent = new Daiz.NES.Reuben.BlockViewer();
            this.BlsBlocks = new Daiz.NES.Reuben.BlockSelector();
            this.PtvTable = new Daiz.NES.Reuben.PatternTableViewer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbGraphics1
            // 
            this.CmbGraphics1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics1.FormattingEnabled = true;
            this.CmbGraphics1.Location = new System.Drawing.Point(26, 32);
            this.CmbGraphics1.Name = "CmbGraphics1";
            this.CmbGraphics1.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics1.TabIndex = 1;
            this.CmbGraphics1.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics1_SelectedIndexChanged);
            // 
            // CmbGraphics2
            // 
            this.CmbGraphics2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics2.FormattingEnabled = true;
            this.CmbGraphics2.Location = new System.Drawing.Point(26, 326);
            this.CmbGraphics2.Name = "CmbGraphics2";
            this.CmbGraphics2.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics2.TabIndex = 2;
            this.CmbGraphics2.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics2_SelectedIndexChanged);
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(11, 19);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(256, 21);
            this.CmbPalettes.TabIndex = 3;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // CmbDefinitions
            // 
            this.CmbDefinitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDefinitions.FormattingEnabled = true;
            this.CmbDefinitions.Location = new System.Drawing.Point(358, 32);
            this.CmbDefinitions.Name = "CmbDefinitions";
            this.CmbDefinitions.Size = new System.Drawing.Size(256, 21);
            this.CmbDefinitions.TabIndex = 6;
            this.CmbDefinitions.SelectedIndexChanged += new System.EventHandler(this.CmbDefinitions_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlvCurrent);
            this.groupBox1.Location = new System.Drawing.Point(288, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(64, 64);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Block";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RdoMap16);
            this.groupBox2.Controls.Add(this.RdoNormal);
            this.groupBox2.Location = new System.Drawing.Point(26, 378);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 82);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tile Arrangement";
            // 
            // RdoMap16
            // 
            this.RdoMap16.AutoSize = true;
            this.RdoMap16.Location = new System.Drawing.Point(29, 46);
            this.RdoMap16.Name = "RdoMap16";
            this.RdoMap16.Size = new System.Drawing.Size(83, 17);
            this.RdoMap16.TabIndex = 1;
            this.RdoMap16.Text = "Map16 Tiles";
            this.RdoMap16.UseVisualStyleBackColor = true;
            this.RdoMap16.CheckedChanged += new System.EventHandler(this.RdoMap16_CheckedChanged);
            // 
            // RdoNormal
            // 
            this.RdoNormal.AutoSize = true;
            this.RdoNormal.Checked = true;
            this.RdoNormal.Location = new System.Drawing.Point(29, 23);
            this.RdoNormal.Name = "RdoNormal";
            this.RdoNormal.Size = new System.Drawing.Size(58, 17);
            this.RdoNormal.TabIndex = 0;
            this.RdoNormal.TabStop = true;
            this.RdoNormal.Text = "Normal";
            this.RdoNormal.UseVisualStyleBackColor = true;
            this.RdoNormal.CheckedChanged += new System.EventHandler(this.RdoNormal_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmbPalettes);
            this.groupBox3.Controls.Add(this.PlsView);
            this.groupBox3.Location = new System.Drawing.Point(26, 466);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(279, 97);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Palettes";
            // 
            // BtnSaveClose
            // 
            this.BtnSaveClose.Location = new System.Drawing.Point(374, 533);
            this.BtnSaveClose.Name = "BtnSaveClose";
            this.BtnSaveClose.Size = new System.Drawing.Size(121, 23);
            this.BtnSaveClose.TabIndex = 11;
            this.BtnSaveClose.Text = "Save && Close";
            this.BtnSaveClose.UseVisualStyleBackColor = true;
            this.BtnSaveClose.Click += new System.EventHandler(this.BtnSaveClose_Click);
            // 
            // LblHexGraphics1
            // 
            this.LblHexGraphics1.AutoSize = true;
            this.LblHexGraphics1.Location = new System.Drawing.Point(227, 35);
            this.LblHexGraphics1.Name = "LblHexGraphics1";
            this.LblHexGraphics1.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics1.TabIndex = 12;
            this.LblHexGraphics1.Text = "label1";
            // 
            // LblHexGraphics2
            // 
            this.LblHexGraphics2.AutoSize = true;
            this.LblHexGraphics2.Location = new System.Drawing.Point(227, 329);
            this.LblHexGraphics2.Name = "LblHexGraphics2";
            this.LblHexGraphics2.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics2.TabIndex = 13;
            this.LblHexGraphics2.Text = "label2";
            // 
            // ChkShowSpecials
            // 
            this.ChkShowSpecials.AutoSize = true;
            this.ChkShowSpecials.Location = new System.Drawing.Point(358, 328);
            this.ChkShowSpecials.Name = "ChkShowSpecials";
            this.ChkShowSpecials.Size = new System.Drawing.Size(120, 17);
            this.ChkShowSpecials.TabIndex = 14;
            this.ChkShowSpecials.Text = "Show Special Icons";
            this.ChkShowSpecials.UseVisualStyleBackColor = true;
            this.ChkShowSpecials.CheckedChanged += new System.EventHandler(this.ChkShowSpecials_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(501, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblBlockSelected
            // 
            this.LblBlockSelected.AutoSize = true;
            this.LblBlockSelected.Location = new System.Drawing.Point(288, 225);
            this.LblBlockSelected.Name = "LblBlockSelected";
            this.LblBlockSelected.Size = new System.Drawing.Size(67, 13);
            this.LblBlockSelected.TabIndex = 17;
            this.LblBlockSelected.Text = "Selected: 00";
            // 
            // BtnApplyGlobally
            // 
            this.BtnApplyGlobally.Location = new System.Drawing.Point(288, 95);
            this.BtnApplyGlobally.Name = "BtnApplyGlobally";
            this.BtnApplyGlobally.Size = new System.Drawing.Size(67, 57);
            this.BtnApplyGlobally.TabIndex = 18;
            this.BtnApplyGlobally.Text = "Apply Definition Globally";
            this.BtnApplyGlobally.UseVisualStyleBackColor = true;
            this.BtnApplyGlobally.Click += new System.EventHandler(this.BtnApplyGlobally_Click);
            // 
            // ChkBlockProperties
            // 
            this.ChkBlockProperties.AutoSize = true;
            this.ChkBlockProperties.Location = new System.Drawing.Point(482, 328);
            this.ChkBlockProperties.Name = "ChkBlockProperties";
            this.ChkBlockProperties.Size = new System.Drawing.Size(133, 17);
            this.ChkBlockProperties.TabIndex = 19;
            this.ChkBlockProperties.Text = "Show Block Properties";
            this.ChkBlockProperties.UseVisualStyleBackColor = true;
            this.ChkBlockProperties.CheckedChanged += new System.EventHandler(this.ChkBlockProperties_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Pattern Table 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Object Set";
            // 
            // BlockProp1
            // 
            this.BlockProp1.AutoSize = true;
            this.BlockProp1.Location = new System.Drawing.Point(6, 19);
            this.BlockProp1.Name = "BlockProp1";
            this.BlockProp1.Size = new System.Drawing.Size(49, 17);
            this.BlockProp1.TabIndex = 23;
            this.BlockProp1.Text = "Solid";
            this.BlockProp1.UseVisualStyleBackColor = true;
            this.BlockProp1.CheckedChanged += new System.EventHandler(this.BlockProp1_CheckedChanged);
            // 
            // BlockProp2
            // 
            this.BlockProp2.AutoSize = true;
            this.BlockProp2.Location = new System.Drawing.Point(6, 42);
            this.BlockProp2.Name = "BlockProp2";
            this.BlockProp2.Size = new System.Drawing.Size(95, 17);
            this.BlockProp2.TabIndex = 24;
            this.BlockProp2.Text = "Only Top Solid";
            this.BlockProp2.UseVisualStyleBackColor = true;
            this.BlockProp2.CheckedChanged += new System.EventHandler(this.BlockProp2_CheckedChanged);
            // 
            // BlockProp5
            // 
            this.BlockProp5.AutoSize = true;
            this.BlockProp5.Location = new System.Drawing.Point(127, 42);
            this.BlockProp5.Name = "BlockProp5";
            this.BlockProp5.Size = new System.Drawing.Size(80, 17);
            this.BlockProp5.TabIndex = 25;
            this.BlockProp5.Text = "Foreground";
            this.BlockProp5.UseVisualStyleBackColor = true;
            this.BlockProp5.CheckedChanged += new System.EventHandler(this.BlockProp5_CheckedChanged);
            // 
            // BlockProp3
            // 
            this.BlockProp3.AutoSize = true;
            this.BlockProp3.Location = new System.Drawing.Point(127, 19);
            this.BlockProp3.Name = "BlockProp3";
            this.BlockProp3.Size = new System.Drawing.Size(55, 17);
            this.BlockProp3.TabIndex = 29;
            this.BlockProp3.Text = "Water";
            this.BlockProp3.UseVisualStyleBackColor = true;
            this.BlockProp3.CheckedChanged += new System.EventHandler(this.BlockProp3_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.SpecialList);
            this.groupBox4.Controls.Add(this.BlockProp1);
            this.groupBox4.Controls.Add(this.BlockProp2);
            this.groupBox4.Controls.Add(this.BlockProp3);
            this.groupBox4.Controls.Add(this.BlockProp5);
            this.groupBox4.Location = new System.Drawing.Point(358, 353);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(257, 97);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Special Type";
            // 
            // SpecialList
            // 
            this.SpecialList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpecialList.FormattingEnabled = true;
            this.SpecialList.Items.AddRange(new object[] {
            "None",
            "Harmful",
            "Slick",
            "Conveyor Left",
            "Conveyor Right",
            "Vertical Enterable Pipe",
            "Unstable Block",
            "Waterfall",
            "Climbable",
            "Slope Bottom Left 30 Degrees",
            "Slope Top Left 30 Degrees",
            "Slope Bottom Right 30 Degrees",
            "Slope Top Right 30 Degrees",
            "Slope Left 45 Degrees",
            "Slope Right 45 Degrees",
            "SlopeFiller "});
            this.SpecialList.Location = new System.Drawing.Point(81, 65);
            this.SpecialList.Name = "SpecialList";
            this.SpecialList.Size = new System.Drawing.Size(170, 21);
            this.SpecialList.TabIndex = 32;
            this.SpecialList.SelectedIndexChanged += new System.EventHandler(this.SpecialList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Pattern Table 2";
            // 
            // PlsView
            // 
            this.PlsView.CurrentPalette = null;
            this.PlsView.Location = new System.Drawing.Point(11, 50);
            this.PlsView.Name = "PlsView";
            this.PlsView.SelectablePaletteMode = false;
            this.PlsView.Size = new System.Drawing.Size(256, 32);
            this.PlsView.TabIndex = 4;
            this.PlsView.Text = "paletteSelector1";
            // 
            // BlvCurrent
            // 
            this.BlvCurrent.CurrentBlock = null;
            this.BlvCurrent.Location = new System.Drawing.Point(16, 20);
            this.BlvCurrent.Name = "BlvCurrent";
            this.BlvCurrent.Size = new System.Drawing.Size(32, 32);
            this.BlvCurrent.TabIndex = 7;
            this.BlvCurrent.Text = "blockViewer1";
            this.BlvCurrent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlvCurrent_MouseDown);
            // 
            // BlsBlocks
            // 
            this.BlsBlocks.BlockLayout = null;
            this.BlsBlocks.CurrentDefiniton = null;
            this.BlsBlocks.HaltRendering = false;
            this.BlsBlocks.Location = new System.Drawing.Point(358, 63);
            this.BlsBlocks.Name = "BlsBlocks";
            this.BlsBlocks.SelectedIndex = 0;
            this.BlsBlocks.SelectedTileIndex = 0;
            this.BlsBlocks.ShowBlockProperties = false;
            this.BlsBlocks.ShowSpecialBlocks = false;
            this.BlsBlocks.Size = new System.Drawing.Size(256, 256);
            this.BlsBlocks.SpecialDefnitions = null;
            this.BlsBlocks.SpecialTable = null;
            this.BlsBlocks.TabIndex = 5;
            this.BlsBlocks.Text = "blockSelector1";
            this.BlsBlocks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlsBlocks_MouseDown);
            this.BlsBlocks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsBlocks_MouseMove);
            this.BlsBlocks.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.BlsBlocks_PreviewKeyDown);
            // 
            // PtvTable
            // 
            this.PtvTable.Location = new System.Drawing.Point(26, 63);
            this.PtvTable.Name = "PtvTable";
            this.PtvTable.ShowGrid = false;
            this.PtvTable.Size = new System.Drawing.Size(256, 256);
            this.PtvTable.TabIndex = 0;
            this.PtvTable.Text = "patternTableViewer1";
            this.PtvTable.TileSelectionMode = Daiz.NES.Reuben.TileSelectionMode.SingleTile;
            this.PtvTable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PtvTable_MouseMove);
            // 
            // Map16Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 568);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChkBlockProperties);
            this.Controls.Add(this.BtnApplyGlobally);
            this.Controls.Add(this.LblBlockSelected);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChkShowSpecials);
            this.Controls.Add(this.LblHexGraphics2);
            this.Controls.Add(this.LblHexGraphics1);
            this.Controls.Add(this.BtnSaveClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CmbDefinitions);
            this.Controls.Add(this.BlsBlocks);
            this.Controls.Add(this.CmbGraphics2);
            this.Controls.Add(this.CmbGraphics1);
            this.Controls.Add(this.PtvTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Map16Editor";
            this.Text = "Map16Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PatternTableViewer PtvTable;
        private System.Windows.Forms.ComboBox CmbGraphics1;
        private System.Windows.Forms.ComboBox CmbGraphics2;
        private System.Windows.Forms.ComboBox CmbPalettes;
        private PaletteSelector PlsView;
        private BlockSelector BlsBlocks;
        private System.Windows.Forms.ComboBox CmbDefinitions;
        private BlockViewer BlvCurrent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RdoMap16;
        private System.Windows.Forms.RadioButton RdoNormal;
        private System.Windows.Forms.Button BtnSaveClose;
        private System.Windows.Forms.Label LblHexGraphics1;
        private System.Windows.Forms.Label LblHexGraphics2;
        private System.Windows.Forms.CheckBox ChkShowSpecials;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LblBlockSelected;
        private System.Windows.Forms.Button BtnApplyGlobally;
        private System.Windows.Forms.ToolTip TSAToolTip;
        private System.Windows.Forms.CheckBox ChkBlockProperties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox BlockProp1;
        private System.Windows.Forms.CheckBox BlockProp2;
        private System.Windows.Forms.CheckBox BlockProp5;
        private System.Windows.Forms.CheckBox BlockProp3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SpecialList;
    }
}