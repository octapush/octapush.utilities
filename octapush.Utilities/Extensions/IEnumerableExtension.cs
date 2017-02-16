#region Build Information
// octapush.Utilities : IEnumerableExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-08
// CratedTime  : 15:58
#endregion

#region Namespaces
using System.Collections.Generic;
using System.Data;
using System.Linq;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class EnumerableExtension
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> list, string[] columns = null)
        {
            var result = new DataTable();

            var enumerable = list as IList<T> ?? list.ToList();
            if (list == null || !enumerable.Any())
                return result;

            var pi = typeof (T).GetProperties();

            if (columns == null || columns.Length == 0)
            {
                columns = new string[pi.Length];
                for (var i = 0; i < pi.Length; i++)
                    columns[i] = pi[i].Name;
            }

            foreach (var column in columns)
                result.Columns.Add(column);

            var row = new object[columns.Length];
            foreach (var item in enumerable)
            {
                for (var x = 0; x < row.Length; x++)
                {
                    var firstOrDefault = pi.FirstOrDefault(z => z.Name == columns[x]);
                    if (firstOrDefault != null)
                        row[x] = firstOrDefault
                            .GetValue(item, new object[0]);
                }

                result.Rows.Add(row);
            }

            return result;
        }
    }
}