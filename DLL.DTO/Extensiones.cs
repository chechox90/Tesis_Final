using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO
{
    public static class Extensiones
    {
        #region ==[EXTENSIONES OBJECT]=================================================================

        public static object GetValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static bool IsBoolean(this object boolean)
        {
            try
            {
                bool x = bool.Parse(boolean.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(this object datetime)
        {
            try
            {
                DateTime x = DateTime.Parse(datetime.ToString());
                return (x >= DateTime.Parse("1900-01-01"));
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDouble(this object value)
        {
            try
            {
                double x = double.Parse(value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInt(this object integer)
        {
            try
            {
                int x = int.Parse(integer.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsTime(this object value)
        {
            try
            {
                var valueArray = value.ToString().Split(':');

                if (valueArray.Length == 2)
                    return (valueArray[0].IsInt() && valueArray[1].IsInt() && valueArray[1].ToInt() < 60);

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool ToBoolean(this object boolean)
        {
            if (boolean.ToString().IsInt())
                return boolean.ToInt() == 1 ? true : false;
            else
                if (boolean.IsBoolean())
                return bool.Parse(boolean.ToString());

            return false;
        }

        public static DateTime ToDateTime(this object datetime)
        {
            if (datetime.IsDateTime())
                return DateTime.Parse(datetime.ToString());
            return DateTime.Parse("1900-01-01 00:00:00");
        }

        public static double ToDouble(this object value)
        {
            if (value.IsDouble())
                return double.Parse(value.ToString());
            return 0;
        }

        public static int ToInt(this object integer)
        {
            if (integer.IsInt())
                return int.Parse(integer.ToString());
            return 0;
        }

        #endregion ==[EXTENSIONES OBJECT]=================================================================

        #region ==[EXTENSIONES STRING]=================================================================

        public static bool IsEmpty(this string str)
        {
            try
            {
                return str.Trim().Equals("");
            }
            catch
            {
                return true;
            }
        }

        public static int IsEmptyToInt(this string str)
        {
            return str.IsEmpty() ? 0 : str.IsInt() ? str.ToInt() : 0;
        }

        public static string ToTitleCase(this string str)
        {
            char[] array = str.ToLower().ToCharArray();

            if (array.Length >= 1)
                array[0] = char.ToUpper(array[0]);

            for (int i = 1; i < array.Length; i++)
                if (array[i - 1] == ' ' || array[i - 1] == '.')
                    array[i] = char.ToUpper(array[i]);

            return new string(array);
        }

        #endregion ==[EXTENSIONES STRING]=================================================================

        #region ==[EXTENSIONES INTEGER]================================================================

        public static bool InArray(this int integer, params int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == integer) return true;
            return false;
        }

        #endregion ==[EXTENSIONES INTEGER]=============================================================

        #region ==[EXTENSIONES BOOLEAN]================================================================

        public static int ToInt(this bool boolean)
        {
            return boolean ? 1 : 0;
        }

        #endregion ==[EXTENSIONES BOOLEAN]================================================================

        #region ==[EXTENSIONES DATATABLE]==============================================================

        public static bool IsEmpty(this DataTable datatable)
        {
            try
            {
                return datatable.Columns.Count > 0 ? false : true;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsRowsEmpty(this DataTable datatable)
        {
            try
            {
                return datatable.Columns.Count > 0 ? datatable.Rows.Count > 0 ? false : true : true;
            }
            catch
            {
                return true;
            }
        }

        public static List<Dictionary<string, object>> ToList(this DataTable datatable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in datatable.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                foreach (DataColumn col in datatable.Columns)
                {
                    dict[col.ColumnName] = row[col];//.ToString();
                }
                list.Add(dict);
            }
            return list;
        }

        public static string ToTable(this DataTable datatable)
        {
            var html = new StringBuilder();

            if (!datatable.IsEmpty())
            {
                html.AppendFormat("<table class='table table-bordered'>");
                html.AppendFormat("<thead><tr>");
                foreach (DataColumn col in datatable.Columns)
                    html.AppendFormat("<th class='success centrar'>{0}</th>", col.ColumnName.Replace("_", " ").ToTitleCase());
                html.AppendFormat("</tr></thead>");
                html.AppendFormat("<tbody class='centrar'>");

                if (datatable.IsRowsEmpty())
                    html.AppendFormat("<tr><td colspan='{0}' class='centrar'>No Hay Registros</td></tr>", datatable.Columns.Count);
                else
                    for (int r = 0; r < datatable.Rows.Count; r++)
                    {
                        html.AppendFormat("<tr>");
                        for (int c = 0; c < datatable.Columns.Count; c++)
                            html.AppendFormat("<td class='centrar'>{0}</td>", datatable.Rows[r][c].ToString().ToTitleCase());
                        html.AppendFormat("</tr>");
                    }
                html.AppendFormat("</tbody></table>");
            }

            return html.ToString();
        }

        #endregion ==[EXTENSIONES DATATABLE]==============================================================

        #region ==[EXTENSIONES DATASET]================================================================

        public static List<object> ToList(this DataSet dataset)
        {
            List<object> tables = new List<object>();
            foreach (DataTable datatable in dataset.Tables)
            {
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                foreach (DataRow datarow in datatable.Rows)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    foreach (DataColumn datacolumn in datatable.Columns)
                    {
                        row.Add(datacolumn.ColumnName, datarow[datacolumn]);
                    }
                    rows.Add(row);
                }
                tables.Add(rows);
            }

            return tables;
        }

        #endregion ==[EXTENSIONES DATASET]================================================================

    }
}
