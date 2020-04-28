using CurtLog;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal static class Program
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

        private static void InitLog()
        {
            Log.Settings.CustomLogHeader = "===========" + Application.ProductName + " Log File===========";
            Log.Settings.PerformanceOptions = Log.Performance.StandardPerformance;
            Log.Settings.LogHeaderOptions = Log.LogHeader.CustomHeader;
            var logLocation = PathInfos.LogsPath + @"\CFC_" + DateTime.Now.ToString("d").Replace("/", "-") + ".log";
            Log.InitializeLogger(logLocation);
            var settings = Settings.Instance;
            if (settings.LastLogFile != logLocation)
            {
                try { if (File.Exists(settings.LastLogFile)) File.Delete(settings.LastLogFile); }
                catch (Exception ex) { Log.WriteLine("Failed to delete: " + settings.LastLogFile + Environment.NewLine + ex.ToString()); }
            }
            settings.LastLogFile = logLocation;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            InitLog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                var name = @"CoDUO_FoV_Changer.Resources." + e.Name.Split(',')[0] + ".dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
                {
                    if (stream == null) throw new FileLoadException(name);
                    var data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    return Assembly.Load(data);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
