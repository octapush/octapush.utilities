#region Build Information
// octapush.Utilities : CsvSetting.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-14
// CratedTime  : 11:21
#endregion

namespace octapush.Utilities.Types
{
    public class CsvSetting
    {
        public CsvSetting()
        {
            Delimiter = Delimiter ?? ",";
            ShowHeader = ShowHeader;
        }

        public string Delimiter { get; set; }
        public bool ShowHeader { get; set; }
    }
}