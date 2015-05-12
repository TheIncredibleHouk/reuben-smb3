namespace Reuben.UI
{
    partial class SpriteSelector
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
            this.SuspendLayout();
            // 
            // SpriteSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 256);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SpriteSelector";
            this.Text = "Sprites";
            this.SizeChanged += new System.EventHandler(this.SpriteSelector_SizeChanged_1);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpriteSelector_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SpriteSelector_Move);
            this.Move += new System.EventHandler(this.SpriteSelector_Move);
            this.ResumeLayout(false);

        }

        #endregion
    }
}