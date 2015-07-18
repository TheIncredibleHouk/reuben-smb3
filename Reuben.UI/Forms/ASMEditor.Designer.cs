namespace Reuben.UI
{
    partial class ASMEditor
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new MetroFramework.Controls.MetroPanel();
            this.asmFiles = new System.Windows.Forms.ListView();
            this.panel2 = new MetroFramework.Controls.MetroPanel();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.filesOpened = new System.Windows.Forms.TabControl();
            this.panel3 = new MetroFramework.Controls.MetroPanel();
            this.panel4 = new MetroFramework.Controls.MetroPanel();
            this.closeButton = new MetroFramework.Controls.MetroButton();
            this.saveButton = new MetroFramework.Controls.MetroButton();
            this.asmTextContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.asmTextContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.asmFiles);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.HorizontalScrollbarBarColor = true;
            this.panel1.HorizontalScrollbarHighlightOnWheel = false;
            this.panel1.HorizontalScrollbarSize = 10;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 623);
            this.panel1.TabIndex = 1;
            this.panel1.VerticalScrollbarBarColor = true;
            this.panel1.VerticalScrollbarHighlightOnWheel = false;
            this.panel1.VerticalScrollbarSize = 10;
            // 
            // asmFiles
            // 
            this.asmFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asmFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.asmFiles.HideSelection = false;
            this.asmFiles.LabelWrap = false;
            this.asmFiles.Location = new System.Drawing.Point(0, 51);
            this.asmFiles.Margin = new System.Windows.Forms.Padding(4);
            this.asmFiles.Name = "asmFiles";
            this.asmFiles.Size = new System.Drawing.Size(177, 568);
            this.asmFiles.TabIndex = 4;
            this.asmFiles.UseCompatibleStateImageBehavior = false;
            this.asmFiles.View = System.Windows.Forms.View.Details;
            this.asmFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.asmFiles_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.HorizontalScrollbarBarColor = true;
            this.panel2.HorizontalScrollbarHighlightOnWheel = false;
            this.panel2.HorizontalScrollbarSize = 10;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 51);
            this.panel2.TabIndex = 5;
            this.panel2.VerticalScrollbarBarColor = true;
            this.panel2.VerticalScrollbarHighlightOnWheel = false;
            this.panel2.VerticalScrollbarSize = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code Files";
            // 
            // filesOpened
            // 
            this.filesOpened.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesOpened.Location = new System.Drawing.Point(181, 0);
            this.filesOpened.Name = "filesOpened";
            this.filesOpened.SelectedIndex = 0;
            this.filesOpened.Size = new System.Drawing.Size(604, 588);
            this.filesOpened.TabIndex = 3;
            this.filesOpened.SelectedIndexChanged += new System.EventHandler(this.filesOpened_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.HorizontalScrollbarBarColor = true;
            this.panel3.HorizontalScrollbarHighlightOnWheel = false;
            this.panel3.HorizontalScrollbarSize = 10;
            this.panel3.Location = new System.Drawing.Point(181, 588);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 35);
            this.panel3.TabIndex = 4;
            this.panel3.VerticalScrollbarBarColor = true;
            this.panel3.VerticalScrollbarHighlightOnWheel = false;
            this.panel3.VerticalScrollbarSize = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.closeButton);
            this.panel4.Controls.Add(this.saveButton);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.HorizontalScrollbarBarColor = true;
            this.panel4.HorizontalScrollbarHighlightOnWheel = false;
            this.panel4.HorizontalScrollbarSize = 10;
            this.panel4.Location = new System.Drawing.Point(437, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 31);
            this.panel4.TabIndex = 3;
            this.panel4.VerticalScrollbarBarColor = true;
            this.panel4.VerticalScrollbarHighlightOnWheel = false;
            this.panel4.VerticalScrollbarSize = 10;
            // 
            // closeButton
            // 
            this.closeButton.Enabled = false;
            this.closeButton.Location = new System.Drawing.Point(87, 4);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseSelectable = true;
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(4, 4);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseSelectable = true;
            this.saveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // asmTextContext
            // 
            this.asmTextContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToLabelToolStripMenuItem});
            this.asmTextContext.Name = "asmTextContext";
            this.asmTextContext.Size = new System.Drawing.Size(135, 26);
            // 
            // goToLabelToolStripMenuItem
            // 
            this.goToLabelToolStripMenuItem.Name = "goToLabelToolStripMenuItem";
            this.goToLabelToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.goToLabelToolStripMenuItem.Text = "Go to Label";
            this.goToLabelToolStripMenuItem.Click += new System.EventHandler(this.goToLabelToolStripMenuItem_Click);
            // 
            // ASMEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 623);
            this.Controls.Add(this.filesOpened);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "ASMEditor";
            this.Text = "ASMEditor";
            this.Deactivate += new System.EventHandler(this.ASMEditor_Deactivate);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.asmTextContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel panel1;
        private System.Windows.Forms.TabControl filesOpened;
        private System.Windows.Forms.ListView asmFiles;
        private MetroFramework.Controls.MetroPanel panel2;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroPanel panel3;
        private MetroFramework.Controls.MetroButton closeButton;
        private MetroFramework.Controls.MetroButton saveButton;
        private MetroFramework.Controls.MetroPanel panel4;
        private System.Windows.Forms.ContextMenuStrip asmTextContext;
        private System.Windows.Forms.ToolStripMenuItem goToLabelToolStripMenuItem;
    }
}