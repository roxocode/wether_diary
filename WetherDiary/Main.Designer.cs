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
            this.rowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbLastMonthTemperature = new System.Windows.Forms.GroupBox();
            this.lblAvgTemperature = new System.Windows.Forms.Label();
            this.lblMaxTemperature = new System.Windows.Forms.Label();
            this.lblMinTemperature = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.BooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrecipitationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvYearsDays = new System.Windows.Forms.DataGridView();
            this.btnAddTest = new System.Windows.Forms.Button();
            this.cbChartPeriod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpChrPeriodFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpChrPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.lblChrPeriodFrom = new System.Windows.Forms.Label();
            this.lblChrPeriodTo = new System.Windows.Forms.Label();
            this.clbCharts = new System.Windows.Forms.CheckedListBox();
            this.lblMesPeriodTo = new System.Windows.Forms.Label();
            this.lblMesPeriodFrom = new System.Windows.Forms.Label();
            this.dtpMesPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.dtpMesPeriodFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMeasurePeriods = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRainyDays = new System.Windows.Forms.Label();
            this.scChartData = new System.Windows.Forms.SplitContainer();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.rowMenu.SuspendLayout();
            this.gbLastMonthTemperature.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYearsDays)).BeginInit();
            this.scChartData.Panel1.SuspendLayout();
            this.scChartData.Panel2.SuspendLayout();
            this.scChartData.SuspendLayout();
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
            this.label1.Location = new System.Drawing.Point(17, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата:";
            // 
            // dgvMain
            // 
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Location = new System.Drawing.Point(1, 29);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(754, 168);
            this.dgvMain.TabIndex = 2;
            // 
            // rowMenu
            // 
            this.rowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowItem});
            this.rowMenu.Name = "rowMenu";
            this.rowMenu.Size = new System.Drawing.Size(159, 26);
            // 
            // deleteRowItem
            // 
            this.deleteRowItem.Name = "deleteRowItem";
            this.deleteRowItem.Size = new System.Drawing.Size(158, 22);
            this.deleteRowItem.Text = "Удалить запись";
            // 
            // gbLastMonthTemperature
            // 
            this.gbLastMonthTemperature.Controls.Add(this.lblAvgTemperature);
            this.gbLastMonthTemperature.Controls.Add(this.lblMaxTemperature);
            this.gbLastMonthTemperature.Controls.Add(this.lblMinTemperature);
            this.gbLastMonthTemperature.Controls.Add(this.label11);
            this.gbLastMonthTemperature.Controls.Add(this.label10);
            this.gbLastMonthTemperature.Controls.Add(this.label9);
            this.gbLastMonthTemperature.Location = new System.Drawing.Point(15, 79);
            this.gbLastMonthTemperature.Name = "gbLastMonthTemperature";
            this.gbLastMonthTemperature.Size = new System.Drawing.Size(219, 100);
            this.gbLastMonthTemperature.TabIndex = 6;
            this.gbLastMonthTemperature.TabStop = false;
            // 
            // lblAvgTemperature
            // 
            this.lblAvgTemperature.AutoSize = true;
            this.lblAvgTemperature.Location = new System.Drawing.Point(171, 65);
            this.lblAvgTemperature.Name = "lblAvgTemperature";
            this.lblAvgTemperature.Size = new System.Drawing.Size(13, 13);
            this.lblAvgTemperature.TabIndex = 1;
            this.lblAvgTemperature.Text = "0";
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
            this.menuStrip1.Size = new System.Drawing.Size(756, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // BooksToolStripMenuItem
            // 
            this.BooksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrecipitationToolStripMenuItem,
            this.CloudToolStripMenuItem,
            this.WindToolStripMenuItem,
            this.WindForceToolStripMenuItem});
            this.BooksToolStripMenuItem.Name = "BooksToolStripMenuItem";
            this.BooksToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.BooksToolStripMenuItem.Text = "Справочники";
            // 
            // PrecipitationToolStripMenuItem
            // 
            this.PrecipitationToolStripMenuItem.Name = "PrecipitationToolStripMenuItem";
            this.PrecipitationToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.PrecipitationToolStripMenuItem.Text = "Осадки";
            this.PrecipitationToolStripMenuItem.Click += new System.EventHandler(this.FalloutToolStripMenuItem_Click);
            // 
            // CloudToolStripMenuItem
            // 
            this.CloudToolStripMenuItem.Name = "CloudToolStripMenuItem";
            this.CloudToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.CloudToolStripMenuItem.Text = "Облачность";
            this.CloudToolStripMenuItem.Click += new System.EventHandler(this.CloudToolStripMenuItem_Click);
            // 
            // WindToolStripMenuItem
            // 
            this.WindToolStripMenuItem.Name = "WindToolStripMenuItem";
            this.WindToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.WindToolStripMenuItem.Text = "Ветер";
            this.WindToolStripMenuItem.Click += new System.EventHandler(this.WindToolStripMenuItem_Click);
            // 
            // WindForceToolStripMenuItem
            // 
            this.WindForceToolStripMenuItem.Name = "WindForceToolStripMenuItem";
            this.WindForceToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.WindForceToolStripMenuItem.Text = "Сила ветра";
            this.WindForceToolStripMenuItem.Click += new System.EventHandler(this.WindForceToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.AboutToolStripMenuItem.Text = "О программе";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvYearsDays);
            this.groupBox2.Location = new System.Drawing.Point(240, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 155);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Аналогия по годам";
            // 
            // dgvYearsDays
            // 
            this.dgvYearsDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYearsDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvYearsDays.Location = new System.Drawing.Point(3, 16);
            this.dgvYearsDays.Name = "dgvYearsDays";
            this.dgvYearsDays.Size = new System.Drawing.Size(215, 136);
            this.dgvYearsDays.TabIndex = 10;
            // 
            // btnAddTest
            // 
            this.btnAddTest.Image = global::WetherDiary.Properties.Resources.add;
            this.btnAddTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddTest.Location = new System.Drawing.Point(489, 99);
            this.btnAddTest.Name = "btnAddTest";
            this.btnAddTest.Size = new System.Drawing.Size(92, 36);
            this.btnAddTest.TabIndex = 1;
            this.btnAddTest.Text = "Добавить";
            this.btnAddTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddTest.UseVisualStyleBackColor = true;
            this.btnAddTest.Click += new System.EventHandler(this.btnAddMeasure_Click);
            // 
            // cbChartPeriod
            // 
            this.cbChartPeriod.FormattingEnabled = true;
            this.cbChartPeriod.Location = new System.Drawing.Point(64, 3);
            this.cbChartPeriod.Name = "cbChartPeriod";
            this.cbChartPeriod.Size = new System.Drawing.Size(121, 21);
            this.cbChartPeriod.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Период:";
            // 
            // dtpChrPeriodFrom
            // 
            this.dtpChrPeriodFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpChrPeriodFrom.Location = new System.Drawing.Point(389, 3);
            this.dtpChrPeriodFrom.Name = "dtpChrPeriodFrom";
            this.dtpChrPeriodFrom.Size = new System.Drawing.Size(111, 20);
            this.dtpChrPeriodFrom.TabIndex = 13;
            // 
            // dtpChrPeriodTo
            // 
            this.dtpChrPeriodTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpChrPeriodTo.Location = new System.Drawing.Point(531, 3);
            this.dtpChrPeriodTo.Name = "dtpChrPeriodTo";
            this.dtpChrPeriodTo.Size = new System.Drawing.Size(111, 20);
            this.dtpChrPeriodTo.TabIndex = 13;
            // 
            // lblChrPeriodFrom
            // 
            this.lblChrPeriodFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChrPeriodFrom.AutoSize = true;
            this.lblChrPeriodFrom.Location = new System.Drawing.Point(370, 6);
            this.lblChrPeriodFrom.Name = "lblChrPeriodFrom";
            this.lblChrPeriodFrom.Size = new System.Drawing.Size(13, 13);
            this.lblChrPeriodFrom.TabIndex = 14;
            this.lblChrPeriodFrom.Text = "с";
            // 
            // lblChrPeriodTo
            // 
            this.lblChrPeriodTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChrPeriodTo.AutoSize = true;
            this.lblChrPeriodTo.Location = new System.Drawing.Point(506, 6);
            this.lblChrPeriodTo.Name = "lblChrPeriodTo";
            this.lblChrPeriodTo.Size = new System.Drawing.Size(19, 13);
            this.lblChrPeriodTo.TabIndex = 14;
            this.lblChrPeriodTo.Text = "по";
            // 
            // clbCharts
            // 
            this.clbCharts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbCharts.FormattingEnabled = true;
            this.clbCharts.Location = new System.Drawing.Point(646, 29);
            this.clbCharts.Name = "clbCharts";
            this.clbCharts.Size = new System.Drawing.Size(110, 184);
            this.clbCharts.TabIndex = 3;
            // 
            // lblMesPeriodTo
            // 
            this.lblMesPeriodTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesPeriodTo.AutoSize = true;
            this.lblMesPeriodTo.Location = new System.Drawing.Point(506, 9);
            this.lblMesPeriodTo.Name = "lblMesPeriodTo";
            this.lblMesPeriodTo.Size = new System.Drawing.Size(19, 13);
            this.lblMesPeriodTo.TabIndex = 20;
            this.lblMesPeriodTo.Text = "по";
            this.lblMesPeriodTo.Visible = false;
            // 
            // lblMesPeriodFrom
            // 
            this.lblMesPeriodFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesPeriodFrom.AutoSize = true;
            this.lblMesPeriodFrom.Location = new System.Drawing.Point(370, 9);
            this.lblMesPeriodFrom.Name = "lblMesPeriodFrom";
            this.lblMesPeriodFrom.Size = new System.Drawing.Size(13, 13);
            this.lblMesPeriodFrom.TabIndex = 21;
            this.lblMesPeriodFrom.Text = "с";
            this.lblMesPeriodFrom.Visible = false;
            // 
            // dtpMesPeriodTo
            // 
            this.dtpMesPeriodTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpMesPeriodTo.Location = new System.Drawing.Point(531, 3);
            this.dtpMesPeriodTo.Name = "dtpMesPeriodTo";
            this.dtpMesPeriodTo.Size = new System.Drawing.Size(111, 20);
            this.dtpMesPeriodTo.TabIndex = 18;
            this.dtpMesPeriodTo.Visible = false;
            this.dtpMesPeriodTo.ValueChanged += new System.EventHandler(this.dtpMesPeriod_ValueChanged);
            // 
            // dtpMesPeriodFrom
            // 
            this.dtpMesPeriodFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpMesPeriodFrom.Location = new System.Drawing.Point(389, 3);
            this.dtpMesPeriodFrom.Name = "dtpMesPeriodFrom";
            this.dtpMesPeriodFrom.Size = new System.Drawing.Size(111, 20);
            this.dtpMesPeriodFrom.TabIndex = 19;
            this.dtpMesPeriodFrom.Visible = false;
            this.dtpMesPeriodFrom.ValueChanged += new System.EventHandler(this.dtpMesPeriod_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Период:";
            // 
            // cbMeasurePeriods
            // 
            this.cbMeasurePeriods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMeasurePeriods.FormattingEnabled = true;
            this.cbMeasurePeriods.Location = new System.Drawing.Point(64, 3);
            this.cbMeasurePeriods.Name = "cbMeasurePeriods";
            this.cbMeasurePeriods.Size = new System.Drawing.Size(121, 21);
            this.cbMeasurePeriods.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Кол-во дней с осадками";
            // 
            // lblRainyDays
            // 
            this.lblRainyDays.AutoSize = true;
            this.lblRainyDays.Location = new System.Drawing.Point(603, 51);
            this.lblRainyDays.Name = "lblRainyDays";
            this.lblRainyDays.Size = new System.Drawing.Size(13, 13);
            this.lblRainyDays.TabIndex = 23;
            this.lblRainyDays.Text = "0";
            // 
            // scChartData
            // 
            this.scChartData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scChartData.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.scChartData.Location = new System.Drawing.Point(0, 185);
            this.scChartData.Name = "scChartData";
            this.scChartData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scChartData.Panel1
            // 
            this.scChartData.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.scChartData.Panel1.Controls.Add(this.clbCharts);
            this.scChartData.Panel1.Controls.Add(this.label2);
            this.scChartData.Panel1.Controls.Add(this.cbChartPeriod);
            this.scChartData.Panel1.Controls.Add(this.dtpChrPeriodFrom);
            this.scChartData.Panel1.Controls.Add(this.dtpChrPeriodTo);
            this.scChartData.Panel1.Controls.Add(this.lblChrPeriodFrom);
            this.scChartData.Panel1.Controls.Add(this.lblChrPeriodTo);
            // 
            // scChartData.Panel2
            // 
            this.scChartData.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scChartData.Panel2.Controls.Add(this.lblRowsCount);
            this.scChartData.Panel2.Controls.Add(this.lblTotalCount);
            this.scChartData.Panel2.Controls.Add(this.label5);
            this.scChartData.Panel2.Controls.Add(this.cbMeasurePeriods);
            this.scChartData.Panel2.Controls.Add(this.dtpMesPeriodFrom);
            this.scChartData.Panel2.Controls.Add(this.dtpMesPeriodTo);
            this.scChartData.Panel2.Controls.Add(this.lblMesPeriodTo);
            this.scChartData.Panel2.Controls.Add(this.lblMesPeriodFrom);
            this.scChartData.Panel2.Controls.Add(this.dgvMain);
            this.scChartData.Size = new System.Drawing.Size(756, 398);
            this.scChartData.SplitterDistance = 199;
            this.scChartData.SplitterWidth = 2;
            this.scChartData.TabIndex = 24;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.Location = new System.Drawing.Point(719, 9);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(13, 13);
            this.lblRowsCount.TabIndex = 23;
            this.lblRowsCount.Text = "0";
            this.lblRowsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(658, 9);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(64, 13);
            this.lblTotalCount.TabIndex = 22;
            this.lblTotalCount.Text = "Total count:";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 583);
            this.Controls.Add(this.scChartData);
            this.Controls.Add(this.lblRainyDays);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAddTest);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gbLastMonthTemperature);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Weather Diary =)";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.rowMenu.ResumeLayout(false);
            this.gbLastMonthTemperature.ResumeLayout(false);
            this.gbLastMonthTemperature.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYearsDays)).EndInit();
            this.scChartData.Panel1.ResumeLayout(false);
            this.scChartData.Panel1.PerformLayout();
            this.scChartData.Panel2.ResumeLayout(false);
            this.scChartData.Panel2.PerformLayout();
            this.scChartData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ContextMenuStrip rowMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteRowItem;
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
        private System.Windows.Forms.ToolStripMenuItem WindForceToolStripMenuItem;
        private System.Windows.Forms.Label lblAvgTemperature;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvYearsDays;
        private System.Windows.Forms.Button btnAddTest;
        private System.Windows.Forms.ComboBox cbChartPeriod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpChrPeriodFrom;
        private System.Windows.Forms.DateTimePicker dtpChrPeriodTo;
        private System.Windows.Forms.Label lblChrPeriodFrom;
        private System.Windows.Forms.Label lblChrPeriodTo;
        private System.Windows.Forms.CheckedListBox clbCharts;
        private System.Windows.Forms.Label lblMesPeriodTo;
        private System.Windows.Forms.Label lblMesPeriodFrom;
        private System.Windows.Forms.DateTimePicker dtpMesPeriodTo;
        private System.Windows.Forms.DateTimePicker dtpMesPeriodFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMeasurePeriods;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRainyDays;
        private System.Windows.Forms.SplitContainer scChartData;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.Label lblTotalCount;
    }
}

