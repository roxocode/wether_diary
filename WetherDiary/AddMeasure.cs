using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
        private ErrorProvider errorProvider;

        private SQLiteDBEngine engine;

        public AddMeasure()
        {
            InitializeComponent();

            this.engine = new SQLiteDBEngine(Properties.Settings.Default.DbName);
            // Облачность
            cbCloud.DataSource = engine.AddBlankRow(engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT ID, Name FROM cloud")));
            cbCloud.DisplayMember = "Name";
            cbCloud.ValueMember = "ID";
            // Ветер
            cbWind.DataSource = engine.AddBlankRow(engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT ID, Name FROM wind")));
            cbWind.DisplayMember = "Name";
            cbWind.ValueMember = "ID";
            // Сила ветра
            cbWindForce.DataSource = engine.AddBlankRow(engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT ID, Name FROM windForce")));
            cbWindForce.DisplayMember = "Name";
            cbWindForce.ValueMember = "ID";
            // Осадки
            cbFallouts.DataSource = engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT ID, Name, IconPath FROM fallout"));
            cbFallouts.DisplayMember = "Name";
            cbFallouts.ValueMember = "ID";
            cbFallouts.DropDownStyle = ComboBoxStyle.DropDownList;

            btnAddFallout.Click += btnAddFallout_Click;
            btnDelFallout.Click += btnDelFallout_Click;

            // Осадки
            dgvFallouts.ReadOnly = true;
            dgvFallouts.RowHeadersVisible = false;
            dgvFallouts.ColumnHeadersVisible = false;
            dgvFallouts.AutoGenerateColumns = false;
            dgvFallouts.AllowUserToAddRows = false;
            dgvFallouts.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvFallouts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFallouts.RowTemplate.Height = 36;
            
            // Precipitation table
            var flColumn = new DataGridViewTextBoxColumn();
            flColumn.DataPropertyName = "Name";
            flColumn.Name = "Name";
            flColumn.HeaderText = "Имя";
            flColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFallouts.Columns.Add(flColumn);

            var flIconColumn = new DataGridViewImageColumn();
            flIconColumn.DataPropertyName = "FalloutIcon";
            flIconColumn.Name = "FalloutIcon";
            flIconColumn.HeaderText = "Иконка";
            flIconColumn.Width = 36;
            dgvFallouts.Columns.Add(flIconColumn);
            
            errorProvider = new ErrorProvider();
            tbTemperature.Validating += new CancelEventHandler(tbTemperature_Validating);
            dgvFallouts.DataSource = this.engine.ExecuteQueryReturnDataTable(new SQLiteCommand("SELECT fs.ID, fs.Measure_ID, fs.Fallout_ID, f.Name, f.IconPath, NULL AS FalloutIcon FROM fallouts fs INNER JOIN fallout f ON fs.Fallout_ID = f.ID WHERE fs.Measure_ID IS NULL"));
            dgvFallouts.RowsAdded += new DataGridViewRowsAddedEventHandler(dgvFallouts_RowsAdded);
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
                
                // Fallout icons
                SQLiteCommand selectFallouts = new SQLiteCommand(string.Format("SELECT fs.ID, fs.Measure_ID, fs.Fallout_ID, f.Name, f.IconPath FROM fallouts fs INNER JOIN fallout f ON fs.Fallout_ID = f.ID WHERE fs.Measure_ID = {0}", row["ID"]));
                DataTable falloutsTable = this.engine.ExecuteQueryReturnDataTable(selectFallouts);
                falloutsTable.Columns.Add(new DataColumn("FalloutIcon", typeof(Image)));
                foreach (DataRow dr in falloutsTable.Rows)
                    dr["FalloutIcon"] = Icons.GetIconImage(dr["IconPath"].ToString());
                dgvFallouts.DataSource = falloutsTable;
            }
        }

        /// <summary>
        /// Удалить осадок
        /// </summary>
        void btnDelFallout_Click(object sender, EventArgs e)
        {
            if (dgvFallouts.SelectedRows.Count != 0)
                ((DataRowView)dgvFallouts.SelectedRows[0].DataBoundItem).Row.Delete();
        }

        /// <summary>
        /// Добавить осадок
        /// </summary>
        void btnAddFallout_Click(object sender, EventArgs e)
        {
            // Проверка на наличие такого же вида осадка
            DataTable dt = (DataTable)dgvFallouts.DataSource;
            DataRow[] dra = dt.Select(string.Format("Fallout_ID = '{0}'", cbFallouts.SelectedValue));
            if (dra.Length > 0)
            {
                MessageBox.Show("Такой вид осадков уже добавлен");
                return;
            }

            DataRow dr = dt.NewRow();
            // TODO: Если замер новый, то Measure_ID будет известен только после его сохранения
            dr["Fallout_ID"] = cbFallouts.SelectedValue;
            dr["Name"] = cbFallouts.Text;
            string iconPath = ((DataRowView)cbFallouts.SelectedItem).Row["IconPath"].ToString();
            dr["FalloutIcon"] = Icons.GetIconImage(iconPath);
            
            dt.Rows.Add(dr);
        }

        void dgvFallouts_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // добавить событие в мою справку
        }
        
        private void btnAddMeasure_Click(object sender, EventArgs e)
        {
            // Валидация значений
            // Проверка на заполнение обязательных полей
            if (!IsTemperatureValid())
            {
                MessageBox.Show("Необходимо задать корректную температуру.");
                return;
            }
            // Сохранение в базу (Insert OR Update)
            
            // Update
            if (this.row != null)
            {
                string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
                this.row["Measure_Date"] = fullDate;
                this.row["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
                this.row["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
                this.row["Cloud_ID"] = cbCloud.SelectedValue ?? DBNull.Value;
                this.row["Wind_ID"] = cbWind.SelectedValue ?? DBNull.Value;
                this.row["WindForce_ID"] = cbWindForce.SelectedValue ?? DBNull.Value;
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
                    if (this.checkMeasureDateDublicate(dtpDate.Value))
                    {
                        MessageBox.Show(string.Format("Замер с датой '{0}' уже существует!", 
                            dtpDate.Value.ToString("yyyy-MM-dd")), 
                            "Ошибка", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning);
                        return;
                    }

                    DataRow t = ((DataTable)this.parentSource.DataSource).NewRow();
                    string fullDate = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpTime.Value.ToString("HH:mm");
                    t["Measure_Date"] = fullDate;
                    t["Temperature"] = Converters.ConvertNumbToAccess(tbTemperature.Text);
                    t["Pressure"] = Converters.ConvertNumbToAccess(tbPressure.Text);
                    t["Cloud_ID"] = (cbCloud.SelectedValue == null ? DBNull.Value : cbCloud.SelectedValue);
                    t["Wind_ID"] = (cbWind.SelectedValue == null ? DBNull.Value : cbWind.SelectedValue);
                    t["WindForce_ID"] = (cbWindForce.SelectedValue == null ? DBNull.Value : cbWindForce.SelectedValue);
                    ((DataTable)this.parentSource.DataSource).Rows.Add(t);
                    // fallouts сохраняем в Main.cs через функцию AddMeasure.SaveFallouts(measureID)
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Проверка на существование записи с датой date
        /// </summary>
        private bool checkMeasureDateDublicate(DateTime date)
        {
            SQLiteCommand hasMeasure = new SQLiteCommand(
                       string.Format("SELECT 1 FROM weather WHERE date(Measure_Date) = '{0}'", date.ToString("yyyy-MM-dd")));
            DataRow row = this.engine.ExecuteQueryReturnDataRow(hasMeasure);
            if (row != null)
                return true;
            else
                return false;
        }

        // Сохраняем осадки нового замера
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

        void tbTemperature_Validating(object sender, CancelEventArgs e)
        {
            IsTemperatureValid();
        }

        // Проверка температуры на корректность
        bool IsTemperatureValid()
        {
            double tmp;
            if (Double.TryParse(tbTemperature.Text, out tmp))
            {
                errorProvider.SetError(tbTemperature, "");
                return true;
            }
            else
            {
                errorProvider.SetError(tbTemperature, "Температура заданна некорректно.");
                return false;
            }
        }
    }
}
