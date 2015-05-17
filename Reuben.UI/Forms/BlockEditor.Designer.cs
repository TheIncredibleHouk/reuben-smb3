namespace Reuben.UI.Forms
{
    partial class BlockEditor
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
            this.patternTableView1 = new Reuben.UI.PatternTableView();
            this.blockSelector1 = new Reuben.UI.BlockSelector();
            this.SuspendLayout();
            // 
            // patternTableView1
            // 
            this.patternTableView1.Location = new System.Drawing.Point(13, 13);
            this.patternTableView1.Name = "patternTableView1";
            this.patternTableView1.Size = new System.Drawing.Size(256, 256);
            this.patternTableView1.TabIndex = 0;
            this.patternTableView1.Text = "patternTableView1";
            // 
            // blockSelector1
            // 
            this.blockSelector1.Location = new System.Drawing.Point(415, 12);
            this.blockSelector1.Name = "blockSelector1";
            this.blockSelector1.Size = new System.Drawing.Size(254, 256);
            this.blockSelector1.TabIndex = 1;
            // 
            // BlockEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 315);
            this.Controls.Add(this.blockSelector1);
            this.Controls.Add(this.patternTableView1);
            this.Name = "BlockEditor";
            this.Text = "BlockEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private PatternTableView patternTableView1;
        private BlockSelector blockSelector1;
    }
}