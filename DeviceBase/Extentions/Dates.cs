using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MetroSupport.Commons
{
    public static class Dates
    {
        public static string DateNullConversion(DateTime date)
        {
            string result = date.ToString() == "01.01.0001 0:00:00" ? String.Empty : date.ToShortDateString();
            return result;
        }

        public static string TimeNullConversion(DateTime time)
        {
            string result = time.ToString() == "01.01.0001 0:00:00" ? String.Empty : time.ToShortTimeString();
            return result;
        }
       
    }
}