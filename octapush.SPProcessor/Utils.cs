#region Build Information
// octapush.SPProcessor : Utils.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-15
// CratedTime  : 21:25
#endregion

#region Namespaces
using System;
using Newtonsoft.Json;

#endregion

namespace octapush.SPProcessor
{
    public static class Utils
    {
        public static string ToJson(this object val, bool useIndentation = true)
        {
            return JsonConvert
                .SerializeObject(val, (Formatting) (Convert.ToInt32(useIndentation)));
        }
    }
}