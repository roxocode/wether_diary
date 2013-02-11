namespace WetherDiary
{
    partial class MainForm
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.mtbTime = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbPrecipitation = new System.Windows.Forms.ComboBox();
            this.cbWind = new System.Windows.Forms.ComboBox();
            this.cbCloud = new System.Windows.Forms.ComboBox();
            this.tbPressure = new System.Windows.Forms.TextBox();
            this.tbTemperature = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbLastMonthTemperature = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblMaxTemperature = new System.Windows.Forms.Label();
            this.lblMinTemperature = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.BooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrecipitationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindForceВетраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.rowMenu.SuspendLayout();
            this.gbLastMonthTemperature.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(59, 44);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(140, 20);
            this.dtpDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата";
            // 
            // dgvMain
            // 
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(15, 173);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(576, 189);
            this.dgvMain.TabIndex = 2;
            // 
            // mtbTime
            // 
            this.mtbTime.Location = new System.Drawing.Point(107, 29);
            this.mtbTime.Mask = "00:00";
            this.mtbTime.Name = "mtbTime";
            this.mtbTime.PromptChar = ' ';
            this.mtbTime.Size = new System.Drawing.Size(39, 20);
            this.mtbTime.TabIndex = 3;
            this.mtbTime.ValidatingType = typeof(System.DateTime);
            this.mtbTime.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbPrecipitation);
            this.groupBox1.Controls.Add(this.cbWind);
            this.groupBox1.Controls.Add(this.cbCloud);
            this.groupBox1.Controls.Add(this.tbPressure);
            this.groupBox1.Controls.Add(this.tbTemperature);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mtbTime);
            this.groupBox1.Location = new System.Drawing.Point(15, 368);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 175);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Характеристики";
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "HH:mm";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(107, 29);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(55, 20);
            this.dtpTime.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(473, 126);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 29);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Примечание";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 108);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 57);
            this.textBox1.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Осадки";
            // 
            // cbPrecipitation
            // 
            this.cbPrecipitation.FormattingEnabled = true;
            this.cbPrecipitation.Location = new System.Drawing.Point(418, 82);
            this.cbPrecipitation.Name = "cbPrecipitation";
            this.cbPrecipitation.Size = new System.Drawing.Size(151, 21);
            this.cbPrecipitation.TabIndex = 8;
            // 
            // cbWind
            // 
            this.cbWind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWind.FormattingEnabled = true;
            this.cbWind.Location = new System.Drawing.Point(418, 56);
            this.cbWind.Name = "cbWind";
            this.cbWind.Size = new System.Drawing.Size(151, 21);
            this.cbWind.TabIndex = 7;
            // 
            // cbCloud
            // 
            this.cbCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCloud.FormattingEnabled = true;
            this.cbCloud.Location = new System.Drawing.Point(418, 28);
            this.cbCloud.Name = "cbCloud";
            this.cbCloud.Size = new System.Drawing.Size(151, 21);
            this.cbCloud.TabIndex = 7;
            // 
            // tbPressure
            // 
            this.tbPressure.Location = new System.Drawing.Point(107, 82);
            this.tbPressure.Name = "tbPressure";
            this.tbPressure.Size = new System.Drawing.Size(55, 20);
            this.tbPressure.TabIndex = 6;
            // 
            // tbTemperature
            // 
            this.tbTemperature.Location = new System.Drawing.Point(107, 56);
            this.tbTemperature.Name = "tbTemperature";
            this.tbTemperature.Size = new System.Drawing.Size(55, 20);
            this.tbTemperature.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(330, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ветер";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Облачность";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Давление";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Температура";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Время";
            // 
            // rowMenu
            // 
            this.rowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowItem});
            this.rowMenu.Name = "rowMenu";
            this.rowMenu.Size = new System.Drawing.Size(167, 26);
            // 
            // deleteRowItem
            // 
            this.deleteRowItem.Name = "deleteRowItem";
            this.deleteRowItem.Size = new System.Drawing.Size(166, 22);
            this.deleteRowItem.Text = "Удалить запись";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(509, 43);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbLastMonthTemperature
            // 
            this.gbLastMonthTemperature.Controls.Add(this.label12);
            this.gbLastMonthTemperature.Controls.Add(this.lblMaxTemperature);
            this.gbLastMonthTemperature.Controls.Add(this.lblMinTemperature);
            this.gbLastMonthTemperature.Controls.Add(this.label11);
            this.gbLastMonthTemperature.Controls.Add(this.label10);
            this.gbLastMonthTemperature.Controls.Add(this.label9);
            this.gbLastMonthTemperature.Location = new System.Drawing.Point(15, 79);
            this.gbLastMonthTemperature.Name = "gbLastMonthTemperature";
            this.gbLastMonthTemperature.Size = new System.Drawing.Size(219, 88);
            this.gbLastMonthTemperature.TabIndex = 6;
            this.gbLastMonthTemperature.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(171, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "0";
            // 
            // lblMaxTemperature
            // 
            this.lblMaxTemperature.AutoSize = true;
            this.lblMaxTemperature.Location = new System.Drawing.Point(171, 43);
            this.lblMaxTemperature.Name = "lblMaxTemperature";
            this.lblMaxTemperature.Size = new System.Drawing.Size(13, 13);
            this.lblMaxTemperature.TabIndex = 1;
            this.lblMaxTemperature.Text = "0";
            // 
            // lblMinTemperature
            // 
            this.lblMinTemperature.AutoSize = true;
            this.lblMinTemperature.Location = new System.Drawing.Point(171, 20);
            this.lblMinTemperature.Name = "lblMinTemperature";
            this.lblMinTemperature.Size = new System.Drawing.Size(13, 13);
            this.lblMinTemperature.TabIndex = 1;
            this.lblMinTemperature.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Средняя температура";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Максимальная температура";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Минимальная температура";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BooksToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // BooksToolStripMenuItem
            // 
            this.BooksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloudToolStripMenuItem,
            this.PrecipitationToolStripMenuItem,
            this.WindToolStripMenuItem,
            this.WindForceВетраToolStripMenuItem});
            this.BooksToolStripMenuItem.Name = "BooksToolStripMenuItem";
            this.BooksToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.BooksToolStripMenuItem.Text = "Справочники";
            // 
            // CloudToolStripMenuItem
            // 
            this.CloudToolStripMenuItem.Name = "CloudToolStripMenuItem";
            this.CloudToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CloudToolStripMenuItem.Text = "Облачность";
            this.CloudToolStripMenuItem.Click += new System.EventHandler(this.CloudToolStripMenuItem_Click);
            // 
            // PrecipitationToolStripMenuItem
            // 
            this.PrecipitationToolStripMenuItem.Name = "PrecipitationToolStripMenuItem";
            this.PrecipitationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrecipitationToolStripMenuItem.Text = "Осадки";
            this.PrecipitationToolStripMenuItem.Click += new System.EventHandler(this.PrecipitationToolStripMenuItem_Click);
            // 
            // WindToolStripMenuItem
            // 
            this.WindToolStripMenuItem.Name = "WindToolStripMenuItem";
            this.WindToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.WindToolStripMenuItem.Text = "Ветер";
            this.WindToolStripMenuItem.Click += new System.EventHandler(this.WindToolStripMenuItem_Click);
            // 
            // WindForceВетраToolStripMenuItem
            // 
            this.WindForceВетраToolStripMenuItem.Name = "WindForceВетраToolStripMenuItem";
            this.WindForceВетраToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.WindForceВетраToolStripMenuItem.Text = "Сила ветра";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.AboutToolStripMenuItem.Text = "О программе";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 555);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbLastMonthTemperature);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Wether Diary =)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rowMenu.ResumeLayout(false);
            this.gbLastMonthTemperature.ResumeLayout(false);
            this.gbLastMonthTemperature.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.MaskedTextBox mtbTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbWind;
        private System.Windows.Forms.ComboBox cbCloud;
        private System.Windows.Forms.TextBox tbPressure;
        private System.Windows.Forms.TextBox tbTemperature;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbPrecipitation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip rowMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteRowItem;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbLastMonthTemperature;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMaxTemperature;
        private System.Windows.Forms.Label lblMinTemperature;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem BooksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrecipitationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WindToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WindForceВетраToolStripMenuItem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}

