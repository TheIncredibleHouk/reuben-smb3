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
            this.label1 = new System.Windows.Forms.Label();
            this.spriteName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.definitionCode = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.displayProperty = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.syntaxError = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.spriteViewer = new Reuben.UI.SpriteViewer();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 543);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Editor Name";
            // 
            // spriteName
            // 
            this.spriteName.Location = new System.Drawing.Point(20, 563);
            this.spriteName.Name = "spriteName";
            this.spriteName.Size = new System.Drawing.Size(174, 20);
            this.spriteName.TabIndex = 3;
            this.spriteName.TextChanged += new System.EventHandler(this.spriteName_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.syntaxError);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.definitionCode);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.displayProperty);
            this.panel1.Controls.Add(this.paletteList);
            this.panel1.Controls.Add(this.spriteName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.spriteViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(277, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 808);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 597);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Editor Drawing Code";
            // 
            // definitionCode
            // 
            this.definitionCode.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.definitionCode.Location = new System.Drawing.Point(20, 617);
            this.definitionCode.Multiline = true;
            this.definitionCode.Name = "definitionCode";
            this.definitionCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.definitionCode.Size = new System.Drawing.Size(535, 125);
            this.definitionCode.TabIndex = 20;
            this.definitionCode.WordWrap = false;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(480, 587);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 19;
            this.button10.Text = "Delete";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(397, 587);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "Add";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // displayProperty
            // 
            this.displayProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayProperty.FormattingEnabled = true;
            this.displayProperty.Location = new System.Drawing.Point(381, 562);
            this.displayProperty.Margin = new System.Windows.Forms.Padding(4);
            this.displayProperty.Name = "displayProperty";
            this.displayProperty.Size = new System.Drawing.Size(174, 21);
            this.displayProperty.TabIndex = 14;
            this.displayProperty.SelectedIndexChanged += new System.EventHandler(this.displayProperty_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 543);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Display Palette";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 541);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Display Property";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(480, 777);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(397, 777);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(469, 540);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Display Overlays";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.spriteSelector);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(277, 808);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 773);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 31);
            this.panel3.TabIndex = 1;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(113, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 1;
            this.button8.Text = "Add";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(194, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Delete";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // syntaxError
            // 
            this.syntaxError.AutoSize = true;
            this.syntaxError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxError.ForeColor = System.Drawing.Color.Red;
            this.syntaxError.Location = new System.Drawing.Point(129, 597);
            this.syntaxError.Margin = new System.Windows.Forms.Padding(4);
            this.syntaxError.Name = "syntaxError";
            this.syntaxError.Size = new System.Drawing.Size(149, 13);
            this.syntaxError.TabIndex = 22;
            this.syntaxError.Text = "Syntax error in definition.";
            this.syntaxError.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(480, 746);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(200, 563);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(174, 21);
            this.paletteList.TabIndex = 5;
            // 
            // spriteViewer
            // 
            this.spriteViewer.Location = new System.Drawing.Point(32, 11);
            this.spriteViewer.Margin = new System.Windows.Forms.Padding(4);
            this.spriteViewer.Name = "spriteViewer";
            this.spriteViewer.Size = new System.Drawing.Size(512, 512);
            this.spriteViewer.TabIndex = 0;
            this.spriteViewer.Text = "spriteViewer1";
            // 
            // spriteSelector
            // 
            this.spriteSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSelector.Location = new System.Drawing.Point(0, 0);
            this.spriteSelector.Name = "spriteSelector";
            this.spriteSelector.Size = new System.Drawing.Size(273, 773);
            this.spriteSelector.TabIndex = 0;
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 808);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "SpriteEditor";
            this.Text = "SpriteEditor";
            this.Activated += new System.EventHandler(this.SpriteEditor_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteViewer spriteViewer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox spriteName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SpriteSelector spriteSelector;
        private System.Windows.Forms.Label label3;
        private Controls.PaletteList paletteList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox displayProperty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox definitionCode;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label syntaxError;
        private System.Windows.Forms.Button button3;

    }
}