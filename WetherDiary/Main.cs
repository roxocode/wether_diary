using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBEngine;
using DBEngine.Access;

/*
 * Author: alex
 * 5 december 2012
 * TODO: проставить TabIndex
 * TODO: обработка ошибок RowError при редактировании строк (особено для столбца с датой, выпадающий календарь)
 * TODO: обработка ошибки когда пользователь удаляет запись из справочника которая используется в главной таблице (обработка события DataGridView.DataError)
 * TODO: возможность ввода доробных чисел в поля Температура и Давление (обработка события DataGridView.DataError)
 * TODO: отображать сообщение об ошибке когда нельзя вставить строку (при переименовании колонки Measure_Date в Date)
 * TODO: drop first column (empty) in DataGridView
 * TODO: 2013-06-15 возможность сохранять пользовательские настройки (ширина колонок, цвет) в файл
 */

namespace WetherDiary
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Движок БД
        /// </summary>
        private AccessDBEngine _engine;

        string tableName;

        /// <summary>
        /// ID записи
        /// </summary>
        public object ID;

        public MainForm()
        {
            InitializeComponent();

            #region Аналогия по годам

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

            dtpTime.Value = DateTime.Now;
            // DataGridView Initialize
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.MultiSelect = false;
            dgvMain.AutoGenerateColumns = false;
            dgvMain.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvMain.AllowUserToAddRows = false;
            //dgvMain.FirstDisplayed

            this.tableName = "wether";
            this._engine = new AccessDBEngine("wether.mdb");
            // Add columns
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Measure_Date";
            col.Name = "Measure_Date";
            col.HeaderText = "Дата";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "temperature";
            col.Name = "temperature";
            col.HeaderText = "Температура";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "pressure";
            col.Name = "pressure";
            col.HeaderText = "Давление";
            col.ValueType = typeof(double);
            dgvMain.Columns.Add(col);
            /* TODO: temporary disabled (выяснить причину) */
            var cloudColumn = new DataGridViewComboBoxColumn();
            cloudColumn.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM cloud"));
            cloudColumn.DataPropertyName = "Cloud_ID";
            cloudColumn.Name = "Cloud_ID";
            cloudColumn.HeaderText = "Облачность";
            cloudColumn.DisplayMember = "Name";
            cloudColumn.ValueMember = "ID";
            cloudColumn.ValueType = typeof(string);
            cloudColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(cloudColumn);

            var windColumn = new DataGridViewComboBoxColumn();
            windColumn.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM wind"));
            windColumn.DataPropertyName = "Wind_ID";
            windColumn.Name = "Wind_ID";
            windColumn.HeaderText = "Ветер";
            windColumn.DisplayMember = "Name";
            windColumn.ValueMember = "ID";
            windColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(windColumn);
            /**/
            /*
            var precipitationColumn = new DataGridViewComboBoxColumn();
            precipitationColumn.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM precipitation"));
            precipitationColumn.DataPropertyName = "Fallout_ID";
            precipitationColumn.Name = "Fallout_ID";
            precipitationColumn.HeaderText = "Осадки";
            precipitationColumn.DisplayMember = "Name";
            precipitationColumn.ValueMember = "ID";
            precipitationColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(precipitationColumn);
            */
            
            // Fill controls
            cbCloud.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT ID, Name FROM cloud"));
            cbCloud.DisplayMember = "Name";
            cbCloud.ValueMember = "ID";

            cbWind.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM wind"));
            cbWind.DisplayMember = "Name";
            cbWind.ValueMember = "ID";

            cbPrecipitation.DataSource = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM precipitation"));
            cbPrecipitation.DisplayMember = "Name";
            cbPrecipitation.ValueMember = "ID";

            // Columns color 
            // TODO: make setting by user
            dgvMain.Columns[0].DefaultCellStyle.BackColor = Color.Azure;
            dgvMain.Columns[2].DefaultCellStyle.BackColor = Color.Azure;
            //dgvMain.Columns[4].DefaultCellStyle.BackColor = Color.Azure;

            // TODO: rewrite
            DataTable dt = _engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM wether"));
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dgvMain.DataSource = bs;

            CurrentDateChanged(this, EventArgs.Empty);

            // Context Menu
            dgvMain.ContextMenuStrip = rowMenu;

            // Events
            // TODO: DialogResult on FormClosing event : read article in 'coding' bookmark folder
            // TODO: 2013-06-10 temporary disabled because of error while loading lage file (wether.mdb)
            dgvMain.CurrentCellChanged += CellChanged;
            dgvMain.MouseDown += dgvMain_MouseDown;
            deleteRowItem.Click += deleteRow;
            dtpDate.ValueChanged += CurrentDateChanged;
            dgvMain.DataError += new DataGridViewDataErrorEventHandler(OnGridDataError);
            dgvMain.CellMouseDoubleClick += dgvMain_CellMouseDoubleClick;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // TODO: Check for record with the same date (like in dtpDate) and if exist then select record
            DataTable wetherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
            DataRow row = wetherTable.NewRow();
            string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
            row["Measure_Date"] = fullDate;
            row["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
            row["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
            row["Cloud_ID"] = cbCloud.SelectedValue;
            row["Wind_ID"] = cbWind.SelectedValue;
            //row["Fallout_ID"] = cbPrecipitation.SelectedValue;
            wetherTable.Rows.Add(row);

            this.btnSave_Click(sender, EventArgs.Empty);
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
            //dgvMain.Rows[0].Cells[5].Value = 3;
        }

        /// <summary>
        /// Delete row
        /// </summary>
        void deleteRow(object sender, EventArgs e)
        {
            int deleteRowIndex = dgvMain.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (deleteRowIndex > -1)
            {
                DataTable wetherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
                wetherTable.TableName = this.tableName;
                // удаляем осадки (fallouts)
                string deleteFallouts = string.Format("DELETE FROM {0} WHERE Measure_ID = {1}", 
                    "fallouts",
                    wetherTable.Rows[deleteRowIndex]["ID"]);
                this._engine.ExecuteQuery(new OleDbCommand(deleteFallouts));
                dgvMain.Rows.RemoveAt(deleteRowIndex);
                this._engine.Update(wetherTable);
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
            // Максимальная и минимальная температуры за выбранный месяц
            string sql = @"SELECT 
	                MIN(Temperature) AS MinTemperature, 
	                MAX(Temperature) AS MaxTemperature,
                    AVG(Temperature) AS AvgTemperature 
                FROM wether 
                WHERE MONTH(Measure_Date) = {0} AND YEAR(Measure_Date) = {1}";
            OleDbCommand temperatureCmd = new OleDbCommand(string.Format(sql, 
                new object[] 
                {
                    dtpDate.Value.AddMonths(-1).Month,
                    dtpDate.Value.AddMonths(-1).Year
                }
                ));
            DataRow minMaxTempRow = this._engine.ExecuteQueryReturnDataRow(temperatureCmd);
            lblMinTemperature.Text = minMaxTempRow[0].ToString();
            lblMaxTemperature.Text = minMaxTempRow[1].ToString();
            // TODO: 2013-06-24 number format like xx.x in SQL
            lblAvgTemperature.Text = minMaxTempRow[2].ToString();

            // Температуры за все года аналогичного дня
            string yearDaysSql = "SELECT YEAR(Measure_Date) AS [Year], Temperature FROM wether WHERE MONTH(Measure_Date) = {0} AND DAY(Measure_Date) = {1} ORDER BY YEAR(Measure_Date)";
            OleDbCommand yearDaysCmd = new OleDbCommand(string.Format(yearDaysSql,
                new object[]
                {
                    dtpDate.Value.Month,
                    dtpDate.Value.Day
                }
                ));
            DataTable yearDaysTable = this._engine.ExecuteQueryReturnDataTable(yearDaysCmd);
            dgvYearsDays.DataSource = yearDaysTable;

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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable wetherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
            wetherTable.TableName = this.tableName;
            this._engine.Update(wetherTable);
            wetherTable.AcceptChanges();
        }

        private void OnGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введенные данные неверны. Проверте правильность данных.\nДля отмены нажмите Esc");
        }

        /// <summary>
        /// Справочник - Облачность
        /// </summary>
        private void CloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get DataTable
            DataGridViewComboBoxColumn cbc = dgvMain.Columns["Cloud_ID"] as DataGridViewComboBoxColumn;
            DataTable dt = cbc.DataSource as DataTable;
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
            // Get DataTable
            DataGridViewComboBoxColumn cbc = dgvMain.Columns["Fallout_ID"] as DataGridViewComboBoxColumn;
            DataTable dt = cbc.DataSource as DataTable;
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
            // Get DataTable
            DataGridViewComboBoxColumn cbc = dgvMain.Columns["Wind_ID"] as DataGridViewComboBoxColumn;
            DataTable dt = cbc.DataSource as DataTable;
            dt.TableName = "wind";
            Book precipitation = new Book(dt);
            precipitation.Text = "Ветер";
            precipitation.ShowDialog();
        }

        /// <summary>
        /// Двойной щелчок по строке
        /// </summary>
        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                // Convert DataGridViewRow to DataRow
                AddMeasure addMeasureForm = new AddMeasure(dgvMain.DataSource, ((DataRowView)dgvMain.Rows[e.RowIndex].DataBoundItem).Row);
                //addMeasureForm.Owner = this;
                if (addMeasureForm.ShowDialog() == DialogResult.OK)
                    this.btnSave_Click(addMeasureForm, EventArgs.Empty);
                addMeasureForm.Close();
                addMeasureForm.Dispose();
            }
        }

        /// <summary>
        /// Добавляем новый замер
        /// </summary>
        private void btnAddTest_Click(object sender, EventArgs e)
        {
            AddMeasure addMeasureForm = new AddMeasure(dgvMain.DataSource, null);
            if (addMeasureForm.ShowDialog() == DialogResult.OK)
            {
                int newRowIndex = -1;
                // ToDo: maybe make wetherTable property of this class ?
                DataTable wetherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;
                // ищем индекс добавленной строки
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    if (wetherTable.Rows[i].RowState == DataRowState.Added)
                        newRowIndex = i;
                }
                this.btnSave_Click(addMeasureForm, EventArgs.Empty);
                // и получаем ее ID
                MessageBox.Show(wetherTable.Rows[newRowIndex]["ID"].ToString());
                addMeasureForm.SaveFallouts(wetherTable.Rows[newRowIndex]["ID"]);
            }
            addMeasureForm.Close();
            addMeasureForm.Dispose();
        }
    }
}
