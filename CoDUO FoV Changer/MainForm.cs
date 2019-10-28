using System;
using System.Drawing;
using System.Linq;
using System.Text;
using CurtLog;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using ClampExt;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer
{
    public partial class MainForm : Form
    {
        public static string appdata = Environment.GetEnvironmentVariable("appdata") + @"\";
        public static string temp = Environment.GetEnvironmentVariable("temp") + @"\";
        public static string appdataFoV = appdata + "CoDUO FoV Changer";
        public static string logsPath = appdataFoV + @"\Logs";
        public static string settingsPath = appdataFoV + @"\settings.xml";
        public const decimal hotfix = 7.2M;
        public static readonly string cleanVersion = Application.ProductVersion.Substring(0, 3);
        public static bool isDev = Debugger.IsAttached;
        public bool noLog = false;
        public static Settings settings = Settings.Instance;
        private Image CoDImage = Properties.Resources.CoD1;
        private Image CoDUOImage = Properties.Resources.CoDUO;
        public const string cgameDll = "uo_cgame_mp_x86.dll";
        public static Point location;
        private int currentSessionTime;
        private Memory memory;
        private DateTime lastHotkey;
        private DateTime lastUpdateCheck;
        public string GameVersion { get { return Registry.GetValue(GetRegistryPath(), "Version", string.Empty)?.ToString() ?? string.Empty; } }
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public MainForm() => InitializeComponent();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty


        public void InitLog()
        {
            Log.Settings.CustomLogHeader = "===========" + Application.ProductName + " Log File===========";
            Log.Settings.PerformanceOptions = Log.Performance.StandardPerformance;
            Log.Settings.LogHeaderOptions = Log.LogHeader.CustomHeader;
            var logLocation = logsPath + @"\CFC_" + DateTime.Now.ToString("d").Replace("/", "-") + ".log";
            Log.InitializeLogger(logLocation);
            if (settings.LastLogFile != logLocation)
            {
                try { if (File.Exists(settings.LastLogFile)) File.Delete(settings.LastLogFile); }
                catch (Exception ex) { WriteLog("Failed to delete: " + settings.LastLogFile + Environment.NewLine + ex.ToString()); }
            }
            settings.LastLogFile = logLocation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            CheckForIllegalCrossThreadCalls = true;
            Task.Run(() =>
            {
                var argsSB = new StringBuilder();
                var cmdArgs = Environment.GetCommandLineArgs();
                for (int i = 0; i < cmdArgs.Length; i++)
                {
                    var argu = cmdArgs[i];
                    var arg = argu.ToLower();
                    if (arg.IndexOf(Application.ProductName, StringComparison.OrdinalIgnoreCase) >= 0 || arg.IndexOf(Application.StartupPath, StringComparison.OrdinalIgnoreCase) >= 0) continue;
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

                    if (arg == "-debug") isDev = true;

                    if (arg == "-hotkeys" && Program.IsElevated) new Hotkeys().Show();

                    if (arg.Contains("-fov="))
                    {
                        var FoVStr = arg.Split('=')[1];
                        var FoV = 80;
                        if (int.TryParse(FoVStr, out FoV)) SetFoVNumeric(FoV);
                    }
                    argsSB.Append(argu + " ");
                }
                if (!Directory.Exists(appdataFoV)) Directory.CreateDirectory(appdataFoV);
                if (!noLog)
                {
                    if (!Directory.Exists(logsPath)) Directory.CreateDirectory(logsPath);
                    InitLog();
                }

                var args = argsSB.ToString().TrimEnd();
                if (!string.IsNullOrEmpty(args)) WriteLog("Launched program with args: " + args);
            });
         
            DvarsCheckBox.Visible = false;

            StartUpdates();



            Task.Run(() =>
            {
                try
                {
                    var regPath = Registry.GetValue(GetRegistryPath(), "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                    if (string.IsNullOrEmpty(settings.InstallPath) || !Directory.Exists(settings.InstallPath))
                    {
                        if (regPath.Contains("Call of Duty") || regPath.Contains("CoD"))
                        {
                            MessageBox.Show("Automatically detected game path: " + Environment.NewLine + regPath, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            settings.InstallPath = regPath;
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
                    MessageBox.Show("An error has occurred: " + ex.Message + Environment.NewLine + "Please refer to the log for more information.");
                    WriteLog("An exception happened on Install Path code:" + Environment.NewLine + ex.ToString());
                }
            });
            
            UpdateProcessBox();
            memory = GetProcessMemoryFromBox();

            SetFoVNumeric(settings.FoV);
            MinimizeCheckBox.Checked = settings.MinimizeToTray;
            FogCheckBox.Checked = settings.Fog;
            fogToolStripMenuItem.Checked = settings.Fog;
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
            
            WriteLog("Successfully started application, version " + Application.ProductVersion);
            watch.Stop();
            var timeTaken = watch.Elapsed;
            if (timeTaken.TotalMilliseconds >= 300) WriteLog("Startup took: " + timeTaken.TotalMilliseconds + "ms (this is too long!)");
        }

        private void StartGame()
        {
            try
            {
                if (string.IsNullOrEmpty(settings.InstallPath))
                {
                    MessageBox.Show("Install Path is empty!", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Directory.Exists(settings.InstallPath))
                {
                    MessageBox.Show("Install path: " + settings.InstallPath + " is invalid.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(settings.ExeName))
                {
                    MessageBox.Show("Exe name is empty!", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(settings.InstallPathExe))
                {
                    MessageBox.Show("Unable to find: " + settings.InstallPathExe, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var startInfo = new ProcessStartInfo();
                var startInfoSB = new StringBuilder("+set r_ignorehwgamma 1 +set vid_xpos 0 +set vid_ypos 0 +set win_allowalttab 1");
                if (!string.IsNullOrEmpty(LaunchParametersTB.Text)) startInfoSB.Append(LaunchParametersTB.Text + " " + startInfo.Arguments);
                startInfo.Arguments = startInfoSB.ToString();
                startInfo.FileName = settings.InstallPathExe;
                startInfo.WorkingDirectory = settings.InstallPath;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
                WriteLog("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            var shiftMod = Control.ModifierKeys == Keys.Shift;
            if (string.IsNullOrEmpty(settings.ExeName) || shiftMod)
            {
                ipFDialog.DefaultExt = ".exe";
                ipFDialog.Filter = "|*.exe";
                if (!shiftMod) MessageBox.Show("Please select the exe to launch (this will be saved)", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                var ipfResult = ipFDialog.ShowDialog();
                if (ipfResult != DialogResult.OK) return;
                if (!ipFDialog.FileName.EndsWith(".exe"))
                {
                    MessageBox.Show("Selected file is not an executable.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var filePath = Path.GetDirectoryName(ipFDialog.FileName);
                if (settings.InstallPath != filePath) settings.InstallPath = filePath;
                MessageBox.Show("Selected: " + (settings.ExeName = ipFDialog.SafeFileName) + Environment.NewLine + "You can change this at any time by holding shift then clicking this button again", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Task.Run(() => StartGame());
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
            if (memory != null && memory.IsRunning() && !IsUO())
            {
                FogCheckBox.Checked = true;
                return;
            }
            settings.Fog = FogCheckBox.Checked;
            fogToolStripMenuItem.Checked = settings.Fog;
            ToggleFog(settings.Fog);
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
            memory = GetProcessMemoryFromBox();
            if (memory != null && memory.IsRunning() && !IsUO())
            {
                DvarsCheckBox.Checked = false;
                FogCheckBox.Checked = false;
            }
            Task.Run(() =>
            {
                doRAChecks();
                doFoV();
                ToggleFog(settings.Fog);
                doDvars();
            });
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void HotKeyHandler_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            //we can be super responsive when someone is tapping +- while not accidentally moving it up or down twice by doing manual checks while having the timer at ~5-10ms
            if (lastHotkey == null) lastHotkey = now;
            else if ((now - lastHotkey).TotalMilliseconds < 100) return;
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
                FogCheckBox.Checked = !FogCheckBox.Checked;
                lastHotkey = now.AddMilliseconds(110);
            }
        }


        private void DvarsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (memory != null && memory.IsRunning() && !IsUO())
            {
                DvarsCheckBox.Checked = false;
                return;
            } //no support for dvar unlocking (yet) in cod1
            doDvars();
        }
        #region Util
        public static string GetRegistryPath()
        {
            var path = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive";
            if (!Environment.Is64BitOperatingSystem) path = path.Replace(@"Wow6432Node\", "");
            if (Registry.LocalMachine.OpenSubKey(path.Replace(@"HKEY_LOCAL_MACHINE\", "")) == null) path = path.Replace("United Offensive", ""); //if UO key is null, use cod1 (if it exists)
            return path;
        }

        public bool IsUO() { return (memory == null || !memory.IsRunning()) ? false : (memory.ProcMemory?.DllImageAddress(cgameDll) ?? 0) != 0 || (memory.ProcMemory?.DllImageAddress("uo_ui_mp_x86.dll") ?? 0) != 0; }
        

        private Process[] GetAllGameProcesses() { return Process.GetProcessesByName("CoDUOMP").Concat(Process.GetProcessesByName("CoDMP")).Concat(Process.GetProcessesByName("mohaa")).ToArray(); }

        private void StartUpdates() => Task.Run(() => SetLabelText(CheckUpdatesLabel, (CheckUpdates()) ? "Updates available!" : "No updates found. Click to check again."));

        private bool CheckUpdates()
        {
            try
            {
                var response = WebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")?.GetResponse() ?? null;
                var version = new StreamReader(response?.GetResponseStream() ?? null)?.ReadToEnd() ?? string.Empty;
                decimal hfDec;
                if (!decimal.TryParse(version, out hfDec))
                {
                    WriteLog("Failed to parse: " + version + " (version) as decimal.");
                    return !version.Contains(hotfix.ToString());
                }
                return hfDec > hotfix;
            }
            catch (Exception ex)
            {
                WriteLog("Unable to check for updates: " + ex.Message + Environment.NewLine + ex.ToString());
                return false;
            }
        }

        
        public static bool IsKeyPushedDown(Keys vKey) { return (GetAsyncKeyState((int)vKey) & 0x8000) != 0; }

        public bool IsProcessRunning(int pid) { return Process.GetProcesses().Any(p => p?.Id == pid); }

        public void SetLabelText(Label label, string text)
        {
            if (label == null) return;
            if (label.InvokeRequired) label.BeginInvoke((MethodInvoker)delegate () { label.Text = text; });
            else label.Text = text;
        }

        public bool TryParseKeys(string text, ref Keys value)
        {
            Keys tmp;
            if (Enum.TryParse(text, out tmp))
            {
                value = tmp;
                return true;
            }
            else return false;
        }

        public int GCD(int a, int b)
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

        Memory GetProcessMemoryFromBox()
        {
            try
            {
                var cbString = GamePIDBox?.SelectedItem?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(cbString)) return null;
                var pid = 0;
                if (!int.TryParse(cbString.Split('(')[1].Replace(")", ""), out pid)) return null;
                var mem = new Memory(pid);
                return (mem != null && mem.IsRunning()) ? mem : null;
            }
            catch (Exception ex) { WriteLog(ex.ToString()); }
            return null;
        }

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

        private void SetFoVNumeric(decimal fov)
        {
            ClampEx.Clamp(ref fov, FoVNumeric.Minimum, FoVNumeric.Maximum);
            if (FoVNumeric.InvokeRequired) FoVNumeric.BeginInvoke((MethodInvoker)delegate () { FoVNumeric.Value = fov; });
            else FoVNumeric.Value = fov;
        }

        public static void WriteLog(string message) { if (Log.IsInitialized) Log.WriteLine(message); }
        #endregion
        #region Memory
        private void ToggleFog(bool val)
        {
            try
            {
                if (memory == null || !memory.IsRunning()) return;
                var value = Convert.ToInt32(val);
                var newmpAddr = memory.GetIntPointerAddress(0x489A0D4, 0x20);
                var fogValue = memory.ReadIntAddress(0x489A0D4, 0x20);
                if (fogValue != value) memory.ProcMemory.WriteInt(newmpAddr, value);
            }
            catch (Exception ex) { WriteLog("An exception happened while trying to read/write fog values:" + Environment.NewLine + ex.ToString()); }
        }

        void doRAChecks()
        {
            if (memory == null || !memory.IsRunning()) return;
            try
            {
                var mode = memory.ReadIntAddress(0x4899D50, 0x20);
                var width = memory.ReadIntAddress(0x4899D30, 0x20);
                var height = memory.ReadIntAddress(0x4899FCC, 0x20);
                if (width == 0 || height == 0)
                {
                    WriteLog("Got bad width/height: " + width + ", " + height);
                    return;
                }
                var ratio = (double)width / (double)height;
                var aspectWidth = width / GCD(width, height);
                var aspectHeight = height / GCD(width, height);
                var aspectStr = aspectWidth + ":" + aspectHeight;
                if (aspectWidth == 8 && aspectHeight == 5) aspectStr = aspectStr.Replace("8", "16").Replace("5", "10");
                var maxFoV = ((ratio < 1.7 && mode == -1) || mode != -1) ? 105 : 130;
                if (FoVNumeric.InvokeRequired) FoVNumeric.Invoke((MethodInvoker)delegate () { FoVNumeric.Maximum = maxFoV; });
                else FoVNumeric.Maximum = maxFoV;
                SetFoVNumeric(FoVNumeric.Value); //will set it to the value if it can, if not, falls back on maximum
            }
            catch (Exception ex) { WriteLog("An exception happened while trying to get current game resolution:" + Environment.NewLine + ex.ToString()); }
        }

        private void doFoV()
        {
            try
            {
                var address = (!IsUO()) ? 0x3029CA28 : (memory != null && memory.IsRunning() ? (memory.ProcMemory.DllImageAddress(cgameDll) + 0x52F7C8) : -1);
                if (memory == null || !memory.IsRunning() || (address == -1))
                {
                    SetLabelText(StatusLabel, "Status: not found or failed to write to memory!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate () { toolTip1.SetToolTip(StatusLabel, "Process not found or failed to write to memory!"); });
                    StatusLabel.BeginInvoke((MethodInvoker)delegate () { StatusLabel.ForeColor = Color.Red; });
                }
                else
                {
                    memory.ProcMemory.WriteFloat(address, Convert.ToSingle(FoVNumeric.Value));
                    SetLabelText(StatusLabel, "Status: Game found and wrote to memory!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate () { toolTip1.SetToolTip(StatusLabel, string.Empty); });
                    StatusLabel.BeginInvoke((MethodInvoker)delegate () { StatusLabel.ForeColor = Color.DarkGreen; });
                }
            }
            catch (Exception ex) { WriteLog("An exception happened while trying to read/write FoV addresses: " + Environment.NewLine + ex.ToString()); }
        }

        private void doDvars()
        {
            if (memory == null || !memory.IsRunning()) return;
            try
            {
                var val = (DvarsCheckBox.Checked) ? 235 : 116;
                var curVal = memory.ProcMemory.ReadByte(0x43DD86);
                if (curVal == val) return; //don't write if it's already the value we want
                memory.ProcMemory.WriteInt(0x43DD86, val, 1);
                memory.ProcMemory.WriteInt(0x43DDA3, val, 1);
                memory.ProcMemory.WriteInt(0x43DDC1, val, 1);
            }
            catch (Exception ex) { WriteLog("An exception happened while trying to read/write Dvar addresses:" + Environment.NewLine + ex.ToString()); }
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
            StartUpdates();
            lastUpdateCheck = now;
        }

        private void CheckUpdatesLabel_TextChanged(object sender, EventArgs e) => UpdateButton.Visible = (isDev) ? true : (CheckUpdatesLabel.Text == "Update available!");

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm().Show();

        private void AccessGameTimeLabel()
        {
            try
            {
                if (settings.GameTime >= (int.MaxValue - 1))
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
            catch (Exception ex)
            {
                WriteLog("An exception happened while trying to get total played time!");
                WriteLog(ex.ToString());
            }
        }

        private void GameTracker_Tick(object sender, EventArgs e)
        {
            try
            {
                var processes = GetAllGameProcesses();
                if (processes.Length < 1) return;
                if (settings.GameTime < (int.MaxValue - 1)) settings.GameTime++;
                if (currentSessionTime < (int.MaxValue - 1)) currentSessionTime++;
                AccessGameTimeLabel();
            }
            catch (Exception ex) { WriteLog(ex.ToString()); }
        }


        private void LaunchParametersTB_TextChanged(object sender, EventArgs e)
        {
            LaunchParametersTB.Height = (LaunchParametersTB.Text.Length >= 36) ? 50 : 20;
            LaunchParametersTB.ScrollBars = (LaunchParametersTB.Text.Length >= 139) ? ScrollBars.Vertical : ScrollBars.None;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CheckUpdatesLabel.Text = "Checking for updates...";
            StartUpdates();
        }

        private void StartUpdater()
        {
            try
            {
                var path = temp + "CoDUO FoV Changer Updater.exe";
                if (!File.Exists(path))
                {
                    File.WriteAllBytes(path, Properties.Resources.CoDUO_FoV_Changer_Updater);
                    WriteLog("Created updater at: " + path);
                }
                var updaterInfo = new ProcessStartInfo();
                updaterInfo.Verb = "runas";
                updaterInfo.WorkingDirectory = Application.StartupPath;
                updaterInfo.FileName = path;
                Process.Start(updaterInfo);
                WriteLog("Started Updater, shutting down");
                Application.Exit();
            }
            catch (Exception ex)
            {
                WriteLog("An error happened while trying to write the updater:" + Environment.NewLine + ex.ToString());
                MessageBox.Show("An error happened while trying to write the updater: " + Environment.NewLine + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e) { if ((MessageBox.Show("Are you sure you want to update now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)) StartUpdater(); }

        

        void UpdateProcessBox()
        {
            try
            {
                var selectedIndex = GamePIDBox.SelectedIndex;
                for (int i = 0; i < GamePIDBox.Items.Count; i++)
                {
                    var boxItem = GamePIDBox.Items[i];
                    var pid = 0;
                    var splitPid = (boxItem as string).Split('(')[1].Replace(")", "");
                    if (!int.TryParse(splitPid, out pid)) continue;
                    if (!IsProcessRunning(pid)) GamePIDBox.Items.Remove(boxItem);
                }
                var allProcs = GetAllGameProcesses();
                for (int i = 0; i < allProcs.Length; i++)
                {
                    var proc = allProcs[i];
                    if (proc?.Id == 0) continue;
                    var pidStr = proc.Id.ToString();
                    var hasPid = false;
                    for(int j = 0; j < GamePIDBox.Items.Count; j++)
                    {
                        if ((GamePIDBox.Items[j] as string).Contains(pidStr))
                        {
                            hasPid = true;
                            break;
                        }
                    }
                    if (!hasPid) GamePIDBox.Items.Add(proc.ProcessName + " (" + pidStr + ")");
                }
                GamePIDBox.Visible = GamePIDBox.Items.Count > 0;
                if (GamePIDBox.SelectedItem == null && GamePIDBox.Items.Count > 0) GamePIDBox.SelectedIndex = ClampEx.Clamp(selectedIndex - 1, 0, GamePIDBox.Items.Count);
            }
            catch (Exception ex)
            {
                WriteLog("An error happened while trying to get running Call of Duty/UO processes:" + Environment.NewLine + ex.ToString());
                MessageBox.Show("An error happend while trying to get running Call of Duty/UO processes: " + ex.Message + Environment.NewLine + "Please refer to the log for more info.");
            }
        }

        private void ProccessChecker_Tick(object sender, EventArgs e)
        {
            UpdateProcessBox();
            if (memory != null && memory.IsRunning()) ScalePictureBox(CoDPictureBox, IsUO() ? CoDUOImage : CoDImage);
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChangelogForm.Instance != null && !ChangelogForm.Instance.IsDisposed) ChangelogForm.Instance.BringToFront();
            else new ChangelogForm().Show();
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Created by Shady" + (Environment.NewLine + Environment.NewLine) + "This program is intended to allow you to change the Field of View in Multiplayer for both Call of Duty and Call of Duty: United Offensive, both of which do not normally allow you to do so." + (Environment.NewLine + Environment.NewLine) + "Program version: " + ProductVersion + Environment.NewLine + "Game version: " + (!string.IsNullOrEmpty(GameVersion) ? GameVersion : "Unknown"), ProductName + " (" + ProductVersion + ")", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            MinimizeIcon.Visible = false;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();

        private void fogToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsUO())
            {
                fogToolStripMenuItem.Checked = true;
                return;
            }
            settings.Fog = FogCheckBox.Checked = fogToolStripMenuItem.Checked;
            Task.Run(() => ToggleFog(settings.Fog));
        }

        private void fogToolStripMenuItem_Click(object sender, EventArgs e) =>  fogToolStripMenuItem.Checked = !fogToolStripMenuItem.Checked; //idk why this is needed but it seems like it is??

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e) => new SettingsForm().Show();

        private void GamePIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            memory = GetProcessMemoryFromBox();
            if (memory != null && memory.IsRunning()) ScalePictureBox(CoDPictureBox, IsUO() ? CoDUOImage : CoDImage);
        }

        private void GamePIDBox_VisibleChanged(object sender, EventArgs e) => CoDPictureBox.Visible = GamePIDBox.Visible;
        
    }
}