using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBEngine.Access
{
    public class Converters
    {
        public static object ConvertNumbToAccess(string numb)
        {
            if (numb != string.Empty)
                return numb;
            else
                return DBNull.Value;
            //return numb != string.Empty ? numb : DBNull.Value;
        }

        public static string ConvertDateToAccess(string date)
        {
            return string.Format("#{0}#", date);
        }
    }
}
