namespace Reuben.UI.Forms
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.spriteViewer = new Reuben.UI.SpriteViewer();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.paletteList1 = new Reuben.UI.Controls.PaletteList();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 363);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(197, 132);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 296);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(21, 316);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 343);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Properties";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.paletteList1);
            this.panel1.Controls.Add(this.spriteViewer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(261, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 631);
            this.panel1.TabIndex = 5;
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
            // spriteViewer
            // 
            this.spriteViewer.Location = new System.Drawing.Point(21, 10);
            this.spriteViewer.Name = "spriteViewer";
            this.spriteViewer.Size = new System.Drawing.Size(388, 279);
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
            // paletteList1
            // 
            this.paletteList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList1.DropDownHeight = 400;
            this.paletteList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList1.DropDownWidth = 288;
            this.paletteList1.FormattingEnabled = true;
            this.paletteList1.IntegralHeight = false;
            this.paletteList1.Location = new System.Drawing.Point(278, 316);
            this.paletteList1.Name = "paletteList1";
            this.paletteList1.SelectedPalette = null;
            this.paletteList1.Size = new System.Drawing.Size(121, 21);
            this.paletteList1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 296);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(225, 295);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 200);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 631);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SpriteSelector spriteSelector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Controls.PaletteList paletteList1;

    }
}