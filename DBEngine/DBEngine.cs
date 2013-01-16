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
        private OleDbConnection _connection;

        public void LoadConnectionFromFile(string path)
        {

        }

        public void SaveConnectionToFile(string path)
        {
            // Save To File
        }

        private string GetConnectionString()
        {
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            // Path to file
            builder.DataSource = "wether.mdb";
            // Provider
            builder.Provider = "Microsoft.Jet.Oledb.4.0";
            // Authentification: SQL or Windows
            builder.PersistSecurityInfo = false;
            return builder.ConnectionString;
        }

        public void Connect()
        {
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
        public DataTable ExecuteQuery(string query)
        {
            Connect();
            OleDbCommand command = new OleDbCommand(query, _connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            DataTable resTable = new DataTable();
            adapter.Fill(resTable);
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

        public void Update(DataTable table)
        {
            //ToDo: OleDbCommandBuilder or manualy create insert, update, delete query
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.Update(table);
        }

        /*
        /// <summary>
        /// Return query execution
        /// </summary>
        public OleDbDataReader ExecuteQuery(string query)
        {
            //return 
        }

        /// <summary>
        /// Return query execution
        /// </summary>
        public DataRow ExecuteQuery(string query)
        {
            //return 
        }
         */
    }
}
