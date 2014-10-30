namespace WeatherStarter
{
    partial class Main
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
            this.tbCurrentVersions = new System.Windows.Forms.TextBox();
            this.tbUpdateVersions = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbCurrentVersions
            // 
            this.tbCurrentVersions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCurrentVersions.Location = new System.Drawing.Point(12, 25);
            this.tbCurrentVersions.Multiline = true;
            this.tbCurrentVersions.Name = "tbCurrentVersions";
            this.tbCurrentVersions.ReadOnly = true;
            this.tbCurrentVersions.Size = new System.Drawing.Size(193, 80);
            this.tbCurrentVersions.TabIndex = 0;
            this.tbCurrentVersions.TabStop = false;
            this.tbCurrentVersions.WordWrap = false;
            // 
            // tbUpdateVersions
            // 
            this.tbUpdateVersions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUpdateVersions.Location = new System.Drawing.Point(211, 25);
            this.tbUpdateVersions.Multiline = true;
            this.tbUpdateVersions.Name = "tbUpdateVersions";
            this.tbUpdateVersions.ReadOnly = true;
            this.tbUpdateVersions.Size = new System.Drawing.Size(193, 80);
            this.tbUpdateVersions.TabIndex = 0;
            this.tbUpdateVersions.TabStop = false;
            this.tbUpdateVersions.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current versions:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Update versions:";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 112);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(392, 69);
            this.tbLog.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 193);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUpdateVersions);
            this.Controls.Add(this.tbCurrentVersions);
            this.Name = "Main";
            this.Text = "Weather Diary - Updater";
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCurrentVersions;
        private System.Windows.Forms.TextBox tbUpdateVersions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLog;
    }
}

