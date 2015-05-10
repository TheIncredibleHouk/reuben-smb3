namespace Reuben.UI
{
    partial class StringManager
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
            this.filter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.resourceDisplay = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.values = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter";
            // 
            // filter
            // 
            this.filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filter.Location = new System.Drawing.Point(13, 26);
            this.filter.Margin = new System.Windows.Forms.Padding(4);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(258, 20);
            this.filter.TabIndex = 1;
            this.filter.TextChanged += new System.EventHandler(this.filter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Strings";
            // 
            // resourceDisplay
            // 
            this.resourceDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resourceDisplay.LabelWrap = false;
            this.resourceDisplay.Location = new System.Drawing.Point(13, 67);
            this.resourceDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.resourceDisplay.Name = "resourceDisplay";
            this.resourceDisplay.Size = new System.Drawing.Size(256, 89);
            this.resourceDisplay.TabIndex = 3;
            this.resourceDisplay.UseCompatibleStateImageBehavior = false;
            this.resourceDisplay.View = System.Windows.Forms.View.List;
            this.resourceDisplay.SelectedIndexChanged += new System.EventHandler(this.resourceDisplay_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 322);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(196, 322);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Values";
            // 
            // values
            // 
            this.values.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.values.Location = new System.Drawing.Point(13, 177);
            this.values.Margin = new System.Windows.Forms.Padding(4);
            this.values.Multiline = true;
            this.values.Name = "values";
            this.values.Size = new System.Drawing.Size(256, 137);
            this.values.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 322);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Add New";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // StringManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 356);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.values);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resourceDisplay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StringManager";
            this.Text = "Reuben String Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView resourceDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox values;
        private System.Windows.Forms.Button button3;
    }
}