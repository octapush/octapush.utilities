#region Build Information
// octapush.Utilities : EnumExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 19:59
#endregion

#region Namespaces
using System;
using System.ComponentModel;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum e)
        {
            return ((DescriptionAttribute)
                    Attribute
                        .GetCustomAttribute(e.GetType().GetField(e.ToString()), typeof (DescriptionAttribute)))
                       .Description ?? e.ToString();
        }

        public static object GetDefaultValue(this Enum e)
        {
            return
                ((DefaultValueAttribute)
                 Attribute.GetCustomAttribute(e.GetType().GetField(e.ToString()), typeof (DefaultValueAttribute)))
                    .Value ?? e;
        }
    }
}