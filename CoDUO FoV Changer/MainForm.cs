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

namespace CoDUO_FoV_Changer
{
    public partial class MainForm : Form
    {
        public const decimal HOTFIX = 7.6M;

        public static bool IsDev { get; private set; } = Debugger.IsAttached;

        private readonly Settings settings = Settings.Instance;

        public SessionHandler CurrentSession { get; private set; } = new SessionHandler();

        public Memory MemorySelection { get; private set; }


        private TimeSpan _lastGameTimeSpan;
        private DateTime _lastHotkey;
        private DateTime _lastUpdateCheck;
        private string _gameVersion = string.Empty;

        private bool _needsUpdate = false;

        public static MainForm Instance = null;

        private const string LATEST_DOWNLOAD_URI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases/latest/download/CoDUO.FoV.Changer.exe";
        private const string UPDATE_URI = @"https://raw.githubusercontent.com/Shady7557/CoDUO-FoV-Changer/master/HOTFIX";

        public float CurrentFoV
        {
            get;
            private set;
        }

        public string GameVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_gameVersion))
                {
                    _gameVersion = Registry.GetValue(PathScanner.RegistryPath, "Version", string.Empty)?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(_gameVersion)) _gameVersion = Registry.GetValue(PathScanner.RegistryPathVirtualStore, "Version", string.Empty)?.ToString() ?? string.Empty;
                }
                return _gameVersion;
            }
        }

        public bool IsCheckingForUpdates { get; private set; } = false;

        public MainForm()
        {
            Instance = this;
            InitializeComponent();
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


        private void MainForm_Load(object sender, EventArgs e)
        {
          
            var watch = Pool.Get<Stopwatch>();
          
            try 
            {
                watch.Restart();

                AdminLaunchButton.Visible = false;
                if (!Program.IsElevated) AdminLaunchButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));


                DvarsCheckBox.Visible = Debugger.IsAttached;

                var argsSB = Pool.Get<StringBuilder>();
                try
                {
                    argsSB.Clear();

                    var cmdArgs = Environment.GetCommandLineArgs();
                    for (int i = 0; i < cmdArgs.Length; i++)
                    {
                        var arg = cmdArgs[i];
                        if (arg.IndexOf(Application.ProductName, StringComparison.OrdinalIgnoreCase) >= 0 || arg.IndexOf(Application.StartupPath, StringComparison.OrdinalIgnoreCase) >= 0) continue;
                        if (arg.Equals("-unlock", StringComparison.OrdinalIgnoreCase)) DvarsCheckBox.Visible = true;
                        if (arg.Equals("-unlock=1", StringComparison.OrdinalIgnoreCase))
                        {
                            DvarsCheckBox.Visible = true;
                            DvarsCheckBox.Checked = true;
                        }

                        if (arg.Equals("-fog=1", StringComparison.OrdinalIgnoreCase)) settings.Fog = true;
                        else if (arg.Equals("-fog=0", StringComparison.OrdinalIgnoreCase)) settings.Fog = false;

                        if (arg.Equals("-launch", StringComparison.OrdinalIgnoreCase)) StartGameButton.PerformClick();

                        if (arg.Equals("-debug", StringComparison.OrdinalIgnoreCase)) IsDev = true;

                        if (Program.IsElevated) //ensure elevation before checking these args, otherwise a user could potentially make these forms appear without being elevated & cause an exception
                        {
                            if (arg.Equals("-hotkeys", StringComparison.OrdinalIgnoreCase)) new Hotkeys().Show();

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
                    Log.WriteLine(argsSB.Clear().Append("Launched program with args: ").Append(argStr).ToString());
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
                FogCheckBox.Checked = settings.Fog;
                fogToolStripMenuItem.Checked = settings.Fog;
                LaunchParametersTB.Text = settings.CommandLine;

                if (settings.TrackGameTime) AccessGameTimeLabel();
                else
                {
                    GameTimeLabel.Visible = false;
                    CurSessionGT.Visible = false;
                    GameTracker.Enabled = false;
                }

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

                var startInfo = new ProcessStartInfo
                {
                    Arguments = !string.IsNullOrEmpty(LaunchParametersTB.Text) ? LaunchParametersTB.Text : string.Empty,
                    FileName = settings.InstallPathExe,
                    WorkingDirectory = settings.InstallPath
                };

                Process.Start(startInfo);
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

        private void FogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MemorySelection != null && MemorySelection.IsRunning() && !IsUO())
            {
                FogCheckBox.Checked = true;
                return;
            }

            settings.Fog = FogCheckBox.Checked;
            fogToolStripMenuItem.Checked = settings.Fog;

            Task.Run(() => ToggleFog(settings.Fog));
        }

        private void MinimizeCheckBox_CheckedChanged(object sender, EventArgs e) => settings.MinimizeToTray = MinimizeCheckBox.Checked;

        private void MainForm_Resize(object sender, EventArgs e)
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
            Task.Run(() =>
            {
                var wantedButtonState = false;

                if (MemorySelection == null || !MemorySelection.IsRunning())
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
                    wantedButtonState = !Program.IsElevated && (MemorySelection?.ProcMemory?.RequiresElevation() ?? false);

                    DoRAChecks();
                    DoFoV();
                    ToggleFog(settings.Fog);
                    DoDvars();
                }

                if (AdminLaunchButton.Visible != wantedButtonState)
                {
                    AdminLaunchButton.BeginInvoke((MethodInvoker)delegate { AdminLaunchButton.Visible = wantedButtonState; });
                }
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
            if (HotkeyHandler.IsKeyPushedDown(fogModifier) && HotkeyHandler.IsKeyPushedDown(toggleFog))
            {
                FogCheckBox.Checked = !FogCheckBox.Checked;
                _lastHotkey = now;
            }
        }


        private void DvarsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MemorySelection != null && MemorySelection.IsRunning() && !IsUO())
            {
                DvarsCheckBox.Checked = false;
                return;
            } //no support for dvar unlocking (yet) in cod1
            Task.Run(() => DoDvars());
        }
        #region Util

        public bool IsUO()
        {
            if (MemorySelection == null || !MemorySelection.IsRunning()) return false; //I don't get why a null operator doesn't work here, but it doesn't. so we have this full 'check' here instead

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
                if (proc == null) continue;

                if (proc.ProcessName.Equals("CoDUOMP", StringComparison.OrdinalIgnoreCase) || proc.ProcessName.Equals("CoDMP", StringComparison.OrdinalIgnoreCase) || proc.ProcessName.Equals("mohaa", StringComparison.OrdinalIgnoreCase)) list.Add(proc);
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
        private void ToggleFog(bool val)
        {
            try
            {
                if (MemorySelection == null || !MemorySelection.IsRunning() || MemorySelection.ProcMemory.RequiresElevation()) return;
                var value = val ? 1 : 0; // Convert.ToInt32(val);

                var fogValue = MemorySelection.ReadIntAddress(MemoryAddresses.UO_FOG_POINTER_ADDRESS, 0x20);
                if (fogValue != value) MemorySelection.ProcMemory.WriteInt(MemorySelection.GetIntPointerAddress(MemoryAddresses.UO_FOG_POINTER_ADDRESS, 0x20), value);
            }
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to read/write fog values:" + Environment.NewLine + ex.ToString()); }
        }

        private void DoRAChecks() //this is where we do aspect ratio checks to ensure we limit FoV at non-widescreen values to reduce potential for any cheating
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


                var maxFoV = (mode != -1 || (ratio < 1.7 && mode == -1)) ? 105 : 130;

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
                if (isRunning && (MemorySelection?.ProcMemory?.RequiresElevation() ?? false))
                {
                    SetLabelText(StatusLabel, "Status: game requires elevation!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process requires elevation!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                    return;
                }

                var address = !isRunning ? -1 : !IsUO() ? MemoryAddresses.COD_FOV_ADDRESS : (isRunning ? (MemorySelection.ProcMemory.DllImageAddress(MemoryAddresses.UO_CGAME_MP_DLL) + MemoryAddresses.UO_FOV_OFFSET) : -1);
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

        private void DoDvars()
        {
            if (MemorySelection == null || !MemorySelection.IsRunning() || MemorySelection.ProcMemory.RequiresElevation()) return;
            try
            {
                var val = DvarsCheckBox.Checked ? 235 : 116;

                var curVal = MemorySelection.ProcMemory.ReadByte(MemoryAddresses.UO_DVAR_ADDRESS_1);
                if (curVal == val) return; //don't write if it's already the value we want

                MemorySelection.ProcMemory.WriteInt(MemoryAddresses.UO_DVAR_ADDRESS_1, val, 1);
                MemorySelection.ProcMemory.WriteInt(MemoryAddresses.UO_DVAR_ADDRESS_2, val, 1);
                MemorySelection.ProcMemory.WriteInt(MemoryAddresses.UO_DVAR_ADDRESS_3, val, 1);
            }
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to read/write Dvar addresses:" + Environment.NewLine + ex.ToString()); }
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
                if (settings.GameTime >= (int.MaxValue - 1))
                {
                    GameTimeLabel.Text = "Game Time: >= 68 Years";
                    settings.GameTime--;
                }
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

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CheckUpdatesLabel.Text = "Checking for updates...";
            StartUpdateChecking();
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
                        wc.Headers.Add("user-agent", "CoDUO FoV Changer/" + Application.ProductVersion);
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
                var selectedIndex = GamePIDBox.SelectedIndex;

                GamePIDBox.BeginUpdate();

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
                        if (proc?.Id == 0) continue;

                        var hasPid = false;
                        for (int j = 0; j < GamePIDBox.Items.Count; j++)
                        {
                            if (GamePIDBox?.GetMemoryFromIndex(j) != null)
                            {
                                hasPid = true;
                                break;
                            }
                        }

                        if (!hasPid) GamePIDBox.AddProcessMemory(new Memory(proc.Id));
                    }
                }
                finally { Pool.FreeList(ref allProcs); }


                GamePIDBox.Visible = GamePIDBox.Items.Count > 0;

                if (GamePIDBox.SelectedItem == null && GamePIDBox.Items.Count > 0) GamePIDBox.SelectedIndex = ClampEx.Clamp(selectedIndex - 1, 0, GamePIDBox.Items.Count);

                GamePIDBox.EndUpdate();
            }
            catch (Exception ex)
            {
                Log.WriteLine("An error happened while trying to get running Call of Duty/UO processes:" + Environment.NewLine + ex.ToString());
                MessageBox.Show("An error happend while trying to get running Call of Duty/UO processes: " + ex.Message + Environment.NewLine + "Please refer to the log for more info.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProccessChecker_Tick(object sender, EventArgs e)
        {
            UpdateProcessBox();
            if (MemorySelection != null && MemorySelection.IsRunning()) BitmapHelper.ScalePictureBox(CoDPictureBox, IsUO() ? Properties.Resources.CoDUO : Properties.Resources.CoD1);
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/Shady7557/CoDUO-FoV-Changer/releases");
            /*/
            if (ChangelogForm.Instance != null && !ChangelogForm.Instance.IsDisposed)
            {
                ChangelogForm.Instance.Show();
                ChangelogForm.Instance.BringToFront();
            }
            else new ChangelogForm().Show();/*/
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Created with love by Shady" + Environment.NewLine + Environment.NewLine + "This program is intended to allow you to change the Field of View in Multiplayer for both Call of Duty and Call of Duty: United Offensive, both of which do not normally allow you to do so." + Environment.NewLine + Environment.NewLine + "Program version: " + ProductVersion + Environment.NewLine + "Game version: " + (!string.IsNullOrEmpty(GameVersion) ? GameVersion : "Unknown"), ProductName + " (" + ProductVersion + ")", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void fogToolStripMenuItem_Click(object sender, EventArgs e) => fogToolStripMenuItem.Checked = !fogToolStripMenuItem.Checked; //idk why this is needed but it seems like it is??

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SettingsForm.Instance != null && !SettingsForm.Instance.Disposing && !SettingsForm.Instance.IsDisposed) SettingsForm.Instance.Show();
            else new SettingsForm().Show();
        }

        private void GamePIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemorySelection = GamePIDBox.GetMemoryFromIndex(GamePIDBox.SelectedIndex);

            if (MemorySelection != null && MemorySelection.IsRunning()) BitmapHelper.ScalePictureBox(CoDPictureBox, IsUO() ? Properties.Resources.CoDUO : Properties.Resources.CoD1);
        }

        private void GamePIDBox_VisibleChanged(object sender, EventArgs e) => CoDPictureBox.Visible = GamePIDBox.Visible;

        private void AdminLaunchButton_Click(object sender, EventArgs e)
        {
            if (Program.IsElevated)
            {
                MessageBox.Show("Program is already running as an administrator!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fileNameDir = Process.GetCurrentProcess()?.MainModule?.FileName ?? string.Empty;

            if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
            {
                MessageBox.Show("Application path doesn't exist. Cannot start: " + fileNameDir + Environment.NewLine + " Please manually run the program as an Admin if you wish to change your hotkeys.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                var startInfo = new ProcessStartInfo
                {
                    Verb = "runas",
                    Arguments = string.Join(" ", Environment.GetCommandLineArgs()),
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
                    return;
                }
            }
        }


    }
}