namespace Reuben.UI.Controls
{
    partial class SpriteInfoEditor
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
            this.bank = new System.Windows.Forms.TextBox();
            this.spriteValue = new System.Windows.Forms.TextBox();
            this.y = new System.Windows.Forms.TextBox();
            this.x = new System.Windows.Forms.TextBox();
            this.overlay = new System.Windows.Forms.CheckBox();
            this.selected = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.property1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.property8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hFlip = new System.Windows.Forms.CheckBox();
            this.vFlip = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.palette = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bank
            // 
            this.bank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bank.Location = new System.Drawing.Point(44, 5);
            this.bank.Name = "bank";
            this.bank.Size = new System.Drawing.Size(25, 20);
            this.bank.TabIndex = 17;
            this.bank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bank.TextChanged += new System.EventHandler(this.bank_TextChanged);
            // 
            // spriteValue
            // 
            this.spriteValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spriteValue.Location = new System.Drawing.Point(93, 5);
            this.spriteValue.Name = "spriteValue";
            this.spriteValue.Size = new System.Drawing.Size(25, 20);
            this.spriteValue.TabIndex = 16;
            this.spriteValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spriteValue.TextChanged += new System.EventHandler(this.spriteValue_TextChanged);
            // 
            // y
            // 
            this.y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.y.Location = new System.Drawing.Point(206, 5);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(35, 20);
            this.y.TabIndex = 15;
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.y.TextChanged += new System.EventHandler(this.y_TextChanged);
            // 
            // x
            // 
            this.x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.x.Location = new System.Drawing.Point(165, 5);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(35, 20);
            this.x.TabIndex = 14;
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.x.TextChanged += new System.EventHandler(this.x_TextChanged);
            // 
            // overlay
            // 
            this.overlay.AutoSize = true;
            this.overlay.Location = new System.Drawing.Point(250, 7);
            this.overlay.Margin = new System.Windows.Forms.Padding(4);
            this.overlay.Name = "overlay";
            this.overlay.Size = new System.Drawing.Size(15, 14);
            this.overlay.TabIndex = 18;
            this.overlay.UseVisualStyleBackColor = true;
            this.overlay.CheckedChanged += new System.EventHandler(this.overlay_CheckedChanged);
            // 
            // selected
            // 
            this.selected.AutoSize = true;
            this.selected.Location = new System.Drawing.Point(4, 7);
            this.selected.Margin = new System.Windows.Forms.Padding(4);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(15, 14);
            this.selected.TabIndex = 27;
            this.selected.UseVisualStyleBackColor = true;
            this.selected.CheckedChanged += new System.EventHandler(this.selected_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.properties});
            this.menuStrip1.Location = new System.Drawing.Point(273, 3);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(225, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // properties
            // 
            this.properties.BackColor = System.Drawing.SystemColors.Control;
            this.properties.CheckOnClick = true;
            this.properties.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.property1ToolStripMenuItem,
            this.property8ToolStripMenuItem});
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(131, 24);
            this.properties.Text = "Applies To Properties";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.CheckOnClick = true;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem6.Text = "property1";
            this.toolStripMenuItem6.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.CheckOnClick = true;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem5.Text = "property2";
            this.toolStripMenuItem5.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "property3";
            this.toolStripMenuItem4.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "property4";
            this.toolStripMenuItem3.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "property5";
            this.toolStripMenuItem2.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "property6";
            this.toolStripMenuItem1.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // property1ToolStripMenuItem
            // 
            this.property1ToolStripMenuItem.CheckOnClick = true;
            this.property1ToolStripMenuItem.Name = "property1ToolStripMenuItem";
            this.property1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.property1ToolStripMenuItem.Text = "property7";
            this.property1ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // property8ToolStripMenuItem
            // 
            this.property8ToolStripMenuItem.CheckOnClick = true;
            this.property8ToolStripMenuItem.Name = "property8ToolStripMenuItem";
            this.property8ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.property8ToolStripMenuItem.Text = "property8";
            this.property8ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.toolStripMenuItem6_CheckedChanged);
            // 
            // hFlip
            // 
            this.hFlip.AutoSize = true;
            this.hFlip.Location = new System.Drawing.Point(413, 7);
            this.hFlip.Margin = new System.Windows.Forms.Padding(4);
            this.hFlip.Name = "hFlip";
            this.hFlip.Size = new System.Drawing.Size(15, 14);
            this.hFlip.TabIndex = 29;
            this.hFlip.UseVisualStyleBackColor = true;
            this.hFlip.CheckedChanged += new System.EventHandler(this.hFlip_CheckedChanged);
            // 
            // vFlip
            // 
            this.vFlip.AutoSize = true;
            this.vFlip.Location = new System.Drawing.Point(455, 7);
            this.vFlip.Margin = new System.Windows.Forms.Padding(4);
            this.vFlip.Name = "vFlip";
            this.vFlip.Size = new System.Drawing.Size(15, 14);
            this.vFlip.TabIndex = 30;
            this.vFlip.UseVisualStyleBackColor = true;
            this.vFlip.CheckedChanged += new System.EventHandler(this.vFlip_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "0x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "0x";
            // 
            // palette
            // 
            this.palette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.palette.Location = new System.Drawing.Point(124, 5);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(35, 20);
            this.palette.TabIndex = 33;
            this.palette.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.palette.TextChanged += new System.EventHandler(this.palette_TextChanged);
            // 
            // SpriteInfoEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.palette);
            this.Controls.Add(this.bank);
            this.Controls.Add(this.spriteValue);
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vFlip);
            this.Controls.Add(this.hFlip);
            this.Controls.Add(this.selected);
            this.Controls.Add(this.overlay);
            this.Controls.Add(this.menuStrip1);
            this.Name = "SpriteInfoEditor";
            this.Size = new System.Drawing.Size(475, 29);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bank;
        private System.Windows.Forms.TextBox spriteValue;
        private System.Windows.Forms.TextBox y;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.CheckBox overlay;
        private System.Windows.Forms.CheckBox selected;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem properties;
        private System.Windows.Forms.CheckBox hFlip;
        private System.Windows.Forms.CheckBox vFlip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem property1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem property8ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox palette;
    }
}
