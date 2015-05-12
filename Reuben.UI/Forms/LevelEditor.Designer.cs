﻿namespace Reuben.UI
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
            this.lvlHost = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.editList = new System.Windows.Forms.ComboBox();
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
            this.levelViewer = new Reuben.UI.LevelViewer();
            this.paletteList = new Reuben.UI.Controls.PaletteList();
            this.panel1.SuspendLayout();
            this.lvlHost.SuspendLayout();
            this.panel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lvlHost);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 502);
            this.panel1.TabIndex = 0;
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
            // panel23
            // 
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
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(692, 177);
            this.panel23.TabIndex = 1;
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
            this.editList.FormattingEnabled = true;
            this.editList.Items.AddRange(new object[] {
            "Blocks",
            "Sprites",
            "Pointers"});
            this.editList.Location = new System.Drawing.Point(16, 145);
            this.editList.Margin = new System.Windows.Forms.Padding(4);
            this.editList.Name = "editList";
            this.editList.Size = new System.Drawing.Size(121, 21);
            this.editList.TabIndex = 18;
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
            // levelViewer
            // 
            this.levelViewer.Location = new System.Drawing.Point(0, 0);
            this.levelViewer.Name = "levelViewer";
            this.levelViewer.SelectionRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.levelViewer.Size = new System.Drawing.Size(6912, 432);
            this.levelViewer.TabIndex = 0;
            this.levelViewer.Text = "levelViewer1";
            this.levelViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseDown);
            this.levelViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseMove);
            this.levelViewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.levelViewer_MouseUp);
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
            this.paletteList.SelectedIndexChanged += new System.EventHandler(this.paletteList_SelectedIndexChanged);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 679);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel23);
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

    }
}