namespace Daiz.NES.Reuben
{
    partial class SpecialEditor
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
            this.CmbDefinitions = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BlvCurrent = new Daiz.NES.Reuben.BlockViewer();
            this.BlsBlocks = new Daiz.NES.Reuben.BlockSelector();
            this.PlsView = new Daiz.NES.Reuben.PaletteSelector();
            this.PtvTable = new Daiz.NES.Reuben.PatternTableViewer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RdoMap16 = new System.Windows.Forms.RadioButton();
            this.RdoNormal = new System.Windows.Forms.RadioButton();
            this.BtnSaveClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LblBlockSelected = new System.Windows.Forms.Label();
            this.BtnApplyGlobally = new System.Windows.Forms.Button();
            this.TSAToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbDefinitions
            // 
            this.CmbDefinitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDefinitions.FormattingEnabled = true;
            this.CmbDefinitions.Location = new System.Drawing.Point(358, 12);
            this.CmbDefinitions.Name = "CmbDefinitions";
            this.CmbDefinitions.Size = new System.Drawing.Size(256, 21);
            this.CmbDefinitions.TabIndex = 6;
            this.CmbDefinitions.SelectedIndexChanged += new System.EventHandler(this.CmbDefinitions_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BlvCurrent);
            this.groupBox1.Location = new System.Drawing.Point(288, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(64, 64);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Block";
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
            this.BlsBlocks.Location = new System.Drawing.Point(358, 43);
            this.BlsBlocks.Name = "BlsBlocks";
            this.BlsBlocks.SelectedIndex = 0;
            this.BlsBlocks.SelectedTileIndex = 0;
            this.BlsBlocks.ShowBlockProperties = false;
            this.BlsBlocks.ShowSpecialBlocks = false;
            this.BlsBlocks.Size = new System.Drawing.Size(256, 256);
            this.BlsBlocks.SpecialTable = null;
            this.BlsBlocks.TabIndex = 5;
            this.BlsBlocks.Text = "blockSelector1";
            this.BlsBlocks.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.BlsBlocks_PreviewKeyDown);
            this.BlsBlocks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsBlocks_MouseMove);
            this.BlsBlocks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlsBlocks_MouseDown);
            // 
            // PlsView
            // 
            this.PlsView.CurrentPalette = null;
            this.PlsView.Location = new System.Drawing.Point(358, 317);
            this.PlsView.Name = "PlsView";
            this.PlsView.SelectablePaletteMode = false;
            this.PlsView.Size = new System.Drawing.Size(256, 32);
            this.PlsView.TabIndex = 4;
            this.PlsView.Text = "paletteSelector1";
            // 
            // PtvTable
            // 
            this.PtvTable.Location = new System.Drawing.Point(26, 43);
            this.PtvTable.Name = "PtvTable";
            this.PtvTable.ShowGrid = false;
            this.PtvTable.Size = new System.Drawing.Size(256, 256);
            this.PtvTable.TabIndex = 0;
            this.PtvTable.Text = "patternTableViewer1";
            this.PtvTable.TileSelectionMode = Daiz.NES.Reuben.TileSelectionMode.SingleTile;
            this.PtvTable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PtvTable_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RdoMap16);
            this.groupBox2.Controls.Add(this.RdoNormal);
            this.groupBox2.Location = new System.Drawing.Point(26, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 71);
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
            // BtnSaveClose
            // 
            this.BtnSaveClose.Location = new System.Drawing.Point(358, 355);
            this.BtnSaveClose.Name = "BtnSaveClose";
            this.BtnSaveClose.Size = new System.Drawing.Size(121, 23);
            this.BtnSaveClose.TabIndex = 11;
            this.BtnSaveClose.Text = "Save && Close";
            this.BtnSaveClose.UseVisualStyleBackColor = true;
            this.BtnSaveClose.Click += new System.EventHandler(this.BtnSaveClose_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 355);
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
            this.LblBlockSelected.Location = new System.Drawing.Point(288, 205);
            this.LblBlockSelected.Name = "LblBlockSelected";
            this.LblBlockSelected.Size = new System.Drawing.Size(67, 13);
            this.LblBlockSelected.TabIndex = 17;
            this.LblBlockSelected.Text = "Selected: 00";
            // 
            // BtnApplyGlobally
            // 
            this.BtnApplyGlobally.Location = new System.Drawing.Point(288, 75);
            this.BtnApplyGlobally.Name = "BtnApplyGlobally";
            this.BtnApplyGlobally.Size = new System.Drawing.Size(67, 57);
            this.BtnApplyGlobally.TabIndex = 18;
            this.BtnApplyGlobally.Text = "Apply Definition Globally";
            this.BtnApplyGlobally.UseVisualStyleBackColor = true;
            this.BtnApplyGlobally.Click += new System.EventHandler(this.BtnApplyGlobally_Click);
            // 
            // SpecialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 401);
            this.Controls.Add(this.BtnApplyGlobally);
            this.Controls.Add(this.PlsView);
            this.Controls.Add(this.LblBlockSelected);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnSaveClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CmbDefinitions);
            this.Controls.Add(this.BlsBlocks);
            this.Controls.Add(this.PtvTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SpecialEditor";
            this.Text = "Map16Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PatternTableViewer PtvTable;
        private PaletteSelector PlsView;
        private BlockSelector BlsBlocks;
        private System.Windows.Forms.ComboBox CmbDefinitions;
        private BlockViewer BlvCurrent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RdoMap16;
        private System.Windows.Forms.RadioButton RdoNormal;
        private System.Windows.Forms.Button BtnSaveClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LblBlockSelected;
        private System.Windows.Forms.Button BtnApplyGlobally;
        private System.Windows.Forms.ToolTip TSAToolTip;
    }
}