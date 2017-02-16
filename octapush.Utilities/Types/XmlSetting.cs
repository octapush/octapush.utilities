#region Build Information
// octapush.Utilities : XmlSetting.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-14
// CratedTime  : 11:21
#endregion

namespace octapush.Utilities.Types
{
    public class XmlSetting
    {
        public XmlSetting()
        {
            ShowAttribute = ShowAttribute;
            ShowWrapperOnSingleRow = ShowWrapperOnSingleRow;
            RootTitle = (RootTitle ?? "Root");
            AttributesTitle = (AttributesTitle ?? "Attributes");
            DataTitle = (DataTitle ?? "Data");
            DataWrapperTitle = (DataWrapperTitle ?? "Item");
        }

        public bool ShowAttribute { get; set; }
        public bool ShowWrapperOnSingleRow { get; set; }
        public string RootTitle { get; set; }
        public string AttributesTitle { get; set; }
        public string DataTitle { get; set; }
        public string DataWrapperTitle { get; set; }
    }
}