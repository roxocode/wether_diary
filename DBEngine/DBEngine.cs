using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

 /*
 *  Движок для работы с БД (MS Access - Jet)
  *  
  * FixMe: remove Connection private variable from class and implement ExecuteQuery
  * 
  * ToDo: Load and save connection string in encripted config.xml (page 87)
 */

namespace DBEngine
{
    public class AccessDBEngine
    {
        const string accessProvider = "Microsoft.Jet.Oledb.4.0";

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
            /*
            // Path to file
            builder.DataSource = this.DbPath;
            // Provider
            builder.Provider = this.DbProvider;
            // Authentification: SQL or Windows
            builder.PersistSecurityInfo = false;
            return builder.ConnectionString;
            */
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

        public void Connect()
        {
            /* shit ...
            if (_connection != null && _connection.State != ConnectionState.Closed)
                return;

            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            // Path to file
            builder.DataSource = "wether.mdb";
            builder.Provider = "Microsoft.Jet.Oledb.4.0";
            builder.PersistSecurityInfo = false;

            this._connection = new OleDbConnection(builder.ConnectionString);
            try
            {
                this._connection.Open();
                // some stuff ...
                SaveConnectionToFile("haha.xml");
                //connection.Close();
                // debug
                Console.WriteLine(builder.ConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Some error!");
                throw e;
            }
            */
        }

        public DataTable Test()
        {
            Connect();
            DataTable dt = new DataTable("wether");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM wether", _connection);
            Console.WriteLine("Row quantity: " + adapter.Fill(dt).ToString());
            return dt;
        }
        
        /// <summary>
        /// Return query execution
        /// </summary>
        // TODO: refactoring replace for ExecuteQueryReturnDataTable in code
        public DataTable ExecuteQuery(string query)
        {
            DataTable resTable;
            using (OleDbConnection connection = new OleDbConnection(this.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = command;
                resTable = new DataTable();
                adapter.Fill(resTable);
            }
            return resTable;
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
                //OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM wether", connection);
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
}
