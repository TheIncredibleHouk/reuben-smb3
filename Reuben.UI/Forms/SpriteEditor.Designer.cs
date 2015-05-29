namespace Reuben.UI
{
    partial class SpriteEditor
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
            this.editorPropertyList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.displayProperty = new System.Windows.Forms.ComboBox();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.spriteViewer = new Reuben.UI.SpriteViewer();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorPropertyList
            // 
            this.editorPropertyList.Location = new System.Drawing.Point(20, 474);
            this.editorPropertyList.Multiline = true;
            this.editorPropertyList.Name = "editorPropertyList";
            this.editorPropertyList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.editorPropertyList.Size = new System.Drawing.Size(306, 132);
            this.editorPropertyList.TabIndex = 1;
            this.editorPropertyList.TextChanged += new System.EventHandler(this.editorPropertyList_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 308);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Editor Name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 328);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(306, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 454);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Editor Properties";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.displayProperty);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.paletteList);
            this.panel1.Controls.Add(this.spriteViewer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.editorPropertyList);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(261, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 631);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 355);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Display Palette";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.spriteSelector);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 631);
            this.panel2.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 283);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Display Overlays";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 404);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Display Property";
            // 
            // displayProperty
            // 
            this.displayProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayProperty.FormattingEnabled = true;
            this.displayProperty.Location = new System.Drawing.Point(20, 425);
            this.displayProperty.Margin = new System.Windows.Forms.Padding(4);
            this.displayProperty.Name = "displayProperty";
            this.displayProperty.Size = new System.Drawing.Size(306, 21);
            this.displayProperty.TabIndex = 14;
            this.displayProperty.SelectedIndexChanged += new System.EventHandler(this.displayProperty_SelectedIndexChanged);
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(20, 376);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(306, 21);
            this.paletteList.TabIndex = 5;
            // 
            // spriteViewer
            // 
            this.spriteViewer.Location = new System.Drawing.Point(42, 10);
            this.spriteViewer.Name = "spriteViewer";
            this.spriteViewer.Size = new System.Drawing.Size(256, 256);
            this.spriteViewer.TabIndex = 0;
            this.spriteViewer.Text = "spriteViewer1";
            // 
            // spriteSelector
            // 
            this.spriteSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSelector.Location = new System.Drawing.Point(0, 0);
            this.spriteSelector.Name = "spriteSelector";
            this.spriteSelector.Size = new System.Drawing.Size(257, 627);
            this.spriteSelector.TabIndex = 0;
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 631);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "SpriteEditor";
            this.Text = "SpriteEditor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteViewer spriteViewer;
        private System.Windows.Forms.TextBox editorPropertyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SpriteSelector spriteSelector;
        private System.Windows.Forms.Label label3;
        private Controls.PaletteList paletteList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox displayProperty;
        private System.Windows.Forms.Label label7;

    }
}