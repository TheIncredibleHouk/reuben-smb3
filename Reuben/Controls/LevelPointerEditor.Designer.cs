namespace Daiz.NES.Reuben
{
    partial class LevelPointerEditor
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
            this.CmbActions = new System.Windows.Forms.ComboBox();
            this.LblPointsToLevel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnChange = new System.Windows.Forms.Button();
            this.LblXExit = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NumXExit = new System.Windows.Forms.NumericUpDown();
            this.NumYExit = new System.Windows.Forms.NumericUpDown();
            this.LblYEnter = new System.Windows.Forms.Label();
            this.LblXEnter = new System.Windows.Forms.Label();
            this.ChkExitsLevel = new System.Windows.Forms.CheckBox();
            this.LblPointsToWorld = new System.Windows.Forms.Label();
            this.BtnOpenLevel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbWorldExit = new System.Windows.Forms.ComboBox();
            this.ChkRedraw = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumXExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumYExit)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbActions
            // 
            this.CmbActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbActions.FormattingEnabled = true;
            this.CmbActions.Items.AddRange(new object[] {
            "None",
            "Up Pipe",
            "Down Pipe",
            "Right Pipe",
            "Left Pipe"});
            this.CmbActions.Location = new System.Drawing.Point(69, 129);
            this.CmbActions.Name = "CmbActions";
            this.CmbActions.Size = new System.Drawing.Size(159, 21);
            this.CmbActions.TabIndex = 25;
            this.CmbActions.SelectedIndexChanged += new System.EventHandler(this.CmbActions_SelectedIndexChanged);
            // 
            // LblPointsToLevel
            // 
            this.LblPointsToLevel.AutoSize = true;
            this.LblPointsToLevel.Location = new System.Drawing.Point(9, 26);
            this.LblPointsToLevel.Margin = new System.Windows.Forms.Padding(3);
            this.LblPointsToLevel.Name = "LblPointsToLevel";
            this.LblPointsToLevel.Size = new System.Drawing.Size(66, 13);
            this.LblPointsToLevel.TabIndex = 26;
            this.LblPointsToLevel.Text = "Points to 1-1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 136);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Exit Action";
            // 
            // BtnChange
            // 
            this.BtnChange.Location = new System.Drawing.Point(7, 48);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(88, 23);
            this.BtnChange.TabIndex = 28;
            this.BtnChange.Text = "Change Level";
            this.BtnChange.UseVisualStyleBackColor = true;
            this.BtnChange.Click += new System.EventHandler(this.BtnChange_Click);
            // 
            // LblXExit
            // 
            this.LblXExit.AutoSize = true;
            this.LblXExit.Location = new System.Drawing.Point(33, 79);
            this.LblXExit.Margin = new System.Windows.Forms.Padding(3);
            this.LblXExit.Name = "LblXExit";
            this.LblXExit.Size = new System.Drawing.Size(34, 13);
            this.LblXExit.TabIndex = 29;
            this.LblXExit.Text = "X Exit";
            this.LblXExit.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Y Exit";
            // 
            // NumXExit
            // 
            this.NumXExit.Hexadecimal = true;
            this.NumXExit.Location = new System.Drawing.Point(69, 77);
            this.NumXExit.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumXExit.Name = "NumXExit";
            this.NumXExit.Size = new System.Drawing.Size(48, 20);
            this.NumXExit.TabIndex = 31;
            this.NumXExit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumXExit.ValueChanged += new System.EventHandler(this.NumXExit_ValueChanged);
            // 
            // NumYExit
            // 
            this.NumYExit.Hexadecimal = true;
            this.NumYExit.Location = new System.Drawing.Point(69, 103);
            this.NumYExit.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumYExit.Name = "NumYExit";
            this.NumYExit.Size = new System.Drawing.Size(48, 20);
            this.NumYExit.TabIndex = 32;
            this.NumYExit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumYExit.ValueChanged += new System.EventHandler(this.NumYExit_ValueChanged);
            // 
            // LblYEnter
            // 
            this.LblYEnter.AutoSize = true;
            this.LblYEnter.Location = new System.Drawing.Point(124, 105);
            this.LblYEnter.Margin = new System.Windows.Forms.Padding(3);
            this.LblYEnter.Name = "LblYEnter";
            this.LblYEnter.Size = new System.Drawing.Size(34, 13);
            this.LblYEnter.TabIndex = 34;
            this.LblYEnter.Text = "Y Exit";
            // 
            // LblXEnter
            // 
            this.LblXEnter.AutoSize = true;
            this.LblXEnter.Location = new System.Drawing.Point(124, 79);
            this.LblXEnter.Margin = new System.Windows.Forms.Padding(3);
            this.LblXEnter.Name = "LblXEnter";
            this.LblXEnter.Size = new System.Drawing.Size(34, 13);
            this.LblXEnter.TabIndex = 33;
            this.LblXEnter.Text = "X Exit";
            this.LblXEnter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ChkExitsLevel
            // 
            this.ChkExitsLevel.AutoSize = true;
            this.ChkExitsLevel.Location = new System.Drawing.Point(69, 155);
            this.ChkExitsLevel.Name = "ChkExitsLevel";
            this.ChkExitsLevel.Size = new System.Drawing.Size(77, 17);
            this.ChkExitsLevel.TabIndex = 35;
            this.ChkExitsLevel.Text = "Exits Level";
            this.ChkExitsLevel.UseVisualStyleBackColor = true;
            this.ChkExitsLevel.CheckedChanged += new System.EventHandler(this.ChkExitsLevel_CheckedChanged);
            // 
            // LblPointsToWorld
            // 
            this.LblPointsToWorld.AutoSize = true;
            this.LblPointsToWorld.Location = new System.Drawing.Point(9, 7);
            this.LblPointsToWorld.Margin = new System.Windows.Forms.Padding(3);
            this.LblPointsToWorld.Name = "LblPointsToWorld";
            this.LblPointsToWorld.Size = new System.Drawing.Size(66, 13);
            this.LblPointsToWorld.TabIndex = 36;
            this.LblPointsToWorld.Text = "Points to 1-1";
            // 
            // BtnOpenLevel
            // 
            this.BtnOpenLevel.Location = new System.Drawing.Point(101, 48);
            this.BtnOpenLevel.Name = "BtnOpenLevel";
            this.BtnOpenLevel.Size = new System.Drawing.Size(88, 23);
            this.BtnOpenLevel.TabIndex = 37;
            this.BtnOpenLevel.Text = "Open Level";
            this.BtnOpenLevel.UseVisualStyleBackColor = true;
            this.BtnOpenLevel.Click += new System.EventHandler(this.BtnOpenLevel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "World";
            // 
            // CmbWorldExit
            // 
            this.CmbWorldExit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbWorldExit.FormattingEnabled = true;
            this.CmbWorldExit.Location = new System.Drawing.Point(69, 199);
            this.CmbWorldExit.Name = "CmbWorldExit";
            this.CmbWorldExit.Size = new System.Drawing.Size(159, 21);
            this.CmbWorldExit.TabIndex = 39;
            this.CmbWorldExit.SelectedIndexChanged += new System.EventHandler(this.CmbWorldExit_SelectedIndexChanged);
            // 
            // ChkRedraw
            // 
            this.ChkRedraw.AutoSize = true;
            this.ChkRedraw.Location = new System.Drawing.Point(69, 175);
            this.ChkRedraw.Name = "ChkRedraw";
            this.ChkRedraw.Size = new System.Drawing.Size(92, 17);
            this.ChkRedraw.TabIndex = 40;
            this.ChkRedraw.Text = "Redraw Level";
            this.ChkRedraw.UseVisualStyleBackColor = true;
            this.ChkRedraw.CheckedChanged += new System.EventHandler(this.ChkRedraw_CheckedChanged);
            // 
            // LevelPointerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkRedraw);
            this.Controls.Add(this.CmbWorldExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnOpenLevel);
            this.Controls.Add(this.LblPointsToWorld);
            this.Controls.Add(this.ChkExitsLevel);
            this.Controls.Add(this.LblYEnter);
            this.Controls.Add(this.LblXEnter);
            this.Controls.Add(this.NumYExit);
            this.Controls.Add(this.NumXExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblXExit);
            this.Controls.Add(this.BtnChange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblPointsToLevel);
            this.Controls.Add(this.CmbActions);
            this.Name = "LevelPointerEditor";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(239, 237);
            ((System.ComponentModel.ISupportInitialize)(this.NumXExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumYExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbActions;
        private System.Windows.Forms.Label LblPointsToLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnChange;
        private System.Windows.Forms.Label LblXExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumXExit;
        private System.Windows.Forms.NumericUpDown NumYExit;
        private System.Windows.Forms.Label LblYEnter;
        private System.Windows.Forms.Label LblXEnter;
        private System.Windows.Forms.CheckBox ChkExitsLevel;
        private System.Windows.Forms.Label LblPointsToWorld;
        private System.Windows.Forms.Button BtnOpenLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbWorldExit;
        private System.Windows.Forms.CheckBox ChkRedraw;
    }
}
