using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeatherStarter
{
    public static class ExtentionMethods
    {
        public static void AppendLine(this TextBox source, string value)
        {
            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText(Environment.NewLine + value);
        }
    }
}
