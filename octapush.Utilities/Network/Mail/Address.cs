#region Build Information
// octapush.Utilities : Address.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-15
// CratedTime  : 13:56
#endregion

#region Namespaces
using System.Collections.Generic;
using System.Net.Mail;

#endregion

namespace octapush.Utilities.Network.Mail
{
    public class Address
    {
        private MailAddressCollection _addressCollection;

        internal MailAddressCollection AddressCollection
        {
            get { return _addressCollection ?? (_addressCollection = new MailAddressCollection()); }
        }

        public Address Add(string address)
        {
            AddressCollection.Add(new MailAddress(address));
            return this;
        }

        public Address Add(string displayName, string address)
        {
            AddressCollection.Add(new MailAddress(address, displayName));
            return this;
        }

        public Address Add(IEnumerable<string> addresses)
        {
            foreach (var address in addresses)
                AddressCollection.Add(address);

            return this;
        }

        public Address Add(Dictionary<string, string> addresses)
        {
            foreach (var address in addresses)
                AddressCollection.Add(new MailAddress(address.Value, address.Key));

            return this;
        }
    }
}