namespace Daiz.NES.Reuben
{
    partial class LayoutEditor
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
            this.CmbPalettes = new System.Windows.Forms.ComboBox();
            this.CmbDefinitions = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PlsView = new Daiz.NES.Reuben.PaletteSelector();
            this.BtnSaveClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BlsFrom = new Daiz.NES.Reuben.BlockSelector();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BlsTo = new Daiz.NES.Reuben.BlockSelector();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnRename = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.CmbLayouts = new System.Windows.Forms.ComboBox();
            this.ChkShowSpecials = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LayoutToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ChkBlockProperties = new System.Windows.Forms.CheckBox();
            this.LblHexGraphics1 = new System.Windows.Forms.Label();
            this.LblHexGraphics2 = new System.Windows.Forms.Label();
            this.CmbGraphics2 = new System.Windows.Forms.ComboBox();
            this.CmbGraphics1 = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(15, 19);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(256, 21);
            this.CmbPalettes.TabIndex = 3;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // CmbDefinitions
            // 
            this.CmbDefinitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDefinitions.FormattingEnabled = true;
            this.CmbDefinitions.Location = new System.Drawing.Point(353, 15);
            this.CmbDefinitions.Name = "CmbDefinitions";
            this.CmbDefinitions.Size = new System.Drawing.Size(256, 21);
            this.CmbDefinitions.TabIndex = 6;
            this.CmbDefinitions.SelectedIndexChanged += new System.EventHandler(this.CmbDefinitions_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmbPalettes);
            this.groupBox3.Controls.Add(this.PlsView);
            this.groupBox3.Location = new System.Drawing.Point(26, 361);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 97);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Palettes";
            // 
            // PlsView
            // 
            this.PlsView.CurrentPalette = null;
            this.PlsView.Location = new System.Drawing.Point(15, 50);
            this.PlsView.Name = "PlsView";
            this.PlsView.SelectablePaletteMode = false;
            this.PlsView.Size = new System.Drawing.Size(256, 32);
            this.PlsView.TabIndex = 4;
            this.PlsView.Text = "paletteSelector1";
            // 
            // BtnSaveClose
            // 
            this.BtnSaveClose.Location = new System.Drawing.Point(373, 458);
            this.BtnSaveClose.Name = "BtnSaveClose";
            this.BtnSaveClose.Size = new System.Drawing.Size(121, 23);
            this.BtnSaveClose.TabIndex = 11;
            this.BtnSaveClose.Text = "Save && Close";
            this.BtnSaveClose.UseVisualStyleBackColor = true;
            this.BtnSaveClose.Click += new System.EventHandler(this.BtnSaveClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BlsFrom);
            this.groupBox2.Location = new System.Drawing.Point(26, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 287);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Layout";
            // 
            // BlsFrom
            // 
            this.BlsFrom.BlockLayout = null;
            this.BlsFrom.CurrentDefiniton = null;
            this.BlsFrom.HaltRendering = false;
            this.BlsFrom.Location = new System.Drawing.Point(15, 15);
            this.BlsFrom.Name = "BlsFrom";
            this.BlsFrom.SelectedIndex = 0;
            this.BlsFrom.SelectedTileIndex = 0;
            this.BlsFrom.ShowBlockProperties = false;
            this.BlsFrom.ShowSpecialBlocks = false;
            this.BlsFrom.Size = new System.Drawing.Size(256, 256);
            this.BlsFrom.SpecialTable = null;
            this.BlsFrom.TabIndex = 14;
            this.BlsFrom.Text = "blockSelector1";
            this.BlsFrom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsFrom_MouseMove);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BlsTo);
            this.groupBox4.Location = new System.Drawing.Point(334, 41);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 287);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Custom Layout";
            // 
            // BlsTo
            // 
            this.BlsTo.BlockLayout = null;
            this.BlsTo.CurrentDefiniton = null;
            this.BlsTo.HaltRendering = false;
            this.BlsTo.Location = new System.Drawing.Point(15, 15);
            this.BlsTo.Name = "BlsTo";
            this.BlsTo.SelectedIndex = 0;
            this.BlsTo.SelectedTileIndex = 0;
            this.BlsTo.ShowBlockProperties = false;
            this.BlsTo.ShowSpecialBlocks = false;
            this.BlsTo.Size = new System.Drawing.Size(256, 256);
            this.BlsTo.SpecialTable = null;
            this.BlsTo.TabIndex = 5;
            this.BlsTo.Text = "blockSelector1";
            this.BlsTo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsTo_MouseMove);
            this.BlsTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlsTo_MouseDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnRename);
            this.groupBox5.Controls.Add(this.BtnDelete);
            this.groupBox5.Controls.Add(this.BtnAdd);
            this.groupBox5.Controls.Add(this.CmbLayouts);
            this.groupBox5.Location = new System.Drawing.Point(334, 370);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(287, 82);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Custom Layouts";
            // 
            // BtnRename
            // 
            this.BtnRename.Enabled = false;
            this.BtnRename.Location = new System.Drawing.Point(206, 51);
            this.BtnRename.Name = "BtnRename";
            this.BtnRename.Size = new System.Drawing.Size(65, 23);
            this.BtnRename.TabIndex = 3;
            this.BtnRename.Text = "Rename";
            this.BtnRename.UseVisualStyleBackColor = true;
            this.BtnRename.Click += new System.EventHandler(this.BtnRename_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(139, 51);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(61, 23);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(85, 51);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(48, 23);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // CmbLayouts
            // 
            this.CmbLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLayouts.FormattingEnabled = true;
            this.CmbLayouts.Location = new System.Drawing.Point(6, 24);
            this.CmbLayouts.Name = "CmbLayouts";
            this.CmbLayouts.Size = new System.Drawing.Size(269, 21);
            this.CmbLayouts.TabIndex = 0;
            this.CmbLayouts.SelectedIndexChanged += new System.EventHandler(this.CmbLayouts_SelectedIndexChanged);
            // 
            // ChkShowSpecials
            // 
            this.ChkShowSpecials.AutoSize = true;
            this.ChkShowSpecials.Location = new System.Drawing.Point(334, 333);
            this.ChkShowSpecials.Name = "ChkShowSpecials";
            this.ChkShowSpecials.Size = new System.Drawing.Size(120, 17);
            this.ChkShowSpecials.TabIndex = 18;
            this.ChkShowSpecials.Text = "Show Special Icons";
            this.ChkShowSpecials.UseVisualStyleBackColor = true;
            this.ChkShowSpecials.CheckedChanged += new System.EventHandler(this.ChkShowSpecials_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChkBlockProperties
            // 
            this.ChkBlockProperties.AutoSize = true;
            this.ChkBlockProperties.Location = new System.Drawing.Point(460, 333);
            this.ChkBlockProperties.Name = "ChkBlockProperties";
            this.ChkBlockProperties.Size = new System.Drawing.Size(133, 17);
            this.ChkBlockProperties.TabIndex = 20;
            this.ChkBlockProperties.Text = "Show Block Properties";
            this.ChkBlockProperties.UseVisualStyleBackColor = true;
            this.ChkBlockProperties.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LblHexGraphics1
            // 
            this.LblHexGraphics1.AutoSize = true;
            this.LblHexGraphics1.Location = new System.Drawing.Point(227, 15);
            this.LblHexGraphics1.Name = "LblHexGraphics1";
            this.LblHexGraphics1.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics1.TabIndex = 12;
            this.LblHexGraphics1.Text = "label1";
            // 
            // LblHexGraphics2
            // 
            this.LblHexGraphics2.AutoSize = true;
            this.LblHexGraphics2.Location = new System.Drawing.Point(227, 337);
            this.LblHexGraphics2.Name = "LblHexGraphics2";
            this.LblHexGraphics2.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics2.TabIndex = 13;
            this.LblHexGraphics2.Text = "label2";
            // 
            // CmbGraphics2
            // 
            this.CmbGraphics2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics2.FormattingEnabled = true;
            this.CmbGraphics2.Location = new System.Drawing.Point(26, 334);
            this.CmbGraphics2.Name = "CmbGraphics2";
            this.CmbGraphics2.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics2.TabIndex = 2;
            this.CmbGraphics2.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics2_SelectedIndexChanged);
            // 
            // CmbGraphics1
            // 
            this.CmbGraphics1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics1.FormattingEnabled = true;
            this.CmbGraphics1.Location = new System.Drawing.Point(26, 12);
            this.CmbGraphics1.Name = "CmbGraphics1";
            this.CmbGraphics1.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics1.TabIndex = 1;
            this.CmbGraphics1.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics1_SelectedIndexChanged);
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 506);
            this.Controls.Add(this.ChkBlockProperties);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChkShowSpecials);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.LblHexGraphics2);
            this.Controls.Add(this.LblHexGraphics1);
            this.Controls.Add(this.BtnSaveClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CmbDefinitions);
            this.Controls.Add(this.CmbGraphics2);
            this.Controls.Add(this.CmbGraphics1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LayoutEditor";
            this.Text = "Custom Layout Manager";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbPalettes;
        private PaletteSelector PlsView;
        private BlockSelector BlsTo;
        private System.Windows.Forms.ComboBox CmbDefinitions;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnSaveClose;
        private BlockSelector BlsFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BtnRename;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ComboBox CmbLayouts;
        private System.Windows.Forms.CheckBox ChkShowSpecials;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip LayoutToolTip;
        private System.Windows.Forms.CheckBox ChkBlockProperties;
        private System.Windows.Forms.Label LblHexGraphics1;
        private System.Windows.Forms.Label LblHexGraphics2;
        private System.Windows.Forms.ComboBox CmbGraphics2;
        private System.Windows.Forms.ComboBox CmbGraphics1;
    }
}