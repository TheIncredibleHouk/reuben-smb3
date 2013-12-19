namespace Daiz.NES.Reuben
{
    partial class WorldPointerEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblPointsToLevel = new System.Windows.Forms.Label();
            this.BtnChange = new System.Windows.Forms.Button();
            this.LblPointsToWorld = new System.Windows.Forms.Label();
            this.BtnOpenLevel = new System.Windows.Forms.Button();
            this.LblXEnter = new System.Windows.Forms.Label();
            this.LblYEnter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblPointsToLevel
            // 
            this.LblPointsToLevel.AutoSize = true;
            this.LblPointsToLevel.Location = new System.Drawing.Point(9, 26);
            this.LblPointsToLevel.Margin = new System.Windows.Forms.Padding(3);
            this.LblPointsToLevel.Name = "LblPointsToLevel";
            this.LblPointsToLevel.Size = new System.Drawing.Size(66, 13);
            this.LblPointsToLevel.TabIndex = 26;
            this.LblPointsToLevel.Text = "Points to 1-1";
            // 
            // BtnChange
            // 
            this.BtnChange.Location = new System.Drawing.Point(7, 48);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(88, 23);
            this.BtnChange.TabIndex = 28;
            this.BtnChange.Text = "Change Level";
            this.BtnChange.UseVisualStyleBackColor = true;
            this.BtnChange.Click += new System.EventHandler(this.BtnChange_Click);
            // 
            // LblPointsToWorld
            // 
            this.LblPointsToWorld.AutoSize = true;
            this.LblPointsToWorld.Location = new System.Drawing.Point(9, 7);
            this.LblPointsToWorld.Margin = new System.Windows.Forms.Padding(3);
            this.LblPointsToWorld.Name = "LblPointsToWorld";
            this.LblPointsToWorld.Size = new System.Drawing.Size(66, 13);
            this.LblPointsToWorld.TabIndex = 36;
            this.LblPointsToWorld.Text = "Points to 1-1";
            // 
            // BtnOpenLevel
            // 
            this.BtnOpenLevel.Location = new System.Drawing.Point(101, 48);
            this.BtnOpenLevel.Name = "BtnOpenLevel";
            this.BtnOpenLevel.Size = new System.Drawing.Size(88, 23);
            this.BtnOpenLevel.TabIndex = 37;
            this.BtnOpenLevel.Text = "Open Level";
            this.BtnOpenLevel.UseVisualStyleBackColor = true;
            this.BtnOpenLevel.Click += new System.EventHandler(this.BtnOpenLevel_Click);
            // 
            // LblXEnter
            // 
            this.LblXEnter.AutoSize = true;
            this.LblXEnter.Location = new System.Drawing.Point(9, 77);
            this.LblXEnter.Margin = new System.Windows.Forms.Padding(3);
            this.LblXEnter.Name = "LblXEnter";
            this.LblXEnter.Size = new System.Drawing.Size(34, 13);
            this.LblXEnter.TabIndex = 33;
            this.LblXEnter.Text = "X Exit";
            this.LblXEnter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblYEnter
            // 
            this.LblYEnter.AutoSize = true;
            this.LblYEnter.Location = new System.Drawing.Point(9, 96);
            this.LblYEnter.Margin = new System.Windows.Forms.Padding(3);
            this.LblYEnter.Name = "LblYEnter";
            this.LblYEnter.Size = new System.Drawing.Size(34, 13);
            this.LblYEnter.TabIndex = 34;
            this.LblYEnter.Text = "Y Exit";
            // 
            // WorldPointerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnOpenLevel);
            this.Controls.Add(this.LblPointsToWorld);
            this.Controls.Add(this.LblYEnter);
            this.Controls.Add(this.LblXEnter);
            this.Controls.Add(this.BtnChange);
            this.Controls.Add(this.LblPointsToLevel);
            this.Name = "WorldPointerEditor";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(239, 133);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPointsToLevel;
        private System.Windows.Forms.Button BtnChange;
        private System.Windows.Forms.Label LblPointsToWorld;
        private System.Windows.Forms.Button BtnOpenLevel;
        private System.Windows.Forms.Label LblXEnter;
        private System.Windows.Forms.Label LblYEnter;
    }
}
