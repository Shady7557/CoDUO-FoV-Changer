using System;
using System.Drawing;
using System.Text;
using CurtLog;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using HotkeyHandling;
using SessionHandling;
using System.Collections.Generic;
using ShadyPool;
using ProcessExtensions;
using TimerExtensions;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using CoDUO_FoV_Changer.Util;
using StringExtension;

namespace CoDUO_FoV_Changer
{
    public partial class MainForm : ExtendedForm
    {
        public static bool IsDev { get; private set; } = Debugger.IsAttached;

        private readonly Settings settings = Settings.Instance;

        public SessionHandler CurrentSession { get; private set; } = new SessionHandler();

        public Memory MemorySelection { get; private set; }

        private TimeSpan _lastGameTimeSpan;
        private DateTime _lastHotkey;
        private DateTime _lastUpdateCheck;

        private bool _needsUpdate = false;

        private const string LATEST_DOWNLOAD_URI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases/latest/download/CoDUO.FoV.Changer.exe";
        private const string UPDATE_URI = @"https://raw.githubusercontent.com/Shady7557/CoDUO-FoV-Changer/master/HOTFIX";

        private const string GITHUB_RELEASES_URI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases";

        public float CurrentFoV
        {
            get;
            private set;
        }

        private int DesktopHeight => ScreenUtil.GetPrimaryDisplayResolution().Height;
        private int DesktopWidth => ScreenUtil.GetPrimaryDisplayResolution().Width;

        public bool IsCheckingForUpdates { get; private set; } = false;

        public MainForm()
        {
            InitializeComponent();

            ListenForMaximizeSignal();
        }

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
        //But it works perfectly.
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

        private void UpdateStartGameButtonText(string gameProcName)
        {
            var sb = Pool.Get<StringBuilder>();
            try
            {
                var newTxt = sb.Clear().Append("Start Game");

                if (!string.IsNullOrWhiteSpace(gameProcName))
                    newTxt.Append(" (").Append(gameProcName).Append(")").Replace(".exe", string.Empty);

                // Truncate if too long

                if (newTxt.Length > 21)
                {
                    newTxt.Length = 21;
                    newTxt.Append("...");
                }

                StartGameButton.Text = newTxt.ToString();

            }
            finally { Pool.Free(ref sb); }
        }

        public void UpdateStartGameButtonContextOptions()
        {
            startGameStrip.Items.Clear();

            Icon icon = null;
            for (int i = 0; i < settings.GameExes.Count; i++)
            {
                var exe = settings.GameExes[i];

                var exePath = Path.Combine(settings.BaseGamePath, exe);

                if (!File.Exists(exePath))
                    continue;

                try { icon = Icon.ExtractAssociatedIcon(exePath); }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }

                var image = icon != null ? icon.ToBitmap() : Properties.Resources.CoD1_UO_icon;

                startGameStrip.Items.Add(exe, image).Click += (s, e) =>
                {
                    settings.SelectedExecutable = exe;
                    settings.SelectedFavoriteServer = string.Empty;

                    UpdateStartGameButtonText(exe);

                    if (settings.LaunchWhenSelectedExeChanged)
                        StartGame();
                };

            }

            var favorites = settings?.FavoriteServers;

            if (favorites is null)
                return;

            

            for (int i = 0; i < Math.Min(5, favorites.Count); i++)
            {
                var favorite = favorites[i];

                var displayName = favorite?.Hostname ?? favorite?.IpAndPort ?? string.Empty;

                if (string.IsNullOrWhiteSpace(displayName))
                    continue;

                var desiredExe = favorite.GameType == GameConfig.GameType.CoDMP ? "CoDMP.exe" : favorite.GameType == GameConfig.GameType.CoDUOMP ? "CoDUOMP.exe" : string.Empty;

                if (string.IsNullOrWhiteSpace(desiredExe))
                    continue;

                var exePath = Path.Combine(settings.BaseGamePath, desiredExe);

                if (!File.Exists(exePath))
                    continue;

                try { icon = Icon.ExtractAssociatedIcon(exePath); }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }

                var image = icon != null ? icon.ToBitmap() : Properties.Resources.CoD1_UO_icon;

                startGameStrip.Items.Add(displayName, image).Click += (s, e) =>
                {
                    settings.SelectedExecutable = desiredExe;
                    settings.SelectedFavoriteServer = favorite.IpAndPort;

                    var buttonTxt = favorite.Hostname;

                    UpdateStartGameButtonText(buttonTxt);

                    if (settings.LaunchWhenSelectedExeChanged)
                        StartGame();
                };

            }

