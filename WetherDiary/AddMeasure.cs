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

namespace WetherDiary
{
    public partial class AddMeasure : Form
    {
        private DataRow row;
        private BindingSource parentSource;

        private AccessDBEngine engine;

        public AddMeasure()
        {
            InitializeComponent();

            // DBEngine with parameter
            this.engine = new AccessDBEngine("wether.mdb");
            // Облачность
            cbCloud.DataSource = engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT ID, Name FROM cloud"));
            cbCloud.DisplayMember = "Name";
            cbCloud.ValueMember = "ID";
            // Ветер
            cbWind.DataSource = engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT ID, Name FROM wind"));
            cbWind.DisplayMember = "Name";
            cbWind.ValueMember = "ID";
            // Осадки
            dgvFallouts.AutoGenerateColumns = false;
            // Add columns
            var measureIDColumn = new DataGridViewTextBoxColumn();
            measureIDColumn.DataPropertyName = "Measure_ID";
            measureIDColumn.Name = "Measure_ID";
            measureIDColumn.ValueType = typeof(int);
            dgvFallouts.Columns.Add(measureIDColumn);
            var falloutColumn = new DataGridViewComboBoxColumn();
            falloutColumn.DataSource = engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT ID, Name FROM fallout"));
            falloutColumn.Width = 125;
            falloutColumn.DataPropertyName = "Fallout_ID";
            falloutColumn.Name = "Fallout_ID";
            falloutColumn.HeaderText = "Осадки";
            falloutColumn.DisplayMember = "Name";
            falloutColumn.ValueMember = "ID";
            dgvFallouts.Columns.Add(falloutColumn);
            dgvFallouts.DataSource = this.engine.ExecuteQueryReturnDataTable(new OleDbCommand("SELECT * FROM fallouts WHERE Measure_ID IS NULL"));
            dgvFallouts.RowsAdded += new DataGridViewRowsAddedEventHandler(dgvFallouts_RowsAdded);
        }

        void dgvFallouts_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // добавить событие в мою справку
        }

        public AddMeasure(object bs, DataRow row) : this()
        {
            // если не вызывать конструктор по уполчанию (this()) то расскомментировать
            // иначае инициализацию comboBox'ов необходимо проводить в этом конструкторе
            //InitializeComponent();

            this.parentSource = bs as BindingSource;
            if (row != null)
            {
                this.row = row;
                // Загружаем замер
                dtpDate.Value = Convert.ToDateTime(row["Measure_Date"]);
                dtpTime.Value = Convert.ToDateTime(row["Measure_Date"]);
                tbTemperature.Text = row["Temperature"].ToString();
                tbPressure.Text = row["Pressure"].ToString();
                //tbNote.Text = row["note"];
                cbCloud.SelectedValue = (row["Cloud_ID"] == DBNull.Value ? 0 : row["Cloud_ID"]);
                cbWind.SelectedValue = (row["Wind_ID"] == DBNull.Value ? 0 : row["Wind_ID"]);
                OleDbCommand selectFallouts = new OleDbCommand(string.Format("SELECT * FROM fallouts WHERE Measure_ID = {0}", row["ID"]));
                dgvFallouts.DataSource = this.engine.ExecuteQueryReturnDataTable(selectFallouts);
            }
        }

        private void btnAddMeasure_Click(object sender, EventArgs e)
        {
            // Валидация значений
            MessageBox.Show(dgvFallouts.DataSource.ToString());
            // Сохранение в базу (Insert OR Update)
            // ...
            // Update
            if (this.row != null)
            {
                string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
                this.row["Measure_Date"] = fullDate;
                this.row["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
                this.row["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
                this.row["Cloud_ID"] = cbCloud.SelectedValue;
                this.row["Wind_ID"] = cbWind.SelectedValue;
                // Fallout_ID
                DataTable fallouts = (DataTable)dgvFallouts.DataSource;
                fallouts.TableName = "fallouts";
                for (int i = 0; i < fallouts.Rows.Count; i++)
                {
                    if (fallouts.Rows[i].RowState != DataRowState.Deleted && fallouts.Rows[i]["Measure_ID"] != this.row["ID"])
                        fallouts.Rows[i]["Measure_ID"] = this.row["ID"];
                }
                this.engine.Update(fallouts);
                fallouts.AcceptChanges();
            }
            // Insert
            else
            {
                if (this.parentSource != null)
                {
                    DataRow t = ((DataTable)this.parentSource.DataSource).NewRow();
                    string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
                    t["Measure_Date"] = fullDate;
                    t["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
                    t["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
                    t["Cloud_ID"] = cbCloud.SelectedValue;
                    t["Wind_ID"] = cbWind.SelectedValue;
                    ((DataTable)this.parentSource.DataSource).Rows.Add(t);
                    // fallouts сохраняем в Main.cs через функцию AddMeasure.SaveFallouts(measureID)
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        public void SaveFallouts(object measureID)
        {
            if (measureID != null)
            {
                DataTable fallouts = (DataTable)dgvFallouts.DataSource;
                fallouts.TableName = "fallouts";
                for (int i = 0; i < fallouts.Rows.Count; i++)
                    fallouts.Rows[i]["Measure_ID"] = measureID;
                this.engine.Update(fallouts);
                fallouts.AcceptChanges();
            }
        }
    }
}
