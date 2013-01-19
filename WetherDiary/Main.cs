using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBEngine;
using DBEngine.Access;

/*
 * Author: alex
 * 5 december 2012
 * TODO: проставить TabIndex
 * /

namespace WetherDiary
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Движок БД
        /// </summary>
        private AccessDBEngine _engine;

        /// <summary>
        /// Ключ записи
        /// </summary>
        public object ID;

        public MainForm()
        {
            InitializeComponent();

            dtpTime.Value = DateTime.Now;
            // DataGridView Initialize
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.AutoGenerateColumns = false;
            dgvMain.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvMain.AllowUserToAddRows = false;

            this._engine = new AccessDBEngine();
            // Add columns
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "ID";
            col.Name = "ID";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Sample_Date";
            col.Name = "Дата";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "time";
            col.Name = "Время";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "temperature";
            col.Name = "Температура";
            dgvMain.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "pressure";
            col.Name = "Давление";
            dgvMain.Columns.Add(col);

            // FixMe:
            var cloudColumn = new DataGridViewComboBoxColumn();
            cloudColumn.DataSource = _engine.ExecuteQuery("SELECT * FROM cloud");
            cloudColumn.DataPropertyName = "CloudID";
            cloudColumn.DisplayMember = "Name";
            cloudColumn.ValueMember = "ID";
            cloudColumn.HeaderText = "Облачность";
            cloudColumn.Name = "CloudID";
            cloudColumn.ValueType = typeof(string);
            cloudColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(cloudColumn);

            var windColumn = new DataGridViewComboBoxColumn();
            windColumn.DataSource = _engine.ExecuteQuery("SELECT * FROM wind");
            windColumn.DataPropertyName = "Wind_ID";
            windColumn.DisplayMember = "Name";
            windColumn.ValueMember = "ID";
            windColumn.Name = "Ветер";
            windColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(windColumn);

            var precipitationColumn = new DataGridViewComboBoxColumn();
            precipitationColumn.DataSource = _engine.ExecuteQuery("SELECT * FROM precipitation");
            precipitationColumn.DataPropertyName = "Precipitation_ID";
            precipitationColumn.DisplayMember = "Name";
            precipitationColumn.ValueMember = "ID";
            precipitationColumn.Name = "Осадки";
            precipitationColumn.FlatStyle = FlatStyle.Flat;
            dgvMain.Columns.Add(precipitationColumn);

            // Fill controls
            cbCloud.DataSource = _engine.ExecuteQuery("SELECT ID, Name FROM cloud");
            cbCloud.DisplayMember = "Name";
            cbCloud.ValueMember = "ID";

            cbWind.DataSource = _engine.ExecuteQuery("SELECT * FROM wind");
            cbWind.DisplayMember = "Name";
            cbWind.ValueMember = "ID";

            cbPrecipitation.DataSource = _engine.ExecuteQuery("SELECT * FROM precipitation");
            cbPrecipitation.DisplayMember = "Name";
            cbPrecipitation.ValueMember = "ID";

            DataTable dt = _engine.Test();
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dgvMain.DataSource = bs;

            // Context Menu
            dgvMain.ContextMenuStrip = rowMenu;

            // Events
            dgvMain.CurrentCellChanged += CellChanged;
            dgvMain.MouseDown += dgvMain_MouseDown;
            deleteRowItem.Click += deleteRow;
            dtpTime.ValueChanged += CurrentDateChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable wetherTable = (dgvMain.DataSource as BindingSource).DataSource as DataTable;


            string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
            DataRow row = wetherTable.NewRow();
            row["Sample_Date"] = fullDate;
            row["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
            row["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
            row["Cloud_ID"] = cbCloud.SelectedValue;
            row["Wind_ID"] = cbWind.SelectedValue;
            row["Precipitation_ID"] = cbPrecipitation.SelectedValue;
            wetherTable.Rows.Add(row);

            this._engine.Update(wetherTable);

            /*
            OleDbCommand saveCommand = new OleDbCommand();

            // TODO: Allow input only numbers (with decimal and also negative)
            string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");

            saveCommand = new OleDbCommand(string.Format("INSERT INTO wether (Sample_Date, Temperature, Pressure, Cloud_ID, Wind_ID, Precipitation_ID) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                new object[]
                {
                    Converters.ConvertDateToAccess(fullDate),
                    Converters.ConvertNumbToAccess(tbTemperature.Text),
                    Converters.ConvertNumbToAccess(tbPressure.Text),
                    cbCloud.SelectedValue,
                    cbWind.SelectedValue,
                    cbPrecipitation.SelectedValue
                }));           

            this._engine.ExecuteQuery(saveCommand);
            */
        }

        /*
        // Save button action for adding new record and edit existing
        private void btnSave_Click(object sender, EventArgs e)
        {
            // FIXME: don't execute query with paremeters
            // Durty fix: replace query without parameters
            //OleDbCommand saveCommand = new OleDbCommand("INSERT INTO wether (Sample_Date, Temperature, Pressure, Cloud_ID, Wind_ID, Precipitation_ID) VALUES (?, ?, ?, ?, ?, ?)");

            //saveCommand.Parameters.Add(new OleDbParameter("Sample_Date", "#2012-09-20 00:00:00#"));
            //saveCommand.Parameters.Add(new OleDbParameter("@Temperature", "1"));
            //saveCommand.Parameters.Add(new OleDbParameter("@Pressure", "2"));
            //saveCommand.Parameters.Add(new OleDbParameter("@Cloud_ID", "3"));
            //saveCommand.Parameters.Add(new OleDbParameter("@Wind_ID", "4"));
            //saveCommand.Parameters.Add(new OleDbParameter("@Precipitation_ID", "5"));
            
            OleDbCommand saveCommand = new OleDbCommand();
            
            if (this.ID != null)
            {
                // Changing record
                // UPDATE Command
                if (DialogResult.Yes == MessageBox.Show("", "", MessageBoxButtons.YesNo))
                {
                    // TODO: Allow input only numbers (with decimal and also negative)
                    string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
                    saveCommand = new OleDbCommand(string.Format("UPDATE wether SET Sample_Date = {2}, Temperature = {3}, Pressure = {4}, Cloud_ID = {5}, Wind_ID = {6}, Precipitation_ID = {7} WHERE {0} = {1}",
                        new object[]
                    {
                        "ID",
                        this.ID,
                        Converters.ConvertDateToAccess(fullDate),
                        // TODO: Replace function for specific control (NumericBox)
                        Converters.ConvertNumbToAccess(tbTemperature.Text),
                        Converters.ConvertNumbToAccess(tbPressure.Text),
                        cbCloud.SelectedValue,
                        cbWind.SelectedValue,
                        cbPrecipitation.SelectedValue
                    }));
                }
            }
            else
            {
                // New record
                // INSERT Command
                saveCommand = new OleDbCommand("INSERT INTO wether (Sample_Date, Temperature, Pressure, Cloud_ID, Wind_ID, Precipitation_ID) VALUES ({0}, {1}, {2}, {3}, {4}, {5})");
            }

            this._engine.ExecuteQuery(saveCommand);
            this.dgvMain.Update();
        }
        */

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
            if (deleteRowIndex > 0)
                dgvMain.Rows.RemoveAt(deleteRowIndex);
            // TODO: !!! attention check 2013.01.17
            /*
            object cellID = dgvMain.Rows[deleteRowIndex].Cells["ID"].Value;
            if (DialogResult.Yes == MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                if (cellID != null && cellID != DBNull.Value)
                {
                    OleDbCommand deleteCommand = new OleDbCommand(string.Format("DELETE FROM wether WHERE ID = {0}", cellID));
                    //deleteCommand.Parameters.Add(new OleDbParameter("ID", id));
                    this._engine.ExecuteQuery(deleteCommand);
                }
                dgvMain.Rows.RemoveAt(deleteRowIndex);
            }
            */
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
            
        }
    }
}
