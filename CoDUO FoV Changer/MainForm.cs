using System;
using System.Drawing;
using System.Text;
using CurtLog;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using ClampExt;
using System.Threading.Tasks;
using HotkeyHandling;
using SessionHandling;
using System.Collections.Generic;
using BitmapExtension;
using ShadyPool;
using ProcessExtensions;
using TimerExtensions;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using CoDRegistryExtensions;
using System.Text.RegularExpressions;

namespace CoDUO_FoV_Changer
{
    public partial class MainForm : Form
    {
        public const decimal HOTFIX = 7.8M;

        public static bool IsDev { get; private set; } = Debugger.IsAttached;

        private readonly Settings settings = Settings.Instance;

        public SessionHandler CurrentSession { get; private set; } = new SessionHandler();

        public Memory MemorySelection { get; private set; }


        private TimeSpan _lastGameTimeSpan;
        private DateTime _lastHotkey;
        private DateTime _lastUpdateCheck;

        private bool _needsUpdate = false;

        public static MainForm Instance = null;

        private const string LATEST_DOWNLOAD_URI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases/latest/download/CoDUO.FoV.Changer.exe";
        private const string UPDATE_URI = @"https://raw.githubusercontent.com/Shady7557/CoDUO-FoV-Changer/master/HOTFIX";

        private const string GITHUB_RELEASES_URI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases";

        public float CurrentFoV
        {
            get;
            private set;
        }
      

        public bool IsCheckingForUpdates { get; private set; } = false;

