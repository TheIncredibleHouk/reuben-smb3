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
            this.selectedPalette = new Reuben.UI.Controls.PaletteView();
            this.colorView = new Reuben.UI.Controls.ColorView();
            this.allPalettes = new Reuben.UI.Controls.PaletteList();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Palettes";
            // 
            // selectedPalette
            // 
            this.selectedPalette.ColorReference = null;
            this.selectedPalette.Location = new System.Drawing.Point(12, 71);
            this.selectedPalette.Name = "selectedPalette";
            this.selectedPalette.Size = new System.Drawing.Size(256, 32);
            this.selectedPalette.TabIndex = 3;
            this.selectedPalette.Text = "paletteView1";
            // 
            // colorView
            // 
            this.colorView.Location = new System.Drawing.Point(13, 128);
            this.colorView.Margin = new System.Windows.Forms.Padding(4);
            this.colorView.Name = "colorView";
            this.colorView.Size = new System.Drawing.Size(256, 64);
            this.colorView.TabIndex = 2;
            this.colorView.Text = "colorView1";
            // 
            // allPalettes
            // 
            this.allPalettes.ColorReference = null;
            this.allPalettes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.allPalettes.DropDownHeight = 200;
            this.allPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allPalettes.DropDownWidth = 288;
            this.allPalettes.FormattingEnabled = true;
            this.allPalettes.IntegralHeight = false;
            this.allPalettes.Location = new System.Drawing.Point(66, 13);
            this.allPalettes.Margin = new System.Windows.Forms.Padding(4);
            this.allPalettes.Name = "allPalettes";
            this.allPalettes.Palettes = null;
            this.allPalettes.Size = new System.Drawing.Size(203, 21);
            this.allPalettes.TabIndex = 0;
            this.allPalettes.SelectedIndexChanged += new System.EventHandler(this.allPalettes_SelectedIndexChanged);
            // 
            // PaletteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 259);
            this.Controls.Add(this.selectedPalette);
            this.Controls.Add(this.colorView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allPalettes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PaletteManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PaletteManager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.PaletteList allPalettes;
        private System.Windows.Forms.Label label1;
        private Controls.ColorView colorView;
        private Controls.PaletteView selectedPalette;
    }
}