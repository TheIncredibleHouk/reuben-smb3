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
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.solidity = new MetroFramework.Controls.MetroComboBox();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.interaction = new MetroFramework.Controls.MetroComboBox();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.levelTypes = new MetroFramework.Controls.MetroComboBox();
            this.label4 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new MetroFramework.Controls.MetroLabel();
            this.label6 = new MetroFramework.Controls.MetroLabel();
            this.graphics1 = new MetroFramework.Controls.MetroComboBox();
            this.graphics2 = new MetroFramework.Controls.MetroComboBox();
            this.label7 = new MetroFramework.Controls.MetroLabel();
            this.label8 = new MetroFramework.Controls.MetroLabel();
            this.setDefaultButton = new MetroFramework.Controls.MetroButton();
            this.button2 = new MetroFramework.Controls.MetroButton();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.typeName = new MetroFramework.Controls.MetroTextBox();
            this.label9 = new MetroFramework.Controls.MetroLabel();
            this.solidityOverlay = new MetroFramework.Controls.MetroCheckBox();
            this.interactionOverlay = new MetroFramework.Controls.MetroCheckBox();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.blockView = new Reuben.UI.BlockViewer();
            this.blockList = new Reuben.UI.BlockSelector();
            this.patternTable = new Reuben.UI.PatternTableView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Solidity";
            // 
            // solidity
            // 
            this.solidity.FormattingEnabled = true;
            this.solidity.ItemHeight = 23;
            this.solidity.Location = new System.Drawing.Point(286, 111);
            this.solidity.Margin = new System.Windows.Forms.Padding(4);
            this.solidity.Name = "solidity";
            this.solidity.Size = new System.Drawing.Size(130, 29);
            this.solidity.TabIndex = 4;
            this.solidity.UseSelectable = true;
            this.solidity.SelectedIndexChanged += new System.EventHandler(this.solidity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 148);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Interaction";
            // 
            // interaction
            // 
            this.interaction.FormattingEnabled = true;
            this.interaction.ItemHeight = 23;
            this.interaction.Location = new System.Drawing.Point(286, 175);
            this.interaction.Margin = new System.Windows.Forms.Padding(4);
            this.interaction.Name = "interaction";
            this.interaction.Size = new System.Drawing.Size(130, 29);
            this.interaction.TabIndex = 6;
            this.interaction.UseSelectable = true;
            this.interaction.SelectedIndexChanged += new System.EventHandler(this.interaction_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(22, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(656, 1);
            this.label3.TabIndex = 7;
            // 
            // levelTypes
            // 
            this.levelTypes.FormattingEnabled = true;
            this.levelTypes.ItemHeight = 23;
            this.levelTypes.Location = new System.Drawing.Point(13, 35);
            this.levelTypes.Margin = new System.Windows.Forms.Padding(4);
            this.levelTypes.Name = "levelTypes";
            this.levelTypes.Size = new System.Drawing.Size(130, 29);
            this.levelTypes.TabIndex = 9;
            this.levelTypes.UseSelectable = true;
            this.levelTypes.SelectedIndexChanged += new System.EventHandler(this.levelTypes_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Level Types";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 358);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Graphics Bank 1";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(22, 349);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(656, 1);
            this.label6.TabIndex = 12;
            // 
            // graphics1
            // 
            this.graphics1.FormattingEnabled = true;
            this.graphics1.ItemHeight = 23;
            this.graphics1.Location = new System.Drawing.Point(22, 385);
            this.graphics1.Margin = new System.Windows.Forms.Padding(4);
            this.graphics1.Name = "graphics1";
            this.graphics1.Size = new System.Drawing.Size(118, 29);
            this.graphics1.TabIndex = 13;
            this.graphics1.UseSelectable = true;
            this.graphics1.SelectedIndexChanged += new System.EventHandler(this.graphics1_SelectedIndexChanged);
            // 
            // graphics2
            // 
            this.graphics2.FormattingEnabled = true;
            this.graphics2.ItemHeight = 23;
            this.graphics2.Location = new System.Drawing.Point(148, 385);
            this.graphics2.Margin = new System.Windows.Forms.Padding(4);
            this.graphics2.Name = "graphics2";
            this.graphics2.Size = new System.Drawing.Size(118, 29);
            this.graphics2.TabIndex = 15;
            this.graphics2.UseSelectable = true;
            this.graphics2.SelectedIndexChanged += new System.EventHandler(this.graphics1_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 358);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 19);
            this.label7.TabIndex = 14;
            this.label7.Text = "Graphics Bank 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(274, 358);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "Palette";
            // 
            // setDefaultButton
            // 
            this.setDefaultButton.Location = new System.Drawing.Point(518, 385);
            this.setDefaultButton.Margin = new System.Windows.Forms.Padding(4);
            this.setDefaultButton.Name = "setDefaultButton";
            this.setDefaultButton.Size = new System.Drawing.Size(160, 23);
            this.setDefaultButton.TabIndex = 17;
            this.setDefaultButton.Text = "Set As Level Type Defaults";
            this.setDefaultButton.UseSelectable = true;
            this.setDefaultButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(528, 36);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Save";
            this.button2.UseSelectable = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(613, 36);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Cancel";
            this.button3.UseSelectable = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // typeName
            // 
            this.typeName.Lines = new string[0];
            this.typeName.Location = new System.Drawing.Point(151, 35);
            this.typeName.Margin = new System.Windows.Forms.Padding(4);
            this.typeName.MaxLength = 32767;
            this.typeName.Name = "typeName";
            this.typeName.PasswordChar = '\0';
            this.typeName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.typeName.SelectedText = "";
            this.typeName.Size = new System.Drawing.Size(143, 20);
            this.typeName.TabIndex = 20;
            this.typeName.UseSelectable = true;
            this.typeName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(151, 8);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "Type Name";
            // 
            // solidityOverlay
            // 
            this.solidityOverlay.AutoSize = true;
            this.solidityOverlay.Location = new System.Drawing.Point(302, 44);
            this.solidityOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.solidityOverlay.Name = "solidityOverlay";
            this.solidityOverlay.Size = new System.Drawing.Size(142, 15);
            this.solidityOverlay.TabIndex = 25;
            this.solidityOverlay.Text = "Show Solidity Overlays";
            this.solidityOverlay.UseSelectable = true;
            this.solidityOverlay.CheckedChanged += new System.EventHandler(this.solidityOverlay_CheckedChanged);
            // 
            // interactionOverlay
            // 
            this.interactionOverlay.AutoSize = true;
            this.interactionOverlay.Location = new System.Drawing.Point(302, 21);
            this.interactionOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.interactionOverlay.Name = "interactionOverlay";
            this.interactionOverlay.Size = new System.Drawing.Size(160, 15);
            this.interactionOverlay.TabIndex = 24;
            this.interactionOverlay.Text = "Show Interaction Overlays";
            this.interactionOverlay.UseSelectable = true;
            this.interactionOverlay.CheckedChanged += new System.EventHandler(this.interactionOverlay_CheckedChanged);
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(274, 385);
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
            this.blockView.Location = new System.Drawing.Point(318, 212);
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
            this.blockList.Location = new System.Drawing.Point(424, 83);
            this.blockList.Margin = new System.Windows.Forms.Padding(4);
            this.blockList.Name = "blockList";
            this.blockList.ShowInteractionOverlays = false;
            this.blockList.ShowSolidityOverlays = false;
            this.blockList.Size = new System.Drawing.Size(256, 256);
            this.blockList.TabIndex = 1;
            this.blockList.BubbledMouseDown += new System.EventHandler(this.blockList_MouseDown);
            this.blockList.SelectedBlockChanged += new System.EventHandler(this.blockList_SelectedBlockChanged);
            // 
            // patternTable
            // 
            this.patternTable.Location = new System.Drawing.Point(22, 84);
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
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 424);
            this.ControlBox = false;
            this.Controls.Add(this.solidityOverlay);
            this.Controls.Add(this.interactionOverlay);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.typeName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.setDefaultButton);
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
            this.Activated += new System.EventHandler(this.BlockEditor_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PatternTableView patternTable;
        private BlockSelector blockList;
        private BlockViewer blockView;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroComboBox solidity;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroComboBox interaction;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroComboBox levelTypes;
        private MetroFramework.Controls.MetroLabel label4;
        private Controls.PaletteList paletteList;
        private MetroFramework.Controls.MetroLabel label5;
        private MetroFramework.Controls.MetroLabel label6;
        private MetroFramework.Controls.MetroComboBox graphics1;
        private MetroFramework.Controls.MetroComboBox graphics2;
        private MetroFramework.Controls.MetroLabel label7;
        private MetroFramework.Controls.MetroLabel label8;
        private MetroFramework.Controls.MetroButton setDefaultButton;
        private MetroFramework.Controls.MetroButton button2;
        private MetroFramework.Controls.MetroButton button3;
        private MetroFramework.Controls.MetroTextBox typeName;
        private MetroFramework.Controls.MetroLabel label9;
        private MetroFramework.Controls.MetroCheckBox solidityOverlay;
        private MetroFramework.Controls.MetroCheckBox interactionOverlay;
    }
}