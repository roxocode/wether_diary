using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBEngine;
using System.Reflection;

namespace WetherDiary
{
    public partial class Book : Form
    {
        SQLiteDBEngine engine;

        public Book()
        {
            InitializeComponent();
            string dbPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                Properties.Settings.Default.DbName);
            this.engine = new DBEngine.SQLiteDBEngine(dbPath);

            dgvBook.RowHeadersVisible = false;
            dgvBook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBook.AutoGenerateColumns = true;
        }
        
        public Book(DataTable table) : this()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            dgvBook.DataSource = bs;
            dgvBook.Columns["ID"].Visible = false;
            dgvBook.Columns["Name"].HeaderText = "Наименование";
            // Автозаполнение только для одной колонки, в будущем возможно потребуется сделать для всех (в цикле)
            dgvBook.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Только для справочника Осадки, добавление колонки с кнопкой
            // TODO: 2014-08-30 Disable ToolTip for buttons
            var buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "SelectIconBtn";
            buttonColumn.HeaderText = string.Empty;
            buttonColumn.Width = 22;
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.Text = "...";
            if (table.TableName == "fallout")
            {
                dgvBook.Columns.Add(buttonColumn);
            }
            dgvBook.CellContentClick += dgvBook_CellContentClick;
        }

        /// <summary>
        /// Click on cell button: Select Icon
        /// </summary>
        void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvBook.Columns["SelectIconBtn"].Index)
            {
                string iconsPath = Path.Combine(Directory.GetCurrentDirectory(), Icons.IconsDirectory);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = iconsPath;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Проверка что иконка Icons.IconSize х Icons.IconSize
                    // TODO: Add question to stackoverflow how should code this part
                    Image icon;
                    try
                    {
                        icon = Image.FromFile(ofd.FileName);
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("This is not an image!");
                        return;
                    }

                    if (icon.Height != Icons.IconSize || icon.Width != Icons.IconSize)
                    {
                        MessageBox.Show(string.Format("Image has another size! Should be {0} x {0} px", Icons.IconSize));
                        return;
                    }

                    MessageBox.Show(string.Format("It's image {0} x {0} px", Icons.IconSize));
                    // Сравнение путей
                    if (Directory.GetParent(ofd.FileName).ToString() != iconsPath)
                    {
                        string newIconName = GetUniqeFilePath(Path.Combine(iconsPath, Path.GetFileName(ofd.FileName)));
                        MessageBox.Show(newIconName);
                        File.Copy(ofd.FileName, Path.Combine(Icons.IconsDirectory, newIconName));
                        dgvBook.Rows[e.RowIndex].Cells["IconPath"].Value = Path.Combine(Icons.IconsDirectory, newIconName);
                    }
                    else
                    {
                        dgvBook.Rows[e.RowIndex].Cells["IconPath"].Value = Path.Combine(Icons.IconsDirectory, Path.GetFileName(ofd.FileName));
                    }
                }
            }
        }

        /// <summary>
        /// If necessary generate new unique file name for duplicate
        /// </summary>
        private string GetUniqeFilePath(string path)
        {
            if (!File.Exists(path))
                return Path.GetFileName(path);

            string resultPath = string.Empty;
            int i = 1;
            do
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                resultPath = string.Format("{0}({1}){2}", fileName, i, Path.GetExtension(path));
                i++;
            }
            while (File.Exists(resultPath));

            return resultPath;
        }
 
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable bookTable = (dgvBook.DataSource as BindingSource).DataSource as DataTable;
            this.engine.Update(bookTable);
            bookTable.AcceptChanges();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
