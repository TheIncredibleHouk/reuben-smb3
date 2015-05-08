namespace Reuben.UI
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.paletteName = new System.Windows.Forms.TextBox();
            this.selectedPalette = new Reuben.UI.Controls.PaletteView();
            this.colorView = new Reuben.UI.Controls.ColorView();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Palettes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Selected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Color Choices";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 292);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(112, 292);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 63);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(112, 63);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 243);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Name";
            // 
            // paletteName
            // 
            this.paletteName.Enabled = false;
            this.paletteName.Location = new System.Drawing.Point(13, 264);
            this.paletteName.Margin = new System.Windows.Forms.Padding(4);
            this.paletteName.Name = "paletteName";
            this.paletteName.Size = new System.Drawing.Size(257, 20);
            this.paletteName.TabIndex = 12;
            this.paletteName.TextChanged += new System.EventHandler(this.paletteName_TextChanged_1);
            // 
            // selectedPalette
            // 
            this.selectedPalette.ColorReference = null;
            this.selectedPalette.Location = new System.Drawing.Point(12, 111);
            this.selectedPalette.Name = "selectedPalette";
            this.selectedPalette.Size = new System.Drawing.Size(256, 32);
            this.selectedPalette.TabIndex = 3;
            this.selectedPalette.Text = "paletteView1";
            this.selectedPalette.MouseClick += new System.Windows.Forms.MouseEventHandler(this.selectedPalette_MouseClick);
            // 
            // colorView
            // 
            this.colorView.Location = new System.Drawing.Point(13, 171);
            this.colorView.Margin = new System.Windows.Forms.Padding(4);
            this.colorView.Name = "colorView";
            this.colorView.SelectionPoint = new System.Drawing.Point(0, 0);
            this.colorView.Size = new System.Drawing.Size(256, 64);
            this.colorView.TabIndex = 2;
            this.colorView.Text = "colorView1";
            this.colorView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.colorView_MouseClick);
            // 
            // allPalettes
            // 
            this.paletteList.ColorReference = null;
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 200;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(13, 34);
            this.paletteList.Margin = new System.Windows.Forms.Padding(4);
            this.paletteList.Name = "allPalettes";
            this.paletteList.Palettes = null;
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(256, 21);
            this.paletteList.TabIndex = 0;
            this.paletteList.SelectedIndexChanged += new System.EventHandler(this.allPalettes_SelectedIndexChanged);
            // 
            // PaletteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 326);
            this.Controls.Add(this.paletteName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectedPalette);
            this.Controls.Add(this.colorView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paletteList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PaletteManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PaletteManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.PaletteList paletteList;
        private System.Windows.Forms.Label label1;
        private Controls.ColorView colorView;
        private Controls.PaletteView selectedPalette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox paletteName;
    }
}