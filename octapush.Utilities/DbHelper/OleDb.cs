#region Build Information
// octapush.Utilities : OleDb.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-12
// CratedTime  : 0:24
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.Utilities.DbHelper
{
    public static class OleDb
    {
        public static DataTable ExecQuery(this string query, string connectionString)
        {
            if (query.IsNullOrEmpty())
                throw new Exception("Query is not defined.");

            if (connectionString.IsNullOrEmpty())
                throw new Exception("ConnectionString is not defined.");

            var result = new DataTable();
            using (var con = new OleDbConnection(connectionString))
            {
                con.Open();

                using (var cmd = new OleDbCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query
                })
                    using (var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        if (dr != null)
                        {
                            var dtSchema = dr.GetSchemaTable();
                            var dt = new DataTable();
                            var listDc = new List<DataColumn>();

                            if (dtSchema != null)
                                foreach (var dc in from DataRow dRow in dtSchema.Rows
                                    let colName = dRow["ColumnName"].ToString()
                                    select new DataColumn(colName, (Type) dRow["DataType"])
                                    {
                                        Unique = (bool) dRow["IsUnique"],
                                        AllowDBNull = (bool) dRow["AllowDBNull"],
                                        AutoIncrement = (bool) dRow["IsAutoIncrement"]
                                    })
                                {
                                    listDc.Add(dc);
                                    dt.Columns.Add(dc);
                                }

                            while (dr.Read())
                            {
                                var dRow = dt.NewRow();
                                for (var i = 0; i < listDc.Count; i++)
                                    dRow[listDc[i]] = dr[i];

                                dt.Rows.Add(dRow);
                            }

                            result = dt;
                        }

                con.Close();
            }

            return result;
        }

        public static object ExecScalar(this string query, string connectionString)
        {
            if (query.IsNullOrEmpty())
                throw new Exception("Query is not defined.");

            if (connectionString.IsNullOrEmpty())
                throw new Exception("ConnectionString is not defined.");

            return query.ExecQuery(connectionString).Rows[0][0];
        }

        public static void ExecNonQuery(this string query, string connectionString)
        {
            if (query.IsNullOrEmpty())
                throw new Exception("Query is not defined.");

            if (connectionString.IsNullOrEmpty())
                throw new Exception("ConnectionString is not defined.");

            using (var con = new OleDbConnection(connectionString))
            {
                con.Open();

                using (var cmd = new OleDbCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query
                })
                {
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }
    }
}