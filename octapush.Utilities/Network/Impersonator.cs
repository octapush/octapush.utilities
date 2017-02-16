#region Build Information
// octapush.Utilities : Impersonator.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2017-01-08
// CratedTime  : 14:20
#endregion

#region Namespaces
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

#endregion

namespace octapush.Utilities.Network
{
    public class Impersonater : IDisposable
    {
        #region IMPORT        
        private const int Logon32LogonInteractive = 2;
        private const int Logon32ProviderDefault = 0;
        private const int NewCredentials = 9;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType,
                                            int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);
        #endregion IMPORT

        #region CTOR
        private WindowsImpersonationContext _impersonationContext;

        public Impersonater(string domainName, string userName, string password)
        {
            ImpersonateValidUser(domainName, userName, password);
        }

        public void Dispose()
        {
            UndoImpersonation();
        }

        private void UndoImpersonation()
        {
            if (_impersonationContext != null)
                _impersonationContext.Undo();
        }

        private void ImpersonateValidUser(string domain, string username, string password)
        {
            var token = IntPtr.Zero;
            var tokenDuplicate = IntPtr.Zero;
            try
            {
                if (!RevertToSelf())
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                if (LogonUser(username, domain, password, 9, 0, ref token) == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                if (DuplicateToken(token, 2, ref tokenDuplicate) == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                _impersonationContext = new WindowsIdentity(tokenDuplicate).Impersonate();
            }
            finally
            {
                if (token != IntPtr.Zero)
                    CloseHandle(token);

                if (tokenDuplicate != IntPtr.Zero)
                    CloseHandle(tokenDuplicate);
            }
        }
        #endregion CTOR
    }
}