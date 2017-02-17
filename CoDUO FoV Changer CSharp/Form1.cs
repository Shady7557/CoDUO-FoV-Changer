using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CurtLog;
using System.Security.Principal;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using ReadWriteMemory;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;

namespace CoDUO_FoV_Changer_CSharp
{
    public partial class MainForm : Form
    {
        public static string appdata = Environment.GetEnvironmentVariable("appdata") + @"\";
        public static string temp = Environment.GetEnvironmentVariable("temp") + @"\";
        public static string appdataFoV = appdata + "CoDUO FoV Changer";
        public static string logsPath = appdataFoV + @"\Logs";
        public static string settingsPath = appdataFoV + @"\settings.xml";
        public static readonly string hotfix = "7.0";
        public static readonly string ostype = (Environment.Is64BitOperatingSystem) ? "x64" : "x86";
        public static readonly string registryPathX64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive";
        public static readonly string registryPathX86 = registryPathX64.Replace(@"Wow6432Node\", "");
        string gameRegistry = string.Empty;
        public static readonly string cleanVersion = Application.ProductVersion.Substring(0, 3);
        public static bool isDev = Debugger.IsAttached;
        public static readonly bool isElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        public bool noLog = false;
        Settings settings = Settings.Instance;
        private Image CoDImage = Properties.Resources.CoD1;
        private Image CoDUOImage = Properties.Resources.CoDUO;
        public static readonly string cgameDll = "uo_cgame_mp_x86.dll";
        public static Point location;
        private int currentSessionTime;
        private ProcessMemory procMem;
        private DateTime lastHotkey;
        private DateTime lastUpdateCheck;
        public static string RegistryPath
        {
            get
            {
                if (ostype == "64") return registryPathX64;
                else return registryPathX86;
            }
        }
        public string GameVersion { get { return Registry.GetValue(RegistryPath, "Version", string.Empty)?.ToString() ?? string.Empty; } }
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);



        public MainForm()
        {
            InitializeComponent();
        }



        public void InitLog()
        {
            Log.Settings.CustomLogHeader = "===========CoDUO FoV Changer Log File===========";
            Log.Settings.PerformanceOptions = Log.Performance.StandardPerformance;
            Log.Settings.LogHeaderOptions = Log.LogHeader.CustomHeader;
            var logLocation = logsPath + @"\CFC_" + DateTime.Now.ToString("d").Replace("/", "-") + ".log";
            Log.InitializeLogger(logLocation);
            if (settings.LastLogFile != logLocation)
            {
                try
                {
                    if (File.Exists(settings.LastLogFile)) File.Delete(settings.LastLogFile);
                }
                catch (Exception ex)
                {
                    WriteLog("Failed to delete: " + settings.LastLogFile);
                    WriteLog(ex.ToString());
                }
            }
            settings.LastLogFile = logLocation;
        }



        private void CheckConnection()
        {
            if (CheckUpdates()) AccessLabel("No updates found. Click to check again.");
            else AccessLabel("Update available!");
        }


