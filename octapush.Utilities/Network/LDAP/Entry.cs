#region Build Information
// octapush.Utilities : Entry.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-15
// CratedTime  : 20:01
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;

#endregion

namespace octapush.Utilities.Network.LDAP
{
    public sealed class Entry : IDisposable
    {
        public Entry(DirectoryEntry directoryEntry)
        {
            DirectoryEntry = directoryEntry;
        }

        public DirectoryEntry DirectoryEntry { get; set; }

        public string Email
        {
            get { return (string) GetValue("mail"); }
            set { SetValue("mail", value); }
        }

        public string DistinguishedName
        {
            get { return (string) GetValue("distinguishedname"); }
            set { SetValue("distinguishedname", value); }
        }

        public string CountryCode
        {
            get { return (string) GetValue("countrycode"); }
            set { SetValue("countrycode", value); }
        }

        public string Company
        {
            get { return (string) GetValue("company"); }
            set { SetValue("company", value); }
        }

        public IEnumerable<string> MemberOf
        {
            get { return DirectoryEntry.Properties["memberof"].Cast<string>().ToList(); }
        }

        public string DisplayName
        {
            get { return (string) GetValue("displayname"); }
            set { SetValue("displayname", value); }
        }

        public string Initials
        {
            get { return (string) GetValue("initials"); }
            set { SetValue("initials", value); }
        }

        public string Title
        {
            get { return (string) GetValue("title"); }
            set { SetValue("title", value); }
        }

        public string SamAccountName
        {
            get { return (string) GetValue("samaccountname"); }
            set { SetValue("samaccountname", value); }
        }

        public string GivenName
        {
            get { return (string) GetValue("givenname"); }
            set { SetValue("givenname", value); }
        }

        public string Cn
        {
            get { return (string) GetValue("cn"); }
            set { SetValue("cn", value); }
        }

        public string Name
        {
            get { return (string) GetValue("name"); }
            set { SetValue("name", value); }
        }

        public string Office
        {
            get { return (string) GetValue("physicaldeliveryofficename"); }
            set { SetValue("physicaldeliveryofficename", value); }
        }

        public string TelephoneNumber
        {
            get { return (string) GetValue("telephonenumber"); }
            set { SetValue("telephonenumber", value); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            if (DirectoryEntry == null)
                throw new Exception("DirectoryEntry shouldn't be null.");

            DirectoryEntry.CommitChanges();
        }

        public object GetValue(string property)
        {
            var collection = DirectoryEntry.Properties[property];
            return collection == null ? null : collection.Value;
        }

        public object GetValue(string property, int index)
        {
            var collection = DirectoryEntry.Properties[property];
            return collection == null ? null : collection[index];
        }

        public void SetValue(string property, object value)
        {
            var collection = DirectoryEntry.Properties[property];
            if (collection != null)
                collection.Value = value;
        }

        public void SetValue(string property, int index, object value)
        {
            var collection = DirectoryEntry.Properties[property];
            if (collection != null)
                collection[index] = value;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (PropertyValueCollection property in DirectoryEntry.Properties)
                builder.Append(property.PropertyName).Append(" = ").AppendLine(property.Value.ToString());

            var text = builder.ToString();
            return text;
        }

        private void Dispose(bool disposing)
        {
            if (DirectoryEntry == null) return;
            DirectoryEntry.Dispose();
            DirectoryEntry = null;
        }

        ~Entry()
        {
            Dispose(false);
        }
    }
}