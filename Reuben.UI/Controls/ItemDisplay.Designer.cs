namespace Reuben.UI
{
    partial class ItemDisplay
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.header = new Reuben.UI.MetroHeader();
            this.hostPanel = new MetroFramework.Controls.MetroPanel();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.hostPanel);
            this.metroPanel1.Controls.Add(this.header);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(310, 230);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(310, 19);
            this.header.TabIndex = 2;
            this.header.Text = "metroHeader1";
            // 
            // hostPanel
            // 
            this.hostPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostPanel.HorizontalScrollbarBarColor = true;
            this.hostPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.hostPanel.HorizontalScrollbarSize = 10;
            this.hostPanel.Location = new System.Drawing.Point(0, 19);
            this.hostPanel.Name = "hostPanel";
            this.hostPanel.Size = new System.Drawing.Size(310, 211);
            this.hostPanel.TabIndex = 3;
            this.hostPanel.VerticalScrollbarBarColor = true;
            this.hostPanel.VerticalScrollbarHighlightOnWheel = false;
            this.hostPanel.VerticalScrollbarSize = 10;
            // 
            // ItemDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel1);
            this.Name = "ItemDisplay";
            this.Size = new System.Drawing.Size(310, 230);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroHeader header;
        private MetroFramework.Controls.MetroPanel hostPanel;
    }
}
