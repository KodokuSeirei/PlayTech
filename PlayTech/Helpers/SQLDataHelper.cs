using System;
using System.Data;

namespace PlayTech.Helpers
{
    public class SQLDataHelper
    {
        public static int GetInt(IDataReader rdr, string columnName)
        {
            int index = rdr.GetOrdinal(columnName);
            if (rdr.IsDBNull(index))
            {
                return 0;
            }
            return Convert.ToInt32(rdr.GetValue(index));
        }

        public static string GetString(IDataReader rdr, string columnName)
        {
            int index = rdr.GetOrdinal(columnName);
            if (rdr.IsDBNull(index))
            {
                return String.Empty;
            }
            return Convert.ToString(rdr.GetValue(index));
        }

        public static string MultiWhereQuery(string columnName, params int[] values)
        {
            string result = String.Empty;
            if (values != null && values.Length > 0) { }
            {
                result = " WHERE " + columnName + " IN (";
                for (int i = 0; i < values.Length; i++)
                {
                    result += values[i].ToString();
                    if (i < values.Length - 1)
                        result += ", ";
                    else
                        result += ") ";
                }
            }
            return result;
        }
    }
}
