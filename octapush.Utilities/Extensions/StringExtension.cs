#region Build Information
// octapush.Utilities : StringExtension.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 20:25
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using octapush.Utilities.Enums;

#endregion

namespace octapush.Utilities.Extensions
{
    public static class StringExtension
    {
        #region Data Binder
        public static string BindData(this string str, Dictionary<string, object> data, string openBracket = "{{",
                                      string closeBracket = "}}")
        {
            if (str.IsNullOrEmpty() || data.Count == 0)
                return str;

            return data
                .Aggregate(
                    str,
                    (current, d) =>
                    current.Replace(
                        string.Format("{0}{1}{2}", openBracket, d.Key, closeBracket), d.Value.ToString())
                );
        }

        public static string BindData<T>(this string str, T data, string openBracket = "{{", string closeBracket = "}}")
        {
            return
                str.IsNullOrEmpty()
                    ? str
                    : Activator
                          .CreateInstance<T>()
                          .GetType()
                          .GetProperties()
                          .Where(
                              prop => prop.GetValue(data) != null
                          )
                          .Aggregate(
                              str,
                              (current, prop) =>
                              current.Replace(string.Format("{0}{1}{2}", openBracket, prop.Name, closeBracket),
                                              prop.GetValue(data).ToString())
                          );
        }
        #endregion Data Binder

        #region Encoder
        public static string Base64Encode(this string str, Encoding encoding = null)
        {
            return str.IsNullOrEmpty()
                       ? ""
                       : Convert.ToBase64String((encoding ?? new UTF8Encoding()).GetBytes(str));
        }

        public static string Base64Decode(this string str, Encoding encoding = null)
        {
            return str.IsNullOrEmpty()
                       ? ""
                       : (encoding ?? new UTF8Encoding()).GetString(Convert.FromBase64String(str));
        }

        public static byte[] ToByteArray(this string str, Encoding encoding = null)
        {
            return (encoding ?? new UTF8Encoding()).GetBytes(str);
        }
        #endregion Encoder

        #region String Case Changer
        public static string ChangeCase(this string str, EnumStringCase stringCase)
        {
            if (str.IsNullOrEmpty())
                return "";

            var result = "";
            switch (stringCase)
            {
                case EnumStringCase.LowerCase:
                    result = str.ToLower();
                    break;
                case EnumStringCase.UpperCase:
                    result = str.ToUpper();
                    break;
                case EnumStringCase.FirstCharacterUpperCase:
                {
                    var arrChar = str.ToCharArray();
                    for (var i = 0; i < arrChar.Length; i++)
                    {
                        if (arrChar[i] == ' ' || arrChar[i] == '\t') continue;
                        arrChar[i] = char.ToUpper(arrChar[i], CultureInfo.InvariantCulture);
                        break;
                    }

                    result = new string(arrChar);
                }
                    break;
                case EnumStringCase.SentenceCapitalize:
                {
                    var arrStr = str.Split(new[] {".", "?", "!"}, StringSplitOptions.None);
                    for (var i = 0; i < arrStr.Length; i++)
                    {
                        if (arrStr[i].IsNullOrEmpty()) continue;

                        var r = new Regex(arrStr[i]);
                        arrStr[i] = arrStr[i].ChangeCase(EnumStringCase.FirstCharacterUpperCase);
                        str = r.Replace(str, arrStr[i]);
                    }

                    result = str;
                }
                    break;
                case EnumStringCase.TitleCase:
                {
                    var arrStr = str.Split(new[] {" ", ".", "\t", Environment.NewLine, "!", "?"},
                                           StringSplitOptions.None);
                    for (var i = 0; i < arrStr.Length; i++)
                    {
                        if (arrStr[i].IsNullOrEmpty() || arrStr[i].Length <= 3) continue;
                        var r = new Regex(arrStr[i].Replace(")", "\\)").Replace("(", "\\(").Replace("*", "\\*"));
                        arrStr[i] = arrStr[i].ChangeCase(EnumStringCase.FirstCharacterUpperCase);
                        str = r.Replace(str, arrStr[i]);
                    }

                    result = str;
                }
                    break;
            }

            return result;
        }
        #endregion String Case Changer

        #region Utils
        public static string Repeat(this string str, int count = 2)
        {
            if (str.IsNullOrEmpty() || count < 2)
                return str;

            var oldStr = str;
            for (var i = 0; i < count - 1; i++)
                str += oldStr;

            return str;
        }

        public static string Left(this string str, int length)
        {
            return
                str.IsNullOrEmpty()
                    ? str
                    : str.Substring(0, str.Length > length ? length : str.Length);
        }

        public static string Right(this string str, int length)
        {
            if (str.IsNullOrEmpty())
                return str;

            length = ((str.Length > length) ? length : str.Length);
            return str.Substring(str.Length - length, length);
        }

        public static string Reverse(this string str)
        {
            return new string(str.Reverse<char>().ToArray<char>());
        }

        public static string GetAlphaOnly(this string str)
        {
            return new Regex("[A-Za-z ]", RegexOptions.None).Match(str).Value;
        }

        public static string GetNumericOnly(this string str)
        {
            return new Regex("[0-9]", RegexOptions.None).Match(str).Value;
        }

        public static string GetAlphaNumericOnly(this string str)
        {
            return new Regex("[A-Za-z0-9]", RegexOptions.None).Match(str).Value;
        }

        public static string TrimChars(this string str, string regexPattern)
        {
            return new Regex(regexPattern, RegexOptions.None).Replace(str, "");
        }
        #endregion Utils

        #region Comparer
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool NotIsNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool NotIsNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        #endregion
    }
}