using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    static class Program
    {
        private static bool? _isElevated;
        public static bool IsElevated
        {
            get
            {
                if (!_isElevated.HasValue)
                {
                    try { _isElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator); } //unclear if this will ever throw an exception, but it's better to be safe, aye?
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        _isElevated = null;
                    }
                }
                return _isElevated ?? false;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
