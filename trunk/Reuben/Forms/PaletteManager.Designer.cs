namespace Daiz.NES.Reuben
{
    partial class PaletteManager
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.LblSelected = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnRename = new System.Windows.Forms.Button();
            this.FpsFull = new Daiz.NES.Reuben.FullPaletteSelector();
            this.PslCurrent = new Daiz.NES.Reuben.PaletteSelector();
            this.BtnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbPalettes
            // 
            this.CmbPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPalettes.FormattingEnabled = true;
            this.CmbPalettes.Location = new System.Drawing.Point(27, 46);
            this.CmbPalettes.Name = "CmbPalettes";
            this.CmbPalettes.Size = new System.Drawing.Size(256, 21);
            this.CmbPalettes.TabIndex = 0;
            this.CmbPalettes.SelectedIndexChanged += new System.EventHandler(this.CmbPalettes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 196);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sprite Colors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 136);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(76, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Object Colors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 218);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(65, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Full Palette";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Available Palettes";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(88, 73);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(51, 23);
            this.BtnAdd.TabIndex = 10;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnRemove
            // 
            this.BtnRemove.Enabled = false;
            this.BtnRemove.Location = new System.Drawing.Point(145, 73);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(59, 23);
            this.BtnRemove.TabIndex = 11;
            this.BtnRemove.Text = "Remove";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // LblSelected
            // 
            this.LblSelected.AutoSize = true;
            this.LblSelected.Location = new System.Drawing.Point(24, 310);
            this.LblSelected.Margin = new System.Windows.Forms.Padding(3);
            this.LblSelected.Name = "LblSelected";
            this.LblSelected.Padding = new System.Windows.Forms.Padding(3);
            this.LblSelected.Size = new System.Drawing.Size(58, 19);
            this.LblSelected.TabIndex = 13;
            this.LblSelected.Text = "Selected:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(27, 335);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 53);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Help";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "Hold Ctrl when applying a color to spread it to\r\nevery offset.";
            // 
            // BtnRename
            // 
            this.BtnRename.Enabled = false;
            this.BtnRename.Location = new System.Drawing.Point(208, 73);
            this.BtnRename.Name = "BtnRename";
            this.BtnRename.Size = new System.Drawing.Size(75, 23);
            this.BtnRename.TabIndex = 16;
            this.BtnRename.Text = "Rename";
            this.BtnRename.UseVisualStyleBackColor = true;
            this.BtnRename.Click += new System.EventHandler(this.BtnRename_Click);
            // 
            // FpsFull
            // 
            this.FpsFull.Location = new System.Drawing.Point(27, 240);
            this.FpsFull.Name = "FpsFull";
            this.FpsFull.SelectedColor = 0;
            this.FpsFull.Size = new System.Drawing.Size(256, 64);
            this.FpsFull.TabIndex = 7;
            this.FpsFull.Text = "fullPaletteSelector1";
            // 
            // PslCurrent
            // 
            this.PslCurrent.CurrentPalette = null;
            this.PslCurrent.Location = new System.Drawing.Point(27, 158);
            this.PslCurrent.Name = "PslCurrent";
            this.PslCurrent.SelectablePaletteMode = false;
            this.PslCurrent.Size = new System.Drawing.Size(256, 32);
            this.PslCurrent.TabIndex = 4;
            this.PslCurrent.Text = "paletteSelector1";
            this.PslCurrent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PslCurrent_MouseDown);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(189, 399);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(94, 23);
            this.BtnClose.TabIndex = 17;
            this.BtnClose.Text = "Save && Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PaletteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 434);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnRename);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblSelected);
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FpsFull);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PslCurrent);
            this.Controls.Add(this.CmbPalettes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PaletteManager";
            this.Text = "Palette Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbPalettes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PaletteSelector PslCurrent;
        private FullPaletteSelector FpsFull;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.Label LblSelected;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnRename;
        private System.Windows.Forms.Button BtnClose;
    }
}