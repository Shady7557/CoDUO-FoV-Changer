using CurtLog;
using Localization;
using ShadyPool;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal static class Program
    {
        private const string MUTEX_NAME = nameof(CoDUO_FoV_Changer) + "_SingleInstanceMutex";

        public const string MEMORY_MAPPED_NAME = nameof(CoDUO_FoV_Changer) + "_SingleInstanceMemoryMappedFile";
        public const int MEMORY_MAPPED_SIZE = 1024;

        private static bool? _isElevated;
        public static bool IsElevated
        {
            get
            {
                if (!_isElevated.HasValue)
                {
                    try 
                    { 
                        _isElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator); 
                    } //unclear if this will ever throw an exception, but it's better to be safe, aye?
                    catch (Exception ex)
                    {
                        try 
                        { 
                            if (Log.IsInitialized)
                                Log.WriteLine(ex.ToString());
                        }
                        finally
                        {
                            _isElevated = null;
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
                return _isElevated ?? false;
            }
        }

        private static string _currentUserSID = string.Empty;
        public static string CurrentUserSID
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_currentUserSID))
                        _currentUserSID = WindowsIdentity.GetCurrent().User.Value;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    try 
                    {
                        if (Log.IsInitialized)
                            Log.WriteLine(ex.ToString());
                    }
                    catch(Exception ex2) { Console.WriteLine(ex2.ToString());}

                }
               


                return _currentUserSID;
            }
        }

        private static void InitLog()
        {
            var sb = Pool.Get<StringBuilder>();

            try
            {
                sb.Clear();

                Log.Settings.CustomLogHeader = sb.Clear().Append("===========").Append(Application.ProductName).Append(" Log File===========").ToString();
                Log.Settings.PerformanceOptions = Log.Performance.StandardPerformance;
                Log.Settings.LogHeaderOptions = Log.LogHeader.CustomHeader;

                var logLocation = sb.Clear().Append(PathInfos.LogsPath).Append(@"\CFC_").Append(DateTime.Now.ToString("d")).Replace("/", "-").Append(".log").ToString();
                Log.InitializeLogger(logLocation);

                var settings = Settings.Instance;


                if (settings.LastLogFile != logLocation)
                {
                    try { if (File.Exists(settings.LastLogFile)) File.Delete(settings.LastLogFile); }
                    catch (Exception ex) { Log.WriteLine(sb.Append("Failed to delete: ").Append(settings.LastLogFile).Append(Environment.NewLine).Append(ex.ToString()).ToString()); }
                }

                settings.LastLogFile = logLocation;
            }
            finally { Pool.Free(ref sb); }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        { 
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);

            var isMultiInstance = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-multi", StringComparison.OrdinalIgnoreCase))
                {
                    isMultiInstance = true;
                    break;
                }
            }

            using (var mutex = new Mutex(true, MUTEX_NAME, out var createdNew))
            {
               
                if (createdNew || isMultiInstance)
                {
                    // No other instance is running, start the application
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                    var logInit = false;
                    try
                    {
                        InitLog();
                        logInit = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Application.Exit();
                    }

                    if (!logInit)
                        return;

                    var lm = LocalizationManager.CreateInstance();

                    lm.LoadLocalization("fr-FR", false);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                    BringRunningInstanceToFront();
                
                
            }

           
        }

        // The code below is used to try and resolve assemblies from the app's 'Resources' if necessary.
        private static Assembly AssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                if (sender == null || e == null)
                    return null;


                var sb = Pool.Get<StringBuilder>();
                try 
                {
                    var name = sb.Clear()
                        .Append(typeof(Program).Namespace)
                        .Append(".Resources.")
                        .Append(new AssemblyName(e.Name).Name)
                        .Append(".dll").ToString();

                    var appPath = AppDomain.CurrentDomain.BaseDirectory;

                    var resName = sb.Clear().Append(e.Name.Split(',')[0]).Append(".dll").ToString();


                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
                    {
                        try
                        {
                            if (stream == null)
                                throw new FileLoadException(name);

                            var data = new byte[stream.Length];
                            stream.Read(data, 0, data.Length);

                            try
                            {
                                var fileNamePath = sb.Clear().Append(appPath).Append("\\").Append(resName).ToString();
                                File.WriteAllBytes(fileNamePath, data);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                try
                                {
                                    if (Log.IsInitialized)
                                        Log.WriteLine(ex.ToString());
                                }
                                catch (Exception ex2) { Console.WriteLine(ex2.ToString()); }

                            }

                            return Assembly.Load(data);
                        }
                        catch(Exception ex) 
                        {
                            Console.WriteLine(ex.ToString()); 
                            try { 
                                if (Log.IsInitialized) 
                                    Log.WriteLine(ex.ToString()); 
                            } 
                            catch (Exception ex2) { Console.WriteLine(ex2.ToString());}
                        }
                    }

                }
                finally { Pool.Free(ref sb); }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                try
                {
                    if (Log.IsInitialized)
                        Log.WriteLine(ex.ToString());
                }
                catch (Exception ex2) { Console.WriteLine(ex2.ToString()); }
            }

            return null;
        }

        //this is wild.
        private static void BringRunningInstanceToFront()
        {
            try
            {
                using (var mmf = MemoryMappedFile.OpenExisting(MEMORY_MAPPED_NAME))
                using (var accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(0, (byte)1); // Write a signal byte to indicate a subsequent instance
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            try
            {
                var currentProcess = Process.GetCurrentProcess();

                var runningProcesses = Process.GetProcessesByName(currentProcess.ProcessName);

                for (int i = 0; i < runningProcesses.Length; i++)
                {
                    var process = runningProcesses[i];

                    try
                    {
                        if (process.Id == currentProcess.Id)
                            continue;

                        var hWnd = process.MainWindowHandle;

                        if (hWnd == IntPtr.Zero)
                            continue;

                        ShowWindow(hWnd, SW_SHOW);
                        ShowWindow(hWnd, SW_SHOWNORMAL);
                        ShowWindow(hWnd, SW_RESTORE);

                        SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) & ~WS_EX_TOOLWINDOW);

                        SetForegroundWindow(hWnd);

                        break;

                    }
                    catch (Exception ex) 
                    { 
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

           
        }

        // Windows API constants and functions
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOW = 5;

        // Constants for window styles
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        // Import the necessary WinAPI functions
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

    }
}