            //startGameStrip.Update();
        }

        // Move:
        private void ToggleGameTracking(bool desired)
        {
            GameTimeLabel.Visible = desired;
            CurSessionGT.Visible = desired;
            GameTracker.Enabled = desired;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var watch = Pool.Get<Stopwatch>();

            try
            {
                watch.Restart();

                Text = Application.ProductName;

                if (!string.IsNullOrWhiteSpace(settings?.BaseGamePath))
                    UpdateGameExes(settings.BaseGamePath);


                // Set the initial exe if one was not already selected.
                if (settings?.GameExes != null && settings.GameExes.Count > 0 
                    && (string.IsNullOrWhiteSpace(settings.SelectedExecutablePath) || !File.Exists(settings.SelectedExecutablePath)))
                {
                    settings.SelectedExecutable = settings.GameExes[0];
                }

             
                // Set the button to launch the last selected favorite server, if one was selected.
                // Else, set it to the selected executable.
                if (!string.IsNullOrWhiteSpace(settings?.SelectedFavoriteServer))
                {
                    var hostName = string.Empty;

                    for (int i = 0; i < settings.FavoriteServers.Count; i++)
                    {
                        var favorite = settings.FavoriteServers[i];

                        if (favorite.IpAndPort.Equals(settings.SelectedFavoriteServer, StringComparison.OrdinalIgnoreCase))
                        {
                            hostName = favorite.Hostname;
                            break;
                        }

                    }

                    if (!string.IsNullOrWhiteSpace(hostName))
                        UpdateStartGameButtonText(hostName);

                }
                else
                    UpdateStartGameButtonText(settings?.SelectedExecutable);

                UpdateStartGameButtonContextOptions();

                BitmapHelper.ScalePictureBox(CoDPictureBox, Properties.Resources.CoD1_UO_icon);

                AdminLaunchButton.Visible = false;

               if (!Program.IsElevated)
                  AdminLaunchButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));

                var argsSB = Pool.Get<StringBuilder>();
                try
                {
                    argsSB.Clear();

                    var cmdArgs = Environment.GetCommandLineArgs();
                    for (int i = 0; i < cmdArgs.Length; i++)
                    {
                        var arg = cmdArgs[i];
                        if (arg.Contains(Application.ProductName, StringComparison.OrdinalIgnoreCase) || arg.Contains(Application.StartupPath, StringComparison.OrdinalIgnoreCase)) 
                            continue;

                        if (arg.Equals("-launch", StringComparison.OrdinalIgnoreCase))
                            StartGame();

                        if (arg.Equals("-debug", StringComparison.OrdinalIgnoreCase)) 
                            IsDev = true;

                        if (Program.IsElevated) //ensure elevation before checking these args, otherwise a user could potentially make these forms appear without being elevated & cause an exception
                        {
                            if (arg.Equals("-hotkeys", StringComparison.OrdinalIgnoreCase)) 
                                new HotkeysForm() { Owner = this, AttachToOwner = true }.Show();

                            if (arg.Equals("-cdkeymanager", StringComparison.OrdinalIgnoreCase))
                            {
                                var form = new CDKeyManagerForm { Owner = this, AttachToOwner = true };
                                form.Show();
                                form.BeginInvoke((MethodInvoker)delegate
                                {
                                    form.UnminimizeAndSelect();
                                });
                            }
                        }



                        if (arg.Contains("-fov=", StringComparison.OrdinalIgnoreCase) && decimal.TryParse(arg.Split('=')[1], out decimal FoV))
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


                        if (string.IsNullOrEmpty(settings.BaseGamePath) || !Directory.Exists(settings.BaseGamePath))
                        {
                            var scannedPath = string.Empty;

                            try { scannedPath = PathScanner.ScanForGamePath(); }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                Log.WriteLine(ex.ToString());
                            }

                            if (!string.IsNullOrEmpty(scannedPath))
                            {
                                MessageBox.Show("Automatically detected game path: " + Environment.NewLine + scannedPath, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                settings.BaseGamePath = scannedPath;
                            }
                            else
                            {
                                ipDialog.Description = "Locate your Call of Duty installation directory";

                                if (ipDialog.ShowDialog() == DialogResult.Cancel)
                                {
                                    Application.Exit();
                                    return;
                                }

                                var selectedPath = ipDialog.SelectedPath;
                                settings.BaseGamePath = selectedPath;

                                // Improve this, but it works for now:
                                UpdateGameExes(selectedPath);
                               

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
                else ToggleGameTracking(false);

                TimerEx.Every(3f, MemoryTimer_Tick);

                UpdateButton.Visible = IsDev;
            }
            finally
            {
                watch.Stop();

                var sb = Pool.Get<StringBuilder>();
                try
                {
                    Log.WriteLine(sb
                        .Clear()
                        .Append("Successfully started application in ")
                        .Append(watch.Elapsed.TotalMilliseconds.ToString("0.00")).Replace(".00", string.Empty)
                        .Append("ms, version ")
                        .Append(Application.ProductVersion)
                        .ToString());
                }
                finally { Pool.Free(ref sb); }

            }
        }

        // Move this:

        private void UpdateGameExes(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(basePath));

            if (settings == null)
                throw new Exception("Settings is null");

            var files = Directory.GetFiles(basePath, "*.exe", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var fileName = Path.GetFileName(file);
                var fileNameNoExt = Path.GetFileNameWithoutExtension(file);

                foreach (var codName in PathInfos.GAME_PROCESS_NAMES)
                {
                    if (codName.Equals(fileNameNoExt, StringComparison.OrdinalIgnoreCase) && !settings.GameExes.Contains(fileName))
                    {
                        settings.GameExes.Add(fileName);
                        break;
                    }
                }
            }
        }
     

        public void StartGame(bool forceSelectionDialog = false, string additionalArgs = "")
        {
            var shiftMod = ModifierKeys == Keys.Shift;

            if (string.IsNullOrEmpty(settings.SelectedExecutable) || shiftMod || forceSelectionDialog)
            {
                ipFDialog.InitialDirectory = settings.BaseGamePath;
                ipFDialog.DefaultExt = ".exe";
                ipFDialog.Filter = "|*.exe";

                if (!shiftMod && !forceSelectionDialog) 
                    MessageBox.Show("Please select the exe to launch (this will be saved)", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (ipFDialog.ShowDialog() != DialogResult.OK)
                    return;

                if (!ipFDialog.FileName.EndsWith(".exe"))
                {
                    MessageBox.Show("Selected file is not an executable.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var filePath = Path.GetDirectoryName(ipFDialog.FileName);

                settings.BaseGamePath = filePath;
                settings.SelectedExecutable = ipFDialog.SafeFileName;

                UpdateStartGameButtonText(settings.SelectedExecutable);

                MessageBox.Show("Selected: " + settings.SelectedExecutable + Environment.NewLine + "You can change this again at any time.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            settings.CommandLine = LaunchParametersTB.Text;

            Task.Run(() =>
            {
                var forceStartArgs = string.Empty;

                var sb = Pool.Get<StringBuilder>();

                try
                {
                    if (checkBoxDesktopRes.Checked)
                        sb
                           .Clear()
                           .Append("+set r_mode -1 +set r_customwidth ")
                           .Append(DesktopWidth)
                           .Append(" +set r_customheight ")
                           .Append(DesktopHeight);


                    if (!string.IsNullOrWhiteSpace(settings.SelectedFavoriteServer))
                        sb.Append(" +connect ").Append(settings.SelectedFavoriteServer);

                    if (!string.IsNullOrWhiteSpace(additionalArgs))
                        sb.Append(" ").Append(additionalArgs);

                    forceStartArgs = sb.ToString();

                }
                finally { Pool.Free(ref sb); }

               
                var startGameLog = "Calling " + nameof(GameStarter.StartGame) + " with args: " + settings.SelectedExecutablePath + Environment.NewLine + LaunchParametersTB.Text + Environment.NewLine + forceStartArgs;

                Log.WriteLine(startGameLog);
                Console.WriteLine(startGameLog);

                GameStarter.StartGame(settings.SelectedExecutablePath, LaunchParametersTB.Text, forceStartArgs);
            });

           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (LaunchParametersTB.Text != settings.CommandLine) settings.CommandLine = LaunchParametersTB.Text;

                if (settings.HasChanged)
                {
                    Log.WriteLine(nameof(settings.HasChanged) + ", so writing to disk");

                    Settings.SaveInstanceToDisk();

                    Log.WriteLine("Wrote settings to disk");
                }
                else
                    Log.WriteLine("Settings did not change, no need to save to disk.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }
           

            try { CodMapImage.SaveNoImgCacheToDisk(); }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
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
            Task.Run(() =>
            {
                var now = DateTime.UtcNow;

                //we can be super responsive when someone is tapping +- while not accidentally moving it up or down twice by doing manual checks while having the timer at ~5-10ms
                if (_lastHotkey == null) _lastHotkey = now;
                else if ((now - _lastHotkey).TotalMilliseconds < 100) return;

                TryParseKeys(settings.HotKeyModifier, out var modifier);

                TryParseKeys(settings.HotKeyUp, out var up);
                TryParseKeys(settings.HotKeyDown, out var down);

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
            });
        }

        #region Util

        public bool IsUOMemory()
        {
            if (MemorySelection?.ProcMemory?.Process == null || !ProcessExtension.IsProcessAlive(MemorySelection.ProcMemory.ProcessPID))
                return false;
            
            var gameType = GameConfig.GetGameTypeFromProcess(MemorySelection?.ProcMemory?.Process);

            return gameType == GameConfig.GameType.CoDUOMP || gameType == GameConfig.GameType.CoDUOSP;
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

                var sb = Pool.Get<StringBuilder>();

                try
                {
                    using (var wc = new WebClient())
                    {
                        wc.Headers.Add("user-agent", sb.Clear().Append("CoDUO FoV Changer/").Append(Application.ProductVersion).ToString());
                        using (var reader = new StreamReader(wc.OpenRead(UPDATE_URI)))
                        {
                            try { version = reader?.ReadToEnd() ?? string.Empty; }
                            finally { reader.Close(); }
                        }
                    }
                }
                finally { Pool.Free(ref sb); }

                if (!decimal.TryParse(version, out decimal hfDec))
                {
                    Log.WriteLine("Failed to parse: " + version + " (version) as decimal.");
                    return !version.Contains(HotfixVersion.HOTFIX.ToString());
                }

                return hfDec > HotfixVersion.HOTFIX;
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

        private bool TryParseKeys(string text, out Keys value) => Enum.TryParse(text, out value);

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


                if (mode <= -1)
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


                var maxFoV = (mode != -1 || (ratio < 1.7 && mode == -1)) ? 105 : 120; //"120" is not real 120! remember: the screen is literally stretched in wide screen. this is equivalent to ~107 'real' FoV in a game; still lower than a common max value of (real) 120.

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

                if (MemorySelection?.ProcMemory?.RequiresElevation() ?? false)
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
            if (IsCheckingForUpdates)
                return;

            var now = DateTime.UtcNow;

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

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e) => ShowSettingsForm();
        
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
                if (spanCurrent.TotalSeconds > 0 && totalMinutesCur < 1) CurSessionGT.Text = "Session Time: " + spanCurrent.TotalSeconds.ToString("N0") + " seconds";
                if (totalMinutesCur >= 1 && totalHoursCur < 1) CurSessionGT.Text = "Session Time: " + totalMinutesCur.ToString("N0") + " minutes";
                if (totalHoursCur >= 1) CurSessionGT.Text = "Session Time: " + totalHoursCur.ToString("N0") + " hours";
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

        private void GameTimeLabelTimer_Tick(object sender, EventArgs e)
        {
            if (!settings.TrackGameTime)
            {
                ToggleGameTracking(false);
                return;
            }

            AccessGameTimeLabel();
        }

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

                    try
                    {
                        if (File.Exists(tempFoVPath))
                            File.Delete(tempFoVPath);
                    }
                    catch(Exception ex)
                    {
                        Log.WriteLine($"Failed to delete {nameof(tempFoVPath)}: {tempFoVPath}{Environment.NewLine}{ex}");
                    }

               

                    using (var wc = new WebClient())
                    {
                        wc.Headers.Add("user-agent", sb.Clear().Append("CoDUO FoV Changer/").Append(Application.ProductVersion).ToString());
                        wc.DownloadFile(LATEST_DOWNLOAD_URI, tempFoVPath);
                    }

                    var path = sb.Clear().Append(PathInfos.Temp).Append(@"\Mover.exe").ToString();


                    try
                    {
                        if (File.Exists(path))
                            File.Delete(path);
                    }
                    catch(Exception ex)
                    {
                        Log.WriteLine($"Failed to delete {nameof(path)}: {path}{Environment.NewLine}{ex}");
                    }

                  

                    File.WriteAllBytes(path, Properties.Resources.Mover);
                    Log.WriteLine($"Created mover at: {path}");


                    var updaterInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = Application.StartupPath,
                        FileName = path,
                        Arguments = sb.Clear()
                        .Append("-movefrom=\"")
                        .Append(tempFoVPath)
                        .Append("\" -moveto=\"")
                        .Append(fileNameDir)
                        .Append("\" -wait=1000 -autostart -waitstart=1000 -exitwait=1000 -waitforpid=")
                        .Append(currentProc.Id)
                        .ToString()
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
                    if (!memory?.IsRunning() ?? false) 
                        GamePIDBox.Items.Remove(GamePIDBox.Items[i]);
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

            BitmapHelper.ScalePictureBox(CoDPictureBox, 
                (ProcessExtension.IsAnyProcessRunning("CoDUOMP") || IsUOMemory())
                ? Properties.Resources.CoDUO 
                : ProcessExtension.IsAnyProcessRunning("CoDMP") 
                ? Properties.Resources.CoD1 
                : Properties.Resources.CoD1_UO_icon);
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(GITHUB_RELEASES_URI);

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ins = GetInstance<AboutForm>();

            if (ins != null) ins.UnminimizeAndSelect();
            else new AboutForm() { Owner = this, AttachToOwner = true }.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) => UnminimizeFromTray();
        
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) => Application.Exit();

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e) => ShowSettingsForm();
        

        private void GamePIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemorySelection = GamePIDBox.GetMemoryFromIndex(GamePIDBox.SelectedIndex);

            BitmapHelper.ScalePictureBox(CoDPictureBox, (IsUOMemory() || (ProcessExtension.IsAnyProcessRunning("CoDUOMP") && !ProcessExtension.IsAnyProcessRunning("mohaa") && !ProcessExtension.IsAnyProcessRunning("codmp"))) ? Properties.Resources.CoDUO : Properties.Resources.CoD1);
        }

        private void AdminLaunchButton_Click(object sender, EventArgs e) => TryRestartAsAdmin();

        private void ShowSettingsForm()
        {
            var ins = GetInstance<SettingsForm>();
            if (ins != null) ins.UnminimizeAndSelect();
            else new SettingsForm() { Owner = this, AttachToOwner = true }.Show();
        }

        private bool TryRestartAsAdmin()
        {
            if (Program.IsElevated)
            {
                var msg = $"{nameof(TryRestartAsAdmin)} called but {nameof(Program.IsElevated)} is true.";
                Console.WriteLine(msg);
                Log.WriteLine(msg);
                return false;
            }

            var fileNameDir = Process.GetCurrentProcess()?.MainModule?.FileName ?? string.Empty;

            if (string.IsNullOrWhiteSpace(fileNameDir) || !File.Exists(fileNameDir)) return false;
            else
            {

                //The -multi argument must be passed or else this will fail to open a second instance of the app
                //(if not passed it will instead try to select the existing app; an intentional feature).
                var startInfo = new ProcessStartInfo
                {
                    Verb = "runas",
                    Arguments = "-multi " + string.Join(" ", Environment.GetCommandLineArgs()),
                    WorkingDirectory = Application.StartupPath,
                    FileName = fileNameDir
                };

                try 
                { 
                    Process.Start(startInfo);
                    Close();
                }
                catch (System.ComponentModel.Win32Exception win32ex) when (win32ex.NativeErrorCode == 1223)
                {
                    Log.WriteLine("User canceled UAC prompt (" + win32ex.Message + " )");
                    return false;
                }

            }

            return true;
        }

        
        private void MinimizeIcon_BalloonTipClicked(object sender, EventArgs e) => UnminimizeFromTray();

        private void singleplayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ins = GetInstance<SingleplayerForm>();

            if (ins == null) new SingleplayerForm() { Owner = this, AttachToOwner = true }.Show();
            else ins.UnminimizeAndSelect();
        }

        // Used to ensure that only left clicks result in the game starting/click event.
        // Right clicks will open the strip attached to the button.
        private void StartGameButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                StartGame();
            else if (e.Button == MouseButtons.Right)
                StartGameButton.ContextMenuStrip.Show(Cursor.Position);
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ins = GetInstance<ServersForm>();

            if (ins == null) new ServersForm() { Owner = this, AttachToOwner = true }.Show();
            else ins.UnminimizeAndSelect();
        }
    }
}