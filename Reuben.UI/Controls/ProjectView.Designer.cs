namespace Reuben.UI.Controls
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
            this.projectTree = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.savebutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.projectName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.defaultsButton = new System.Windows.Forms.Button();
            this.textButton = new System.Windows.Forms.Button();
            this.asmButton = new System.Windows.Forms.Button();
            this.spritesButton = new System.Windows.Forms.Button();
            this.blocksButton = new System.Windows.Forms.Button();
            this.palettesButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectTree
            // 
            this.projectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTree.Location = new System.Drawing.Point(0, 0);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(263, 275);
            this.projectTree.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 93);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(263, 93);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.savebutton);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.projectName);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(255, 67);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // savebutton
            // 
            this.savebutton.Enabled = false;
            this.savebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebutton.Location = new System.Drawing.Point(170, 35);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(75, 23);
            this.savebutton.TabIndex = 3;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(89, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4);
            this.label1.Size = new System.Drawing.Size(43, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // projectName
            // 
            this.projectName.Enabled = false;
            this.projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectName.Location = new System.Drawing.Point(57, 8);
            this.projectName.Margin = new System.Windows.Forms.Padding(4);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(188, 20);
            this.projectName.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.defaultsButton);
            this.tabPage2.Controls.Add(this.textButton);
            this.tabPage2.Controls.Add(this.asmButton);
            this.tabPage2.Controls.Add(this.spritesButton);
            this.tabPage2.Controls.Add(this.blocksButton);
            this.tabPage2.Controls.Add(this.palettesButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(255, 67);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // defaultsButton
            // 
            this.defaultsButton.Enabled = false;
            this.defaultsButton.Location = new System.Drawing.Point(173, 37);
            this.defaultsButton.Margin = new System.Windows.Forms.Padding(4);
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.Size = new System.Drawing.Size(75, 23);
            this.defaultsButton.TabIndex = 5;
            this.defaultsButton.Text = "Defaults";
            this.defaultsButton.UseVisualStyleBackColor = true;
            // 
            // textButton
            // 
            this.textButton.Enabled = false;
            this.textButton.Location = new System.Drawing.Point(90, 37);
            this.textButton.Margin = new System.Windows.Forms.Padding(4);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(75, 23);
            this.textButton.TabIndex = 4;
            this.textButton.Text = "Text";
            this.textButton.UseVisualStyleBackColor = true;
            // 
            // asmButton
            // 
            this.asmButton.Enabled = false;
            this.asmButton.Location = new System.Drawing.Point(7, 37);
            this.asmButton.Margin = new System.Windows.Forms.Padding(4);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(75, 23);
            this.asmButton.TabIndex = 3;
            this.asmButton.Tag = "";
            this.asmButton.Text = "ASM";
            this.asmButton.UseVisualStyleBackColor = true;
            // 
            // spritesButton
            // 
            this.spritesButton.Enabled = false;
            this.spritesButton.Location = new System.Drawing.Point(173, 7);
            this.spritesButton.Margin = new System.Windows.Forms.Padding(4);
            this.spritesButton.Name = "spritesButton";
            this.spritesButton.Size = new System.Drawing.Size(75, 23);
            this.spritesButton.TabIndex = 2;
            this.spritesButton.Text = "Sprites";
            this.spritesButton.UseVisualStyleBackColor = true;
            // 
            // blocksButton
            // 
            this.blocksButton.Enabled = false;
            this.blocksButton.Location = new System.Drawing.Point(90, 7);
            this.blocksButton.Margin = new System.Windows.Forms.Padding(4);
            this.blocksButton.Name = "blocksButton";
            this.blocksButton.Size = new System.Drawing.Size(75, 23);
            this.blocksButton.TabIndex = 1;
            this.blocksButton.Text = "Blocks";
            this.blocksButton.UseVisualStyleBackColor = true;
            // 
            // palettesButton
            // 
            this.palettesButton.Enabled = false;
            this.palettesButton.Location = new System.Drawing.Point(7, 7);
            this.palettesButton.Margin = new System.Windows.Forms.Padding(4);
            this.palettesButton.Name = "palettesButton";
            this.palettesButton.Size = new System.Drawing.Size(75, 23);
            this.palettesButton.TabIndex = 0;
            this.palettesButton.Text = "Palettes";
            this.palettesButton.UseVisualStyleBackColor = true;
            this.palettesButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectTree);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(263, 368);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button defaultsButton;
        private System.Windows.Forms.Button textButton;
        private System.Windows.Forms.Button asmButton;
        private System.Windows.Forms.Button spritesButton;
        private System.Windows.Forms.Button blocksButton;
        private System.Windows.Forms.Button palettesButton;
    }
}