        void StartUpdatesThread()
        {
            var updatesThread = new Thread(CheckConnection);
            updatesThread.IsBackground = true;
            updatesThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            CheckForIllegalCrossThreadCalls = true;
            var argsSB = new StringBuilder();
            for (int i = 0; i < Environment.GetCommandLineArgs().Length; i++)
            {
                var argu = Environment.GetCommandLineArgs()[i];
                var arg = argu.ToLower();
                if (arg.Contains(Application.ProductName.ToLower()) || arg.Contains(Application.StartupPath.ToLower())) continue;
                if (arg == "-nolog") noLog = true;
                if (arg == "-unlock") DvarsCheckBox.Visible = true;
                if (arg == "-unlock=1")
                {
                    DvarsCheckBox.Visible = true;
                    DvarsCheckBox.Checked = true;
                }

                if (arg == "-fog=1") settings.Fog = true;
                else if (arg == "-fog=0") settings.Fog = false;

                if (arg == "-launch") StartGameButton.PerformClick();

                if (arg == "-vcod") CoD1CheckBox.Checked = true;

                if (arg == "-debug") isDev = true;

                if (arg == "-hotkeys" && isElevated) new Hotkeys().Show();

                if (arg.Contains("-fov="))
                {
                    var FoVStr = arg.Split('=')[1];
                    var FoV = 80;
                    if (int.TryParse(FoVStr, out FoV)) SetFoVNumeric(FoV);
                }
                argsSB.Append(argu + " ");
            }
            var args = argsSB.ToString();
            DvarsCheckBox.Visible = false;

            StartUpdatesThread();




            if (!Directory.Exists(appdata + "CoDUO FoV Changer"))
            {
                Directory.CreateDirectory(appdata + "CoDUO FoV Changer");
                Directory.CreateDirectory(appdata + @"CoDUO FoV Changer\Logs");
            }

            if (!Directory.Exists(appdata + @"CoDUO FoV Changer\Logs")) Directory.CreateDirectory(appdata + @"CoDUO FoV Changer\Logs");

            if (!noLog)
            {
                var initLogger = new Thread(InitLog);
                initLogger.IsBackground = true;
                initLogger.Start();
            }
            args.TrimEnd();
            if (!string.IsNullOrEmpty(args)) WriteLog("Launched program with args: " + args);

            try
            {
                gameRegistry = Registry.GetValue(RegistryPath, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(settings.InstallPath) || !Directory.Exists(settings.InstallPath))
                {
                    if (!string.IsNullOrEmpty(gameRegistry) && gameRegistry.Contains("Call of Duty") || gameRegistry.Contains("CoD"))
                    {
                        if (File.Exists(gameRegistry + @"\CoDUOMP.exe")) MessageBox.Show("CODUOMP found at: " + gameRegistry, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        settings.InstallPath = gameRegistry;
                    }
                    else
                    {
                        ipDialog.Description = "Locate your Call of Duty installation directory";
                        var ipResult = ipDialog.ShowDialog();
                        if (ipResult == DialogResult.Cancel)
                        {
                            Application.Exit();
                            return;
                        }
                        var selectedPath = ipDialog.SelectedPath;
                        settings.InstallPath = selectedPath;
                        MessageBox.Show("Set install path to: " + selectedPath, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message);
                WriteLog("An exception happened on Install Path code:");
                WriteLog(ex.ToString());
            }

            SetFoVNumeric(settings.FoV);
            MinimizeCheckBox.Checked = settings.MinimizeToTray;
            FogCheckBox.Checked = settings.Fog;
            if (CoD1CheckBox.Checked) ScalePictureBox(CoDPictureBox, CoDImage);
            else ScalePictureBox(CoDPictureBox, CoDUOImage);
            LaunchParametersTB.Text = settings.CommandLine;
            location = Location;
            if (settings.TrackGameTime) AccessGameTimeLabel();
            else
            {
                GameTimeLabel.Visible = false;
                CurSessionGT.Visible = false;
                GameTracker.Enabled = false;
            }

            if (isDev) UpdateButton.Visible = true;
            UpdateProcessBox();
            var timeTaken = DateTime.Now - startTime;
            WriteLog("Successfully started application, version " + Application.ProductVersion + " (HF " + hotfix + ")");
            if (timeTaken.TotalMilliseconds >= 400) WriteLog("Startup took: " + timeTaken.TotalMilliseconds + " (this is too long!)");
        }

        private void StartGame()
        {
            try
            {
                    var procName = "CoDUOMP.exe";
                    if (CoD1CheckBox.Checked) procName = "CoDMP.exe";
                    if (string.IsNullOrEmpty(settings.InstallPath))
                    {
                        MessageBox.Show("Install Path is empty!", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!File.Exists(settings.InstallPath + @"\" + procName))
                    {
                        MessageBox.Show("Unable to find " + procName + " in: " + settings.InstallPath, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                
                    var startInfo = new ProcessStartInfo();
                    var startInfoSB = new StringBuilder();
                    startInfoSB.Append("+set r_ignorehwgamma 1 +set vid_xpos 0 +set vid_ypos 0 +set win_allowalttab 1");
                    if (!string.IsNullOrEmpty(LaunchParametersTB.Text)) startInfoSB.Append(LaunchParametersTB.Text + " " + startInfo.Arguments);
                    if (CoD1CheckBox.Checked) startInfoSB.Append(" +set com_hunkmegs 128"); //force 128 hunkmegs if cod1
                    startInfo.Arguments = startInfoSB.ToString();
                    startInfo.FileName = settings.InstallPath + @"\" + procName;
                    startInfo.WorkingDirectory = settings.InstallPath;
                    Process.Start(startInfo);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
                WriteLog("Failed to start process game process!");
                WriteLog(ex.ToString());
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            var startThread = new Thread(StartGame);
            startThread.IsBackground = true;
            startThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LaunchParametersTB.Text != settings.CommandLine) settings.CommandLine = LaunchParametersTB.Text;
            if (settings.HasChanged) DatabaseFile.Write(settings, settingsPath);
        }

        private void FoVNumeric_ValueChanged(object sender, EventArgs e)
        {
            settings.FoV = FoVNumeric.Value;
            doFoV();
        }

        private void FogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CoD1CheckBox.Checked)
            {
                FogCheckBox.Checked = true;
                return;
            }
            settings.Fog = FogCheckBox.Checked;
            doFog(Convert.ToInt32(FogCheckBox.Checked));
        }

        private void CoD1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CoD1CheckBox.Checked)
            {
                FogCheckBox.Checked = true;
                FogCheckBox.Enabled = false;
                DvarsCheckBox.Checked = false;
                DvarsCheckBox.Enabled = false;
                Text += (" (in CoD1 mode)");
            }
            else
            {
                FogCheckBox.Enabled = true;
                DvarsCheckBox.Enabled = true;
                Text = Application.ProductName;
            }
            if (CoD1CheckBox.Checked) ScalePictureBox(CoDPictureBox, CoDImage);
            else ScalePictureBox(CoDPictureBox, CoDUOImage);
        }

        private void MinimizeCheckBox_CheckedChanged(object sender, EventArgs e) => settings.MinimizeToTray = MinimizeCheckBox.Checked;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (MinimizeCheckBox.Checked && WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                MinimizeIcon.Visible = true;
                MinimizeIcon.BalloonTipIcon = ToolTipIcon.Info;
                MinimizeIcon.BalloonTipText = Application.ProductName + " is minimized. Double click to restore full-size.";
                MinimizeIcon.BalloonTipTitle = "Minimized to Tray";
                MinimizeIcon.ShowBalloonTip(4300, "Minimized to Tray", Application.ProductName + " is minimized. Double click to restore full-size.", ToolTipIcon.Info);
            }
        }

        private void MinimizeIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            MinimizeIcon.Visible = false;
        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            procMem = getProcessMemoryFromBox();
            doRAChecks();
            doFoV();
            doFog(Convert.ToInt32(FogCheckBox.Checked));
            doDvars();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();


        private void HotKeyHandler_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var conv = now - lastHotkey;
            //we can be super responsive when someone is tapping +- while not accidentally moving it up or down twice by doing manual checks while having the timer at ~5-10ms
            if (lastHotkey == null) lastHotkey = now;
            else if (conv.TotalMilliseconds < 100) return;
            var modifier = (Keys)0;
            var fogModifier = (Keys)0;
            var up = (Keys)0;
            var toggleFog = (Keys)0;
            var down = (Keys)0;
            TryParseKeys(settings.HotKeyModifier, ref modifier);
            TryParseKeys(settings.HotKeyFogModifier, ref fogModifier);
            TryParseKeys(settings.HotKeyUp, ref up);
            TryParseKeys(settings.HotKeyFog, ref toggleFog);
            TryParseKeys(settings.HotKeyDown, ref down);
            
            if (IsKeyPushedDown(modifier))
            {
                if (IsKeyPushedDown(up))
                {
                    SetFoVNumeric(FoVNumeric.Value + 1);
                    lastHotkey = now;
                }
                if (IsKeyPushedDown(down))
                {
                    SetFoVNumeric(FoVNumeric.Value - 1);
                    lastHotkey = now;
                }
            }
            if (IsKeyPushedDown(fogModifier) && IsKeyPushedDown(toggleFog))
            {
                if (FogCheckBox.Checked) FogCheckBox.Checked = false;
                else FogCheckBox.Checked = true;

                lastHotkey = now.AddMilliseconds(110);
            }

        }


        private void DvarsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CoD1CheckBox.Checked)
            {
                DvarsCheckBox.Checked = false;
                return;
            }
            doDvars();
        }
        #region Util
        public bool TryParseFloat(string text, ref float value)
        {
            float tmp;
            if (float.TryParse(text, out tmp))
            {
                value = tmp;
                return true;
            }
            else return false;
        }

