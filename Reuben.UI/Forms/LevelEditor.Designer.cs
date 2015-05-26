namespace Reuben.UI
{
    partial class LevelEditor
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
            this.mouseCap = new System.Windows.Forms.Button();
            this.lvlHost = new System.Windows.Forms.Panel();
            this.levelViewer = new Reuben.UI.LevelViewer();
            this.panel23 = new System.Windows.Forms.Panel();
            this.solidityOverlay = new System.Windows.Forms.CheckBox();
            this.interactionOverlay = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.editList = new System.Windows.Forms.ComboBox();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.label9 = new System.Windows.Forms.Label();
            this.effectList = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.animationList = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.graphicsList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.levelTypeList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.scrollList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.screenList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.musicList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.blockSelector = new Reuben.UI.BlockSelector();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.interaction = new System.Windows.Forms.Label();
            this.solidity = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.lvlHost.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mouseCap);
            this.panel1.Controls.Add(this.lvlHost);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(267, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 425);
            this.panel1.TabIndex = 0;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // mouseCap
            // 
            this.mouseCap.Location = new System.Drawing.Point(16, 5);
            this.mouseCap.Name = "mouseCap";
            this.mouseCap.Size = new System.Drawing.Size(0, 0);
            this.mouseCap.TabIndex = 1;
            this.mouseCap.UseVisualStyleBackColor = true;
            // 
            // lvlHost
            // 
            this.lvlHost.Controls.Add(this.levelViewer);
            this.lvlHost.Location = new System.Drawing.Point(0, 0);
            this.lvlHost.Margin = new System.Windows.Forms.Padding(0);
            this.lvlHost.Name = "lvlHost";
            this.lvlHost.Size = new System.Drawing.Size(0, 432);
            this.lvlHost.TabIndex = 0;
            // 
            // levelViewer
            // 
            this.levelViewer.EditMode = Reuben.UI.EditMode.Blocks;
            this.levelViewer.Location = new System.Drawing.Point(0, 0);
            this.levelViewer.Name = "levelViewer";
            this.levelViewer.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.levelViewer.SelectionType = Reuben.UI.SelectionType.Draw;
            this.levelViewer.ShowInteractionOverlays = false;
            this.levelViewer.ShowSolidityOverlays = false;
            this.levelViewer.Size = new System.Drawing.Size(6912, 432);
            this.levelViewer.TabIndex = 0;
            this.levelViewer.Text = "levelViewer1";
            this.levelViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseDown);
            this.levelViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseMove);
            this.levelViewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseUp);
            // 
            // panel23
            // 
            this.panel23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel23.Controls.Add(this.solidityOverlay);
            this.panel23.Controls.Add(this.interactionOverlay);
            this.panel23.Controls.Add(this.label10);
            this.panel23.Controls.Add(this.editList);
            this.panel23.Controls.Add(this.paletteList);
            this.panel23.Controls.Add(this.label9);
            this.panel23.Controls.Add(this.effectList);
            this.panel23.Controls.Add(this.label8);
            this.panel23.Controls.Add(this.animationList);
            this.panel23.Controls.Add(this.label7);
            this.panel23.Controls.Add(this.label6);
            this.panel23.Controls.Add(this.graphicsList);
            this.panel23.Controls.Add(this.label5);
            this.panel23.Controls.Add(this.levelTypeList);
            this.panel23.Controls.Add(this.label4);
            this.panel23.Controls.Add(this.scrollList);
            this.panel23.Controls.Add(this.label3);
            this.panel23.Controls.Add(this.screenList);
            this.panel23.Controls.Add(this.label2);
            this.panel23.Controls.Add(this.musicList);
            this.panel23.Controls.Add(this.label1);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(267, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(649, 177);
            this.panel23.TabIndex = 1;
            // 
            // solidityOverlay
            // 
            this.solidityOverlay.AutoSize = true;
            this.solidityOverlay.Location = new System.Drawing.Point(156, 147);
            this.solidityOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.solidityOverlay.Name = "solidityOverlay";
            this.solidityOverlay.Size = new System.Drawing.Size(133, 17);
            this.solidityOverlay.TabIndex = 23;
            this.solidityOverlay.Text = "Show Solidity Overlays";
            this.solidityOverlay.UseVisualStyleBackColor = true;
            this.solidityOverlay.CheckedChanged += new System.EventHandler(this.solidityOverlay_CheckedChanged);
            // 
            // interactionOverlay
            // 
            this.interactionOverlay.AutoSize = true;
            this.interactionOverlay.Location = new System.Drawing.Point(156, 121);
            this.interactionOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.interactionOverlay.Name = "interactionOverlay";
            this.interactionOverlay.Size = new System.Drawing.Size(150, 17);
            this.interactionOverlay.TabIndex = 22;
            this.interactionOverlay.Text = "Show Interaction Overlays";
            this.interactionOverlay.UseVisualStyleBackColor = true;
            this.interactionOverlay.CheckedChanged += new System.EventHandler(this.interactionOverlay_CheckedChanged);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(16, 113);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(552, 1);
            this.label10.TabIndex = 19;
            // 
            // editList
            // 
            this.editList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editList.FormattingEnabled = true;
            this.editList.Items.AddRange(new object[] {
            "Blocks",
            "Sprites",
            "Pointers"});
            this.editList.Location = new System.Drawing.Point(16, 145);
            this.editList.Margin = new System.Windows.Forms.Padding(4);
            this.editList.Name = "editList";
            this.editList.Size = new System.Drawing.Size(132, 21);
            this.editList.TabIndex = 18;
            this.editList.SelectedIndexChanged += new System.EventHandler(this.editList_SelectedIndexChanged);
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(156, 84);
            this.paletteList.Margin = new System.Windows.Forms.Padding(4);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(132, 21);
            this.paletteList.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 122);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Edit Mode";
            // 
            // effectList
            // 
            this.effectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.effectList.FormattingEnabled = true;
            this.effectList.Location = new System.Drawing.Point(436, 84);
            this.effectList.Margin = new System.Windows.Forms.Padding(4);
            this.effectList.Name = "effectList";
            this.effectList.Size = new System.Drawing.Size(132, 21);
            this.effectList.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(433, 63);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Palette Effect";
            // 
            // animationList
            // 
            this.animationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.animationList.FormattingEnabled = true;
            this.animationList.Location = new System.Drawing.Point(296, 84);
            this.animationList.Margin = new System.Windows.Forms.Padding(4);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(132, 21);
            this.animationList.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Animation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Palette";
            // 
            // graphicsList
            // 
            this.graphicsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphicsList.FormattingEnabled = true;
            this.graphicsList.Location = new System.Drawing.Point(16, 84);
            this.graphicsList.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsList.Name = "graphicsList";
            this.graphicsList.Size = new System.Drawing.Size(132, 21);
            this.graphicsList.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Graphics";
            // 
            // levelTypeList
            // 
            this.levelTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelTypeList.FormattingEnabled = true;
            this.levelTypeList.Location = new System.Drawing.Point(436, 34);
            this.levelTypeList.Margin = new System.Windows.Forms.Padding(4);
            this.levelTypeList.Name = "levelTypeList";
            this.levelTypeList.Size = new System.Drawing.Size(132, 21);
            this.levelTypeList.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(433, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Type";
            // 
            // scrollList
            // 
            this.scrollList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scrollList.FormattingEnabled = true;
            this.scrollList.Location = new System.Drawing.Point(296, 34);
            this.scrollList.Margin = new System.Windows.Forms.Padding(4);
            this.scrollList.Name = "scrollList";
            this.scrollList.Size = new System.Drawing.Size(132, 21);
            this.scrollList.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Scrolling";
            // 
            // screenList
            // 
            this.screenList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenList.FormattingEnabled = true;
            this.screenList.Location = new System.Drawing.Point(156, 34);
            this.screenList.Margin = new System.Windows.Forms.Padding(4);
            this.screenList.Name = "screenList";
            this.screenList.Size = new System.Drawing.Size(132, 21);
            this.screenList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Screens";
            // 
            // musicList
            // 
            this.musicList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.musicList.FormattingEnabled = true;
            this.musicList.Location = new System.Drawing.Point(16, 34);
            this.musicList.Margin = new System.Windows.Forms.Padding(4);
            this.musicList.Name = "musicList";
            this.musicList.Size = new System.Drawing.Size(132, 21);
            this.musicList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Music";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 679);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.spriteSelector);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(263, 387);
            this.panel3.TabIndex = 1;
            // 
            // spriteSelector
            // 
            this.spriteSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteSelector.Location = new System.Drawing.Point(0, 28);
            this.spriteSelector.Name = "spriteSelector";
            this.spriteSelector.SelectedSprite = null;
            this.spriteSelector.Size = new System.Drawing.Size(263, 359);
            this.spriteSelector.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label12);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 28);
            this.panel5.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(101, 7);
            this.label12.Margin = new System.Windows.Forms.Padding(4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Objects";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.blockSelector);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(263, 288);
            this.panel4.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(106, 7);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Blocks";
            // 
            // blockSelector
            // 
            this.blockSelector.Location = new System.Drawing.Point(5, 27);
            this.blockSelector.Margin = new System.Windows.Forms.Padding(0);
            this.blockSelector.Name = "blockSelector";
            this.blockSelector.ShowInteractionOverlays = false;
            this.blockSelector.ShowSolidityOverlays = false;
            this.blockSelector.Size = new System.Drawing.Size(256, 256);
            this.blockSelector.TabIndex = 0;
            this.blockSelector.DoubleClicked += new System.EventHandler(this.blockSelector_MouseDoubleClick);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.interaction);
            this.panel6.Controls.Add(this.solidity);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(267, 602);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(649, 77);
            this.panel6.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(194, 28);
            this.label15.Margin = new System.Windows.Forms.Padding(4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 25;
            this.label15.Text = "Solidity:";
            // 
            // interaction
            // 
            this.interaction.AutoSize = true;
            this.interaction.Location = new System.Drawing.Point(85, 49);
            this.interaction.Margin = new System.Windows.Forms.Padding(4);
            this.interaction.Name = "interaction";
            this.interaction.Size = new System.Drawing.Size(35, 13);
            this.interaction.TabIndex = 24;
            this.interaction.Text = "Music";
            // 
            // solidity
            // 
            this.solidity.AutoSize = true;
            this.solidity.Location = new System.Drawing.Point(85, 28);
            this.solidity.Margin = new System.Windows.Forms.Padding(4);
            this.solidity.Name = "solidity";
            this.solidity.Size = new System.Drawing.Size(35, 13);
            this.solidity.TabIndex = 20;
            this.solidity.Text = "Music";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 49);
            this.label16.Margin = new System.Windows.Forms.Padding(4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "Interaction:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 28);
            this.label14.Margin = new System.Windows.Forms.Padding(4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Solidity:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Information";
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 679);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel23);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "LevelEditor";
            this.Text = "LevelEditor";
            this.SizeChanged += new System.EventHandler(this.LevelEditor_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelEditor_KeyDown);
            this.Move += new System.EventHandler(this.LevelEditor_Move);
            this.panel1.ResumeLayout(false);
            this.lvlHost.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel lvlHost;
        private LevelViewer levelViewer;
        private System.Windows.Forms.Panel panel23;
        private Controls.PaletteList paletteList;
        private System.Windows.Forms.ComboBox effectList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox animationList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox graphicsList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox levelTypeList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox scrollList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox screenList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox musicList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox editList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private SpriteSelector spriteSelector;
        private BlockSelector blockSelector;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label interaction;
        private System.Windows.Forms.Label solidity;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button mouseCap;
        private System.Windows.Forms.CheckBox solidityOverlay;
        private System.Windows.Forms.CheckBox interactionOverlay;

    }
}