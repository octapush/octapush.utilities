#region Build Information
// octapush.Utilities : DataTableExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-18
// CratedTime  : 2:17
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using octapush.Utilities.Types;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class DataTableExtension
    {
        public static List<T> ToListOf<T>(this DataTable dataTable) where T : class
        {
            var oRet = new List<T>();

            foreach (var row in dataTable.Rows)
            {
                var obj = Activator.CreateInstance<T>();
                var pi = obj.GetType().GetProperties();
                foreach (var o in pi)
                {
                    o.SetValue(obj, ((DataRow) row)[o.Name]);
                }

                oRet.Add(obj);
            }

            return oRet;
        }

        public static string ToJson(this DataTable dataTable, JsonSetting jsonSetting)
        {
            if (dataTable.Rows.Count <= 0) return "";

            var attributes = "";
            var data = "";

            // build attributes
            if (jsonSetting.ShowAttribute)
            {
                var iLoop = 0x0;
                foreach (var column in dataTable.Columns)
                {
                    attributes += "{";
                    iLoop++;

                    var cpi = column.GetType().GetProperties();

                    for (var i = 0x0; i < cpi.Length; i++)
                    {
                        var pi = cpi[i];
                        var val = pi.GetValue(column);

                        if (val != null && val != new object() && !ReferenceEquals(val, "") &&
                            !ReferenceEquals(val, new List<object>()) && val != new List<object>())
                            attributes += @"""{{columnName}}"":{{showQuote}}{{columnValue}}{{showQuote}}{{comma}}"
                                .BindData(new Dictionary<string, object>
                                {
                                    {"columnName", pi.Name},
                                    {"columnValue", val},
                                    {"comma", 1 + i == cpi.Length ? "" : ","},
                                    {
                                        "showQuote",
                                        val is bool || val is int || val is Int16 || val is Int64 ||
                                        val is UInt16 || val is UInt32 || val is UInt64 || val is float ||
                                        val is decimal ||
                                        val is byte
                                            ? ""
                                            : @""""
                                    }
                                });
                    }

                    attributes += "}{{comma}}".BindData(new Dictionary<string, object>
                    {
                        {"comma", iLoop == dataTable.Columns.Count ? "" : ","}
                    });
                }

                attributes = string.Format("\"{0}\":{2}{1}{3}",
                                           jsonSetting.AttributeTitle,
                                           attributes,
                                           (dataTable.Columns.Count > 1 ? "[" : ""),
                                           (dataTable.Columns.Count > 1 ? "]" : "")
                    );
            }

            // build data
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                data += "{";
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    data += @"""{{columnName}}"":""{{columnData}}""{{addComma}}"
                        .BindData(new Dictionary<string, object>
                        {
                            {"addComma", j + 1 == dataTable.Columns.Count ? "" : ","},
                            {"columnName", dataTable.Columns[j].ColumnName},
                            {"columnData", dataTable.Rows[i][j]}
                        });
                }
                data += string.Format("}}{0}", (i + 1 == dataTable.Rows.Count ? "" : ","));
            }

            if (dataTable.Rows.Count > 1)
                data = string.Format("{0}{2}{1}{3}",
                                     !jsonSetting.ShowAttribute ? "" : string.Format("\"{0}\":", jsonSetting.DataTitle),
                                     data,
                                     (dataTable.Rows.Count > 1 ? "[" : ""),
                                     (dataTable.Rows.Count > 1 ? "]" : "")
                    );

            return !jsonSetting.ShowAttribute
                       ? data
                       : string.Format("{{{0},{1}}}", attributes, data);
        }

        public static string ToCsv(this DataTable dataTable, CsvSetting csvSetting)
        {
            if (dataTable.Rows.Count <= 0) return "";

            var stRes = "";
            if (csvSetting.ShowHeader)
            {
                for (var i = 0; i < dataTable.Columns.Count; i++)
                    stRes += string.Format("{0}{1}", dataTable.Columns[i].ColumnName,
                                           (i + 1 == dataTable.Columns.Count ? "" : csvSetting.Delimiter));
            }

            stRes += stRes.IsNullOrEmpty() ? "" : Environment.NewLine;

            for (var i = 0; i < dataTable.Rows.Count; i++)
                for (var j = 0; j < dataTable.Columns.Count; j++)
                    stRes += dataTable.Rows[i][j] +
                             (1 + j == dataTable.Columns.Count ? Environment.NewLine : csvSetting.Delimiter);

            return stRes;
        }

        public static string ToXml(this DataTable dataTable, XmlSetting xmlSetting)
        {
            if (dataTable.Columns.Count < 1)
                return
                    string.Format("<{0}></{0}>", xmlSetting.RootTitle);

            var strAttr = "";
            var strData = "";
            var strBuff = "";

            // build attributes
            if (xmlSetting.ShowAttribute)
            {
                strAttr += string.Format("<{0}>", xmlSetting.AttributesTitle);

                foreach (DataColumn column in dataTable.Columns)
                {
                    strBuff += string.Format("<{0}>", xmlSetting.DataWrapperTitle);
                    var cpi = column.GetType().GetProperties();

                    strBuff = cpi.Where(info => info.GetValue(column) != null
                                                &&
                                                info.GetValue(column) != new object()
                                                &&
                                                !ReferenceEquals(info.GetValue(column), ""))
                                 .Aggregate(strBuff,
                                            (current, info) =>
                                            current + string.Format("<{0}>{1}</{0}>", info.Name, info.GetValue(column)));

                    strBuff += string.Format("</{0}>", xmlSetting.DataWrapperTitle);
                }

                strAttr += string.Format("{0}</{1}>", strBuff, xmlSetting.AttributesTitle);
            }

            // build data
            strData += string.Format("<{0}>", xmlSetting.DataTitle);

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows.Count != 1 || xmlSetting.ShowWrapperOnSingleRow)
                    strData += string.Format("<{0}>", xmlSetting.DataWrapperTitle);

                strBuff = "";
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    strBuff += @"<{{columnName}}>{{columnData}}</{{columnName}}>"
                        .BindData(new Dictionary<string, object>
                        {
                            {"{{columnName}}", dataTable.Columns[j].ColumnName},
                            {"{{columnData}}", dataTable.Rows[i][j]}
                        });
                }

                strData += strBuff;

                if (dataTable.Rows.Count != 1 || xmlSetting.ShowWrapperOnSingleRow)
                    strData += string.Format("</{0}>", xmlSetting.DataWrapperTitle);
            }

            strData += string.Format("</{0}>", xmlSetting.DataTitle);

            return string.Format("<{0}>{1}{2}</{0}>", xmlSetting.RootTitle, strAttr, strData);
        }
    }
}