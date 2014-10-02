using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

 /*
 *  Движок для работы с БД (MS Access - Jet)
  *  
  * FixMe: remove Connection private variable from class and implement ExecuteQuery
  * 
  * ToDo: Load and save connection string in encripted config.xml (page 87)
 */

namespace DBEngine
{
    public class SQLiteDBEngine
    {
        private SQLiteConnection _connection;

        string DbPath { get; set; }
        int DbVersion { get; set; }

        public SQLiteDBEngine()
        {
            this.DbVersion = 3;
        }

        public SQLiteDBEngine(string path): this()
        {
            this.DbPath = path;
        }

        private string GetConnectionString()
        {
             SQLiteConnectionStringBuilder stringBuilder = new SQLiteConnectionStringBuilder();
            stringBuilder.DataSource = this.DbPath;
            stringBuilder.Version = this.DbVersion;

            return stringBuilder.ConnectionString;
        }

        public int ExecuteQuery(SQLiteCommand command)
        {
            int res = 0;
            using (SQLiteConnection connection = new SQLiteConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex) { throw ex; }
            }
            return res;
        }

        /// <summary>
        /// Execute query and return identity
        /// </summary>
        public object ExecuteQueryReturnID(SQLiteCommand command)
        {
            object res;
            using (SQLiteConnection connection = new SQLiteConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SQLiteCommand id = new SQLiteCommand("SELECT last_insert_rowid()", connection);
                    res = id.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res;
        }

        public DataTable ExecuteQueryReturnDataTable(SQLiteCommand command)
        {
            DataTable resTable = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(resTable);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return resTable;
        }

        public DataRow ExecuteQueryReturnDataRow(SQLiteCommand command)
        {
            DataTable table = ExecuteQueryReturnDataTable(command);
            if (table.Rows.Count > 0)
                return table.Rows[0];
            else
                return null;
        }

        public void Update(DataTable table)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.GetConnectionString()))
            {
                this._connection = connection;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                    string.Format("SELECT * FROM {0}", table.TableName),
                    connection);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.RowUpdated += OnRowUpdated;
                try
                {
                    adapter.Update(table);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        void OnRowUpdated(object sender, System.Data.Common.RowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                //SQLiteCommand cmdNewID = new SQLiteCommand("SELECT @@IDENTITY", _connection);
                SQLiteCommand cmdNewID = new SQLiteCommand("SELECT last_insert_rowid()", _connection);
                e.Row["ID"] = Convert.ToInt32(cmdNewID.ExecuteScalar());
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        /// <summary>
        /// Добавляем пустой элемент
        /// </summary>
        public DataView AddBlankRow(DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["ID"] = DBNull.Value;
            row["Name"] = "";
            dt.Rows.Add(row);

            DataView dv = new DataView(dt, string.Empty, "Name", DataViewRowState.CurrentRows);
            return dv;
        }
    }
    /*
    public class AccessDBEngine
    {
        const string accessProvider = "Microsoft.ACE.OLEDB.12.0";
        //const string accessProvider = "Microsoft.Jet.Oledb.4.0";

        string DbPath { get; set; }
        string DbProvider { get; set; }
        bool DbWindowsAuth { get; set; }

        private OleDbConnection _connection;

        public AccessDBEngine()
        {
            this.DbProvider = accessProvider;
            this.DbWindowsAuth = false;
        }

        public AccessDBEngine(string path) : this()
        {
            this.DbPath = path;
        }

        private string GetConnectionString()
        {
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            try
            {
                builder.DataSource = this.DbPath;
                builder.Provider = this.DbProvider;
                builder.PersistSecurityInfo = this.DbWindowsAuth;
            }
            catch (Exception e)
            {
                throw e;
            }
            return builder.ConnectionString;
        }

        public DataTable Test()
        {
            Connect();
            DataTable dt = new DataTable("wether");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM wether", _connection);
            Console.WriteLine("Row quantity: " + adapter.Fill(dt).ToString());
            return dt;
        }

        public int ExecuteQuery(OleDbCommand command)
        {
            int res = 0;
            using (OleDbConnection connection = new OleDbConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return res;
        }

        /// <summary>
        /// Execute query and return identity
        /// </summary>
        public object ExecuteQueryReturnID(OleDbCommand command)
        {
            object res = null;
            using (OleDbConnection connection = new OleDbConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    // Get row id
                    OleDbCommand id = new OleDbCommand("SELECT @@IDENTITY", connection);
                    res = id.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return res;
        }

        public DataTable ExecuteQueryReturnDataTable(OleDbCommand command)
        {
            DataTable resTable = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(this.GetConnectionString()))
            {
                command.Connection = connection;
                try
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(resTable);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return resTable;
        }

        public DataRow ExecuteQueryReturnDataRow(OleDbCommand command)
        {
            DataTable resTable = ExecuteQueryReturnDataTable(command);
            return resTable.Rows[0];
        }

        public void Update(DataTable table)
        {
            using (OleDbConnection connection = new OleDbConnection(this.GetConnectionString()))
            {
                this._connection = connection;
                OleDbDataAdapter adapter = new OleDbDataAdapter(
                    string.Format("SELECT * FROM {0}", table.TableName), 
                    connection);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                //DataTable changedData = table.GetChanges();
                adapter.RowUpdated += new OleDbRowUpdatedEventHandler(OnRowUpdated);

                try
                {
                    adapter.Update(table);
                    //adapter.Update(changedData);
                    //table.Merge(changedData);
                    //table.AcceptChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void OnRowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand cmdNewID = new OleDbCommand("SELECT @@IDENTITY", this._connection);
                e.Row["ID"] = Convert.ToInt32(cmdNewID.ExecuteScalar());
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }
    }
    */
}
