#region Build Information
// octapush.Utilities : ExceptionExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-08
// CratedTime  : 15:15
#endregion

#region Namespaces
using System;
using System.Collections.Generic;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class ExceptionExtension
    {
        public static string ToJson(this Exception exception)
        {
            if (exception == null) return "";

            var str = "{";

            str += @"""Exception"":""{{excMessage}}"",
""Exception Type"":""{{excType}}"",
""Data"":""{{excData}}"",
""StackTrace"":""{{excStackTrace}}"",
""Source"":""{{excSource}}"",
""Target Site"":""{{excTargetSite}}"""
                .BindData(new Dictionary<string, object>
                {
                    {"{{excMessage}}", exception.Message},
                    {"{{excType}}", exception.GetType().FullName},
                    {"{{excStackTrace}}", exception.StackTrace},
                    {"{{excSource}}", exception.Source},
                    {"{{excTargetSite}}", exception.TargetSite},
                    {"{{excData}}", exception.Data},
                });

            str += "}";

            return str;
        }
    }
}