namespace Daiz.NES.Reuben
{
    partial class GraphicsEditor
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
            this.CmbPalettes = new System.Windows.Forms.ComboBox();
            this.CmbGraphics1 = new System.Windows.Forms.ComboBox();
            this.CmbGraphics2 = new System.Windows.Forms.ComboBox();
            this.CmbGraphics3 = new System.Windows.Forms.ComboBox();
            this.CmbGraphics4 = new System.Windows.Forms.ComboBox();
            this.TxtGName1 = new System.Windows.Forms.TextBox();
            this.BtnRename1 = new System.Windows.Forms.Button();
            this.BtnRename2 = new System.Windows.Forms.Button();
            this.TxtGName2 = new System.Windows.Forms.TextBox();
            this.BtnRename3 = new System.Windows.Forms.Button();
            this.TxtGName3 = new System.Windows.Forms.TextBox();
            this.BtnRename4 = new System.Windows.Forms.Button();
            this.TxtGName4 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.RdoHalf = new System.Windows.Forms.RadioButton();
            this.RdoQuarter = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RdoSixteen = new System.Windows.Forms.RadioButton();
            this.RdoNormal = new System.Windows.Forms.RadioButton();
            this.ChkTileGrid = new System.Windows.Forms.CheckBox();
            this.ChkTableGrid = new System.Windows.Forms.CheckBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.TlvEditTiles = new Daiz.NES.Reuben.TileViewer();
            this.PslView = new Daiz.NES.Reuben.PaletteSelector();
            this.PtvTileSelector = new Daiz.NES.Reuben.PatternTableViewer();
            this.LblHexGraphics1 = new System.Windows.Forms.Label();
            this.LblHexGraphics2 = new System.Windows.Forms.Label();
            this.LblHexGraphics3 = new System.Windows.Forms.Label();
            this.LblHexGraphics4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(492, 34);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(256, 21);
            this.CmbPalettes.TabIndex = 1;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // CmbGraphics1
            // 
            this.CmbGraphics1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics1.FormattingEnabled = true;
            this.CmbGraphics1.Location = new System.Drawing.Point(12, 140);
            this.CmbGraphics1.Name = "CmbGraphics1";
            this.CmbGraphics1.Size = new System.Drawing.Size(137, 21);
            this.CmbGraphics1.TabIndex = 4;
            this.CmbGraphics1.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics1_SelectedIndexChanged);
            // 
            // CmbGraphics2
            // 
            this.CmbGraphics2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics2.FormattingEnabled = true;
            this.CmbGraphics2.Location = new System.Drawing.Point(12, 204);
            this.CmbGraphics2.Name = "CmbGraphics2";
            this.CmbGraphics2.Size = new System.Drawing.Size(137, 21);
            this.CmbGraphics2.TabIndex = 5;
            this.CmbGraphics2.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics2_SelectedIndexChanged);
            // 
            // CmbGraphics3
            // 
            this.CmbGraphics3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics3.FormattingEnabled = true;
            this.CmbGraphics3.Location = new System.Drawing.Point(12, 268);
            this.CmbGraphics3.Name = "CmbGraphics3";
            this.CmbGraphics3.Size = new System.Drawing.Size(137, 21);
            this.CmbGraphics3.TabIndex = 6;
            this.CmbGraphics3.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics3_SelectedIndexChanged);
            // 
            // CmbGraphics4
            // 
            this.CmbGraphics4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics4.FormattingEnabled = true;
            this.CmbGraphics4.Location = new System.Drawing.Point(12, 332);
            this.CmbGraphics4.Name = "CmbGraphics4";
            this.CmbGraphics4.Size = new System.Drawing.Size(137, 21);
            this.CmbGraphics4.TabIndex = 7;
            this.CmbGraphics4.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics4_SelectedIndexChanged);
            // 
            // TxtGName1
            // 
            this.TxtGName1.Location = new System.Drawing.Point(12, 167);
            this.TxtGName1.Name = "TxtGName1";
            this.TxtGName1.Size = new System.Drawing.Size(137, 20);
            this.TxtGName1.TabIndex = 8;
            // 
            // BtnRename1
            // 
            this.BtnRename1.Location = new System.Drawing.Point(155, 167);
            this.BtnRename1.Name = "BtnRename1";
            this.BtnRename1.Size = new System.Drawing.Size(56, 20);
            this.BtnRename1.TabIndex = 9;
            this.BtnRename1.Text = "Rename";
            this.BtnRename1.UseVisualStyleBackColor = true;
            this.BtnRename1.Click += new System.EventHandler(this.BtnRename1_Click);
            // 
            // BtnRename2
            // 
            this.BtnRename2.Location = new System.Drawing.Point(155, 231);
            this.BtnRename2.Name = "BtnRename2";
            this.BtnRename2.Size = new System.Drawing.Size(56, 20);
            this.BtnRename2.TabIndex = 11;
            this.BtnRename2.Text = "Rename";
            this.BtnRename2.UseVisualStyleBackColor = true;
            this.BtnRename2.Click += new System.EventHandler(this.BtnRename2_Click);
            // 
            // TxtGName2
            // 
            this.TxtGName2.Location = new System.Drawing.Point(12, 231);
            this.TxtGName2.Name = "TxtGName2";
            this.TxtGName2.Size = new System.Drawing.Size(137, 20);
            this.TxtGName2.TabIndex = 10;
            // 
            // BtnRename3
            // 
            this.BtnRename3.Location = new System.Drawing.Point(155, 295);
            this.BtnRename3.Name = "BtnRename3";
            this.BtnRename3.Size = new System.Drawing.Size(56, 20);
            this.BtnRename3.TabIndex = 13;
            this.BtnRename3.Text = "Rename";
            this.BtnRename3.UseVisualStyleBackColor = true;
            this.BtnRename3.Click += new System.EventHandler(this.BtnRename3_Click);
            // 
            // TxtGName3
            // 
            this.TxtGName3.Location = new System.Drawing.Point(12, 295);
            this.TxtGName3.Name = "TxtGName3";
            this.TxtGName3.Size = new System.Drawing.Size(137, 20);
            this.TxtGName3.TabIndex = 12;
            // 
            // BtnRename4
            // 
            this.BtnRename4.Location = new System.Drawing.Point(155, 359);
            this.BtnRename4.Name = "BtnRename4";
            this.BtnRename4.Size = new System.Drawing.Size(56, 20);
            this.BtnRename4.TabIndex = 15;
            this.BtnRename4.Text = "Rename";
            this.BtnRename4.UseVisualStyleBackColor = true;
            this.BtnRename4.Click += new System.EventHandler(this.BtnRename4_Click);
            // 
            // TxtGName4
            // 
            this.TxtGName4.Location = new System.Drawing.Point(12, 359);
            this.TxtGName4.Name = "TxtGName4";
            this.TxtGName4.Size = new System.Drawing.Size(137, 20);
            this.TxtGName4.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.RdoHalf);
            this.groupBox1.Controls.Add(this.RdoQuarter);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 94);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graphics Selection Mode";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(19, 68);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 17);
            this.radioButton3.TabIndex = 20;
            this.radioButton3.Text = "Full Table";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // RdoHalf
            // 
            this.RdoHalf.AutoSize = true;
            this.RdoHalf.Location = new System.Drawing.Point(19, 45);
            this.RdoHalf.Name = "RdoHalf";
            this.RdoHalf.Size = new System.Drawing.Size(79, 17);
            this.RdoHalf.TabIndex = 19;
            this.RdoHalf.Text = "Half Tables";
            this.RdoHalf.UseVisualStyleBackColor = true;
            this.RdoHalf.CheckedChanged += new System.EventHandler(this.RdoHalf_CheckedChanged);
            // 
            // RdoQuarter
            // 
            this.RdoQuarter.AutoSize = true;
            this.RdoQuarter.Checked = true;
            this.RdoQuarter.Location = new System.Drawing.Point(19, 22);
            this.RdoQuarter.Name = "RdoQuarter";
            this.RdoQuarter.Size = new System.Drawing.Size(95, 17);
            this.RdoQuarter.TabIndex = 18;
            this.RdoQuarter.TabStop = true;
            this.RdoQuarter.Text = "Quarter Tables";
            this.RdoQuarter.UseVisualStyleBackColor = true;
            this.RdoQuarter.CheckedChanged += new System.EventHandler(this.RdoQuarter_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Palette View";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(493, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Selected Palette";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 121);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Tile Viewer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RdoSixteen);
            this.groupBox2.Controls.Add(this.RdoNormal);
            this.groupBox2.Location = new System.Drawing.Point(221, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 94);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tile Arrangement";
            // 
            // RdoSixteen
            // 
            this.RdoSixteen.AutoSize = true;
            this.RdoSixteen.Location = new System.Drawing.Point(21, 45);
            this.RdoSixteen.Name = "RdoSixteen";
            this.RdoSixteen.Size = new System.Drawing.Size(83, 17);
            this.RdoSixteen.TabIndex = 1;
            this.RdoSixteen.Text = "Map16 Tiles";
            this.RdoSixteen.UseVisualStyleBackColor = true;
            this.RdoSixteen.CheckedChanged += new System.EventHandler(this.RdoSixteen_CheckedChanged);
            // 
            // RdoNormal
            // 
            this.RdoNormal.AutoSize = true;
            this.RdoNormal.Checked = true;
            this.RdoNormal.Location = new System.Drawing.Point(21, 22);
            this.RdoNormal.Name = "RdoNormal";
            this.RdoNormal.Size = new System.Drawing.Size(58, 17);
            this.RdoNormal.TabIndex = 0;
            this.RdoNormal.TabStop = true;
            this.RdoNormal.Text = "Normal";
            this.RdoNormal.UseVisualStyleBackColor = true;
            this.RdoNormal.CheckedChanged += new System.EventHandler(this.RdoNormal_CheckedChanged);
            // 
            // ChkTileGrid
            // 
            this.ChkTileGrid.AutoSize = true;
            this.ChkTileGrid.Location = new System.Drawing.Point(673, 121);
            this.ChkTileGrid.Name = "ChkTileGrid";
            this.ChkTileGrid.Size = new System.Drawing.Size(75, 17);
            this.ChkTileGrid.TabIndex = 23;
            this.ChkTileGrid.Text = "Show Grid";
            this.ChkTileGrid.UseVisualStyleBackColor = true;
            this.ChkTileGrid.CheckedChanged += new System.EventHandler(this.ChkTileGrid_CheckedChanged);
            // 
            // ChkTableGrid
            // 
            this.ChkTableGrid.AutoSize = true;
            this.ChkTableGrid.Location = new System.Drawing.Point(393, 121);
            this.ChkTableGrid.Name = "ChkTableGrid";
            this.ChkTableGrid.Size = new System.Drawing.Size(75, 17);
            this.ChkTableGrid.TabIndex = 24;
            this.ChkTableGrid.Text = "Show Grid";
            this.ChkTableGrid.UseVisualStyleBackColor = true;
            this.ChkTableGrid.CheckedChanged += new System.EventHandler(this.ChkTableGrid_CheckedChanged);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(536, 402);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(100, 23);
            this.BtnClose.TabIndex = 25;
            this.BtnClose.Text = "Save && Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // TlvEditTiles
            // 
            this.TlvEditTiles.Location = new System.Drawing.Point(492, 140);
            this.TlvEditTiles.Name = "TlvEditTiles";
            this.TlvEditTiles.SelectedOffset = ((byte)(0));
            this.TlvEditTiles.ShowGrid = false;
            this.TlvEditTiles.Size = new System.Drawing.Size(256, 256);
            this.TlvEditTiles.TabIndex = 22;
            this.TlvEditTiles.Text = "tileViewer1";
            // 
            // PslView
            // 
            this.PslView.CurrentPalette = null;
            this.PslView.Location = new System.Drawing.Point(492, 80);
            this.PslView.Name = "PslView";
            this.PslView.SelectablePaletteMode = true;
            this.PslView.Size = new System.Drawing.Size(256, 32);
            this.PslView.TabIndex = 2;
            this.PslView.Text = "paletteSelector1";
            // 
            // PtvTileSelector
            // 
            this.PtvTileSelector.Location = new System.Drawing.Point(217, 140);
            this.PtvTileSelector.Name = "PtvTileSelector";
            this.PtvTileSelector.ShowGrid = false;
            this.PtvTileSelector.Size = new System.Drawing.Size(256, 256);
            this.PtvTileSelector.TabIndex = 0;
            this.PtvTileSelector.Text = "patternTableViewer1";
            this.PtvTileSelector.TileSelectionMode = Daiz.NES.Reuben.TileSelectionMode.TileBlock;
            // 
            // LblHexGraphics1
            // 
            this.LblHexGraphics1.AutoSize = true;
            this.LblHexGraphics1.Location = new System.Drawing.Point(155, 143);
            this.LblHexGraphics1.Name = "LblHexGraphics1";
            this.LblHexGraphics1.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics1.TabIndex = 26;
            this.LblHexGraphics1.Text = "label3";
            // 
            // LblHexGraphics2
            // 
            this.LblHexGraphics2.AutoSize = true;
            this.LblHexGraphics2.Location = new System.Drawing.Point(155, 207);
            this.LblHexGraphics2.Name = "LblHexGraphics2";
            this.LblHexGraphics2.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics2.TabIndex = 27;
            this.LblHexGraphics2.Text = "label5";
            // 
            // LblHexGraphics3
            // 
            this.LblHexGraphics3.AutoSize = true;
            this.LblHexGraphics3.Location = new System.Drawing.Point(155, 271);
            this.LblHexGraphics3.Name = "LblHexGraphics3";
            this.LblHexGraphics3.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics3.TabIndex = 28;
            this.LblHexGraphics3.Text = "label6";
            // 
            // LblHexGraphics4
            // 
            this.LblHexGraphics4.AutoSize = true;
            this.LblHexGraphics4.Location = new System.Drawing.Point(155, 335);
            this.LblHexGraphics4.Name = "LblHexGraphics4";
            this.LblHexGraphics4.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics4.TabIndex = 29;
            this.LblHexGraphics4.Text = "label7";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GraphicsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 432);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LblHexGraphics4);
            this.Controls.Add(this.LblHexGraphics3);
            this.Controls.Add(this.LblHexGraphics2);
            this.Controls.Add(this.LblHexGraphics1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.ChkTableGrid);
            this.Controls.Add(this.ChkTileGrid);
            this.Controls.Add(this.TlvEditTiles);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnRename4);
            this.Controls.Add(this.TxtGName4);
            this.Controls.Add(this.BtnRename3);
            this.Controls.Add(this.TxtGName3);
            this.Controls.Add(this.BtnRename2);
            this.Controls.Add(this.TxtGName2);
            this.Controls.Add(this.BtnRename1);
            this.Controls.Add(this.TxtGName1);
            this.Controls.Add(this.CmbGraphics4);
            this.Controls.Add(this.CmbGraphics3);
            this.Controls.Add(this.CmbGraphics2);
            this.Controls.Add(this.CmbGraphics1);
            this.Controls.Add(this.PslView);
            this.Controls.Add(this.CmbPalettes);
            this.Controls.Add(this.PtvTileSelector);
            this.Name = "GraphicsEditor";
            this.Text = "GraphicsEditor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Daiz.NES.Reuben.PatternTableViewer PtvTileSelector;
        private System.Windows.Forms.ComboBox CmbPalettes;
        private PaletteSelector PslView;
        private System.Windows.Forms.ComboBox CmbGraphics1;
        private System.Windows.Forms.ComboBox CmbGraphics2;
        private System.Windows.Forms.ComboBox CmbGraphics3;
        private System.Windows.Forms.ComboBox CmbGraphics4;
        private System.Windows.Forms.TextBox TxtGName1;
        private System.Windows.Forms.Button BtnRename1;
        private System.Windows.Forms.Button BtnRename2;
        private System.Windows.Forms.TextBox TxtGName2;
        private System.Windows.Forms.Button BtnRename3;
        private System.Windows.Forms.TextBox TxtGName3;
        private System.Windows.Forms.Button BtnRename4;
        private System.Windows.Forms.TextBox TxtGName4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton RdoHalf;
        private System.Windows.Forms.RadioButton RdoQuarter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RdoSixteen;
        private System.Windows.Forms.RadioButton RdoNormal;
        private TileViewer TlvEditTiles;
        private System.Windows.Forms.CheckBox ChkTileGrid;
        private System.Windows.Forms.CheckBox ChkTableGrid;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label LblHexGraphics1;
        private System.Windows.Forms.Label LblHexGraphics2;
        private System.Windows.Forms.Label LblHexGraphics3;
        private System.Windows.Forms.Label LblHexGraphics4;
        private System.Windows.Forms.Button button1;

    }
}