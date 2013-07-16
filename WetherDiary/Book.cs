using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBEngine;

namespace WetherDiary
{
    public partial class Book : Form
    {
        AccessDBEngine engine;
        string tableName;

        public Book()
        {
            InitializeComponent();
            this.engine = new DBEngine.AccessDBEngine("wether.mdb");

            dgvBook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBook.AutoGenerateColumns = true;
        }
        /*
        public Book(string tableName)
        {
            InitializeComponent();

            this.tableName = tableName;
            this.engine = new DBEngine.AccessDBEngine();

            dgvBook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBook.AutoGenerateColumns = true;
            
            DataTable bookTable = this.engine.ExecuteQueryReturnDataTable(new OleDbCommand(string.Format("SELECT * FROM {0}", this.tableName)));
            BindingSource bs = new BindingSource();
            bs.DataSource = bookTable;
            dgvBook.DataSource = bs;
            dgvBook.Columns["ID"].Visible = false;
            dgvBook.Columns["Name"].HeaderText = "Наименование";
            dgvBook.Columns["Name"].Width = 250;
        }
        */

        public Book(DataTable table) : this()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = table;
            dgvBook.DataSource = bs;
            dgvBook.Columns["ID"].Visible = false;
            dgvBook.Columns["Name"].HeaderText = "Наименование";
            dgvBook.Columns["Name"].Width = 250;
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
