using System;
using System.Collections.Generic;
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
            else if (typeof(bool) == Type)
            {
                return (bool)Value ? "1" : "0";
            }

            return Convert.ToString(Value);
        }
    }
}
