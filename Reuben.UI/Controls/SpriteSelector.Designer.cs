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
            this.filter = new System.Windows.Forms.TextBox();
            this.groupFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.graphicsFilter = new System.Windows.Forms.ComboBox();
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.mouseCap = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sprites = new Reuben.UI.Controls.SpriteListViewer();
            this.scrollPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(11, 29);
            this.filter.Margin = new System.Windows.Forms.Padding(4);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(196, 20);
            this.filter.TabIndex = 1;
            this.filter.TextChanged += new System.EventHandler(this.filter_TextChanged);
            // 
            // groupFilter
            // 
            this.groupFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupFilter.FormattingEnabled = true;
            this.groupFilter.Location = new System.Drawing.Point(11, 74);
            this.groupFilter.Margin = new System.Windows.Forms.Padding(4);
            this.groupFilter.Name = "groupFilter";
            this.groupFilter.Size = new System.Drawing.Size(110, 21);
            this.groupFilter.TabIndex = 3;
            this.groupFilter.SelectedIndexChanged += new System.EventHandler(this.groupFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Groups";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Graphics Section";
            // 
            // graphicsFilter
            // 
            this.graphicsFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphicsFilter.FormattingEnabled = true;
            this.graphicsFilter.Items.AddRange(new object[] {
            "Any",
            "Global",
            "Bank 3",
            "Bank 4"});
            this.graphicsFilter.Location = new System.Drawing.Point(129, 74);
            this.graphicsFilter.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsFilter.Name = "graphicsFilter";
            this.graphicsFilter.Size = new System.Drawing.Size(110, 21);
            this.graphicsFilter.TabIndex = 5;
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.mouseCap);
            this.scrollPanel.Controls.Add(this.sprites);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 109);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(279, 261);
            this.scrollPanel.TabIndex = 7;
            this.scrollPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // mouseCap
            // 
            this.mouseCap.Location = new System.Drawing.Point(176, 0);
            this.mouseCap.Name = "mouseCap";
            this.mouseCap.Size = new System.Drawing.Size(0, 0);
            this.mouseCap.TabIndex = 1;
            this.mouseCap.Text = "FOCUS!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Filter";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.filter);
            this.panel2.Controls.Add(this.groupFilter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.graphicsFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4);
            this.panel2.Size = new System.Drawing.Size(279, 109);
            this.panel2.TabIndex = 9;
            // 
            // sprites
            // 
            this.sprites.GraphicsSet = 0;
            this.sprites.Group = null;
            this.sprites.Location = new System.Drawing.Point(0, 0);
            this.sprites.Margin = new System.Windows.Forms.Padding(0);
            this.sprites.Name = "sprites";
            this.sprites.Size = new System.Drawing.Size(256, 18);
            this.sprites.TabIndex = 0;
            this.sprites.Text = "spritesViewer1";
            this.sprites.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpriteSelector_MouseDown);
            // 
            // SpriteSelector
            // 
            this.Controls.Add(this.scrollPanel);
            this.Controls.Add(this.panel2);
            this.Name = "SpriteSelector";
            this.Size = new System.Drawing.Size(279, 370);
            this.scrollPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SpriteListViewer sprites;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.ComboBox groupFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox graphicsFilter;
        private System.Windows.Forms.Panel scrollPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel mouseCap;
        private System.Windows.Forms.Panel panel2;
    }
}