using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SQLite;
using DBEngine;
using ExcelLibrary.SpreadSheet;



namespace WetherImporter
{
    class Program
    {
        static void importData()
        {
            string file = "weather.xls";
            SQLiteDBEngine engine = new SQLiteDBEngine("weather.s3db");

            DateTime? dateMes = null;
            string timeMes = null;
            object tempMes = null;
            object pressureMes = null;
            object falloutMes = null;

            Console.WriteLine("====== --- Wether Importer --- =====");
            Console.WriteLine("Don't forget to delete all unnecessary sheets!");

            Workbook book = Workbook.Load(file);
            Worksheet sheet = book.Worksheets[0];

            // start row
            int rowIndex = 2;
            while (true)
            {
                Row row = sheet.Cells.GetRow(rowIndex);
                if (row.GetCell(0).IsEmpty)
                    return;

                // Date
                dateMes = row.GetCell(0).DateTimeValue;                
                // Time
                // 2 вида времени: одна цифра "10" или в формате "hh:mm:ss" например "9:30"
                Cell timeCell = row.GetCell(1);
                if (timeCell.Format.FormatType == CellFormatType.Time)
                    timeMes = timeCell.DateTimeValue.ToString("HH:mm");
                else
                    timeMes = timeCell.StringValue;

                // Temperature
                tempMes = row.GetCell(2).StringValue;
                // Pressure
                pressureMes = row.GetCell(3).StringValue;
                // Fallout
                falloutMes = row.GetCell(4).StringValue;

                // Parse time column
                DateTime fullDateTime;
                string[] timeParts = timeMes.ToString().Split(':');
                if (timeParts.Length == 2)
                    fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, Convert.ToInt16(timeParts[0]), Convert.ToInt16(timeParts[1]), 0);
                else if (timeParts.Length == 1)
                    fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, Convert.ToInt16(timeParts[0]), 0, 0);
                else
                    fullDateTime = new DateTime(dateMes.Value.Year, dateMes.Value.Month, dateMes.Value.Day, 0, 0, 0);
                
                //Console.WriteLine(fullDateTime.ToString());

                string sql = string.Format("INSERT INTO weather (Measure_Date, Temperature, Pressure, Cloud_ID, Wind_ID) VALUES ({0}, {1}, {2}, {3}, {4})",
                    new object[] {
                            // TODO: 2013-11-08 how to save datetime to database
                            // 2014-12-28: Add seconds ('ss') to datetime value for correct update in WeatherDiary
                            string.Format("'{0}'", fullDateTime.ToString("yyyy-MM-dd HH:mm:ss")),
                            tempMes,
                            pressureMes,
                            "NULL",
                            "NULL"
                        });

                object id = engine.ExecuteQueryReturnID(new SQLiteCommand(sql));

                Console.WriteLine(id.ToString());
                
                if (id != null && falloutMes != null)
                {
                    // Write fallouts
                    string fallouts = Convert.ToString(falloutMes);
                    string falloutSql = string.Empty;
                    if (fallouts.Contains("дождь"))
                    {
                        falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 8);
                        engine.ExecuteQuery(new SQLiteCommand(falloutSql));
                    }
                    if (fallouts.Contains("снег"))
                    {
                        falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 9);
                        engine.ExecuteQuery(new SQLiteCommand(falloutSql));
                    }
                    if (fallouts.Contains("гроза"))
                    {
                        falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 11);
                        engine.ExecuteQuery(new SQLiteCommand(falloutSql));
                    }
                }
                
                if (dateMes == null || timeMes == null || pressureMes == null)
                    Console.WriteLine(rowIndex.ToString());

                //Console.SetCursorPosition(0, Console.CursorTop);
                rowIndex++;
            }
        }

        static void Main(string[] args)
        {
            importData();

            #region old code with COM
            /*
            SQLiteDBEngine engine = new SQLiteDBEngine("weather.s3db");

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
                    object id = engine.ExecuteQueryReturnID(new SQLiteCommand(sql));

                    if (id != null && falloutMes != null)
                    {
                        // Write fallouts
                        string fallouts = Convert.ToString(falloutMes);
                        string falloutSql = string.Empty;
                        if (fallouts.Contains("дождь"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 1);
                            engine.ExecuteQuery(new SQLiteCommand(falloutSql));
                        }
                        if (fallouts.Contains("снег"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 2);
                            engine.ExecuteQuery(new SQLiteCommand(falloutSql));
                        }
                        if (fallouts.Contains("гроза"))
                        {
                            falloutSql = string.Format("INSERT INTO fallouts (Measure_ID, Fallout_ID) VALUES ({0}, {1})", id, 3);
                            engine.ExecuteQuery(new SQLiteCommand(falloutSql));
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
            */
            #endregion

            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
