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
            this.filter = new MetroFramework.Controls.MetroTextBox();
            this.groupFilter = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.graphicsFilter = new MetroFramework.Controls.MetroComboBox();
            this.scrollPanel = new MetroFramework.Controls.MetroPanel();
            this.mouseCap = new System.Windows.Forms.LinkLabel();
            this.sprites = new Reuben.UI.Controls.SpriteListViewer();
            this.panel2 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.scrollPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // filter
            // 
            this.filter.Lines = new string[0];
            this.filter.Location = new System.Drawing.Point(11, 94);
            this.filter.Margin = new System.Windows.Forms.Padding(4);
            this.filter.MaxLength = 32767;
            this.filter.Name = "filter";
            this.filter.PasswordChar = '\0';
            this.filter.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.filter.SelectedText = "";
            this.filter.Size = new System.Drawing.Size(258, 22);
            this.filter.TabIndex = 1;
            this.filter.UseSelectable = true;
            this.filter.TextChanged += new System.EventHandler(this.filter_TextChanged);
            // 
            // groupFilter
            // 
            this.groupFilter.FormattingEnabled = true;
            this.groupFilter.ItemHeight = 23;
            this.groupFilter.Location = new System.Drawing.Point(11, 30);
            this.groupFilter.Margin = new System.Windows.Forms.Padding(4);
            this.groupFilter.Name = "groupFilter";
            this.groupFilter.Size = new System.Drawing.Size(125, 29);
            this.groupFilter.TabIndex = 3;
            this.groupFilter.UseSelectable = true;
            this.groupFilter.SelectedIndexChanged += new System.EventHandler(this.groupFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Groups";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Graphics Section";
            // 
            // graphicsFilter
            // 
            this.graphicsFilter.FormattingEnabled = true;
            this.graphicsFilter.ItemHeight = 23;
            this.graphicsFilter.Items.AddRange(new object[] {
            "Any",
            "Global",
            "Bank 3",
            "Bank 4"});
            this.graphicsFilter.Location = new System.Drawing.Point(144, 30);
            this.graphicsFilter.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsFilter.Name = "graphicsFilter";
            this.graphicsFilter.Size = new System.Drawing.Size(125, 29);
            this.graphicsFilter.TabIndex = 5;
            this.graphicsFilter.UseSelectable = true;
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.mouseCap);
            this.scrollPanel.Controls.Add(this.sprites);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.HorizontalScrollbar = true;
            this.scrollPanel.HorizontalScrollbarBarColor = true;
            this.scrollPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.scrollPanel.HorizontalScrollbarSize = 10;
            this.scrollPanel.Location = new System.Drawing.Point(0, 125);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(279, 245);
            this.scrollPanel.TabIndex = 7;
            this.scrollPanel.VerticalScrollbar = true;
            this.scrollPanel.VerticalScrollbarBarColor = true;
            this.scrollPanel.VerticalScrollbarHighlightOnWheel = false;
            this.scrollPanel.VerticalScrollbarSize = 10;
            this.scrollPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // mouseCap
            // 
            this.mouseCap.Location = new System.Drawing.Point(176, 0);
            this.mouseCap.Name = "mouseCap";
            this.mouseCap.Size = new System.Drawing.Size(0, 0);
            this.mouseCap.TabIndex = 1;
            this.mouseCap.TabStop = true;
            this.mouseCap.Text = "FOCUS!";
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
            // panel2
            // 
            this.panel2.Controls.Add(this.metroLabel1);
            this.panel2.Controls.Add(this.filter);
            this.panel2.Controls.Add(this.groupFilter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.graphicsFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.HorizontalScrollbarBarColor = true;
            this.panel2.HorizontalScrollbarHighlightOnWheel = false;
            this.panel2.HorizontalScrollbarSize = 10;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4);
            this.panel2.Size = new System.Drawing.Size(279, 125);
            this.panel2.TabIndex = 9;
            this.panel2.VerticalScrollbarBarColor = true;
            this.panel2.VerticalScrollbarHighlightOnWheel = false;
            this.panel2.VerticalScrollbarSize = 10;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(8, 67);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(51, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Groups";
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
        private MetroFramework.Controls.MetroTextBox filter;
        private MetroFramework.Controls.MetroComboBox groupFilter;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroComboBox graphicsFilter;
        private MetroFramework.Controls.MetroPanel scrollPanel;
        private System.Windows.Forms.LinkLabel mouseCap;
        private MetroFramework.Controls.MetroPanel panel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
    }
}