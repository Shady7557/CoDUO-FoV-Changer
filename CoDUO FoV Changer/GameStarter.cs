using CurtLog;
using ProcessExtensions;
using StringExtension;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class GameStarter
    {

        [ThreadStatic]
        private static readonly ProcessStartInfo _startInfo = new ProcessStartInfo();

        [ThreadStatic]
        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        // enum for result of StartGame:

        public enum StartStatus
        {
            GenericFailure,
            Success,
            SteamNotRunning,
            FileNotFound
        }

        public static StartStatus StartGame(string exePath, string optionalArgs = "", string forcedArgs = "")
        {
            if (string.IsNullOrWhiteSpace(exePath))
                throw new ArgumentNullException(nameof(exePath));

            try
            {
                if (!File.Exists(exePath))
                    return StartStatus.FileNotFound;

                var exeDirectory = Path.GetDirectoryName(exePath);

                var hasForcedArgs = !string.IsNullOrWhiteSpace(forcedArgs);


                var launchFileName = Path.GetFileName(exePath);

                var oldCfg = string.Empty;
                var gameType = launchFileName.Contains("coduo", StringComparison.OrdinalIgnoreCase) ? GameConfig.GameType.CoDUOMP : GameConfig.GameType.CoDMP;

                var useSteam = GameConfig.ShouldUseSteam(exePath);


                try
                {
                    if (useSteam)
                    {
                        SteamUtil.EnsureSteamDll(exeDirectory);

                        if (!ProcessExtension.IsAnyProcessRunning("steam"))
                            return StartStatus.SteamNotRunning;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }


                var argSb = _stringBuilder
                  .Clear()
                  .Append(forcedArgs)
                  .Append(" ")
                  .Append(optionalArgs);


                _startInfo.Arguments = argSb.ToString();
                _startInfo.FileName = exePath;
                _startInfo.WorkingDirectory = exeDirectory;

                var gameProc = Process.Start(_startInfo);

                try
                {
                    if (useSteam && Settings.Instance.UseSteamOverlay)
                        SteamUtil.EnsureSteamOverlay(gameProc.Id, gameType);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.WriteLine("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }

            return StartStatus.Success;
        }
    }
}
