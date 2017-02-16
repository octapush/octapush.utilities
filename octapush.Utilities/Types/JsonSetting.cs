#region Build Information
// octapush.Utilities : JsonSetting.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-14
// CratedTime  : 11:21
#endregion

namespace octapush.Utilities.Types
{
    public class JsonSetting
    {
        public JsonSetting()
        {
            ShowAttribute = ShowAttribute;
            AttributeTitle = AttributeTitle ?? "Attributes";
            DataTitle = DataTitle ?? "Data";
        }

        public bool ShowAttribute { get; set; }
        public string AttributeTitle { get; set; }
        public string DataTitle { get; set; }
    }
}