        public MainForm()
        {
            Instance = this;
            InitializeComponent();

            ListenForMaximizeSignal();
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty

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

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOW = 5;

        //I don't like this.
        private void ListenForMaximizeSignal()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using (var mmf = MemoryMappedFile.CreateOrOpen(Program.MEMORY_MAPPED_NAME, Program.MEMORY_MAPPED_SIZE))
                {
                    while (true)
                    {

                        using (var accessor = mmf.CreateViewAccessor())
                        {
                            if (accessor.ReadByte(0) == 1)
                            {
                                BeginInvoke((MethodInvoker)delegate { UnminimizeFromTray(); });

                                // Clear the byte
                                accessor.Write(0, (byte)0);
                            }
                        }


                        Thread.Sleep(250); // Polling interval
                    }
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var watch = Pool.Get<Stopwatch>();

            try
            {
                watch.Restart();

                AdminLaunchButton.Visible = false;
                if (!Program.IsElevated) AdminLaunchButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));

                var argsSB = Pool.Get<StringBuilder>();
                try
                {
                    argsSB.Clear();

                    var cmdArgs = Environment.GetCommandLineArgs();
                    for (int i = 0; i < cmdArgs.Length; i++)
                    {
                        var arg = cmdArgs[i];
                        if (arg.IndexOf(Application.ProductName, StringComparison.OrdinalIgnoreCase) >= 0 || arg.IndexOf(Application.StartupPath, StringComparison.OrdinalIgnoreCase) >= 0) continue;

                        if (arg.Equals("-launch", StringComparison.OrdinalIgnoreCase)) StartGameButton.PerformClick();

                        if (arg.Equals("-debug", StringComparison.OrdinalIgnoreCase)) IsDev = true;

                        if (Program.IsElevated) //ensure elevation before checking these args, otherwise a user could potentially make these forms appear without being elevated & cause an exception
                        {
                            if (arg.Equals("-hotkeys", StringComparison.OrdinalIgnoreCase)) 
                                new Hotkeys().Show();

                            if (arg.Equals("-cdkeymanager", StringComparison.OrdinalIgnoreCase))
                            {
                                var form = new CDKeyManagerForm();
                                form.Show();
                                form.BeginInvoke((MethodInvoker)delegate
                                {
                                    form.BringToFront();
                                    form.Select();
                                });
                                form.Location = new Point((int)(Location.X / 3.25f), (int)(Location.Y / 3f));
                            }
                        }



                        if (arg.IndexOf("-fov=", StringComparison.OrdinalIgnoreCase) >= 0 && decimal.TryParse(arg.Split('=')[1], out decimal FoV))
                            SetFoVNumeric(FoV);

                        argsSB.Append(arg).Append(" ");
                    }

                    if (argsSB.Length > 1)
                        argsSB.Length--;

                    var argStr = argsSB.ToString();

                    argsSB.Clear().Append("Launched program");

                    if (!string.IsNullOrWhiteSpace(argStr))
                        argsSB.Append(" with args: ").Append(argStr);

                    Log.WriteLine(argsSB.ToString());
                }
                finally { Pool.Free(ref argsSB); }

                StartUpdateChecking();

                Task.Run(() =>
                {
                    try
                    {


                        if (string.IsNullOrEmpty(settings.InstallPath) || !Directory.Exists(settings.InstallPath))
                        {
                            var scannedPath = PathScanner.ScanForGamePath();
                            if (!string.IsNullOrEmpty(scannedPath))
                            {
                                MessageBox.Show("Automatically detected game path: " + Environment.NewLine + scannedPath, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                settings.InstallPath = scannedPath;
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
                        Log.WriteLine("An exception happened on Install Path code:" + Environment.NewLine + ex.ToString());
                    }
                });

                UpdateProcessBox();

                AdminLaunchButton.Visible = !Program.IsElevated && (MemorySelection?.ProcMemory?.RequiresElevation() ?? false);

                SetFoVNumeric(settings.FoV);

                MinimizeCheckBox.Checked = settings.MinimizeToTray;
                LaunchParametersTB.Text = settings.CommandLine;

                if (settings.TrackGameTime) AccessGameTimeLabel();
                else
                {
                    GameTimeLabel.Visible = false;
                    CurSessionGT.Visible = false;
                    GameTracker.Enabled = false;
                }

                TimerEx.Every(3f, MemoryTimer_Tick);

                UpdateButton.Visible = IsDev;
            }
            finally
            {
                watch.Stop();

                var sb = Pool.Get<StringBuilder>();
                try
                {
                    var timeTaken = watch.Elapsed;
                    Console.WriteLine(sb.Clear().Append("Form load took: ").Append(timeTaken.TotalMilliseconds).Append("ms").ToString());

                    if (timeTaken.TotalMilliseconds > 100) Log.WriteLine(sb.Clear().Append("Startup took: ").Append(timeTaken.TotalMilliseconds).Append("ms (this is too long!)").ToString());

                    Log.WriteLine(sb.Clear().Append("Successfully started application, version ").Append(Application.ProductVersion).ToString());
                }
                finally { Pool.Free(ref sb); }

            }
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

                var launchFileName = settings.InstallPathExe;

                var oldCfg = string.Empty;
                var isCoD1 = !(settings.InstallPathExe.IndexOf("coduo", StringComparison.OrdinalIgnoreCase) >= 0);

                var useSteam = ShouldUseSteam();

                if (useSteam)
                {
                    try
                    {
                        Log.WriteLine("Path contained 'steamapps' and Steam is running. Should launch with steam, trying!");



                        if (!string.IsNullOrWhiteSpace(LaunchParametersTB.Text))
                        {
                            Log.WriteLine("Writing launch parameters to config temporarily.");

                            oldCfg = GetGameConfig(isCoD1);

                            ApplyLaunchParametersToConfig(LaunchParametersTB.Text, isCoD1);

                            Log.WriteLine("Wrote launch parameters.");
                        }


                        var steamLaunchUrl = string.Empty;

                        var sb = Pool.Get<StringBuilder>();
                        try
                        {
                            //steam://launch/{appid}/dialog
                            //thanks to a guy named Lone who helped me figure out how to launch a game where you can select the option via Steam. Now that Steam lets you permanently select your desired option, this either immediately launches the game via Steam or Steam prompts you to select version.
                            steamLaunchUrl = sb.Clear().Append("steam://launch/26").Append(isCoD1 ? "20" : "40").Append("/dialog").ToString();
                        }
                        finally { Pool.Free(ref sb); }

                        launchFileName = steamLaunchUrl;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                }




                var startInfo = new ProcessStartInfo
                {
                    Arguments = (!useSteam && !string.IsNullOrEmpty(LaunchParametersTB.Text)) ? LaunchParametersTB.Text : string.Empty,
                    FileName = launchFileName,
                    WorkingDirectory = settings.InstallPath
                };

                Process.Start(startInfo);



                if (!string.IsNullOrWhiteSpace(oldCfg))
                {
                    Log.WriteLine($"{nameof(oldCfg)} was not empty, waiting 8000 ms then saving old again");

                    Thread.Sleep(8000);

                    WriteGameConfig(oldCfg, isCoD1);

                    Log.WriteLine("Wrote old config to disk!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
                Log.WriteLine("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            var shiftMod = ModifierKeys == Keys.Shift;

            if (string.IsNullOrEmpty(settings.ExeName) || shiftMod)
            {
                ipFDialog.InitialDirectory = settings.InstallPath;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LaunchParametersTB.Text != settings.CommandLine) settings.CommandLine = LaunchParametersTB.Text;

            if (settings.HasChanged)
            {
                Log.WriteLine(nameof(settings.HasChanged) + ", so writing to disk");
                DatabaseFile.Write(settings, PathInfos.SettingsPath);
                Log.WriteLine("Wrote settings to disk");
            }
        }

        private void FoVNumeric_ValueChanged(object sender, EventArgs e)
        {
            FoVNumeric.DecimalPlaces = FoVNumeric.Value != (int)FoVNumeric.Value ? 1 : 0; //decimal places if the value isn't an int


            Task.Run(() =>
            {
                CurrentFoV = Convert.ToSingle(FoVNumeric.Value);
                settings.FoV = FoVNumeric.Value;
                DoFoV();
            });
        }

        private void MinimizeCheckBox_CheckedChanged(object sender, EventArgs e) => settings.MinimizeToTray = MinimizeCheckBox.Checked;

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (MinimizeCheckBox.Checked && WindowState == FormWindowState.Minimized)
                MinimizeToTray();
        }

        private void MinimizeIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                UnminimizeFromTray();
        }


        private void MinimizeToTray()
        {
            ShowWindow(Handle, SW_HIDE);
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);

            MinimizeIcon.Visible = true;
            MinimizeIcon.BalloonTipIcon = ToolTipIcon.Info;
            MinimizeIcon.BalloonTipText = Application.ProductName + " is minimized. Double click to restore full-size.";
            MinimizeIcon.BalloonTipTitle = "Minimized to Tray";
            MinimizeIcon.ShowBalloonTip(4300, "Minimized to Tray", Application.ProductName + " is minimized. Click to restore full-size.", ToolTipIcon.Info);
        }

        private void UnminimizeFromTray()
        {
            ShowWindow(Handle, SW_SHOW);
            ShowWindow(Handle, SW_SHOWNORMAL);
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) & ~WS_EX_TOOLWINDOW);
            MinimizeIcon.Visible = false;
        }

        private void MemoryTimer_Tick()
        {
            Task.Run(() =>
            {
                var wantedButtonState = false;

                if (MemorySelection == null || !(MemorySelection?.ProcMemory?.RequiresElevation() ?? false) && !MemorySelection.IsRunning())
                {
                    SetLabelText(StatusLabel, "Status: Not running");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process not found or failed to access memory!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                }
                else
                {
                    wantedButtonState = !Program.IsElevated && (MemorySelection?.ProcMemory?.RequiresElevation() ?? false);

                    DoRAChecks();
                    DoFoV();
                }

                if (AdminLaunchButton.Visible != wantedButtonState)
                    AdminLaunchButton.BeginInvoke((MethodInvoker)delegate { AdminLaunchButton.Visible = wantedButtonState; });
                
            });
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void HotKeyHandler_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            //we can be super responsive when someone is tapping +- while not accidentally moving it up or down twice by doing manual checks while having the timer at ~5-10ms
            if (_lastHotkey == null) _lastHotkey = now;
            else if ((now - _lastHotkey).TotalMilliseconds < 100) return;

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

            if (HotkeyHandler.IsKeyPushedDown(modifier))
            {
                if (HotkeyHandler.IsKeyPushedDown(up))
                {
                    SetFoVNumeric(FoVNumeric.Value + 1);
                    _lastHotkey = now;
                }
                if (HotkeyHandler.IsKeyPushedDown(down))
                {
                    SetFoVNumeric(FoVNumeric.Value - 1);
                    _lastHotkey = now;
                }
            }
        }

        #region Util

        public bool ShouldUseSteam()
        {
            return (settings?.InstallPathExe ?? string.Empty).IndexOf("steamapps", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool IsUOMemory()
        {
            if (MemorySelection == null || !MemorySelection.IsRunning())
                return false; //I don't get why a null operator doesn't work here, but it doesn't. so we have this full 'check' here instead

            return (MemorySelection.ProcMemory?.DllImageAddress(MemoryAddresses.UO_UI_MP_DLL) ?? 0) != 0 || (MemorySelection.ProcMemory?.DllImageAddress(MemoryAddresses.UO_CGAME_MP_DLL) ?? 0) != 0;
        }

        private void GetAllGameProcessesNoAlloc(ref List<Process> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            var allProcs = Process.GetProcesses();

            for (int i = 0; i < allProcs.Length; i++)
            {
                var proc = allProcs[i];
                if (ProcessExtension.IsCoDMPProcess(proc))
                    list.Add(proc);
            }
        }

        private void StartUpdateChecking()
        {
            Task.Run(() =>
            {
                _needsUpdate = CheckUpdates();

                if (!IsDev)
                {
                    if (UpdateButton.InvokeRequired) UpdateButton.BeginInvoke((MethodInvoker)delegate { UpdateButton.Visible = _needsUpdate; });
                    else UpdateButton.Visible = _needsUpdate;
                }

                SetLabelText(CheckUpdatesLabel, _needsUpdate ? "Updates available!" : "No updates found. Click to check again.");
            });
        }

        private bool CheckUpdates()
        {
            try
            {
                IsCheckingForUpdates = true;
                string version;
                using (var reader = new StreamReader(WebRequest.Create(UPDATE_URI)?.GetResponse()?.GetResponseStream() ?? null))
                {
                    try { version = reader?.ReadToEnd() ?? string.Empty; }
                    finally { reader.Close(); }
                }

                if (!decimal.TryParse(version, out decimal hfDec))
                {
                    Log.WriteLine("Failed to parse: " + version + " (version) as decimal.");
                    return !version.Contains(HOTFIX.ToString());
                }
                return hfDec > HOTFIX;
            }
            catch (Exception ex)
            {
                Log.WriteLine("Unable to check for updates: " + ex.Message + Environment.NewLine + ex.ToString());
                return false;
            }
            finally { IsCheckingForUpdates = false; }
        }



        private void SetLabelText(Label label, string text)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            if (label.InvokeRequired) label.BeginInvoke((MethodInvoker)delegate () { label.Text = text; });
            else label.Text = text;
        }

        private bool TryParseKeys(string text, ref Keys value)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            if (Enum.TryParse(text, out Keys tmp))
            {
                value = tmp;
                return true;
            }
            else return false;
        }

        private void SetFoVNumeric(decimal fov)
        {
            ClampEx.Clamp(ref fov, FoVNumeric.Minimum, FoVNumeric.Maximum);
            if (FoVNumeric.InvokeRequired) FoVNumeric.BeginInvoke((MethodInvoker)delegate () { FoVNumeric.Value = fov; });
            else FoVNumeric.Value = fov;
        }

        #endregion
        #region Memory
        private void DoRAChecks() //this is where we do aspect ratio checks to ensure we limit FoV at non-widescreen values to reduce potential for any 'cheating' (this affects only non-widescreen resolutions & the advantage gained from significantly higher FoV is *incredibly* minor, but we still do not want to encourage this)
        {
            if (MemorySelection == null || !MemorySelection.IsRunning() || MemorySelection.ProcMemory.RequiresElevation() || CurrentSession == null || CurrentSession.GetSessionTime().TotalSeconds < 30) return; //if you do aspect ratio checks too soon, they'll return invalid values

            try
            {
                var mode = MemorySelection.ReadIntAddress(MemoryAddresses.UO_R_MODE_ADDRESS, 0x20);
                var ratio = 0d;


                if (mode == -1)
                {
                    var width = MemorySelection.ReadIntAddress(MemoryAddresses.UO_R_WIDTH_ADDRESS, 0x20);
                    var height = MemorySelection.ReadIntAddress(MemoryAddresses.UO_R_HEIGHT_ADDRESS, 0x20);

                    if (width <= 0 || height <= 0)
                    {
                        Log.WriteLine("Got bad width/height: " + width + ", " + height);
                        return;
                    }

                    ratio = width / (double)height;
                }


                var maxFoV = (mode != -1 || (ratio < 1.7 && mode == -1)) ? 105 : 120; //"120" is not real 120! remember: the screen is literally stretched in wide screen. this is equivalent to ~110 'real' FoV in a game; still lower than a common max value of (real) 120.

                if (FoVNumeric.InvokeRequired) FoVNumeric.Invoke((MethodInvoker)delegate () { FoVNumeric.Maximum = maxFoV; });
                else FoVNumeric.Maximum = maxFoV;

                SetFoVNumeric(FoVNumeric.Value); //will set it to the value if it can, if not, falls back on maximum
            }
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to get current game resolution:" + Environment.NewLine + ex.ToString()); }
        }

        private void DoFoV()
        {
            try
            {
                var isRunning = MemorySelection?.IsRunning() ?? false;

                if ((MemorySelection?.ProcMemory?.RequiresElevation() ?? false))
                {
                    SetLabelText(StatusLabel, "Status: Game requires elevation!");

                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process requires elevation!");
                        StatusLabel.ForeColor = Color.Orange;
                    });

                    return;
                }

                var address = !isRunning ? -1 : !IsUOMemory() ? MemoryAddresses.COD_FOV_ADDRESS : (isRunning ? (MemorySelection.ProcMemory.DllImageAddress(MemoryAddresses.UO_CGAME_MP_DLL) + MemoryAddresses.UO_FOV_OFFSET) : -1);
                if (!isRunning || address <= 0)
                {
                    SetLabelText(StatusLabel, "Status: Not running");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process not found or failed to write to memory!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                }
                else
                {
                    MemorySelection.ProcMemory.WriteFloat(address, CurrentFoV);
                    SetLabelText(StatusLabel, "Status: Success!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate ()
                    {
                        toolTip1.SetToolTip(StatusLabel, string.Empty);
                        StatusLabel.ForeColor = Color.DarkGreen;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine("An exception happened while trying to read/write FoV addresses: " + Environment.NewLine + ex.ToString());
            }
        }
        #endregion

        private void CheckUpdatesLabel_Click(object sender, EventArgs e)
        {
            if (IsCheckingForUpdates) return;
            var now = DateTime.Now;
            if (_lastUpdateCheck == null) _lastUpdateCheck = now;
            else
            {
                var time = now - _lastUpdateCheck;
                if (time.TotalSeconds < 30)
                {
                    MessageBox.Show("You're checking for updates too quickly!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            CheckUpdatesLabel.Text = "Checking for updates...";
            StartUpdateChecking();
            _lastUpdateCheck = now;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SettingsForm.Instance != null && !SettingsForm.Instance.Disposing && !SettingsForm.Instance.IsDisposed) SettingsForm.Instance.Show();
            else new SettingsForm().Show();
        }

        private void AccessGameTimeLabel()
        {
            try
            {
                var span = TimeSpan.FromSeconds(settings.GameTime);

                var spanCurrent = CurrentSession.GetSessionTime();

                var totalMinutes = Math.Floor(span.TotalMinutes);
                var totalHours = Math.Floor(span.TotalHours);
                var totalMinutesCur = Math.Floor(spanCurrent.TotalMinutes);
                var totalHoursCur = Math.Floor(spanCurrent.TotalHours);

                if (settings.GameTime >= 1 && totalMinutes < 1) GameTimeLabel.Text = "Game Time: " + settings.GameTime.ToString("N0") + " seconds";
                if (totalMinutes >= 1 && totalHours < 1) GameTimeLabel.Text = "Game Time: " + totalMinutes.ToString("N0") + " minutes";
                if (totalHours >= 1) GameTimeLabel.Text = "Game Time: " + totalHours.ToString("N0") + " hours";
                if (spanCurrent.TotalSeconds > 0 && totalMinutesCur < 1) CurSessionGT.Text = "Current Session: " + spanCurrent.TotalSeconds.ToString("N0") + " seconds";
                if (totalMinutesCur >= 1 && totalHoursCur < 1) CurSessionGT.Text = "Current Session: " + totalMinutesCur.ToString("N0") + " minutes";
                if (totalHoursCur >= 1) CurSessionGT.Text = "Current Session: " + totalHoursCur.ToString("N0") + " hours";
            }
            catch (Exception ex)
            {
                Log.WriteLine("An exception happened while trying to get total played time!");
                Log.WriteLine(ex.ToString());
            }
        }


        private void GameTracker_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var span = CurrentSession.GetSessionTime();

                var newSecs = (span - _lastGameTimeSpan).TotalSeconds;
                if (newSecs > 0) settings.GameTime += newSecs;

                _lastGameTimeSpan = span;
            });
        }

        private void GameTimeLabelTimer_Tick(object sender, EventArgs e) => AccessGameTimeLabel();


        private void LaunchParametersTB_TextChanged(object sender, EventArgs e)
        {
            LaunchParametersTB.Height = (LaunchParametersTB.Text.Length >= 36) ? 50 : 20;
            LaunchParametersTB.ScrollBars = (LaunchParametersTB.Text.Length >= 139) ? ScrollBars.Vertical : ScrollBars.None;
        }

        private void StartUpdater()
        {
            try
            {
                var sb = Pool.Get<StringBuilder>();
                try
                {

                    var currentProc = Process.GetCurrentProcess();
                    var fileNameDir = currentProc?.MainModule?.FileName ?? string.Empty;

                    if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
                    {
                        MessageBox.Show("Application path doesn't exist. Cannot update: " + fileNameDir, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var tempFoVPath = sb.Clear().Append(PathInfos.Temp).Append(@"\coduofovchanger_temp.exe").ToString();
                    if (File.Exists(tempFoVPath)) File.Delete(tempFoVPath);

                    using (var wc = new WebClient())
                    {
                        wc.Headers.Add("user-agent", sb.Clear().Append("CoDUO FoV Changer/").Append(Application.ProductVersion).ToString());
                        wc.DownloadFile(LATEST_DOWNLOAD_URI, tempFoVPath);
                    }

                    var path = sb.Clear().Append(PathInfos.Temp).Append(@"\Mover.exe").ToString();

                    if (File.Exists(path)) File.Delete(path);

                    File.WriteAllBytes(path, Properties.Resources.Mover);
                    Log.WriteLine("Created mover at: " + path);


                    var updaterInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = Application.StartupPath,
                        FileName = path,
                        Arguments = sb.Clear().Append("-movefrom=\"").Append(tempFoVPath).Append("\" -moveto=\"").Append(fileNameDir).Append("\" -wait=1000 -autostart -waitstart=1000 -exitwait=1000 -waitforpid=").Append(currentProc.Id).ToString()
                    };

                    Process.Start(updaterInfo);

                    Log.WriteLine(sb.Clear().Append("Started mover with args: ").Append(updaterInfo.Arguments).Append(", now shutting down").ToString());

                    if (InvokeRequired) BeginInvoke((MethodInvoker)delegate { Close(); });
                    else Close();
                }
                finally { Pool.Free(ref sb); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine("An error happened while trying to update:" + Environment.NewLine + ex.ToString());
                MessageBox.Show("An error happened while trying to update: " + Environment.NewLine + ex.Message + Environment.NewLine + "Please refer to the log for more info.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e) { if (MessageBox.Show("Are you sure you want to update now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) Task.Run(() => StartUpdater()); }

        private void UpdateProcessBox()
        {
            try
            {
                GamePIDBox.BeginUpdate();

                var selectedIndex = GamePIDBox.SelectedIndex;

                for (int i = 0; i < GamePIDBox.Items.Count; i++)
                {
                    var memory = GamePIDBox.GetMemoryFromIndex(i);
                    if (!memory?.IsRunning() ?? false) GamePIDBox.Items.Remove(GamePIDBox.Items[i]);
                }

                var allProcs = Pool.GetList<Process>();
                try
                {
                    GetAllGameProcessesNoAlloc(ref allProcs);

                    for (int i = 0; i < allProcs.Count; i++)
                    {
                        var proc = allProcs[i];

                        if (proc?.Id == 0)
                            continue;

                        var hasPid = false;

                        for (int j = 0; j < GamePIDBox.Items.Count; j++)
                        {

                            if (GamePIDBox?.GetMemoryFromIndex(j) != null)
                            {
                                hasPid = true;
                                break;
                            }

                        }

                        if (!hasPid)
                            GamePIDBox.AddProcessMemory(new Memory(proc.Id));
                    }
                }
                finally { Pool.FreeList(ref allProcs); }


                GamePIDBox.Visible = GamePIDBox.Items.Count > 0;

                if (GamePIDBox.Visible && GamePIDBox.SelectedItem == null)
                    GamePIDBox.SelectedIndex = ClampEx.Clamp(selectedIndex - 1, 0, GamePIDBox.Items.Count);


            }
            catch (Exception ex)
            {
                Log.WriteLine("An error happened while trying to get running Call of Duty/UO processes:" + Environment.NewLine + ex.ToString());
                MessageBox.Show("An error happend while trying to get running Call of Duty/UO processes: " + ex.Message + Environment.NewLine + "Please refer to the log for more info.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { GamePIDBox.EndUpdate(); }
        }

        private void ProccessChecker_Tick(object sender, EventArgs e)
        {
            UpdateProcessBox();
            if (MemorySelection != null && MemorySelection.IsRunning()) BitmapHelper.ScalePictureBox(CoDPictureBox, IsUOMemory() ? Properties.Resources.CoDUO : Properties.Resources.CoD1);
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(GITHUB_RELEASES_URI);
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Created with love by Shady" + Environment.NewLine + Environment.NewLine + "This program is intended to allow you to change the Field of View in Multiplayer for both Call of Duty and Call of Duty: United Offensive, both of which do not normally allow you to do so, resulting in an incredibly zoomed-in experience when playing widescreen resolutions\nThis application automatically detects widescreen aspect ratios, and if one is not used, the maximum FoV you can set will be lower so as to provide minimal potential advantage." + Environment.NewLine + Environment.NewLine + "Program version: " + ProductVersion + Environment.NewLine + "Game version: " + (!string.IsNullOrEmpty(CodRex.GameVersion) ? CodRex.GameVersion : "Unknown"), ProductName + " (" + ProductVersion + ")", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnminimizeFromTray();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SettingsForm.Instance != null && !SettingsForm.Instance.Disposing && !SettingsForm.Instance.IsDisposed) SettingsForm.Instance.Show();
            else new SettingsForm().Show();
        }

        private void GamePIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemorySelection = GamePIDBox.GetMemoryFromIndex(GamePIDBox.SelectedIndex);

            BitmapHelper.ScalePictureBox(CoDPictureBox, (IsUOMemory() || (ProcessExtension.IsAnyProcessRunning("CoDUOMP") && (!ProcessExtension.IsAnyProcessRunning("mohaa") && !ProcessExtension.IsAnyProcessRunning("codmp")))) ? Properties.Resources.CoDUO : Properties.Resources.CoD1);
        }

        private void GamePIDBox_VisibleChanged(object sender, EventArgs e) => CoDPictureBox.Visible = GamePIDBox.Visible;

        private void AdminLaunchButton_Click(object sender, EventArgs e)
        {
            if (Program.IsElevated)
                return;

            TryRestartAsAdmin();
        }

        private bool TryRestartAsAdmin()
        {
            var fileNameDir = Process.GetCurrentProcess()?.MainModule?.FileName ?? string.Empty;

            if (string.IsNullOrWhiteSpace(fileNameDir) || !File.Exists(fileNameDir)) return false;
            else
            {

                var startInfo = new ProcessStartInfo
                {
                    Verb = "runas",
                    Arguments = string.Join(" ", Environment.GetCommandLineArgs()),
                    WorkingDirectory = Application.StartupPath,
                    FileName = fileNameDir
                };

                try { Process.Start(startInfo); }
                catch (System.ComponentModel.Win32Exception win32ex) when (win32ex.NativeErrorCode == 1223)
                {
                    Log.WriteLine("User canceled UAC prompt (" + win32ex.Message + " )");
                    return false;
                }
                finally { Close(); }

            }

            return true;
        }

        /// <summary>
        /// Reads config file from disk and returns as string.
        /// </summary>
        /// <param name="vCod"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        private string GetGameConfig(bool vCod = false)
        {
            var installPath = settings.InstallPath;

            if (!Directory.Exists(installPath))
               throw new DirectoryNotFoundException(installPath);

            var cfgDirName = vCod ? "main" : "uo";
            var cfgFileName = vCod ? "config_mp.cfg" : "uoconfig_mp.cfg";

            var cfgPath = Path.Combine(Path.Combine(installPath, cfgDirName), cfgFileName);

            var cfgFileInfo = new FileInfo(cfgPath);
            if (!cfgFileInfo.Exists)
            {
                Console.WriteLine("could not find cfgPath: " + cfgPath);
                throw new FileNotFoundException(cfgPath);
            }

            var sizeInBytes = cfgFileInfo.Length;


            if (sizeInBytes > 1024000)
            {
                Console.WriteLine("cfg file is too large to modify (bytes): " + sizeInBytes);
                throw new IOException($"{cfgPath} is too large ({sizeInBytes})");
            }

            return File.ReadAllText(cfgPath);
        }

        private void WriteGameConfig(string cfgContents,  bool vCod = false)
        {
            var installPath = settings.InstallPath;

            if (!Directory.Exists(installPath))
                throw new DirectoryNotFoundException(installPath);

            var cfgDirName = vCod ? "main" : "uo";
            var cfgFileName = vCod ? "config_mp.cfg" : "uoconfig_mp.cfg";

            var cfgPath = Path.Combine(Path.Combine(installPath, cfgDirName), cfgFileName);

            File.WriteAllText(cfgPath, cfgContents);
        }

        private void ApplyLaunchParametersToConfig(string paramString, bool vCod = false)
        {
            if (string.IsNullOrWhiteSpace(paramString))
                throw new ArgumentNullException(nameof(paramString));

            var cfgReadText = GetGameConfig(vCod);

            if (string.IsNullOrWhiteSpace(cfgReadText))
                throw new ArgumentNullException(nameof(cfgReadText));
            

            var sb = Pool.Get<StringBuilder>();

            try 
            {
                sb.Clear().Append(cfgReadText);

                var argsStr = paramString;

                var splitArguments = Regex.Split(argsStr, @"(?=\+)");

                for (int i = 0; i < splitArguments.Length; i++)
                    sb.Append(splitArguments[i]).Replace("+set", "seta").Append(Environment.NewLine);


                if (splitArguments.Length > 0)
                    sb.Length--;

                try { WriteGameConfig(sb.ToString(), vCod); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

            }
            finally { Pool.Free(ref sb); }


        }

        private void MinimizeIcon_BalloonTipClicked(object sender, EventArgs e) => UnminimizeFromTray();
    }
}