        public bool TryParseInt(string text, ref int value)
        {
            int tmp;
            if (int.TryParse(text, out tmp))
            {
                value = tmp;
                return true;
            }
            else return false;
        }
        public bool TryParseKeys(string text, ref Keys value)
        {
            Keys tmp;
            if (Enum.TryParse<Keys>(text, out tmp))
            {
                value = tmp;
                return true;
            }
            else return false;
        }
        static int GCD(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }
        bool IsProcessRunning(int pid)
        {
            try
            {
                var proc = Process.GetProcessById(pid);
                var id = proc?.Id ?? 0;
                return (id != 0);
            }
            catch (ArgumentException argEx)
            {
                if (argEx.Message.Contains("not running")) return false;
                else WriteLog("An error happened while trying to detect if a process is running (PID: " + pid + ")" + Environment.NewLine + argEx.ToString());
            }
            return false;
        }
        private bool CheckUpdates()
        {
            try
            {
                var response = WebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")?.GetResponse() ?? null;
                var version = new StreamReader(response?.GetResponseStream())?.ReadToEnd() ?? string.Empty;
                if (version.Contains(hotfix) || string.IsNullOrEmpty(version)) return true;
                else return false;
            }
            catch (WebException wex)
            {
                WriteLog("Unable to check for updates: " + wex.Message);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog("Unable to check updates:");
                WriteLog(ex.ToString());
                return true;
            }
        }
        string getCBoxString(ComboBox CBox)
        {
            var value = string.Empty;
            if (CBox == null) return value;
            if (CBox.Items.Count >= 1 && CBox.SelectedIndex >= 0) value = CBox?.Items[CBox.SelectedIndex]?.ToString() ?? string.Empty;
            return value;
        }
        public static bool IsKeyPushedDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }
        delegate void SetTextCallback(string value);

