using Reuben.Controls.Liquid;

namespace Daiz.NES.Reuben
{
    partial class ControlTest
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
            this.liquidButton1 = new LiquidButton();
            this.SuspendLayout();
            // 
            // liquidButton1
            // 
            this.liquidButton1.Location = new System.Drawing.Point(13, 13);
            this.liquidButton1.Name = "liquidButton1";
            this.liquidButton1.Size = new System.Drawing.Size(75, 23);
            this.liquidButton1.TabIndex = 0;
            this.liquidButton1.Text = "liquidButton1";
            this.liquidButton1.UseVisualStyleBackColor = true;
            // 
            // ControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.liquidButton1);
            this.Name = "ControlTest";
            this.Text = "ControlTest";
            this.ResumeLayout(false);

        }

        #endregion

        private LiquidButton liquidButton1;
    }
}