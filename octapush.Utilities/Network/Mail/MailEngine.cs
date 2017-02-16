#region Build Information
// octapush.Utilities : MailEngine.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-15
// CratedTime  : 14:06
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using octapush.Utilities.Extensions;

#endregion

namespace octapush.Utilities.Network.Mail
{
    public class MailEngine : IDisposable
    {
        #region CTOR
        private readonly SmtpClient _smtpClient;
        private MailMessage _message;

        public MailEngine()
        {
            _smtpClient = new SmtpClient();
        }

        public MailEngine(string host)
        {
            _smtpClient = new SmtpClient(host);
        }

        public MailEngine(String host, int port)
        {
            _smtpClient = new SmtpClient(host, port);
        }

        public MailMessage Message
        {
            get { return _message ?? (_message = new MailMessage()); }
        }

        public void Dispose()
        {
            if (Message == null) return;

            if (Message.Attachments.Count > 0)
                foreach (var attachment in Message.Attachments.Where(s => s != null))
                    attachment.Dispose();

            Message.Dispose();
        }
        #endregion CTOR

        #region PUBLIC
        public virtual MailEngine From(MailAddress address)
        {
            Message.From = address;
            return this;
        }

        public virtual MailEngine To(Func<Address, Address> addresses)
        {
            foreach (var address in addresses(new Address()).AddressCollection)
                Message.To.Add(address);

            return this;
        }

        public virtual MailEngine Cc(Func<Address, Address> addresses)
        {
            foreach (var address in addresses(new Address()).AddressCollection)
                Message.CC.Add(address);

            return this;
        }

        public virtual MailEngine Bcc(Func<Address, Address> addresses)
        {
            foreach (var address in addresses(new Address()).AddressCollection)
                Message.Bcc.Add(address);

            return this;
        }

        public virtual MailEngine Subject(string subject)
        {
            Message.Subject = subject;
            return this;
        }

        public virtual MailEngine Body(string body)
        {
            Message.Body = body;
            return this;
        }

        public virtual MailEngine SubJectEncoding(Encoding encoding)
        {
            Message.SubjectEncoding = encoding;
            return this;
        }

        public virtual MailEngine BodyEncoding(Encoding encoding)
        {
            Message.BodyEncoding = encoding;
            return this;
        }

        public virtual MailEngine GlobalEncoding(Encoding encoding)
        {
            Message.SubjectEncoding = Message.BodyEncoding = encoding;
            return this;
        }

        public virtual MailEngine BodyUsingHtmlFormat(bool usingHtmlFormat)
        {
            Message.IsBodyHtml = usingHtmlFormat;
            return this;
        }

        public virtual MailEngine Priority(MailPriority priority)
        {
            Message.Priority = priority;
            return this;
        }

        public virtual MailEngine Host(string host)
        {
            _smtpClient.Host = host;
            return this;
        }

        public virtual MailEngine Port(int port)
        {
            _smtpClient.Port = port;
            return this;
        }

        public virtual MailEngine Ssl(bool usingSsl)
        {
            _smtpClient.EnableSsl = usingSsl;
            return this;
        }

        public virtual MailEngine Credential(string username, string password)
        {
            _smtpClient.Credentials = new NetworkCredential(username, password);
            return this;
        }

        public virtual MailEngine Credential(string username, string password, string domain)
        {
            _smtpClient.Credentials = new NetworkCredential(username, password, domain);
            return this;
        }

        public virtual MailEngine Attachment(List<Attachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                var disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(disposition.FileName);
                disposition.ModificationDate = File.GetLastWriteTime(disposition.FileName);
                disposition.ReadDate = File.GetLastAccessTime(disposition.FileName);
                Message.Attachments.Add(attachment);
            }

            return this;
        }

        public virtual MailEngine Send(out string result)
        {
            if (_smtpClient.Host.IsNullOrEmpty())
                throw new Exception("Mail host is not defined.");

            if (_smtpClient.Credentials == null && _smtpClient.UseDefaultCredentials == false)
                throw new Exception("Mail credential is not defined.");

            if (Message.To.Count < 1)
                throw new Exception("Mail target address (TO) is not defined.");

            if (Message.From == null)
                throw new Exception("Mail sander (From) is not defined.");

            _smtpClient.Send(Message);
            result = "OK";

            return this;
        }
        #endregion PUBLIC
    }
}