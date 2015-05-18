namespace Reuben.UI
{
    partial class BlockEditor
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
            this.solidity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.interaction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.levelTypes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.graphics1 = new System.Windows.Forms.ComboBox();
            this.graphics2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.typeName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.blockView = new Reuben.UI.BlockViewer();
            this.blockList = new Reuben.UI.BlockSelector();
            this.patternTable = new Reuben.UI.PatternTableView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Solidity";
            // 
            // solidity
            // 
            this.solidity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidity.FormattingEnabled = true;
            this.solidity.Location = new System.Drawing.Point(277, 105);
            this.solidity.Margin = new System.Windows.Forms.Padding(4);
            this.solidity.Name = "solidity";
            this.solidity.Size = new System.Drawing.Size(130, 21);
            this.solidity.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Interaction";
            // 
            // interaction
            // 
            this.interaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interaction.FormattingEnabled = true;
            this.interaction.Location = new System.Drawing.Point(277, 155);
            this.interaction.Margin = new System.Windows.Forms.Padding(4);
            this.interaction.Name = "interaction";
            this.interaction.Size = new System.Drawing.Size(130, 21);
            this.interaction.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(656, 1);
            this.label3.TabIndex = 7;
            // 
            // levelTypes
            // 
            this.levelTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelTypes.FormattingEnabled = true;
            this.levelTypes.Location = new System.Drawing.Point(13, 36);
            this.levelTypes.Margin = new System.Windows.Forms.Padding(4);
            this.levelTypes.Name = "levelTypes";
            this.levelTypes.Size = new System.Drawing.Size(130, 21);
            this.levelTypes.TabIndex = 9;
            this.levelTypes.SelectedIndexChanged += new System.EventHandler(this.levelTypes_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Level Types";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 356);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Graphics Bank 1";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(13, 347);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(656, 1);
            this.label6.TabIndex = 12;
            // 
            // graphics1
            // 
            this.graphics1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphics1.FormattingEnabled = true;
            this.graphics1.Location = new System.Drawing.Point(13, 377);
            this.graphics1.Margin = new System.Windows.Forms.Padding(4);
            this.graphics1.Name = "graphics1";
            this.graphics1.Size = new System.Drawing.Size(118, 21);
            this.graphics1.TabIndex = 13;
            this.graphics1.SelectedIndexChanged += new System.EventHandler(this.graphics1_SelectedIndexChanged);
            // 
            // graphics2
            // 
            this.graphics2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphics2.FormattingEnabled = true;
            this.graphics2.Location = new System.Drawing.Point(139, 377);
            this.graphics2.Margin = new System.Windows.Forms.Padding(4);
            this.graphics2.Name = "graphics2";
            this.graphics2.Size = new System.Drawing.Size(118, 21);
            this.graphics2.TabIndex = 15;
            this.graphics2.SelectedIndexChanged += new System.EventHandler(this.graphics1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 356);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Graphics Bank 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(262, 356);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Palette";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 375);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Set As Level Type Defaults";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(509, 36);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(594, 36);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // typeName
            // 
            this.typeName.Location = new System.Drawing.Point(151, 36);
            this.typeName.Margin = new System.Windows.Forms.Padding(4);
            this.typeName.Name = "typeName";
            this.typeName.Size = new System.Drawing.Size(143, 20);
            this.typeName.TabIndex = 20;
            this.typeName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Type Name";
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(265, 377);
            this.paletteList.Margin = new System.Windows.Forms.Padding(4);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(130, 21);
            this.paletteList.TabIndex = 10;
            this.paletteList.SelectedIndexChanged += new System.EventHandler(this.graphics1_SelectedIndexChanged);
            // 
            // blockView
            // 
            this.blockView.Block = null;
            this.blockView.Location = new System.Drawing.Point(308, 184);
            this.blockView.Margin = new System.Windows.Forms.Padding(4);
            this.blockView.Name = "blockView";
            this.blockView.PaletteIndex = 0;
            this.blockView.Size = new System.Drawing.Size(64, 64);
            this.blockView.TabIndex = 2;
            this.blockView.Text = "blockViewer1";
            this.blockView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.blockView_MouseDown);
            // 
            // blockList
            // 
            this.blockList.Location = new System.Drawing.Point(415, 83);
            this.blockList.Margin = new System.Windows.Forms.Padding(4);
            this.blockList.Name = "blockList";
            this.blockList.Size = new System.Drawing.Size(256, 256);
            this.blockList.TabIndex = 1;
            this.blockList.SelectedBlockChanged += new System.EventHandler(this.blockList_SelectedBlockChanged);
            // 
            // patternTable
            // 
            this.patternTable.Location = new System.Drawing.Point(13, 84);
            this.patternTable.Margin = new System.Windows.Forms.Padding(4);
            this.patternTable.Name = "patternTable";
            this.patternTable.PaletteIndex = 0;
            this.patternTable.Size = new System.Drawing.Size(256, 256);
            this.patternTable.TabIndex = 0;
            this.patternTable.Text = "patternTableView1";
            this.patternTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.patternTable_MouseDown);
            // 
            // BlockEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 412);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.typeName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.graphics2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.graphics1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.paletteList);
            this.Controls.Add(this.levelTypes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.interaction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.solidity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blockView);
            this.Controls.Add(this.blockList);
            this.Controls.Add(this.patternTable);
            this.Name = "BlockEditor";
            this.Text = "Level Type Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PatternTableView patternTable;
        private BlockSelector blockList;
        private BlockViewer blockView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox solidity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox interaction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox levelTypes;
        private System.Windows.Forms.Label label4;
        private Controls.PaletteList paletteList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox graphics1;
        private System.Windows.Forms.ComboBox graphics2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox typeName;
        private System.Windows.Forms.Label label9;
    }
}