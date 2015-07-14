namespace Reuben.UI
{
    partial class ProjectView
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.defaultsButton = new MetroFramework.Controls.MetroButton();
            this.palettesButton = new MetroFramework.Controls.MetroButton();
            this.blocksButton = new MetroFramework.Controls.MetroButton();
            this.asmButton = new MetroFramework.Controls.MetroButton();
            this.spritesButton = new MetroFramework.Controls.MetroButton();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.levelsTab = new System.Windows.Forms.TabPage();
            this.levelsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textButton = new MetroFramework.Controls.MetroButton();
            this.metroTabControl1.SuspendLayout();
            this.levelsTab.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(791, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(93, 268);
            this.panel3.TabIndex = 3;
            // 
            // defaultsButton
            // 
            this.defaultsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultsButton.Location = new System.Drawing.Point(14, 264);
            this.defaultsButton.Margin = new System.Windows.Forms.Padding(4);
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.Size = new System.Drawing.Size(469, 48);
            this.defaultsButton.TabIndex = 5;
            this.defaultsButton.Text = "Manage Default Settings";
            this.defaultsButton.UseSelectable = true;
            // 
            // palettesButton
            // 
            this.palettesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palettesButton.Location = new System.Drawing.Point(14, 114);
            this.palettesButton.Margin = new System.Windows.Forms.Padding(4);
            this.palettesButton.Name = "palettesButton";
            this.palettesButton.Size = new System.Drawing.Size(469, 42);
            this.palettesButton.TabIndex = 2;
            this.palettesButton.Text = "Manage Palettes";
            this.palettesButton.UseSelectable = true;
            this.palettesButton.Click += new System.EventHandler(this.palettesButton_Click);
            // 
            // blocksButton
            // 
            this.blocksButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blocksButton.Location = new System.Drawing.Point(14, 214);
            this.blocksButton.Margin = new System.Windows.Forms.Padding(4);
            this.blocksButton.Name = "blocksButton";
            this.blocksButton.Size = new System.Drawing.Size(469, 42);
            this.blocksButton.TabIndex = 4;
            this.blocksButton.Text = "Manage Block Layouts and Properties";
            this.blocksButton.UseSelectable = true;
            this.blocksButton.Click += new System.EventHandler(this.blocksButton_Click_1);
            // 
            // asmButton
            // 
            this.asmButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asmButton.Location = new System.Drawing.Point(14, 164);
            this.asmButton.Margin = new System.Windows.Forms.Padding(4);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(469, 42);
            this.asmButton.TabIndex = 3;
            this.asmButton.Tag = "";
            this.asmButton.Text = "Manage Game Assembly Source Code";
            this.asmButton.UseSelectable = true;
            this.asmButton.Click += new System.EventHandler(this.asmButton_Click_1);
            // 
            // spritesButton
            // 
            this.spritesButton.AutoSize = true;
            this.spritesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spritesButton.Location = new System.Drawing.Point(14, 64);
            this.spritesButton.Margin = new System.Windows.Forms.Padding(4);
            this.spritesButton.Name = "spritesButton";
            this.spritesButton.Size = new System.Drawing.Size(469, 42);
            this.spritesButton.TabIndex = 1;
            this.spritesButton.Text = "Manage Objects and Enemies";
            this.spritesButton.UseSelectable = true;
            this.spritesButton.Click += new System.EventHandler(this.spritesButton_Click_1);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.levelsTab);
            this.metroTabControl1.Controls.Add(this.tabPage5);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.Controls.Add(this.tabPage3);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 3;
            this.metroTabControl1.Size = new System.Drawing.Size(505, 368);
            this.metroTabControl1.TabIndex = 4;
            this.metroTabControl1.UseSelectable = true;
            // 
            // levelsTab
            // 
            this.levelsTab.AutoScroll = true;
            this.levelsTab.BackColor = System.Drawing.Color.Transparent;
            this.levelsTab.Controls.Add(this.levelsPanel);
            this.levelsTab.Location = new System.Drawing.Point(4, 38);
            this.levelsTab.Name = "levelsTab";
            this.levelsTab.Size = new System.Drawing.Size(497, 326);
            this.levelsTab.TabIndex = 0;
            this.levelsTab.Text = "Levels";
            // 
            // levelsPanel
            // 
            this.levelsPanel.AutoSize = true;
            this.levelsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.levelsPanel.ColumnCount = 1;
            this.levelsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.levelsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.levelsPanel.Location = new System.Drawing.Point(0, 0);
            this.levelsPanel.Name = "levelsPanel";
            this.levelsPanel.RowCount = 1;
            this.levelsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.levelsPanel.Size = new System.Drawing.Size(497, 0);
            this.levelsPanel.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.AutoScroll = true;
            this.tabPage5.Location = new System.Drawing.Point(4, 38);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(497, 326);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Project";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(497, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Worlds";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(497, 326);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Assets";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.defaultsButton, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.blocksButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.asmButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.palettesButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.spritesButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(497, 326);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textButton
            // 
            this.textButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textButton.Location = new System.Drawing.Point(13, 13);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(471, 44);
            this.textButton.TabIndex = 0;
            this.textButton.Text = "Manage List and Mapping Strings";
            this.textButton.UseSelectable = true;
            this.textButton.Click += new System.EventHandler(this.textButton_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel3);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(505, 368);
            this.metroTabControl1.ResumeLayout(false);
            this.levelsTab.ResumeLayout(false);
            this.levelsTab.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage levelsTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel levelsPanel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroButton defaultsButton;
        private MetroFramework.Controls.MetroButton blocksButton;
        private MetroFramework.Controls.MetroButton asmButton;
        private MetroFramework.Controls.MetroButton palettesButton;
        private MetroFramework.Controls.MetroButton spritesButton;
        private MetroFramework.Controls.MetroButton textButton;
    }
}
