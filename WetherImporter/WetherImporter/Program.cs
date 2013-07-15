using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DBEngine;
using Excel = Microsoft.Office.Interop.Excel;


namespace WetherImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            AccessDBEngine engine = new AccessDBEngine("wether.mdb");

            // Count of used rows
            int rowStart = 3;
            int rowEnd = 1968;
            // Count of used columns
            int colStart = 1;
            int colEnd = 5;
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Worksheet xlWorksheet = null;

            Console.WriteLine("====== --- Wether Importer --- =====");

            try
            {
                xlApp = new Excel.Application();
                //xlApp.Visible = true;
                xlWorkbook = xlApp.Workbooks.Open(string.Format("{0}\\wether.xls", Directory.GetCurrentDirectory()),
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorksheet = xlWorkbook.Worksheets.get_Item(3) as Excel.Worksheet;

                DateTime? dateMes = null;
                object timeMes = null;
                object tempMes = null;
                object pressureMes = null;
                object falloutMes = null;
                
                for (int i = rowStart; i <= rowEnd; i++)
                {
                    // Date
                    dateMes = DateTime.FromOADate(double.Parse((xlWorksheet.UsedRange[i, 1] as Excel.Range).Value2.ToString()));
                    // Time
                    timeMes = (xlWorksheet.UsedRange[i, 2] as Excel.Range).Text;
                    // Temperature
                    tempMes = (xlWorksheet.UsedRange[i, 3] as Excel.Range).Value2;
                    // Pressure
                    pressureMes = (xlWorksheet.UsedRange[i, 4] as Excel.Range).Value2;
                    // Fallouts
                    falloutMes = (xlWorksheet.UsedRange[i, 5] as Excel.Range).Value2;

                    // Parse time column
                    DateTime fullDateTime;
                    string[] timeParts = timeMes.ToString().Split(':');
                    if (timeParts.Length == 2)
                        fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, Convert.ToInt16(timeParts[0]), Convert.ToInt16(timeParts[1]), 0);
                    else if (timeParts.Length == 1)
                        fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, Convert.ToInt16(timeParts[0]), 0, 0);
                    else
                        fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, 0, 0, 0);

                    string sql = string.Format("INSERT INTO wether (Measure_Date, Temperature, Pressure, Cloud_ID, Wind_ID) VALUES ({0}, {1}, {2}, {3}, {4})",
                        new object[] {
                            DBEngine.Access.Converters.ConvertDateToAccess(fullDateTime.ToString("yyyy-MM-dd HH:mm")),
                            tempMes,
                            pressureMes,
                            "NULL",
                            "NULL"
                        });
                    object id = engine.ExecuteQueryReturnID(new OleDbCommand(sql));

                    if (id != null && falloutMes != null)
                    {
                        // Write fallouts
                        string fallouts = Convert.ToString(falloutMes);
                        string falloutSql = string.Empty;
                        if (fallouts.Contains("дождь"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 1);
                            engine.ExecuteQuery(new OleDbCommand(falloutSql));
                        }
                        if (fallouts.Contains("снег"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 2);
                            engine.ExecuteQuery(new OleDbCommand(falloutSql));
                        }
                        if (fallouts.Contains("гроза"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 3);
                            engine.ExecuteQuery(new OleDbCommand(falloutSql));
                        }
                    }

                    if (dateMes == null || timeMes == null || pressureMes == null)
                        Console.WriteLine(i.ToString());

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(i.ToString());
                }
                Console.WriteLine("Import complete! =)");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (xlWorkbook != null)
                    xlWorkbook.Close(false, null, null);
                xlApp.Quit();
                // TODO: exclude to function release(x) and x = null in finally
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                xlWorksheet = null;
                xlWorkbook = null;
            }
            Console.ReadKey();
        }
    }
}
