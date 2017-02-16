#region Build Information
// octapush.Utilities : DictionaryExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-25
// CratedTime  : 23:05
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class DictionaryExtension
    {
        public static string ToXml(this Dictionary<string, object> data, string strWrapper)
        {
            if (data.Count < 1) return string.Format("<{0}></{0}>", strWrapper);

            var strInner = data.Aggregate("",
                                          (current, o) =>
                                          current +
                                          string.Format("{0}<{1}>{2}</{1}>", Environment.NewLine, data.Keys, data.Values));
            return string.Format("<{0}>{1}</{0}>", strWrapper, strInner);
        }
    }
}