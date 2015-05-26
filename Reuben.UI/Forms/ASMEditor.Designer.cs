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
            this.panel1 = new System.Windows.Forms.Panel();
            this.asmFiles = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.filesOpened = new System.Windows.Forms.TabControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.codeTag = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.asmFiles);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 623);
            this.panel1.TabIndex = 1;
            // 
            // asmFiles
            // 
            this.asmFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asmFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.asmFiles.HideSelection = false;
            this.asmFiles.LabelWrap = false;
            this.asmFiles.Location = new System.Drawing.Point(0, 39);
            this.asmFiles.Margin = new System.Windows.Forms.Padding(4);
            this.asmFiles.Name = "asmFiles";
            this.asmFiles.Size = new System.Drawing.Size(177, 580);
            this.asmFiles.TabIndex = 4;
            this.asmFiles.UseCompatibleStateImageBehavior = false;
            this.asmFiles.View = System.Windows.Forms.View.Details;
            this.asmFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.asmFiles_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 39);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code Files";
            // 
            // filesOpened
            // 
            this.filesOpened.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesOpened.Location = new System.Drawing.Point(181, 41);
            this.filesOpened.Name = "filesOpened";
            this.filesOpened.SelectedIndex = 0;
            this.filesOpened.Size = new System.Drawing.Size(604, 582);
            this.filesOpened.TabIndex = 3;
            this.filesOpened.SelectedIndexChanged += new System.EventHandler(this.filesOpened_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.codeTag);
            this.panel3.Controls.Add(this.closeButton);
            this.panel3.Controls.Add(this.saveButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(181, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 41);
            this.panel3.TabIndex = 4;
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(7, 8);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // closeButton
            // 
            this.closeButton.Enabled = false;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(516, 8);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // codeTag
            // 
            this.codeTag.Cursor = System.Windows.Forms.Cursors.Default;
            this.codeTag.Location = new System.Drawing.Point(89, 10);
            this.codeTag.Name = "codeTag";
            this.codeTag.Size = new System.Drawing.Size(100, 20);
            this.codeTag.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
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
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl filesOpened;
        private System.Windows.Forms.ListView asmFiles;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox codeTag;
    }
}