#region Build Information
// octapush.Utilities : Directory.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-15
// CratedTime  : 20:32
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.Utilities.Network.LDAP
{
    public class Directory : IDisposable
    {
        private DirectoryEntry _entry;
        private string _password = "";
        private string _path = "";
        private string _query = "";
        private DirectorySearcher _searcher;
        private string _sortBy = "";
        private string _userName = "";

        public Directory(string query, string userName, string password, string path)
        {
            _entry = new DirectoryEntry(path, userName, password, AuthenticationTypes.Secure);

            Path = path;
            UserName = userName;
            Password = password;
            Query = query;
            _searcher = new DirectorySearcher(_entry) {Filter = query, PageSize = 1000};
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                if (_entry != null)
                {
                    _entry.Close();
                    _entry.Dispose();
                    _entry = null;
                }

                if (_searcher != null)
                {
                    _searcher.Dispose();
                    _searcher = null;
                }

                _entry = new DirectoryEntry(_path, _userName, _password, AuthenticationTypes.Secure);
                _searcher = new DirectorySearcher(_entry) {Filter = Query, PageSize = 1000};
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;

                if (_entry != null)
                {
                    _entry.Close();
                    _entry.Dispose();
                    _entry = null;
                }

                if (_searcher != null)
                {
                    _searcher.Dispose();
                    _searcher = null;
                }

                _entry = new DirectoryEntry(_path, _userName, _password, AuthenticationTypes.Secure);
                _searcher = new DirectorySearcher(_entry) {Filter = Query, PageSize = 1000};
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;

                if (_entry != null)
                {
                    _entry.Close();
                    _entry.Dispose();
                    _entry = null;
                }

                if (_searcher != null)
                {
                    _searcher.Dispose();
                    _searcher = null;
                }

                _entry = new DirectoryEntry(_path, _userName, _password, AuthenticationTypes.Secure);
                _searcher = new DirectorySearcher(_entry) {Filter = Query, PageSize = 1000};
            }
        }

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                _searcher.Filter = _query;
            }
        }

        public string SortBy
        {
            get { return _sortBy; }
            set
            {
                _sortBy = value;
                _searcher.Sort.PropertyName = _sortBy;
                _searcher.Sort.Direction = SortDirection.Ascending;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public bool Authenticate()
        {
            try
            {
                return _entry.Guid.ToString().Trim().NotIsNullOrEmpty();
            }
                // ReSharper disable once EmptyGeneralCatchClause
            catch {}
            return false;
        }

        public void Close()
        {
            _entry.Close();
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames",
            MessageId = "Entry"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public ICollection<Entry> FindActiveGroupMembers(string groupName, bool recursive = false)
        {
            ICollection<Entry> result;

            try
            {
                var group = FindGroup(groupName);
                var entries = FindActiveUsersAndGroups(string.Format("memberOf={0}", @group.DistinguishedName),
                                                       new object[0]);
                if (recursive)
                {
                    var returnValue = (from entry in entries
                        let tempEntry = FindGroup(entry.Cn)
                        select
                            tempEntry == null ? entry : FindActiveGroupMembers(tempEntry.Cn, true).FirstOrDefault())
                        .ToList();
                    result = returnValue;
                }
                else
                {
                    result = entries;
                }
            }
            catch
            {
                result = new List<Entry>();
            }
            return result;
        }

        public ICollection<Entry> FindActiveGroups(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture,
                                   "(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            return FindGroups(filter, new object[0]);
        }

        public ICollection<Entry> FindActiveUsers(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture,
                                   "(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            return FindUsers(filter, new object[0]);
        }

        public ICollection<Entry> FindActiveUsersAndGroups(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture,
                                   "(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            return FindUsersAndGroups(filter, new object[0]);
        }

        public ICollection<Entry> FindAll()
        {
            var returnedResults = new List<Entry>();
            using (var results = _searcher.FindAll())
                returnedResults.AddRange(from SearchResult result in results
                    select new Entry(result.GetDirectoryEntry()));
            return returnedResults;
        }

        public ICollection<Entry> FindComputers(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture, "(&(objectClass=computer)({0}))", new object[]
            {
                filter
            });
            _searcher.Filter = filter;
            return FindAll();
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"),
         SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames",
             MessageId = "Entry")]
        public ICollection<Entry> FindGroupMembers(string groupName, bool recursive = false)
        {
            ICollection<Entry> result;
            try
            {
                var group = FindGroup(groupName);
                var entries = FindUsersAndGroups("memberOf=" + group.DistinguishedName, new object[0]);
                if (recursive)
                {
                    var returnValue = (from entry in entries
                        let tempEntry = FindGroup(entry.Cn)
                        select
                            tempEntry == null ? entry : FindActiveGroupMembers(tempEntry.Cn, true).FirstOrDefault())
                        .ToList();
                    result = returnValue;
                }
                else
                {
                    result = entries;
                }
            }
            catch
            {
                result = new List<Entry>();
            }
            return result;
        }

        public Entry FindGroup(string groupName)
        {
            return FindGroups(string.Format("cn={0}", groupName), new object[0]).FirstOrDefault();
        }

        public ICollection<Entry> FindGroups(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture, "(&(objectClass=Group)(objectCategory=Group)({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            _searcher.Filter = filter;
            return FindAll();
        }

        public Entry FindOne()
        {
            return new Entry(_searcher.FindOne().GetDirectoryEntry());
        }

        public ICollection<Entry> FindUsersAndGroups(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture,
                                   "(&(|(&(objectClass=Group)(objectCategory=Group))(&(objectClass=User)(objectCategory=Person)))({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            _searcher.Filter = filter;
            return FindAll();
        }

        public Entry FindUserByUserName(string userName)
        {
            if (userName.IsNullOrEmpty())
                throw new Exception("UserName is not defined.");
            return FindUsers(string.Format("samAccountName={0}", userName), new object[0]).FirstOrDefault();
        }

        public ICollection<Entry> FindUsers(string filter, params object[] args)
        {
            filter = string.Format(CultureInfo.InvariantCulture, filter, args);
            filter = string.Format(CultureInfo.InvariantCulture, "(&(objectClass=User)(objectCategory=Person)({0}))",
                                   new object[]
                                   {
                                       filter
                                   });
            _searcher.Filter = filter;
            return FindAll();
        }

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(bool disposing)
        {
            if (_entry != null)
            {
                _entry.Close();
                _entry.Dispose();
                _entry = null;
            }

            if (_searcher == null) return;
            _searcher.Dispose();
            _searcher = null;
        }

        ~Directory()
        {
            Dispose(false);
        }
    }
}