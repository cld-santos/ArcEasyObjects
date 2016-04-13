using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ArcEasyObjects.Attributes
{
    public static class FieldFormatHelper
    {

        public static string FormatField(object Value, Type Type)
        {
            if (typeof(String) == Type)
            {
                return "'" + Convert.ToString(Value) + "'";
            }
            if (typeof(bool) == Type)
            {
                return (bool)Value ? "1" : "0";
            }
            if (typeof(double) == Type || typeof(Double) == Type)
            {
                return ((double)Value).ToString("F2", CultureInfo.GetCultureInfo("en-US"));
            }
            if (typeof(Decimal) == Type || typeof(decimal) == Type)
            {
                return ((decimal)Value).ToString("F2", CultureInfo.GetCultureInfo("en-US"));
            }
            if (typeof(float) == Type)
            {
                return ((float)Value).ToString("F2", CultureInfo.GetCultureInfo("en-US"));
            }

            return Convert.ToString(Value);
        }
    }
}
