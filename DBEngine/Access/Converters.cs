using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBEngine.Access
{
    public class Converters
    {
        public static string ConvertNumbToAccess(string numb)
        {
            return numb != string.Empty ? numb : "NULL";
        }

        public static string ConvertDateToAccess(string date)
        {
            return string.Format("#{0}#", date);
        }
    }
}
