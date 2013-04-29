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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutEditor));
            this.CmbPalettes = new System.Windows.Forms.ComboBox();
            this.CmbDefinitions = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnSaveClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PnlHelp = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GrpHelp = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnHelp = new System.Windows.Forms.Button();
            this.BlsFrom = new Daiz.NES.Reuben.BlockSelector();
            this.PlsView = new Daiz.NES.Reuben.PaletteSelector();
            this.BlsTo = new Daiz.NES.Reuben.BlockSelector();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.PnlHelp.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GrpHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(15, 21);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(256, 21);
            this.CmbPalettes.TabIndex = 3;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // CmbDefinitions
            // 
            this.CmbDefinitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDefinitions.FormattingEnabled = true;
            this.CmbDefinitions.Location = new System.Drawing.Point(333, 26);
            this.CmbDefinitions.Name = "CmbDefinitions";
            this.CmbDefinitions.Size = new System.Drawing.Size(256, 21);
            this.CmbDefinitions.TabIndex = 6;
            this.CmbDefinitions.SelectedIndexChanged += new System.EventHandler(this.CmbDefinitions_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmbPalettes);
            this.groupBox3.Controls.Add(this.PlsView);
            this.groupBox3.Location = new System.Drawing.Point(3, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 97);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Palettes";
            // 
            // BtnSaveClose
            // 
            this.BtnSaveClose.Location = new System.Drawing.Point(344, 496);
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
            this.groupBox2.Location = new System.Drawing.Point(3, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 287);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Layout";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BlsTo);
            this.groupBox4.Location = new System.Drawing.Point(311, 53);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 287);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Custom Layout";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnRename);
            this.groupBox5.Controls.Add(this.BtnDelete);
            this.groupBox5.Controls.Add(this.BtnAdd);
            this.groupBox5.Controls.Add(this.CmbLayouts);
            this.groupBox5.Location = new System.Drawing.Point(311, 382);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(287, 101);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Custom Layouts";
            // 
            // BtnRename
            // 
            this.BtnRename.Enabled = false;
            this.BtnRename.Location = new System.Drawing.Point(206, 52);
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
            this.BtnDelete.Location = new System.Drawing.Point(139, 52);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(61, 23);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(85, 52);
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
            this.CmbLayouts.Location = new System.Drawing.Point(6, 25);
            this.CmbLayouts.Name = "CmbLayouts";
            this.CmbLayouts.Size = new System.Drawing.Size(269, 21);
            this.CmbLayouts.TabIndex = 0;
            this.CmbLayouts.SelectedIndexChanged += new System.EventHandler(this.CmbLayouts_SelectedIndexChanged);
            // 
            // ChkShowSpecials
            // 
            this.ChkShowSpecials.AutoSize = true;
            this.ChkShowSpecials.Location = new System.Drawing.Point(311, 345);
            this.ChkShowSpecials.Name = "ChkShowSpecials";
            this.ChkShowSpecials.Size = new System.Drawing.Size(120, 17);
            this.ChkShowSpecials.TabIndex = 18;
            this.ChkShowSpecials.Text = "Show Special Icons";
            this.ChkShowSpecials.UseVisualStyleBackColor = true;
            this.ChkShowSpecials.CheckedChanged += new System.EventHandler(this.ChkShowSpecials_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 496);
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
            this.ChkBlockProperties.Location = new System.Drawing.Point(437, 345);
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
            this.LblHexGraphics1.Location = new System.Drawing.Point(207, 29);
            this.LblHexGraphics1.Name = "LblHexGraphics1";
            this.LblHexGraphics1.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics1.TabIndex = 12;
            this.LblHexGraphics1.Text = "label1";
            // 
            // LblHexGraphics2
            // 
            this.LblHexGraphics2.AutoSize = true;
            this.LblHexGraphics2.Location = new System.Drawing.Point(204, 349);
            this.LblHexGraphics2.Name = "LblHexGraphics2";
            this.LblHexGraphics2.Size = new System.Drawing.Size(35, 13);
            this.LblHexGraphics2.TabIndex = 13;
            this.LblHexGraphics2.Text = "label2";
            // 
            // CmbGraphics2
            // 
            this.CmbGraphics2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics2.FormattingEnabled = true;
            this.CmbGraphics2.Location = new System.Drawing.Point(3, 346);
            this.CmbGraphics2.Name = "CmbGraphics2";
            this.CmbGraphics2.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics2.TabIndex = 2;
            this.CmbGraphics2.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics2_SelectedIndexChanged);
            // 
            // CmbGraphics1
            // 
            this.CmbGraphics1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGraphics1.FormattingEnabled = true;
            this.CmbGraphics1.Location = new System.Drawing.Point(6, 23);
            this.CmbGraphics1.Name = "CmbGraphics1";
            this.CmbGraphics1.Size = new System.Drawing.Size(195, 21);
            this.CmbGraphics1.TabIndex = 1;
            this.CmbGraphics1.SelectedIndexChanged += new System.EventHandler(this.CmbGraphics1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Object Set";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Pattern Table 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Pattern Table 2";
            // 
            // PnlHelp
            // 
            this.PnlHelp.Controls.Add(this.BtnHelp);
            this.PnlHelp.Controls.Add(this.GrpHelp);
            this.PnlHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHelp.Location = new System.Drawing.Point(0, 0);
            this.PnlHelp.Name = "PnlHelp";
            this.PnlHelp.Size = new System.Drawing.Size(618, 110);
            this.PnlHelp.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.CmbGraphics2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.LblHexGraphics1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.CmbDefinitions);
            this.panel2.Controls.Add(this.BtnSaveClose);
            this.panel2.Controls.Add(this.CmbGraphics1);
            this.panel2.Controls.Add(this.LblHexGraphics2);
            this.panel2.Controls.Add(this.ChkBlockProperties);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.ChkShowSpecials);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 110);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 558);
            this.panel2.TabIndex = 22;
            // 
            // GrpHelp
            // 
            this.GrpHelp.Controls.Add(this.label5);
            this.GrpHelp.Location = new System.Drawing.Point(3, 2);
            this.GrpHelp.Name = "GrpHelp";
            this.GrpHelp.Size = new System.Drawing.Size(533, 100);
            this.GrpHelp.TabIndex = 22;
            this.GrpHelp.TabStop = false;
            this.GrpHelp.Text = "Instructions";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(519, 78);
            this.label5.TabIndex = 21;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // BtnHelp
            // 
            this.BtnHelp.Location = new System.Drawing.Point(542, 3);
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.Size = new System.Drawing.Size(73, 23);
            this.BtnHelp.TabIndex = 23;
            this.BtnHelp.Text = "Hide Help";
            this.BtnHelp.UseVisualStyleBackColor = true;
            this.BtnHelp.Click += new System.EventHandler(this.button2_Click);
            // 
            // BlsFrom
            // 
            this.BlsFrom.BlockLayout = null;
            this.BlsFrom.CurrentDefiniton = null;
            this.BlsFrom.HaltRendering = false;
            this.BlsFrom.Location = new System.Drawing.Point(15, 19);
            this.BlsFrom.Name = "BlsFrom";
            this.BlsFrom.SelectedIndex = 0;
            this.BlsFrom.SelectedTileIndex = 0;
            this.BlsFrom.ShowBlockSolidity = false;
            this.BlsFrom.ShowSpecialBlocks = false;
            this.BlsFrom.Size = new System.Drawing.Size(256, 256);
            this.BlsFrom.SpecialDefnitions = null;
            this.BlsFrom.SpecialTable = null;
            this.BlsFrom.TabIndex = 14;
            this.BlsFrom.Text = "blockSelector1";
            this.BlsFrom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsFrom_MouseMove);
            // 
            // PlsView
            // 
            this.PlsView.CurrentPalette = null;
            this.PlsView.Location = new System.Drawing.Point(15, 52);
            this.PlsView.Name = "PlsView";
            this.PlsView.SelectablePaletteMode = false;
            this.PlsView.Size = new System.Drawing.Size(256, 32);
            this.PlsView.TabIndex = 4;
            this.PlsView.Text = "paletteSelector1";
            // 
            // BlsTo
            // 
            this.BlsTo.BlockLayout = null;
            this.BlsTo.CurrentDefiniton = null;
            this.BlsTo.HaltRendering = false;
            this.BlsTo.Location = new System.Drawing.Point(15, 19);
            this.BlsTo.Name = "BlsTo";
            this.BlsTo.SelectedIndex = 0;
            this.BlsTo.SelectedTileIndex = 0;
            this.BlsTo.ShowBlockSolidity = false;
            this.BlsTo.ShowSpecialBlocks = false;
            this.BlsTo.Size = new System.Drawing.Size(256, 256);
            this.BlsTo.SpecialDefnitions = null;
            this.BlsTo.SpecialTable = null;
            this.BlsTo.TabIndex = 5;
            this.BlsTo.Text = "blockSelector1";
            this.BlsTo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlsTo_MouseMove);
            this.BlsTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlsTo_MouseDown);
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 668);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PnlHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LayoutEditor";
            this.Text = "Custom Layout Manager";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.PnlHelp.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GrpHelp.ResumeLayout(false);
            this.GrpHelp.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel PnlHelp;
        private System.Windows.Forms.Button BtnHelp;
        private System.Windows.Forms.GroupBox GrpHelp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
    }
}