        private void AccessLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (CheckUpdatesLabel.InvokeRequired)
            {
                var d = new SetTextCallback(AccessLabel);
                Invoke(d, new object[] { text });
            }
            else CheckUpdatesLabel.Text = text;
        }
        ProcessMemory getProcessMemory(string processName)
        {
            if (string.IsNullOrEmpty(processName)) return null;
            var mem = new ProcessMemory(processName);
            if (mem != null && mem.CheckProcess())
            {
                mem.StartProcess();
                return mem;
            }
            return null;
        }
        ProcessMemory getProcessMemoryFromBox()
        {
            if (GamePIDBox.Items.Count <= 0)
            {
                return null;
            }
            //   var procName = getCBoxString(GamePIDBox).Split('(')[0];
            var procName = getCBoxString(GamePIDBox).Split('(')[0];
            var pid = 0;
            if (!int.TryParse(getCBoxString(GamePIDBox).Split('(')[1].Replace(")", ""), out pid)) return null;
            if (!IsProcessRunning(pid))
            {
                for (int i = 0; i < GamePIDBox.Items.Count; i++)
                {
                    var boxItem = GamePIDBox.Items[i];
                    if (boxItem.ToString().Contains(pid.ToString())) GamePIDBox.Items.Remove(boxItem);
                }
                return null;
            }
            var mem = new ProcessMemory(pid);
            if (mem != null)
            {
                if (!mem.StartProcess()) return null;
                return mem;
            }
            return null;
        }
        int getIntPointerAddress(IntPtr baseAddress, int offset, ProcessMemory procMem)
        {
            if (procMem == null) return 0;
            var pointer = 0;
            var pointedTo = BitConverter.ToInt32(procMem.ReadMem(baseAddress.ToInt32(), 4), 0);
            pointer = IntPtr.Add((IntPtr)Convert.ToInt32(pointedTo.ToString("X"), 16), offset).ToInt32();
            return pointer;
        }


        int getIntPointerAddress(int baseAddress, int offset, ProcessMemory procMem) { return getIntPointerAddress((IntPtr)baseAddress, offset, procMem); }

        int ReadIntAddress(int baseAddress, int offset, ProcessMemory procMem, int pSize = 4, int startIndex = 0) { return BitConverter.ToInt32(procMem.ReadMem(getIntPointerAddress(baseAddress, offset, procMem), pSize), startIndex); }

        int ReadIntAddress(IntPtr baseAddress, int offset, ProcessMemory procMem, int pSize = 4, int startIndex = 0) { return ReadIntAddress(baseAddress.ToInt32(), offset, procMem, pSize, startIndex); }

        private void ScalePictureBox(PictureBox pictureBox, Image initialImage = null)
        {
            Bitmap finalImg = null;
            if (pictureBox.Image != null) finalImg = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            if (initialImage != null) finalImg = new Bitmap(initialImage, pictureBox.Width, pictureBox.Height);
            if (finalImg != null)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.Image = finalImg;
            }
        }

        private void SetFoVNumeric(decimal FoV) { if (FoV >= FoVNumeric.Minimum && FoV <= FoVNumeric.Maximum) FoVNumeric.Value = FoV; }

        public static void WriteLog(string message)
        {
            if (Log.IsInitialized) Log.WriteLine(message);
        }
        #endregion
        #region Memory
        private void doFog(int value = 1)
        {
            try
            {
                if (procMem == null) return;
                var newmpAddr = getIntPointerAddress(0x489A0D4, 0x20, procMem);
                var fogValue = ReadIntAddress(0x489A0D4, 0x20, procMem);
                if (fogValue != value) procMem.WriteInt(newmpAddr, value);
            }
            catch (Exception ex)
            {
                WriteLog("An exception happened while trying to read/write fog values!");
                WriteLog(ex.ToString());
            }
        }

        void doRAChecks()
        {
            if (procMem == null) return;
            try
            {
                var mode = ReadIntAddress(0x4899D50, 0x20, procMem);
                var width = ReadIntAddress(0x4899D30, 0x20, procMem);
                var height = ReadIntAddress(0x4899FCC, 0x20, procMem);
               // var mode = BitConverter.ToInt32(procMem.ReadMem(getIntPointerAddress(0x4899D50, 0x20, procMem), 4), 0);
                //var width = BitConverter.ToInt32(procMem.ReadMem(getIntPointerAddress(0x4899D30, 0x20, procMem), 4), 0);
              //  var height = BitConverter.ToInt32(procMem.ReadMem(getIntPointerAddress(0x4899FCC, 0x20, procMem), 4), 0);
                if (width == 0 || height == 0) return;
                var ratio = (double)width / (double)height;
                var aspectWidth = width / GCD(width, height);
                var aspectHeight = height / GCD(width, height);
                var aspectStr = aspectWidth + ":" + aspectHeight;
                if (aspectWidth == 8 && aspectHeight == 5) aspectStr = aspectStr.Replace("8", "16").Replace("5", "10");
                if (ratio < 1.7 && mode == -1 || mode != -1) FoVNumeric.Maximum = 95;
                else FoVNumeric.Maximum = 130;
                SetFoVNumeric(FoVNumeric.Value); //will set it to the value if it can, if not, falls back on maximum
            }
            catch(Exception ex)
            {
                WriteLog("An exception happened while trying to get current game resolution!");
                WriteLog(ex.ToString());
            }
        }

        private void doFoV()
        {
            try
            {
                var address = 0x3029CA28;
                //   var address = 0x3052F7C8; //old static com_hunkmegs 128 only
                //var mem = getProcessMemory(process);
                if (procMem == null)
                {
                    StatusLabel.Text = "Status: not found or failed to write to memory!";
                    toolTip1.SetToolTip(StatusLabel, "Unable to write to memory, if this continues, try running this program as an Administrator.");
                    StatusLabel.ForeColor = Color.DarkRed;
                }
                else
                {
                    var value = 80f;
                    TryParseFloat(FoVNumeric.Value.ToString(), ref value);
                    if (!CoD1CheckBox.Checked) address = procMem.DllImageAddress(cgameDll) + 0x52F7C8;
                    procMem.WriteFloat(address, value);
                    StatusLabel.Text = "Status: Game found and wrote to memory!";
                    toolTip1.SetToolTip(StatusLabel, string.Empty);
                    StatusLabel.ForeColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                WriteLog("An exception happened while trying to read/write FoV addresses!");
                WriteLog(ex.ToString());
            }
        }

        private void doDvars()
        {
            if (procMem == null) return;
            try
            {
                var val = 116;
                var curVal = procMem.ReadByte(0x43DD86);
                if (DvarsCheckBox.Checked) val = 235;
                if (curVal == val) return; //don't write if it's already the value we want
                procMem.WriteInt(0x43DD86, val, 1);
                procMem.WriteInt(0x43DDA3, val, 1);
                procMem.WriteInt(0x43DDC1, val, 1);
            }
            catch (Exception ex)
            {
                WriteLog("An exception happened while trying to read/write Dvar addresses!");
                WriteLog(ex.ToString());
            }
        }
        #endregion

        private void CheckUpdatesLabel_Click(object sender, EventArgs e)
        {
            if (CheckUpdatesLabel.Text == "Checking for updates...") return;
            var now = DateTime.Now;
            if (lastUpdateCheck == null) lastUpdateCheck = now;
            else
            {
                var time = now - lastUpdateCheck;
                if (time.TotalSeconds < 30)
                {
                    MessageBox.Show("You're checking for updates too quickly!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            CheckUpdatesLabel.Text = "Checking for updates...";
            StartUpdatesThread();
            lastUpdateCheck = now;
        }

        private void CheckUpdatesLabel_TextChanged(object sender, EventArgs e)
        {
            if (CheckUpdatesLabel.Text == "Update available!") UpdateButton.Visible = true;
            else UpdateButton.Visible = isDev;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm().Show();

        private void AccessGameTimeLabel()
        {
            try
            {
                if (settings.GameTime >= int.MaxValue)
                {
                    GameTimeLabel.Text = "Game Time: >= 68 Years";
                    settings.GameTime--;
                }
                var span = TimeSpan.FromSeconds(settings.GameTime);
                var spanCurrent = TimeSpan.FromSeconds(currentSessionTime);
                var totalMinutes = Math.Floor(span.TotalMinutes);
                var totalHours = Math.Floor(span.TotalHours);
                var totalMinutesCur = Math.Floor(spanCurrent.TotalMinutes);
                var totalHoursCur = Math.Floor(spanCurrent.TotalHours);
                if (settings.GameTime >= 1 && totalMinutes <= 0) GameTimeLabel.Text = "Game Time: " + settings.GameTime + " seconds";
                if (totalMinutes >= 1 && totalHours <= 0) GameTimeLabel.Text = "Game Time: " + totalMinutes + " minutes";
                if (totalHours >= 1 && totalMinutes >= 60) GameTimeLabel.Text = "Game Time: " + totalHours + " hours";
                if (currentSessionTime >= 1 && totalMinutesCur <= 0) CurSessionGT.Text = "Current Session: " + currentSessionTime + " seconds";
                if (totalMinutesCur >= 1 && totalHoursCur <= 0) CurSessionGT.Text = "Current Session: " + totalMinutesCur + " minutes";
                if (totalHoursCur >= 1 && totalMinutes >= 60) CurSessionGT.Text = "Current Session: " + totalHoursCur + " hours";
            }
           catch(Exception ex)
            {
                WriteLog("An exception happened while trying to get total played time!");
                WriteLog(ex.ToString());
            }
        }

        private void GameTracker_Tick(object sender, EventArgs e)
        {
            var procName = "CoDUOMP";
            if (CoD1CheckBox.Checked) procName = "CoDMP";
            var process = Process.GetProcessesByName(procName);
            if (process.Length <= 0) return;
            if (settings.GameTime < int.MaxValue - 1) settings.GameTime++;
            if (currentSessionTime < int.MaxValue - 1) currentSessionTime++;
            AccessGameTimeLabel();

        }


        private void LaunchParametersTB_TextChanged(object sender, EventArgs e)
        {
            if (LaunchParametersTB.Text.Length >= 36) LaunchParametersTB.Height = 50;
            else LaunchParametersTB.Height = 20;
            if (LaunchParametersTB.Text.Length >= 139) LaunchParametersTB.ScrollBars = ScrollBars.Vertical;
            else LaunchParametersTB.ScrollBars = ScrollBars.None;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CheckUpdatesLabel.Text = "Checking for updates...";
            StartUpdatesThread();
        }

        private void CreateUpdater()
        {
            try
            {
                var path = temp + "CoDUO FoV Changer Updater.exe";
                if (!File.Exists(path))
                {
                    File.WriteAllBytes(path, Properties.Resources.CoDUO_FoV_Changer_Updater_CSharp);
                    Log.WriteLine("Created updater at: " + path);
                }
                Process.Start(path);
                Log.WriteLine("Started Updater, shutting down");
                Application.Exit();
            }
            catch (Exception ex)
            {
                Log.WriteLine("An error happened while trying to write the updater:");
                Log.WriteLine(ex.ToString());
                MessageBox.Show("An error happened while trying to write the updater: " + Environment.NewLine + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var updaterThread = new Thread(CreateUpdater);
            updaterThread.IsBackground = true;
            updaterThread.Start();
        }

   

        void UpdateProcessBox()
        {
            try
            {
                var selectedIndex = GamePIDBox.SelectedIndex;
                for (int i = 0; i < GamePIDBox.Items.Count; i++)
                {
                    var boxItem = GamePIDBox.Items[i];
                    var pid = 0;
                    var splitPid = boxItem.ToString().Split('(')[1].Replace(")", "");
                    if (!int.TryParse(splitPid, out pid)) continue;
                    
                    if (!IsProcessRunning(pid))
                    {
                        GamePIDBox.Items.Remove(boxItem);
                            /*/
                        if (GamePIDBox.Items.Count < 1) continue;
                       
                        //work around to select the next item (this is almost definitely shitty)   
                        for (int j = 0; j < GamePIDBox.Items.Count; j++)
                        {
                            var boxItem2 = GamePIDBox.Items[j];
                            var boxItemName = boxItem2.ToString();
                            GamePIDBox.Items.Remove(boxItem2);
                            GamePIDBox.Items.Add(boxItemName);
                            if (GamePIDBox.Items.Count > 0) GamePIDBox.SelectedIndex = 0;
                            else GamePIDBox.SelectedValue = string.Empty;
                        }/*/
                    }
                    
                }
                var procName = "CoDUOMP";
                if (CoD1CheckBox.Checked) procName = "CoDMP";
                var allProcs = Process.GetProcessesByName(procName);
                for (int i = 0; i < allProcs.Length; i++)
                {
                    var proc = allProcs[i];
                    if (proc == null || proc.Id == 0 || !proc.ProcessName.Contains(procName)) continue;

                    var cont = new Dictionary<int, bool>();
                    cont[proc.Id] = true;
                    for (int j = 0; j < GamePIDBox.Items.Count; j++)
                    {
                        var item = GamePIDBox.Items[j];
                        if (item.ToString().Contains(proc.Id.ToString())) cont[proc.Id] = false;
                    }
                    if (cont[proc.Id])
                    {
                        GamePIDBox.Items.Add(proc.ProcessName + " (" + proc.Id + ")");
                   //     if (GamePIDBox.SelectedValue == null && GamePIDBox.Items.Count >= 1) GamePIDBox.SelectedIndex = 0;
                    }
                }
                GamePIDBox.Visible = (GamePIDBox.Items.Count > 0);
                if (GamePIDBox.SelectedItem == null)
                {
                    if (GamePIDBox.Items.Count > 1 && selectedIndex > 0)
                    {
                        GamePIDBox.SelectedIndex = (selectedIndex - 1);
                        MessageBox.Show("items count IS > 1 and selectedIndex is > 0, pre index: " + selectedIndex + " " + (selectedIndex - 1));
                    }
                    else
                    {
                        var listItems = new List<object>();
                        for (int i = 0; i < GamePIDBox.Items.Count; i++)
                        {
                            var item = GamePIDBox.Items[i];
                            listItems.Add(item);
                        }

                        if (listItems.Count > 0)
                        {
                            GamePIDBox.SelectedItem = listItems.FirstOrDefault();
                       //     MessageBox.Show("listitems was set");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("An error happened while trying to get running Call of Duty/UO processes!");
                WriteLog(ex.ToString());
                MessageBox.Show("An error happend while trying to get running Call of Duty/UO processes: " + ex.Message + Environment.NewLine + "Please refer to the log for more info.");
            }
        }

        private void ProccessChecker_Tick(object sender, EventArgs e) => UpdateProcessBox();

        private void CoDPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!CoD1CheckBox.Checked)
            {
                CoD1CheckBox.Checked = true;
                ScalePictureBox(CoDPictureBox, CoDImage);
            }
            else
            {
                CoD1CheckBox.Checked = false;
                ScalePictureBox(CoDPictureBox, CoDUOImage);
            }
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show((new NotImplementedException()).Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show((new NotImplementedException()).Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
