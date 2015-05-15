namespace Reuben.UI
{
    partial class BlockSelector
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
            this.blocks = new Reuben.UI.Controls.BlocksViewer();
            this.SuspendLayout();
            // 
            // blocks
            // 
            this.blocks.Location = new System.Drawing.Point(-2, 0);
            this.blocks.Name = "blocks";
            this.blocks.Size = new System.Drawing.Size(256, 256);
            this.blocks.TabIndex = 0;
            this.blocks.Text = "blocksViewer1";
            this.blocks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blocks_MouseDown);
            // 
            // BlockSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 256);
            this.Controls.Add(this.blocks);
            this.Name = "BlockSelector";
            this.Text = "Blocks";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BlocksViewer blocks;
    }
}