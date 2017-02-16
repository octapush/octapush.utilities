#region Build Information
// tester : Program.cs
// ================================================================
// CreatedBy   : Fadhly Permata
// CreatedDate : 2016-12-10
// CratedTime  : 20:37
#endregion

#region Namespaces
using System;
using System.Windows.Forms;

#endregion

namespace tester
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}