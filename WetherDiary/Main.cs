using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WetherDiary.UserSettings;
using DBEngine;
using DBEngine.Access;
using System.Linq;

/*
 * Author: alex
 * 5 december 2012
 * 
 * TODO: 2014-11-09 move the logic from event handlers (I should separete my business and UI logic, no business logic in my control event handlers)
 * TODO: облачность ветер и осадки в виде иконок
 * TODO: день на графике с 1 числа, а не с 0
 * TODO: проставить TabIndex
 * TODO: обработка ошибок RowError при редактировании строк (особено для столбца с датой, выпадающий календарь)
 * TODO: обработка ошибки когда пользователь удаляет запись из справочника которая используется в главной таблице (обработка события DataGridView.DataError)
 * TODO: возможность ввода доробных чисел в поля Температура и Давление (обработка события DataGridView.DataError)
 * TODO: 2014-11-08 интерфейс для изменения цвета колонок
 * TODO: возможно для функций выполнения запросов тип аргумента сделать отличным от SQLiteCommand, OleDbCommand etc. сделать полиморфизм
 * TODO: сделать определенные цвета на графике
 */

namespace WetherDiary
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Движок БД
        /// </summary>
        private SQLiteDBEngine _engine;

        string tableName;

        /// <summary>
        /// ID записи
        /// </summary>
        public object ID;

        /// <summary>
        /// График
        /// </summary>
        public System.Windows.Forms.DataVisualization.Charting.Chart crtWeather = new System.Windows.Forms.DataVisualization.Charting.Chart();

        public MainForm()
        {
            InitializeComponent();

            // Настраиваем програмно-созданный MSChart
            crtWeather.Name = "WeatherChart";
            crtWeather.Location = new Point(15, 214);
            crtWeather.Size = new System.Drawing.Size(576, 157);
            crtWeather.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea"));
            this.Controls.Add(crtWeather);

            // chart control customization
            crtWeather.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            crtWeather.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            crtWeather.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            crtWeather.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            crtWeather.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.DarkGray;
            crtWeather.Legends.Add("Legend");

            // DataGridView Initialize
            dgvMain.RowHeadersVisible = false;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.MultiSelect = false;
            dgvMain.AutoGenerateColumns = false;
            dgvMain.ReadOnly = true;
            dgvMain.AllowUserToAddRows = false;
            this.tableName = "weather";
            this._engine = new SQLiteDBEngine(Properties.Settings.Default.DbName);
            // TODO: may be make dependence on IconSize ?
            dgvMain.RowTemplate.Height = Properties.Settings.Default.RowHeight;

            cbChartPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChartPeriod.Items.AddRange(new object[] { "7 дней", "Месяц", "Задать вручную ..." });
            cbChartPeriod.SelectedIndex = 1;

            cbMeasurePeriods.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMeasurePeriods.Items.AddRange(new object[] { "7 дней", "Месяц", "Задать вручную ..." });
            cbMeasurePeriods.SelectedIndex = 0;

            clbCharts.CheckOnClick = true;
            clbCharts.ItemCheck += clbCharts_ItemCheck;

            #region Аналогия по годам

            dgvYearsDays.RowHeadersVisible = false;
            dgvYearsDays.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvYearsDays.AllowUserToAddRows = false;
            dgvYearsDays.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Year";
            column.Name = "Год";
            column.Width = 70;
            dgvYearsDays.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Temperature";
            column.Name = "Температура";
            column.Width = 100;
            dgvYearsDays.Columns.Add(column);

            #endregion

            #region Grid Columns

            var idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "ID";
            idColumn.Name = "ID";
            idColumn.Width = 40;
            idColumn.Visible = false;
            dgvMain.Columns.Add(idColumn);

            var dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.DataPropertyName = "Measure_Date";
            dateColumn.Name = "Measure_Date";
            dateColumn.HeaderText = "Дата";
            dgvMain.Columns.Add(dateColumn);

            var tempColumn = new DataGridViewTextBoxColumn();
            tempColumn.DataPropertyName = "temperature";
            tempColumn.Name = "temperature";
            tempColumn.HeaderText = "Температура";
            tempColumn.Width = 90;
            dgvMain.Columns.Add(tempColumn);

            var pressureColumn = new DataGridViewTextBoxColumn();
            pressureColumn.DataPropertyName = "pressure";
            pressureColumn.Name = "pressure";
            pressureColumn.HeaderText = "Давление";
            pressureColumn.Width = 70;
            pressureColumn.ValueType = typeof(double);
            dgvMain.Columns.Add(pressureColumn);
            
            var cloudColumn = new IconTextBox.IconTextColumn();
            cloudColumn.DataPropertyName = "Cloud_Name";
            cloudColumn.Name = "Cloud_Name";
            cloudColumn.HeaderText = "Облачность";
            dgvMain.Columns.Add(cloudColumn);
            
            var windColumn = new IconTextBox.IconTextColumn();
            windColumn.DataPropertyName = "Wind_Name";
            windColumn.Name = "Wind_Name";
            windColumn.HeaderText = "Ветер";
            dgvMain.Columns.Add(windColumn);
            
            var windForceColumn = new IconTextBox.IconTextColumn();
            windForceColumn.DataPropertyName = "WindForce_Name";
            windForceColumn.Name = "WindForce_Name";
            windForceColumn.HeaderText = "Сила ветра";
            dgvMain.Columns.Add(windForceColumn);

            var falloutsImageColumn = new DataGridViewImageColumn();
            falloutsImageColumn.DataPropertyName = "FalloutsImg";
            falloutsImageColumn.Name = "FalloutsImg";
            falloutsImageColumn.HeaderText = "Осадки";
            dgvMain.Columns.Add(falloutsImageColumn);
            // Убираем иконку (красный крестик) для ячейки без значения
            dgvMain.Columns["FalloutsImg"].DefaultCellStyle.NullValue = null;
            
            // Columns color 
            // TODO: make setting by user
            dgvMain.Columns[0].DefaultCellStyle.BackColor = Color.Azure;
            dgvMain.Columns[2].DefaultCellStyle.BackColor = Color.Azure;

            #endregion

            cbMeasurePeriods_SelectedIndexChanged(null, EventArgs.Empty);
            
            // Для перерисовки графика при запуске программы
            CurrentDateChanged(this, EventArgs.Empty);

            // Context Menu
            dgvMain.ContextMenuStrip = rowMenu;

            #region Events
            
            // TODO: DialogResult on FormClosing event: when close AddMeasure
            dgvMain.CurrentCellChanged += CellChanged;
            dgvMain.MouseDown += dgvMain_MouseDown;
            deleteRowItem.Click += deleteRow;
            dgvMain.DataError += new DataGridViewDataErrorEventHandler(OnGridDataError);
            dgvMain.CellMouseDoubleClick += dgvMain_CellMouseDoubleClick;

            dtpDate.ValueChanged += CurrentDateChanged;
            // Needed for stop throwing ValueChanged event while browsing DateTimePicker
            dtpDate.DropDown += (s, e) => { dtpDate.ValueChanged -= CurrentDateChanged; };
            dtpDate.CloseUp += (s, e) => { dtpDate.ValueChanged += CurrentDateChanged; CurrentDateChanged(null, EventArgs.Empty); };
            // Needed for stop throwing ValueChanged event while browsing DateTimePicker
            dtpMesPeriodFrom.DropDown += (s, e) => { dtpMesPeriodFrom.ValueChanged -= dtpMesPeriod_ValueChanged; };
            dtpMesPeriodTo.DropDown += (s, e) => { dtpMesPeriodTo.ValueChanged -= dtpMesPeriod_ValueChanged; };
            dtpMesPeriodFrom.CloseUp += (s, e) => { dtpMesPeriodFrom.ValueChanged += dtpMesPeriod_ValueChanged; 
                dtpMesPeriod_ValueChanged(null, EventArgs.Empty); };
            dtpMesPeriodTo.CloseUp += (s, e) => { dtpMesPeriodTo.ValueChanged -= dtpMesPeriod_ValueChanged; 
                dtpMesPeriod_ValueChanged(null, EventArgs.Empty); };

            cbChartPeriod.SelectedIndexChanged += cbChartPeriod_SelectedIndexChanged;
            cbMeasurePeriods.SelectedIndexChanged += cbMeasurePeriods_SelectedIndexChanged;
            dgvYearsDays.SelectionChanged += (s, e) => { dgvYearsDays.ClearSelection(); };

            // TODO 2014-11-07: test Maybe extract to method
            this.Load += (s, e) => 
            {
                // Restore form position from user settings (Settings.settings)
                this.WindowState = Properties.Settings.Default.WindowState;
                this.Location = Properties.Settings.Default.WindowLocation;
                this.Size = Properties.Settings.Default.WindowSize;

                // columns
                string columnsCode = Properties.Settings.Default.GridColumns;
                Serializer.SetDeserializeColSetting(dgvMain, columnsCode);
            };
            this.FormClosing += (s, e) => 
            {
                // Save current form position to user settings (Settings.settings)
                if (this.WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.WindowState = FormWindowState.Maximized;
                    Properties.Settings.Default.WindowLocation = RestoreBounds.Location;
                    Properties.Settings.Default.WindowSize = RestoreBounds.Size;
                }
                else if (this.WindowState == FormWindowState.Minimized)
                {
                    Properties.Settings.Default.WindowState = FormWindowState.Normal;
                    Properties.Settings.Default.WindowLocation = RestoreBounds.Location;
                    Properties.Settings.Default.WindowSize = RestoreBounds.Size;
                }
                else
                {
                    Properties.Settings.Default.WindowState = FormWindowState.Normal;
                    Properties.Settings.Default.WindowLocation = this.Location;
                    Properties.Settings.Default.WindowSize = this.Size;
                }
                
                // Save DataGridView columns setting
                Properties.Settings.Default.GridColumns = Serializer.GetSerializeColSetting(dgvMain);
                Properties.Settings.Default.Save();
            };
            // test

            #endregion
        }

        void cbMeasurePeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action<bool> ToggleVisiblePeriods = (bool visible) =>
            {
                if (visible)
                {
                    lblMesPeriodFrom.Visible = true;
                    lblMesPeriodTo.Visible = true;
                    dtpMesPeriodFrom.Visible = true;
                    dtpMesPeriodTo.Visible = true;
                }
                else
                {
                    lblMesPeriodFrom.Visible = false;
                    lblMesPeriodTo.Visible = false;
                    dtpMesPeriodFrom.Visible = false;
                    dtpMesPeriodTo.Visible = false;
                }
            };

            string sqlMeasures;
            BindingSource bs = new BindingSource();
            switch (cbMeasurePeriods.SelectedIndex)
            {
                case 0:
                    // 7 days
                    ToggleVisiblePeriods(false);
                    dtpDate.Value.AddDays(-7);
                    //TODO: 2014-11-14 dgvMain.DataSource = GetMeasures(DateTime from, DateTime to);
                    sqlMeasures = string.Format(@"
                        SELECT 
                            wth.*, 
                            w.Name AS Wind_Name,
                            c.Name AS Cloud_Name,
                            wf.Name AS WindForce_Name
                        FROM weather wth 
                            LEFT JOIN wind w ON wth.Wind_ID = w.ID 
                            LEFT JOIN cloud c ON wth.Cloud_ID = c.ID
                            LEFT JOIN windForce wf ON wth.WindForce_ID = wf.ID
                        WHERE date(Measure_Date) <= date('{0}') 
                        ORDER BY Measure_Date DESC LIMIT {1}",
                        dtpDate.Value.ToString("yyyy-MM-dd"),
                        7);
                    bs.DataSource = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand(sqlMeasures));
                    UpdateFalloutsIconColumn(bs.DataSource as DataTable);
                    dgvMain.DataSource = bs;
                    break;
                case 1:
                    // 1 month
                    ToggleVisiblePeriods(false);
                    sqlMeasures = string.Format("SELECT * FROM weather WHERE date(Measure_Date) <= date('{0}') ORDER BY Measure_Date DESC LIMIT {1}",
                        dtpDate.Value.ToString("yyyy-MM-dd"),
                        30);
                    bs.DataSource = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand(sqlMeasures));
                    UpdateFalloutsIconColumn(bs.DataSource as DataTable);
                    dgvMain.DataSource = bs;
                    break;
                case 2:
                    // Custom period
                    ToggleVisiblePeriods(true);
                    sqlMeasures = string.Format("SELECT * FROM weather WHERE date(Measure_Date) BETWEEN date('{0}') AND date('{1}') ORDER BY Measure_Date DESC",
                        dtpMesPeriodFrom.Value.ToString("yyyy-MM-dd"),
                        dtpMesPeriodTo.Value.ToString("yyyy-MM-dd"));
                    bs.DataSource = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand(sqlMeasures));
                    UpdateFalloutsIconColumn(bs.DataSource as DataTable);
                    dgvMain.DataSource = bs;
                    break;
            }
        }

        /// <summary>
        /// Check / Uncheck relevant graph
        /// </summary>
        void clbCharts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                crtWeather.Series[clbCharts.GetItemText(clbCharts.Items[e.Index])].Enabled = true;
            }
            else
            {
                crtWeather.Series[clbCharts.GetItemText(clbCharts.Items[e.Index])].Enabled = false;
            }
        }

        void cbChartPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action<bool> ToggleVisiblePeriods = (bool visible) =>
                {
                    if (visible)
                    {
                        lblChrPeriodFrom.Visible = true;
                        lblChrPeriodTo.Visible = true;
                        dtpChrPeriodFrom.Visible = true;
                        dtpChrPeriodTo.Visible = true;
                    }
                    else
                    {
                        lblChrPeriodFrom.Visible = false;
                        lblChrPeriodTo.Visible = false;
                        dtpChrPeriodFrom.Visible = false;
                        dtpChrPeriodTo.Visible = false;
                    }
                };
            switch (cbChartPeriod.SelectedIndex)
            {
                case 0:
                    ToggleVisiblePeriods(false);
                    break;
                case 1:
                    ToggleVisiblePeriods(false);
                    
                    string sqlGetMonths = string.Format(
                                            "SELECT strftime('%Y', Measure_Date) AS Year, date(Measure_Date) AS Date, Temperature FROM weather WHERE CAST(strftime('%m', Measure_Date) AS INTEGER) = {0}",
                                            dtpDate.Value.Month);
                    DataTable monthsData = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand(sqlGetMonths));
                    // года за которые есть замеры
                    string[] years = 
                        (from row in monthsData.AsEnumerable()
                        group row by row.Field<string>("Year") 
                        into grp
                        orderby grp.Key
                        select grp.Key).ToArray();

                    // формат t['год'] = { 'день' = 'температура' }
                    Dictionary<short, Dictionary<int, short>> t = new Dictionary<short, Dictionary<int, short>>();

                    clbCharts.Items.Clear();
                    crtWeather.Series.Clear();

                    foreach (string year in years)
                    {
                        t.Add(Convert.ToInt16(year), null);
                        clbCharts.Items.Add(year);

                        var tmp = from row in monthsData.AsEnumerable()
                                  where row.Field<string>("Year") == year
                                  select new
                                  {
                                      Day = Convert.ToDateTime(row.Field<string>("Date")).Day,
                                      Temperature = row.Field<double>("Temperature")
                                  };
                        // Сортируем по дням
                        tmp = from row in tmp
                              orderby row.Day
                              select new { row.Day, row.Temperature };

                        t[Convert.ToInt16(year)] = tmp.ToDictionary(x => x.Day, x => Convert.ToInt16(x.Temperature));
                        // Добавляем данные на график
                        crtWeather.Series.Add(year).Points.DataBindXY(t[Convert.ToInt16(year)].Keys, t[Convert.ToInt16(year)].Values);
                        crtWeather.Series[year].Enabled = false;
                        crtWeather.Series[year].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        crtWeather.Series[year].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                        crtWeather.Series[year].ToolTip = "#VAL";
                    }
                    
                    // Отмечаем только текущий год
                    // TODO: LINQ for clbCharts.Items
                    var activeYears = (from year in years
                                   where int.Parse(year) <= dtpDate.Value.Year
                                   orderby int.Parse(year)
                                   select int.Parse(year));
                    
                    if (activeYears.Any())
                        clbCharts.SetItemChecked(clbCharts.Items.IndexOf(activeYears.Max().ToString()), true);

                    break;
                case 2:
                    ToggleVisiblePeriods(true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adopt to row changed
        /// </summary>
        void CellChanged(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null)
            {
                if (!object.Equals(this.ID, dgvMain.CurrentRow.Cells["ID"].Value) )
                {
                    this.ID = dgvMain.CurrentRow.Cells["ID"].Value;
                    RowChanged(sender, e);
                }
            }
            else
                this.ID = null;
        }

        /// <summary>
        /// Change row selection
        /// </summary>
        void RowChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Delete row
        /// </summary>
        void deleteRow(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы действительно хотите удалить выделенный замер?",
                "Удалить замер?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int deleteRowIndex = dgvMain.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (deleteRowIndex > -1)
                {
                    DataTable weatherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
                    weatherTable.TableName = this.tableName;
                    // удаляем осадки (fallouts)
                    string deleteFallouts = string.Format("DELETE FROM {0} WHERE Measure_ID = {1}",
                        "fallouts",
                        weatherTable.Rows[deleteRowIndex]["ID"]);
                    this._engine.ExecuteQuery(new SQLiteCommand(deleteFallouts));
                    dgvMain.Rows.RemoveAt(deleteRowIndex);
                    this._engine.Update(weatherTable);
                    // redraw chart, update precipitation count and max, min and avg temp.
                    cbChartPeriod_SelectedIndexChanged(dtpDate, EventArgs.Empty);
                    RainyDaysCurMonth();
                    MaxMinAgvTemperatureCurMonth();
                }
            }
        }

        void dgvMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dgvMain.HitTest(e.X, e.Y);
                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    dgvMain.ClearSelection();
                    dgvMain.Rows[hti.RowIndex].Selected = true;
                }
            }
        }

        /// <summary>
        /// Change current date
        /// </summary>
        void CurrentDateChanged(object sender, EventArgs e)
        {
            gbLastMonthTemperature.Text = dtpDate.Value.ToString("MMMM");
            
            MaxMinAgvTemperatureCurMonth();

            TemperatureForAllYears();

            RainyDaysCurMonth();

            // Выделяем строку с выбранной датой
            DateTime selectedDate;
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                selectedDate = Convert.ToDateTime(dgvMain.Rows[i].Cells["Measure_Date"].Value);
                if (selectedDate.ToShortDateString() == dtpDate.Value.ToShortDateString())
                {
                    dgvMain.Rows[i].Selected = true;
                    dgvMain.FirstDisplayedScrollingRowIndex = i;
                }
            }

            // Перерисовываем график
            cbChartPeriod_SelectedIndexChanged(dtpDate, EventArgs.Empty);
            // Refresh table with measurements
            cbMeasurePeriods_SelectedIndexChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Maximum, minimum and average temperature for current month
        /// </summary>
        private void MaxMinAgvTemperatureCurMonth()
        {
            string sql = @"SELECT 
	                MIN(Temperature) AS MinTemperature, 
	                MAX(Temperature) AS MaxTemperature,
                    AVG(Temperature) AS AvgTemperature 
                FROM weather 
                WHERE 
                CAST(strftime('%m', Measure_Date) AS INTEGER) = '{0}' 
                AND CAST(strftime('%Y', Measure_Date) AS INTEGER) = '{1}'";
            SQLiteCommand temperatureCmd = new SQLiteCommand(string.Format(sql,
                new object[] 
                {
                    dtpDate.Value.Month,
                    dtpDate.Value.Year
                }));
            DataRow minMaxTempRow = this._engine.ExecuteQueryReturnDataRow(temperatureCmd);
            lblMinTemperature.Text = minMaxTempRow[0].ToString();
            lblMaxTemperature.Text = minMaxTempRow[1].ToString();
            double avgTemperature = (minMaxTempRow[2] == DBNull.Value) ? 0 : Convert.ToDouble(minMaxTempRow[2]);
            lblAvgTemperature.Text = avgTemperature.ToString("##.#");
        }

        /// <summary>
        /// Temperatures for all years
        /// </summary>
        private void TemperatureForAllYears()
        {
            string yearDaysSql = @"
                SELECT strftime('%Y', Measure_Date) AS [Year], Temperature 
                FROM weather 
                WHERE 
                    CAST(strftime('%m', Measure_Date) AS INTEGER) = '{0}' 
                    AND CAST(strftime('%d', Measure_Date) AS INTEGER) = '{1}' 
                ORDER BY strftime('%Y', Measure_Date) DESC";
            SQLiteCommand yearDaysCmd = new SQLiteCommand(string.Format(yearDaysSql,
                new object[]
                {
                    dtpDate.Value.Month,
                    dtpDate.Value.Day
                }));
            DataTable yearDaysTable = this._engine.ExecuteQueryReturnDataTable(yearDaysCmd);
            dgvYearsDays.DataSource = yearDaysTable;
            // Помечаем ячейку с минимальной и максимальной температурой 
            if (yearDaysTable.Rows.Count >= 2)
                MarkMinAndMaxTemp();
        }

        /// <summary>
        /// Mark minimum and maximum teperatures in table
        /// </summary>
        private void MarkMinAndMaxTemp()
        {
            var minTemp = (from row in ((DataTable)dgvYearsDays.DataSource).AsEnumerable() 
                           orderby row.Field<Double>("Temperature")
                           select
                            row.Field<Double>("Temperature")).FirstOrDefault();

            var maxTemp = (from row in ((DataTable)dgvYearsDays.DataSource).AsEnumerable() 
                           orderby row.Field<Double>("Temperature") descending
                           select 
                            row.Field<Double>("Temperature")).FirstOrDefault();

            foreach (DataGridViewRow dgr in dgvYearsDays.Rows)
            {
                if (Convert.ToDouble(dgr.Cells[1].Value) == minTemp)
                    dgr.DefaultCellStyle.BackColor = Color.OrangeRed;
                if (Convert.ToDouble(dgr.Cells[1].Value) == maxTemp)
                    dgr.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        /// <summary>
        /// Quantity of days with precipitations in current month
        /// </summary>
        private void RainyDaysCurMonth()
        {
            string rainyDaysSql = @"
                SELECT
	                COUNT(DISTINCT fs.Measure_ID) AS RainyDays
                FROM fallouts fs 
                INNER JOIN weather w ON fs.Measure_ID = w.ID
                WHERE 
	                fs.Fallout_ID IN (8, 9, 10, 14, 15)
	                AND CAST(strftime('%m', Measure_Date) AS INTEGER) = {0}
	                AND CAST(strftime('%Y', Measure_Date) AS INTEGER) = {1}";
            SQLiteCommand rainyDaysCmd = new SQLiteCommand(string.Format(rainyDaysSql,
                new object[]
                {
                    dtpDate.Value.Month,
                    dtpDate.Value.Year
                }));
            DataRow rainyDays = this._engine.ExecuteQueryReturnDataRow(rainyDaysCmd);
            lblRainyDays.Text = rainyDays["RainyDays"].ToString();
        }

        private void SaveToDB()
        {
            DataTable weatherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
            weatherTable.TableName = this.tableName;
            this._engine.Update(weatherTable);
            weatherTable.AcceptChanges();
        }

        private void OnGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введены неверные данные. Проверте правильность данных.\nДля отмены нажмите Esc");
        }

        /// <summary>
        /// Справочник - Облачность
        /// </summary>
        private void CloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT * FROM cloud"));
            dt.TableName = "cloud";
            Book cloud = new Book(dt);
            cloud.Text = "Облачность";
            cloud.ShowDialog();
        }

        /// <summary>
        /// Справочник - Осадки
        /// </summary>
        private void FalloutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT * FROM fallout"));
            dt.TableName = "fallout";
            Book precipitation = new Book(dt);
            precipitation.Text = "Осадки";
            precipitation.ShowDialog();
        }

        /// <summary>
        /// Справочник - Направление ветера
        /// </summary>
        private void WindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT * FROM wind"));
            dt.TableName = "wind";
            Book precipitation = new Book(dt);
            precipitation.Text = "Ветер";
            precipitation.ShowDialog();
        }

        /// <summary>
        /// Справочник - Сила ветра
        /// </summary>
        private void WindForceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT * FROM windForce"));
            dt.TableName = "windForce";
            Book windForce = new Book(dt);
            windForce.Text = "Сила ветра";
            windForce.ShowDialog();
        }

        /// <summary>
        /// Двойной щелчок по строке
        /// </summary>
        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowIndex >= 0)
            {
                // Convert DataGridViewRow to DataRow
                AddMeasure addMeasureForm = new AddMeasure(dgvMain.DataSource, ((DataRowView)dgvMain.Rows[e.RowIndex].DataBoundItem).Row);
                //addMeasureForm.Owner = this;
                if (addMeasureForm.ShowDialog() == DialogResult.OK)
                {
                    this.SaveToDB();
                    CurrentDateChanged(null, EventArgs.Empty);
                }
                addMeasureForm.Close();
                addMeasureForm.Dispose();
            }
        }

        /// <summary>
        /// Добавляем новый замер
        /// </summary>
        private void btnAddMeasure_Click(object sender, EventArgs e)
        {
            AddMeasure addMeasureForm = new AddMeasure(dgvMain.DataSource, null);
            if (addMeasureForm.ShowDialog() == DialogResult.OK)
            {
                int newRowIndex = -1;
                // TODO: maybe make weatherTable parameter of this class ?
                DataTable weatherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
                // ищем индекс добавленной строки
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (weatherTable.Rows[i].RowState == DataRowState.Added)
                        newRowIndex = i;
                }
                this.SaveToDB();
                // и получаем ее ID
                #if DEBUG
                    MessageBox.Show(String.Format("New row ID: {0}", weatherTable.Rows[newRowIndex]["ID"]));
                #endif
                addMeasureForm.SaveFallouts(weatherTable.Rows[newRowIndex]["ID"]);
                //((BindingSource)dgvMain.DataSource).ResetBindings(true);
                // Updating
                CurrentDateChanged(null, EventArgs.Empty);
            }
            addMeasureForm.Close();
            addMeasureForm.Dispose();
        }

        /// <summary>
        /// Update fallouts icons in appropriate column
        /// </summary>
        private void UpdateFalloutsIconColumn(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("FalloutsImg", typeof(Image)));
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dtIconPaths = _engine.ExecuteQueryReturnDataTable(new SQLiteCommand(string.Format(
                    "SELECT IconPath FROM fallouts fs INNER JOIN fallout f ON fs.Fallout_ID = f.ID WHERE Measure_ID = {0}",// AND IconPath <> ''",
                    dr["ID"])));

                if (dtIconPaths.Rows.Count > 1)
                {
                    // TODO: 2014-04-22 Проверки на существование файла и на подходящий размер
                    // Предполагаем что для каждой записи с непустым IconPath есть валидная иконка и ее размер 
                    // Длину считаем как кол-во иконок * их длину + расстояние между ними (4 px)
                    int spaceBtwIcons = 4;

                    int outputImgWidth = dtIconPaths.Rows.Count * Icons.IconSize + (dtIconPaths.Rows.Count - 1) * spaceBtwIcons;
                    Bitmap outputImage = new Bitmap(outputImgWidth, Icons.IconSize);

                    using (Graphics outputGraphics = Graphics.FromImage(outputImage))
                    {
                        foreach (DataRow drIconPath in dtIconPaths.Rows)
                        {
                            outputGraphics.DrawImage(Icons.GetIconImage(drIconPath["IconPath"].ToString()),
                                (Icons.IconSize + spaceBtwIcons) * dtIconPaths.Rows.IndexOf(drIconPath), 0);
                        }
                    }
                    dr["FalloutsImg"] = outputImage;
                }
                else if (dtIconPaths.Rows.Count == 1)
                {
                    dr["FalloutsImg"] = Icons.GetIconImage(dtIconPaths.Rows[0]["IconPath"].ToString());
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Added to this event because can't change Row.BackColor before the DataGridView will shown
            // because of this, rows don't mark just after start application 
            MarkMinAndMaxTemp();
        }

        private void dtpMesPeriod_ValueChanged(object sender, EventArgs e)
        {
            cbMeasurePeriods_SelectedIndexChanged(null, EventArgs.Empty);
        }
    }
}
