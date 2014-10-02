namespace WetherDiary
{
    partial class AddMeasure
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.tbTemperature = new System.Windows.Forms.TextBox();
            this.tbPressure = new System.Windows.Forms.TextBox();
            this.tbNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCloud = new System.Windows.Forms.ComboBox();
            this.cbWind = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvFallouts = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddMeasure = new System.Windows.Forms.Button();
            this.cbWindForce = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbFallouts = new System.Windows.Forms.ComboBox();
            this.btnAddFallout = new System.Windows.Forms.Button();
            this.btnDelFallout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFallouts)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(97, 14);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(126, 20);
            this.dtpDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Время";
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "HH:mm";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(97, 40);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(58, 20);
            this.dtpTime.TabIndex = 1;
            // 
            // tbTemperature
            // 
            this.tbTemperature.Location = new System.Drawing.Point(97, 66);
            this.tbTemperature.Name = "tbTemperature";
            this.tbTemperature.Size = new System.Drawing.Size(58, 20);
            this.tbTemperature.TabIndex = 3;
            // 
            // tbPressure
            // 
            this.tbPressure.Location = new System.Drawing.Point(97, 92);
            this.tbPressure.Name = "tbPressure";
            this.tbPressure.Size = new System.Drawing.Size(58, 20);
            this.tbPressure.TabIndex = 4;
            // 
            // tbNote
            // 
            this.tbNote.Location = new System.Drawing.Point(97, 316);
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(165, 56);
            this.tbNote.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Температура";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Давление";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 319);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Примечание";
            // 
            // cbCloud
            // 
            this.cbCloud.FormattingEnabled = true;
            this.cbCloud.Location = new System.Drawing.Point(97, 118);
            this.cbCloud.Name = "cbCloud";
            this.cbCloud.Size = new System.Drawing.Size(165, 21);
            this.cbCloud.TabIndex = 5;
            // 
            // cbWind
            // 
            this.cbWind.FormattingEnabled = true;
            this.cbWind.Location = new System.Drawing.Point(97, 145);
            this.cbWind.Name = "cbWind";
            this.cbWind.Size = new System.Drawing.Size(165, 21);
            this.cbWind.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Облачность";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Ветер";
            // 
            // dgvFallouts
            // 
            this.dgvFallouts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFallouts.Location = new System.Drawing.Point(97, 228);
            this.dgvFallouts.Name = "dgvFallouts";
            this.dgvFallouts.Size = new System.Drawing.Size(165, 82);
            this.dgvFallouts.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Осадки";
            // 
            // btnAddMeasure
            // 
            this.btnAddMeasure.Location = new System.Drawing.Point(280, 324);
            this.btnAddMeasure.Name = "btnAddMeasure";
            this.btnAddMeasure.Size = new System.Drawing.Size(87, 39);
            this.btnAddMeasure.TabIndex = 11;
            this.btnAddMeasure.Text = "Сохранить";
            this.btnAddMeasure.UseVisualStyleBackColor = true;
            this.btnAddMeasure.Click += new System.EventHandler(this.btnAddMeasure_Click);
            // 
            // cbWindForce
            // 
            this.cbWindForce.FormattingEnabled = true;
            this.cbWindForce.Location = new System.Drawing.Point(97, 172);
            this.cbWindForce.Name = "cbWindForce";
            this.cbWindForce.Size = new System.Drawing.Size(165, 21);
            this.cbWindForce.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Сила ветра";
            // 
            // cbFallouts
            // 
            this.cbFallouts.FormattingEnabled = true;
            this.cbFallouts.Location = new System.Drawing.Point(97, 199);
            this.cbFallouts.Name = "cbFallouts";
            this.cbFallouts.Size = new System.Drawing.Size(165, 21);
            this.cbFallouts.TabIndex = 8;
            // 
            // btnAddFallout
            // 
            this.btnAddFallout.Location = new System.Drawing.Point(280, 258);
            this.btnAddFallout.Name = "btnAddFallout";
            this.btnAddFallout.Size = new System.Drawing.Size(87, 23);
            this.btnAddFallout.TabIndex = 9;
            this.btnAddFallout.Text = "Добавить";
            this.btnAddFallout.UseVisualStyleBackColor = true;
            // 
            // btnDelFallout
            // 
            this.btnDelFallout.Location = new System.Drawing.Point(280, 287);
            this.btnDelFallout.Name = "btnDelFallout";
            this.btnDelFallout.Size = new System.Drawing.Size(87, 23);
            this.btnDelFallout.TabIndex = 13;
            this.btnDelFallout.Text = "Удалить";
            this.btnDelFallout.UseVisualStyleBackColor = true;
            // 
            // AddMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 383);
            this.Controls.Add(this.btnDelFallout);
            this.Controls.Add(this.btnAddFallout);
            this.Controls.Add(this.cbFallouts);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbWindForce);
            this.Controls.Add(this.btnAddMeasure);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvFallouts);
            this.Controls.Add(this.cbWind);
            this.Controls.Add(this.cbCloud);
            this.Controls.Add(this.tbNote);
            this.Controls.Add(this.tbPressure);
            this.Controls.Add(this.tbTemperature);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDate);
            this.Name = "AddMeasure";
            this.Text = "Еще один хороший день";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFallouts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.TextBox tbTemperature;
        private System.Windows.Forms.TextBox tbPressure;
        private System.Windows.Forms.TextBox tbNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCloud;
        private System.Windows.Forms.ComboBox cbWind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvFallouts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddMeasure;
        private System.Windows.Forms.ComboBox cbWindForce;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbFallouts;
        private System.Windows.Forms.Button btnAddFallout;
        private System.Windows.Forms.Button btnDelFallout;
    }
}