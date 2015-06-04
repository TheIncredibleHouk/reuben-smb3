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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.clipWidth = new System.Windows.Forms.ComboBox();
            this.clipHeight = new System.Windows.Forms.ComboBox();
            this.gamePalette = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.codeTags = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.syntaxError = new System.Windows.Forms.Label();
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
            this.shellStomp = new System.Windows.Forms.CheckBox();
            this.stompApathy = new System.Windows.Forms.CheckBox();
            this.collisionBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.spriteViewer = new Reuben.UI.SpriteViewer();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.collisionBox);
            this.panel1.Controls.Add(this.stompApathy);
            this.panel1.Controls.Add(this.shellStomp);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.clipWidth);
            this.panel1.Controls.Add(this.clipHeight);
            this.panel1.Controls.Add(this.gamePalette);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.codeTags);
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
            this.panel1.Size = new System.Drawing.Size(741, 808);
            this.panel1.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(669, 376);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Clip Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(603, 376);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Clip Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(539, 376);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Palette";
            // 
            // clipWidth
            // 
            this.clipWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clipWidth.FormattingEnabled = true;
            this.clipWidth.Items.AddRange(new object[] {
            "8",
            "16",
            "24",
            "32",
            "40",
            "48",
            "64"});
            this.clipWidth.Location = new System.Drawing.Point(604, 397);
            this.clipWidth.Margin = new System.Windows.Forms.Padding(4);
            this.clipWidth.Name = "clipWidth";
            this.clipWidth.Size = new System.Drawing.Size(56, 21);
            this.clipWidth.TabIndex = 28;
            this.clipWidth.SelectedIndexChanged += new System.EventHandler(this.clipWidth_SelectedIndexChanged);
            // 
            // clipHeight
            // 
            this.clipHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clipHeight.FormattingEnabled = true;
            this.clipHeight.Items.AddRange(new object[] {
            "16",
            "32",
            "48",
            "64"});
            this.clipHeight.Location = new System.Drawing.Point(668, 397);
            this.clipHeight.Margin = new System.Windows.Forms.Padding(4);
            this.clipHeight.Name = "clipHeight";
            this.clipHeight.Size = new System.Drawing.Size(56, 21);
            this.clipHeight.TabIndex = 27;
            this.clipHeight.SelectedIndexChanged += new System.EventHandler(this.clipHeight_SelectedIndexChanged);
            // 
            // gamePalette
            // 
            this.gamePalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gamePalette.FormattingEnabled = true;
            this.gamePalette.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.gamePalette.Location = new System.Drawing.Point(540, 397);
            this.gamePalette.Margin = new System.Windows.Forms.Padding(4);
            this.gamePalette.Name = "gamePalette";
            this.gamePalette.Size = new System.Drawing.Size(56, 21);
            this.gamePalette.TabIndex = 26;
            this.gamePalette.SelectedIndexChanged += new System.EventHandler(this.gamePalette_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(537, 426);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Code Tags";
            // 
            // codeTags
            // 
            this.codeTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeTags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.codeTags.LabelWrap = false;
            this.codeTags.Location = new System.Drawing.Point(540, 447);
            this.codeTags.Margin = new System.Windows.Forms.Padding(4);
            this.codeTags.Name = "codeTags";
            this.codeTags.Size = new System.Drawing.Size(193, 76);
            this.codeTags.TabIndex = 24;
            this.codeTags.UseCompatibleStateImageBehavior = false;
            this.codeTags.View = System.Windows.Forms.View.Details;
            this.codeTags.DoubleClick += new System.EventHandler(this.codeTags_DoubleClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(646, 749);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // syntaxError
            // 
            this.syntaxError.AutoSize = true;
            this.syntaxError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxError.ForeColor = System.Drawing.Color.Red;
            this.syntaxError.Location = new System.Drawing.Point(17, 749);
            this.syntaxError.Margin = new System.Windows.Forms.Padding(4);
            this.syntaxError.Name = "syntaxError";
            this.syntaxError.Size = new System.Drawing.Size(149, 13);
            this.syntaxError.TabIndex = 22;
            this.syntaxError.Text = "Syntax error in definition.";
            this.syntaxError.Visible = false;
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
            this.definitionCode.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.definitionCode.Location = new System.Drawing.Point(20, 617);
            this.definitionCode.Multiline = true;
            this.definitionCode.Name = "definitionCode";
            this.definitionCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.definitionCode.Size = new System.Drawing.Size(701, 125);
            this.definitionCode.TabIndex = 20;
            this.definitionCode.WordWrap = false;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(646, 561);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 19;
            this.button10.Text = "Delete";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(563, 561);
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
            this.button2.Location = new System.Drawing.Point(646, 780);
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
            this.button1.Location = new System.Drawing.Point(563, 780);
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
            // shellStomp
            // 
            this.shellStomp.AutoSize = true;
            this.shellStomp.Location = new System.Drawing.Point(542, 351);
            this.shellStomp.Margin = new System.Windows.Forms.Padding(4);
            this.shellStomp.Name = "shellStomp";
            this.shellStomp.Size = new System.Drawing.Size(95, 17);
            this.shellStomp.TabIndex = 32;
            this.shellStomp.Text = "Has shell state";
            this.shellStomp.UseVisualStyleBackColor = true;
            this.shellStomp.CheckedChanged += new System.EventHandler(this.shellStomp_CheckedChanged);
            // 
            // stompApathy
            // 
            this.stompApathy.AutoSize = true;
            this.stompApathy.Location = new System.Drawing.Point(542, 326);
            this.stompApathy.Margin = new System.Windows.Forms.Padding(4);
            this.stompApathy.Name = "stompApathy";
            this.stompApathy.Size = new System.Drawing.Size(155, 17);
            this.stompApathy.TabIndex = 34;
            this.stompApathy.Text = "Apathetic to being stomped";
            this.stompApathy.UseVisualStyleBackColor = true;
            this.stompApathy.CheckedChanged += new System.EventHandler(this.stompApathy_CheckedChanged);
            // 
            // collisionBox
            // 
            this.collisionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collisionBox.FormattingEnabled = true;
            this.collisionBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.collisionBox.Location = new System.Drawing.Point(540, 297);
            this.collisionBox.Margin = new System.Windows.Forms.Padding(4);
            this.collisionBox.Name = "collisionBox";
            this.collisionBox.Size = new System.Drawing.Size(187, 21);
            this.collisionBox.TabIndex = 35;
            this.collisionBox.SelectedIndexChanged += new System.EventHandler(this.collisionBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(540, 276);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Object Collision Box";
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
            this.spriteViewer.Location = new System.Drawing.Point(20, 11);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(540, 226);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Tile Collision Box";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBox1.Location = new System.Drawing.Point(540, 247);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(187, 21);
            this.comboBox1.TabIndex = 37;
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 808);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView codeTags;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox clipWidth;
        private System.Windows.Forms.ComboBox clipHeight;
        private System.Windows.Forms.ComboBox gamePalette;
        private System.Windows.Forms.CheckBox stompApathy;
        private System.Windows.Forms.CheckBox shellStomp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox collisionBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox1;

    }
}