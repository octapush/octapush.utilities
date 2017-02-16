#region Build Information
// octapush.Utilities : LoggerEngine.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 19:57
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.Utilities.Logger
{
    public class LoggerEngine
    {
        #region CTOR
        public LoggerEngine(string directory, string initialFilename, bool perDay = false)
        {
            var filename = string.Format("{0}_{1}.txt", initialFilename,
                                         (!perDay) ? "" : DateTime.Now.ToString("yyyyMMdd"));
            FileName = Path.Combine(directory, filename);
        }

        public string FileName { get; set; }
        #endregion CTOR

        #region PRIVATE
        private static void PathBuilder(string path)
        {
            if (Directory.Exists(path))
                return;

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        private void LogWriter(string msg, EnumLogStatus logStatus = EnumLogStatus.NoStatus)
        {
            if (FileName.IsNullOrEmpty())
                throw new Exception("Filename has not been initialized.");

            PathBuilder(FileName);

            using (var sw = new StreamWriter(FileName, true))
            {
                var sDt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                sw.BaseStream.Seek(0L, SeekOrigin.End);

                if (logStatus == EnumLogStatus.NoStatus)
                {
                    sw.WriteLine("");
                    sw.WriteLine("[{0}] {1}", sDt, msg);
                }
                else
                {
                    sw.WriteLine("");
                    sw.WriteLine("[{0}] [{1}]", sDt, logStatus.GetDescription());
                    sw.WriteLine(msg);
                }

                sw.Flush();
                sw.Close();
            }
        }
        #endregion PRIVATE

        #region PUBLIC
        public void Write(string message)
        {
            LogWriter(message);
        }

        public void Write(string message, EnumLogStatus status)
        {
            LogWriter(message, status);
        }

        public void Write(Exception ex)
        {
            var str = @"Exception: {{excMessage}},
Exception Type: {{excType}},
Data: {{excData}},
StackTrace: {{excStackTrace}},
Source: {{excSource}},
Target Site: {{excTargetSite}}"
                .BindData(new Dictionary<string, object>
                {
                    {"{{excMessage}}", ex.Message},
                    {"{{excType}}", ex.GetType().FullName},
                    {"{{excStackTrace}}", ex.StackTrace},
                    {"{{excSource}}", ex.Source},
                    {"{{excTargetSite}}", ex.TargetSite},
                    {"{{excData}}", ex.Data},
                });

            LogWriter(str, EnumLogStatus.Error);
        }
        #endregion PUBLIC
    }
}