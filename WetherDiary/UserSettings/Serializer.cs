using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * Alex 2014-11-08
 * Include methods for serializing and deserializing control's settings
 * and receiving and applying them
 */

namespace WetherDiary.UserSettings
{
    public static class Serializer
    {
        private static List<ColumnInfo> GetColumnsSetting(DataGridView grid)
        {
            List<ColumnInfo> res = new List<ColumnInfo>();
            foreach (DataGridViewColumn dc in grid.Columns)
            {
                res.Add(new ColumnInfo(dc.Name, dc.Width, dc.DefaultCellStyle.BackColor));
            }
            return res;
        }

        private static void SetColumnsSetting(DataGridView grid, List<ColumnInfo> lc)
        {
            foreach (ColumnInfo c in lc)
            {
                grid.Columns[c.Name].Width = c.Width;
                grid.Columns[c.Name].DefaultCellStyle.BackColor = c.BackColor;
            }
        }

        private static string SerializeColumnInfo(List<ColumnInfo> arr)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(stream, arr);
                stream.Position = 0;
                byte[] buffer = new byte[(int)stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return Convert.ToBase64String(buffer);
            }
        }

        private static List<ColumnInfo> DeserializeColumnInfo(string code)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(code)))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (List<ColumnInfo>)bf.Deserialize(stream);
            }
        }

        public static string GetSerializeColSetting(DataGridView grid)
        {
            return SerializeColumnInfo(GetColumnsSetting(grid));
        }

        public static void SetDeserializeColSetting(DataGridView grid, string code)
        {
            SetColumnsSetting(grid, DeserializeColumnInfo(code));
        }
    }
}
