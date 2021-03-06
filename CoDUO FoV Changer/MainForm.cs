﻿using System;
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

namespace CoDUO_FoV_Changer
{
    public partial class MainForm : Form
    {
        public const decimal hotfix = 7.6M;
        public static bool isDev = Debugger.IsAttached;
        private readonly Settings settings = Settings.Instance;
        private readonly Image CoDImage = Properties.Resources.CoD1;
        private readonly Image CoDUOImage = Properties.Resources.CoDUO;
        private const string LatestDownloadURI = @"https://github.com/Shady7557/CoDUO-FoV-Changer/releases/latest/download/CoDUO.FoV.Changer.exe";
        public const string cgameDll = "uo_cgame_mp_x86.dll";
        private readonly SessionHandler currentSession = new SessionHandler();
        private Memory memory;
        private TimeSpan _lastGameTimeSpan;
        private DateTime lastHotkey;
        private DateTime lastUpdateCheck;
        private string _gameVersion = string.Empty;
       
        private bool needsUpdate = false;
        private string selectedProcStr = string.Empty;
        public static MainForm Instance = null;
      

        public string GameVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_gameVersion)) _gameVersion = Registry.GetValue(PathScanner.RegistryPath, "Version", string.Empty)?.ToString() ?? string.Empty;
                return _gameVersion;
            }
        }

        public bool IsCheckingForUpdates { get; set; } = false;

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
            var watch = Stopwatch.StartNew();
            CheckForIllegalCrossThreadCalls = true;
            AdminLaunchButton.Visible = false;
            if (!Program.IsElevated) AdminLaunchButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));


            DvarsCheckBox.Visible = false;

            var argsSB = new StringBuilder();
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

                if (arg.Equals("-debug", StringComparison.OrdinalIgnoreCase)) isDev = true;

                if (arg.Equals("-hotkeys", StringComparison.OrdinalIgnoreCase)) new Hotkeys().Show();

                if (arg.IndexOf("-fov=", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (decimal.TryParse(arg.Split('=')[1], out decimal FoV)) SetFoVNumeric(FoV);
                }
                argsSB.Append(arg + " ");
            }


            var args = argsSB.ToString().TrimEnd();
            if (!string.IsNullOrEmpty(args)) Log.WriteLine("Launched program with args: " + args);

            StartUpdates();

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
            memory = GetProcessMemoryFromBox();
            AdminLaunchButton.Visible = !Program.IsElevated && (memory?.ProcMemory?.RequiresElevation() ?? false);

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
            if (isDev) UpdateButton.Visible = true;
            
            Log.WriteLine("Successfully started application, version " + Application.ProductVersion);
            watch.Stop();
            var timeTaken = watch.Elapsed;
            Console.WriteLine("Form load took: " + timeTaken.TotalMilliseconds + "ms");
            if (timeTaken.TotalMilliseconds > 100) Log.WriteLine("Startup took: " + timeTaken.TotalMilliseconds + "ms (this is too long!)");
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
                Log.WriteLine("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            var shiftMod = ModifierKeys == Keys.Shift;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LaunchParametersTB.Text != settings.CommandLine) settings.CommandLine = LaunchParametersTB.Text;
            if (settings.HasChanged) DatabaseFile.Write(settings, PathInfos.SettingsPath);
        }

        private void FoVNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (FoVNumeric.Value != ((int)FoVNumeric.Value)) FoVNumeric.DecimalPlaces = 1;
            else FoVNumeric.DecimalPlaces = 0;
            Task.Run(() =>
            {
                settings.FoV = FoVNumeric.Value;
                doFoV();
            });
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

                memory = GetProcessMemoryFromBox();
                if (memory == null || !memory.IsRunning())
                {
                    SetLabelText(StatusLabel, "Status: not found or failed to write to memory!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process not found or failed to write to memory!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                }
                else
                {
                    wantedButtonState = !Program.IsElevated && (memory?.ProcMemory?.RequiresElevation() ?? false);

                    if (!IsUO())
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            DvarsCheckBox.Checked = false;
                            FogCheckBox.Checked = false;
                        });
                    }

                    doRAChecks();
                    doFoV();
                    ToggleFog(settings.Fog);
                    doDvars();
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

            if (HotkeyHandler.IsKeyPushedDown(modifier))
            {
                if (HotkeyHandler.IsKeyPushedDown(up))
                {
                    SetFoVNumeric(FoVNumeric.Value + 1);
                    lastHotkey = now;
                }
                if (HotkeyHandler.IsKeyPushedDown(down))
                {
                    SetFoVNumeric(FoVNumeric.Value - 1);
                    lastHotkey = now;
                }
            }
            if (HotkeyHandler.IsKeyPushedDown(fogModifier) && HotkeyHandler.IsKeyPushedDown(toggleFog))
            {
                FogCheckBox.Checked = !FogCheckBox.Checked;
                lastHotkey = now;
            }
        }


        private void DvarsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (memory != null && memory.IsRunning() && !IsUO())
            {
                DvarsCheckBox.Checked = false;
                return;
            } //no support for dvar unlocking (yet) in cod1
            Task.Run(() => doDvars());
        }
        #region Util

        public bool IsUO() { return (memory == null || !memory.IsRunning()) ? false : (memory.ProcMemory?.DllImageAddress(cgameDll) ?? 0) != 0 || (memory.ProcMemory?.DllImageAddress("uo_ui_mp_x86.dll") ?? 0) != 0; }
        

        private List<Process> GetAllGameProcesses()
        {
            var allProcs = Process.GetProcesses();
            var procs = new List<Process>();
            for(int i = 0; i < allProcs.Length; i++)
            {
                var proc = allProcs[i];
                if (proc?.ProcessName == "CoDUOMP" || proc?.ProcessName == "CoDMP" || proc?.ProcessName == "mohaa") procs.Add(proc);
            }
            return procs;
        }
        private void StartUpdates()
        {
            Task.Run(() =>
            {
                needsUpdate = CheckUpdates();
                if (UpdateButton.InvokeRequired) UpdateButton.BeginInvoke((MethodInvoker)delegate { UpdateButton.Visible = needsUpdate; });
                else UpdateButton.Visible = needsUpdate;
                SetLabelText(CheckUpdatesLabel, needsUpdate ? "Updates available!" : "No updates found. Click to check again.");
            });
        }

        private bool CheckUpdates()
        {
            try
            {
                var response = WebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")?.GetResponse() ?? null;
                var version = new StreamReader(response?.GetResponseStream() ?? null)?.ReadToEnd() ?? string.Empty;
                if (!decimal.TryParse(version, out decimal hfDec))
                {
                    Log.WriteLine("Failed to parse: " + version + " (version) as decimal.");
                    return !version.Contains(hotfix.ToString());
                }
                return hfDec > hotfix;
            }
            catch (Exception ex)
            {
                Log.WriteLine("Unable to check for updates: " + ex.Message + Environment.NewLine + ex.ToString());
                return false;
            }
            finally { IsCheckingForUpdates = false; }
        }


      
        public void SetLabelText(Label label, string text)
        {
            if (label == null) return;
            if (label.InvokeRequired) label.BeginInvoke((MethodInvoker)delegate () { label.Text = text; });
            else label.Text = text;
        }

        public bool TryParseKeys(string text, ref Keys value)
        {
            if (Enum.TryParse(text, out Keys tmp))
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

        private Memory GetProcessMemoryFromBox()
        {
            try
            {
                if (string.IsNullOrEmpty(selectedProcStr)) return null;
                if (!int.TryParse(selectedProcStr.Split('(')[1].Replace(")", string.Empty), out int pid)) return null;
                return new Memory(pid);
            }
            catch (Exception ex) { Log.WriteLine(ex.ToString()); }
            return null;
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
                if (memory == null || !memory.IsRunning() || memory.ProcMemory.RequiresElevation()) return;
                var value = Convert.ToInt32(val);
                var newmpAddr = memory.GetIntPointerAddress(0x489A0D4, 0x20);
                var fogValue = memory.ReadIntAddress(0x489A0D4, 0x20);
                if (fogValue != value) memory.ProcMemory.WriteInt(newmpAddr, value);
            }
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to read/write fog values:" + Environment.NewLine + ex.ToString()); }
        }

        private void doRAChecks()
        {
            if (memory == null || memory.IsRunning() || memory.ProcMemory.RequiresElevation()) return;
            try
            {
                var mode = memory.ReadIntAddress(0x4899D50, 0x20);
                var width = memory.ReadIntAddress(0x4899D30, 0x20);
                var height = memory.ReadIntAddress(0x4899FCC, 0x20);
                if (width == 0 || height == 0)
                {
                    Log.WriteLine("Got bad width/height: " + width + ", " + height);
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
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to get current game resolution:" + Environment.NewLine + ex.ToString()); }
        }

        private void doFoV()
        {
            try
            {
                var isRunning = memory?.IsRunning() ?? false;
                if (isRunning && (memory?.ProcMemory?.RequiresElevation() ?? false))
                {
                    SetLabelText(StatusLabel, "Status: game requires elevation!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process requires elevation!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                    return;
                }

                var address = !isRunning ? -1 : (!IsUO()) ? 0x3029CA28 : (isRunning ? (memory.ProcMemory.DllImageAddress(cgameDll) + 0x52F7C8) : -1);
                if (!isRunning || address <= 0)
                {
                    SetLabelText(StatusLabel, "Status: not found or failed to write to memory!");
                    StatusLabel.BeginInvoke((MethodInvoker)delegate
                    {
                        toolTip1.SetToolTip(StatusLabel, "Process not found or failed to write to memory!");
                        StatusLabel.ForeColor = Color.Orange;
                    });
                }
                else
                {
                    memory.ProcMemory.WriteFloat(address, Convert.ToSingle(FoVNumeric.Value));
                    SetLabelText(StatusLabel, "Status: game found and wrote to memory!");
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

        private void doDvars()
        {
            if (memory == null || !memory.IsRunning() || memory.ProcMemory.RequiresElevation()) return;
            try
            {
                var val = DvarsCheckBox.Checked ? 235 : 116;
                var curVal = memory.ProcMemory.ReadByte(0x43DD86);
                if (curVal == val) return; //don't write if it's already the value we want
                memory.ProcMemory.WriteInt(0x43DD86, val, 1);
                memory.ProcMemory.WriteInt(0x43DDA3, val, 1);
                memory.ProcMemory.WriteInt(0x43DDC1, val, 1);
            }
            catch (Exception ex) { Log.WriteLine("An exception happened while trying to read/write Dvar addresses:" + Environment.NewLine + ex.ToString()); }
        }
        #endregion

        private void CheckUpdatesLabel_Click(object sender, EventArgs e)
        {
            if (IsCheckingForUpdates) return;
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
            IsCheckingForUpdates = true;
            StartUpdates();
            lastUpdateCheck = now;
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
                var spanCurrent = currentSession.GetSessionTime();
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
                var span = currentSession.GetSessionTime();

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
            StartUpdates();
        }

        private void StartUpdater()
        {
            try
            {
                var tempFoVPath = PathInfos.Temp + "coduofovchanger_temp.exe";
                var currentProc = Process.GetCurrentProcess();
                var fileNameDir = currentProc?.MainModule?.FileName ?? string.Empty;
                if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
                {
                    MessageBox.Show("Application path doesn't exist. Cannot update: " + fileNameDir, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (File.Exists(tempFoVPath)) File.Delete(tempFoVPath);
                
                var wc = new WebClient();
                wc.Headers.Add("user-agent", "CoDUO FoV Changer/" + Application.ProductVersion);
                wc.DownloadFile(LatestDownloadURI, tempFoVPath);


                var path = PathInfos.Temp + "Mover.exe";
                if (File.Exists(path)) File.Delete(path);
                if (!File.Exists(path))
                {
                    File.WriteAllBytes(path, Properties.Resources.Mover);
                    Log.WriteLine("Created mover at: " + path);
                }
               

                var updaterInfo = new ProcessStartInfo();
                updaterInfo.WorkingDirectory = Application.StartupPath;
                updaterInfo.FileName = path;
                updaterInfo.Arguments = "-movefrom=\"" + tempFoVPath + "\" -moveto=\"" + fileNameDir + "\" -wait=1000 -autostart -waitstart=1000 -exitwait=1000 -waitforpid=" + currentProc.Id;
                Process.Start(updaterInfo);
                Log.WriteLine("Started mover with args: " + updaterInfo.Arguments + ", now shutting down");
                if (InvokeRequired) BeginInvoke((MethodInvoker)delegate { Close(); });
                else Close();
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
                    var boxItem = GamePIDBox.Items[i];
                    var splitPid = (boxItem as string).Split('(')[1].Replace(")", string.Empty);
                    if (!int.TryParse(splitPid, out int pid)) continue;
                    if (!ProcessExtensions.ProcessExtension.IsProcessAlive(pid)) GamePIDBox.Items.Remove(boxItem);
                }
                var allProcs = GetAllGameProcesses();
                for (int i = 0; i < allProcs.Count; i++)
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
            if (memory != null && memory.IsRunning()) BitmapHelper.ScalePictureBox(CoDPictureBox, IsUO() ? CoDUOImage : CoDImage);
        }

        private void ChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChangelogForm.Instance != null && !ChangelogForm.Instance.IsDisposed)
            {
                ChangelogForm.Instance.Show();
                ChangelogForm.Instance.BringToFront();
            }
            else new ChangelogForm().Show();
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Created by Shady" + Environment.NewLine + Environment.NewLine + "This program is intended to allow you to change the Field of View in Multiplayer for both Call of Duty and Call of Duty: United Offensive, both of which do not normally allow you to do so." + Environment.NewLine + Environment.NewLine + "Program version: " + ProductVersion + Environment.NewLine + "Game version: " + (!string.IsNullOrEmpty(GameVersion) ? GameVersion : "Unknown"), ProductName + " (" + ProductVersion + ")", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SettingsForm.Instance != null && !SettingsForm.Instance.Disposing && !SettingsForm.Instance.IsDisposed) SettingsForm.Instance.Show();
            else new SettingsForm().Show();
        }

        private void GamePIDBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProcStr = GamePIDBox?.SelectedItem?.ToString() ?? string.Empty;
            memory = GetProcessMemoryFromBox();
            if (memory != null && memory.IsRunning()) BitmapHelper.ScalePictureBox(CoDPictureBox, IsUO() ? CoDUOImage : CoDImage);
        }

        private void GamePIDBox_VisibleChanged(object sender, EventArgs e) => CoDPictureBox.Visible = GamePIDBox.Visible;

        private void AdminLaunchButton_Click(object sender, EventArgs e)
        {
            if (Program.IsElevated)
            {
                MessageBox.Show("Program is already running as an administrator!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var currentProc = Process.GetCurrentProcess();
            var fileNameDir = currentProc?.MainModule?.FileName ?? string.Empty;
            if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
            {
                MessageBox.Show("Application path doesn't exist. Cannot start: " + fileNameDir + Environment.NewLine + " Please manually run the program as an Admin if you wish to change your hotkeys.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                var startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.Arguments = string.Join(" ", Environment.GetCommandLineArgs());
                startInfo.WorkingDirectory = Application.StartupPath;
                startInfo.FileName = fileNameDir;
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