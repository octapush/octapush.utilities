#region Build Information
// octapush.Utilities : EnumLogStatus.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 19:57
#endregion

#region Namespaces
using System.ComponentModel;

#endregion

namespace octapush.Utilities.Logger
{
    public enum EnumLogStatus
    {
        [Description("NO STATUS")] NoStatus,
        [Description("INFORMATION")] Information,
        [Description("WARNING")] Warning,
        [Description("ERROR")] Error
    }
}