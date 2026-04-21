using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hemo.Utilities
{
    public static class DataReporterUtil
    {
        public static string GetDateString(this DateTime date)
        {
            if (date == null || date.Year == 1900)
                return "";
            else
                return date.ToString("yyyy-MM-dd");
        }
    }
}
