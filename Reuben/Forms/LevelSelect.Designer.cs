namespace Daiz.NES.Reuben
{
    partial class LevelSelect
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
            this.LbxWorlds = new System.Windows.Forms.ListBox();
            this.LbxLevels = new System.Windows.Forms.ListBox();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LbxWorlds
            // 
            this.LbxWorlds.FormattingEnabled = true;
            this.LbxWorlds.Location = new System.Drawing.Point(12, 12);
            this.LbxWorlds.Name = "LbxWorlds";
            this.LbxWorlds.Size = new System.Drawing.Size(158, 173);
            this.LbxWorlds.TabIndex = 0;
            this.LbxWorlds.SelectedIndexChanged += new System.EventHandler(this.LbxWorlds_SelectedIndexChanged);
            // 
            // LbxLevels
            // 
            this.LbxLevels.FormattingEnabled = true;
            this.LbxLevels.Location = new System.Drawing.Point(176, 12);
            this.LbxLevels.Name = "LbxLevels";
            this.LbxLevels.Size = new System.Drawing.Size(174, 173);
            this.LbxLevels.TabIndex = 1;
            this.LbxLevels.SelectedIndexChanged += new System.EventHandler(this.LbxLevels_SelectedIndexChanged);
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(189, 191);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(75, 23);
            this.BtnSelect.TabIndex = 2;
            this.BtnSelect.Text = "Select";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LevelSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 224);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.LbxLevels);
            this.Controls.Add(this.LbxWorlds);
            this.Name = "LevelSelect";
            this.Text = "LevelSelect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LbxWorlds;
        private System.Windows.Forms.ListBox LbxLevels;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Button button1;

    }
}