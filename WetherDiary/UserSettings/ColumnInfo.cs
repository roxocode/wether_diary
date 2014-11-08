using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WetherDiary
{
    [Serializable()]
    public class ColumnInfo
    {
        /// <summary>
        /// Name of the column
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Width of the column
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Background color of the column
        /// </summary>
        public Color BackColor { get; set; }

        public ColumnInfo(string name, int width, Color backColor)
        {
            this.Name = name;
            this.Width = width;
            this.BackColor = backColor;
        }
    }
}
