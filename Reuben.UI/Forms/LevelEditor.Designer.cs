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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.levelViewer = new Reuben.UI.LevelViewer();
            this.spriteProperty = new MetroFramework.Controls.MetroComboBox();
            this.mouseCap = new System.Windows.Forms.Button();
            this.lvlHost = new System.Windows.Forms.Panel();
            this.musicList = new MetroFramework.Controls.MetroComboBox();
            this.screenList = new MetroFramework.Controls.MetroComboBox();
            this.scrollList = new MetroFramework.Controls.MetroComboBox();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.levelTypeList = new MetroFramework.Controls.MetroComboBox();
            this.effectList = new MetroFramework.Controls.MetroComboBox();
            this.graphicsList = new MetroFramework.Controls.MetroComboBox();
            this.animationList = new MetroFramework.Controls.MetroComboBox();
            this.spriteOverlay = new System.Windows.Forms.CheckBox();
            this.editList = new MetroFramework.Controls.MetroComboBox();
            this.solidityOverlay = new System.Windows.Forms.CheckBox();
            this.interactionOverlay = new System.Windows.Forms.CheckBox();
            this.spriteSelector = new Reuben.UI.SpriteSelector();
            this.blockSelector = new Reuben.UI.BlockSelector();
            this.panel6 = new System.Windows.Forms.Panel();
            this.status = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel6 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel5 = new MetroFramework.Controls.MetroPanel();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel4.SuspendLayout();
            this.metroPanel6.SuspendLayout();
            this.metroPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.levelViewer);
            this.panel1.Controls.Add(this.spriteProperty);
            this.panel1.Controls.Add(this.mouseCap);
            this.panel1.Controls.Add(this.lvlHost);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(282, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 672);
            this.panel1.TabIndex = 0;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // levelViewer
            // 
            this.levelViewer.EditMode = Reuben.UI.EditMode.Blocks;
            this.levelViewer.Location = new System.Drawing.Point(0, 1);
            this.levelViewer.Name = "levelViewer";
            this.levelViewer.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.levelViewer.SelectionType = Reuben.UI.SelectionType.Draw;
            this.levelViewer.ShowInteractionOverlays = false;
            this.levelViewer.ShowSolidityOverlays = false;
            this.levelViewer.ShowSpriteOverlays = false;
            this.levelViewer.Size = new System.Drawing.Size(75, 23);
            this.levelViewer.TabIndex = 3;
            this.levelViewer.Text = "levelViewer1";
            // 
            // spriteProperty
            // 
            this.spriteProperty.FormattingEnabled = true;
            this.spriteProperty.ItemHeight = 23;
            this.spriteProperty.Location = new System.Drawing.Point(228, 190);
            this.spriteProperty.Name = "spriteProperty";
            this.spriteProperty.Size = new System.Drawing.Size(138, 29);
            this.spriteProperty.TabIndex = 2;
            this.spriteProperty.UseSelectable = true;
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
            this.lvlHost.Location = new System.Drawing.Point(0, 0);
            this.lvlHost.Margin = new System.Windows.Forms.Padding(0);
            this.lvlHost.Name = "lvlHost";
            this.lvlHost.Size = new System.Drawing.Size(0, 432);
            this.lvlHost.TabIndex = 0;
            // 
            // musicList
            // 
            this.musicList.FormattingEnabled = true;
            this.musicList.ItemHeight = 23;
            this.musicList.Location = new System.Drawing.Point(24, 28);
            this.musicList.Margin = new System.Windows.Forms.Padding(4);
            this.musicList.Name = "musicList";
            this.musicList.Size = new System.Drawing.Size(132, 29);
            this.musicList.TabIndex = 1;
            this.musicList.UseSelectable = true;
            // 
            // screenList
            // 
            this.screenList.FormattingEnabled = true;
            this.screenList.ItemHeight = 23;
            this.screenList.Location = new System.Drawing.Point(24, 84);
            this.screenList.Margin = new System.Windows.Forms.Padding(4);
            this.screenList.Name = "screenList";
            this.screenList.Size = new System.Drawing.Size(132, 29);
            this.screenList.TabIndex = 3;
            this.screenList.UseSelectable = true;
            // 
            // scrollList
            // 
            this.scrollList.FormattingEnabled = true;
            this.scrollList.ItemHeight = 23;
            this.scrollList.Location = new System.Drawing.Point(24, 140);
            this.scrollList.Margin = new System.Windows.Forms.Padding(4);
            this.scrollList.Name = "scrollList";
            this.scrollList.Size = new System.Drawing.Size(132, 29);
            this.scrollList.TabIndex = 5;
            this.scrollList.UseSelectable = true;
            this.scrollList.SelectedIndexChanged += new System.EventHandler(this.scrollList_SelectedIndexChanged);
            // 
            // paletteList
            // 
            this.paletteList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.paletteList.DropDownHeight = 400;
            this.paletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteList.DropDownWidth = 288;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.IntegralHeight = false;
            this.paletteList.Location = new System.Drawing.Point(24, 369);
            this.paletteList.Margin = new System.Windows.Forms.Padding(4);
            this.paletteList.Name = "paletteList";
            this.paletteList.SelectedPalette = null;
            this.paletteList.Size = new System.Drawing.Size(132, 21);
            this.paletteList.TabIndex = 16;
            // 
            // levelTypeList
            // 
            this.levelTypeList.FormattingEnabled = true;
            this.levelTypeList.ItemHeight = 23;
            this.levelTypeList.Location = new System.Drawing.Point(24, 196);
            this.levelTypeList.Margin = new System.Windows.Forms.Padding(4);
            this.levelTypeList.Name = "levelTypeList";
            this.levelTypeList.Size = new System.Drawing.Size(132, 29);
            this.levelTypeList.TabIndex = 7;
            this.levelTypeList.UseSelectable = true;
            // 
            // effectList
            // 
            this.effectList.FormattingEnabled = true;
            this.effectList.ItemHeight = 23;
            this.effectList.Location = new System.Drawing.Point(24, 417);
            this.effectList.Margin = new System.Windows.Forms.Padding(4);
            this.effectList.Name = "effectList";
            this.effectList.Size = new System.Drawing.Size(132, 29);
            this.effectList.TabIndex = 15;
            this.effectList.UseSelectable = true;
            // 
            // graphicsList
            // 
            this.graphicsList.FormattingEnabled = true;
            this.graphicsList.ItemHeight = 23;
            this.graphicsList.Location = new System.Drawing.Point(24, 257);
            this.graphicsList.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsList.Name = "graphicsList";
            this.graphicsList.Size = new System.Drawing.Size(132, 29);
            this.graphicsList.TabIndex = 9;
            this.graphicsList.UseSelectable = true;
            // 
            // animationList
            // 
            this.animationList.FormattingEnabled = true;
            this.animationList.ItemHeight = 23;
            this.animationList.Location = new System.Drawing.Point(24, 313);
            this.animationList.Margin = new System.Windows.Forms.Padding(4);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(132, 29);
            this.animationList.TabIndex = 13;
            this.animationList.UseSelectable = true;
            // 
            // spriteOverlay
            // 
            this.spriteOverlay.AutoSize = true;
            this.spriteOverlay.Location = new System.Drawing.Point(7, 85);
            this.spriteOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.spriteOverlay.Name = "spriteOverlay";
            this.spriteOverlay.Size = new System.Drawing.Size(127, 17);
            this.spriteOverlay.TabIndex = 24;
            this.spriteOverlay.Text = "Show Sprite Overlays";
            this.spriteOverlay.UseVisualStyleBackColor = true;
            this.spriteOverlay.CheckedChanged += new System.EventHandler(this.spriteOverlay_CheckedChanged);
            // 
            // editList
            // 
            this.editList.FormattingEnabled = true;
            this.editList.ItemHeight = 23;
            this.editList.Items.AddRange(new object[] {
            "Blocks",
            "Sprites",
            "Pointers"});
            this.editList.Location = new System.Drawing.Point(24, 26);
            this.editList.Margin = new System.Windows.Forms.Padding(4);
            this.editList.Name = "editList";
            this.editList.Size = new System.Drawing.Size(132, 29);
            this.editList.TabIndex = 18;
            this.editList.UseSelectable = true;
            this.editList.SelectedIndexChanged += new System.EventHandler(this.editList_SelectedIndexChanged);
            // 
            // solidityOverlay
            // 
            this.solidityOverlay.AutoSize = true;
            this.solidityOverlay.Location = new System.Drawing.Point(7, 110);
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
            this.interactionOverlay.Location = new System.Drawing.Point(7, 60);
            this.interactionOverlay.Margin = new System.Windows.Forms.Padding(4);
            this.interactionOverlay.Name = "interactionOverlay";
            this.interactionOverlay.Size = new System.Drawing.Size(150, 17);
            this.interactionOverlay.TabIndex = 22;
            this.interactionOverlay.Text = "Show Interaction Overlays";
            this.interactionOverlay.UseVisualStyleBackColor = true;
            this.interactionOverlay.CheckedChanged += new System.EventHandler(this.interactionOverlay_CheckedChanged);
            // 
            // spriteSelector
            // 
            this.spriteSelector.Location = new System.Drawing.Point(3, 9);
            this.spriteSelector.Name = "spriteSelector";
            this.spriteSelector.Size = new System.Drawing.Size(279, 359);
            this.spriteSelector.TabIndex = 0;
            // 
            // blockSelector
            // 
            this.blockSelector.Location = new System.Drawing.Point(10, -2);
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
            this.panel6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.status);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 672);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1137, 26);
            this.panel6.TabIndex = 3;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(5, 5);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 13);
            this.status.TabIndex = 0;
            this.status.Text = "Status";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroPanel2);
            this.metroPanel1.Controls.Add(this.metroPanel3);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(970, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(167, 672);
            this.metroPanel1.TabIndex = 4;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            this.metroPanel2.AutoScroll = true;
            this.metroPanel2.Controls.Add(this.metroLabel8);
            this.metroPanel2.Controls.Add(this.metroLabel7);
            this.metroPanel2.Controls.Add(this.metroLabel6);
            this.metroPanel2.Controls.Add(this.metroLabel5);
            this.metroPanel2.Controls.Add(this.metroLabel4);
            this.metroPanel2.Controls.Add(this.metroLabel3);
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.metroLabel1);
            this.metroPanel2.Controls.Add(this.paletteList);
            this.metroPanel2.Controls.Add(this.effectList);
            this.metroPanel2.Controls.Add(this.musicList);
            this.metroPanel2.Controls.Add(this.screenList);
            this.metroPanel2.Controls.Add(this.animationList);
            this.metroPanel2.Controls.Add(this.scrollList);
            this.metroPanel2.Controls.Add(this.graphicsList);
            this.metroPanel2.Controls.Add(this.levelTypeList);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel2.HorizontalScrollbar = true;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(0, 0);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(167, 483);
            this.metroPanel2.TabIndex = 17;
            this.metroPanel2.VerticalScrollbar = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(7, 346);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(48, 19);
            this.metroLabel8.TabIndex = 24;
            this.metroLabel8.Text = "Palette";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(3, 394);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(89, 19);
            this.metroLabel7.TabIndex = 23;
            this.metroLabel7.Text = "Palette Effects";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(7, 61);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(53, 19);
            this.metroLabel6.TabIndex = 22;
            this.metroLabel6.Text = "Screens";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(7, 117);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(59, 19);
            this.metroLabel5.TabIndex = 21;
            this.metroLabel5.Text = "Scrolling";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(7, 173);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(69, 19);
            this.metroLabel4.TabIndex = 20;
            this.metroLabel4.Text = "Level Type";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(7, 229);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(59, 19);
            this.metroLabel3.TabIndex = 19;
            this.metroLabel3.Text = "Graphics";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(7, 290);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(69, 19);
            this.metroLabel2.TabIndex = 18;
            this.metroLabel2.Text = "Animation";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(7, 5);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(42, 19);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "Music";
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.metroLabel9);
            this.metroPanel3.Controls.Add(this.solidityOverlay);
            this.metroPanel3.Controls.Add(this.spriteOverlay);
            this.metroPanel3.Controls.Add(this.interactionOverlay);
            this.metroPanel3.Controls.Add(this.editList);
            this.metroPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(0, 483);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(167, 189);
            this.metroPanel3.TabIndex = 18;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(7, 3);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(70, 19);
            this.metroLabel9.TabIndex = 25;
            this.metroLabel9.Text = "Edit Mode";
            // 
            // metroPanel4
            // 
            this.metroPanel4.Controls.Add(this.metroPanel6);
            this.metroPanel4.Controls.Add(this.metroPanel5);
            this.metroPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 10;
            this.metroPanel4.Location = new System.Drawing.Point(0, 0);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Size = new System.Drawing.Size(282, 672);
            this.metroPanel4.TabIndex = 5;
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 10;
            // 
            // metroPanel6
            // 
            this.metroPanel6.Controls.Add(this.spriteSelector);
            this.metroPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel6.HorizontalScrollbarBarColor = true;
            this.metroPanel6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel6.HorizontalScrollbarSize = 10;
            this.metroPanel6.Location = new System.Drawing.Point(0, 257);
            this.metroPanel6.Name = "metroPanel6";
            this.metroPanel6.Size = new System.Drawing.Size(282, 415);
            this.metroPanel6.TabIndex = 3;
            this.metroPanel6.VerticalScrollbarBarColor = true;
            this.metroPanel6.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel6.VerticalScrollbarSize = 10;
            // 
            // metroPanel5
            // 
            this.metroPanel5.Controls.Add(this.blockSelector);
            this.metroPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel5.HorizontalScrollbarBarColor = true;
            this.metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel5.HorizontalScrollbarSize = 10;
            this.metroPanel5.Location = new System.Drawing.Point(0, 0);
            this.metroPanel5.Name = "metroPanel5";
            this.metroPanel5.Size = new System.Drawing.Size(282, 257);
            this.metroPanel5.TabIndex = 2;
            this.metroPanel5.VerticalScrollbarBarColor = true;
            this.metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel5.VerticalScrollbarSize = 10;
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.metroPanel4);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel6);
            this.KeyPreview = true;
            this.Name = "LevelEditor";
            this.Text = "LevelEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelEditor_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.metroPanel4.ResumeLayout(false);
            this.metroPanel6.ResumeLayout(false);
            this.metroPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel lvlHost;
        private Controls.PaletteList paletteList;
        private MetroFramework.Controls.MetroComboBox effectList;
        private MetroFramework.Controls.MetroComboBox animationList;
        private MetroFramework.Controls.MetroComboBox graphicsList;
        private MetroFramework.Controls.MetroComboBox levelTypeList;
        private MetroFramework.Controls.MetroComboBox scrollList;
        private MetroFramework.Controls.MetroComboBox screenList;
        private MetroFramework.Controls.MetroComboBox musicList;
        private MetroFramework.Controls.MetroComboBox editList;
        private SpriteSelector spriteSelector;
        private BlockSelector blockSelector;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button mouseCap;
        private System.Windows.Forms.CheckBox solidityOverlay;
        private System.Windows.Forms.CheckBox interactionOverlay;
        private System.Windows.Forms.CheckBox spriteOverlay;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ToolTip toolTip;
        private MetroFramework.Controls.MetroComboBox spriteProperty;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroPanel metroPanel6;
        private MetroFramework.Controls.MetroPanel metroPanel5;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private LevelViewer levelViewer;

    }
}