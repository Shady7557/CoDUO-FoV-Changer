using CurtLog;
using ProcessExtensions;
using ShadyPool;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoD_Widescreen_Suite
{
    internal class GameStarter
    {
        // Likely to be deleted:
        /// <summary>
        /// Uses the config's install path options to start the game based on GameType.
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="optionalArgs"></param>
        /// <param name="forcedArgs"></param>
        /// <returns></returns>
        public static bool StartGame(GameConfig.GameType gameType, string optionalArgs = "", string forcedArgs = "")
        {
            // We'll determine the correct exePath, then call the other StartGame method.

            throw new NotImplementedException();
        }



        public static bool StartGame(string exePath, string optionalArgs = "", string forcedArgs = "")
        {
            if (string.IsNullOrWhiteSpace(exePath))
                throw new ArgumentNullException(nameof(exePath));

            try
            {
                if (!File.Exists(exePath))
                    return false;
                

                var hasForcedArgs = !string.IsNullOrWhiteSpace(forcedArgs);


                var launchFileName = Path.GetFileName(exePath);

                var oldCfg = string.Empty;
                var gameType = (launchFileName.IndexOf("coduo", StringComparison.OrdinalIgnoreCase) >= 0) ? GameConfig.GameType.CoDUOMP : GameConfig.GameType.CoDMP;

                var useSteam = GameConfig.ShouldUseSteam(exePath);

                var sb = Pool.Get<StringBuilder>();

                try
                {

                    if (useSteam)
                    {
                        SteamHack.EnsureSteamDll(Path.GetDirectoryName(exePath));
                    }

                    // need to add overlay injection.


                    var argSb = sb.Clear().Append(forcedArgs).Append(" ").Append(optionalArgs);
                    

                    var startInfo = new ProcessStartInfo
                    {
                        Arguments = argSb.Length > 0 ? argSb.ToString() : string.Empty,
                        FileName = launchFileName,
                        WorkingDirectory = Path.GetDirectoryName(exePath),
                    };

                    Process.Start(startInfo);
                }
                finally { Pool.Free(ref sb); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.");
                Log.WriteLine("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }

            return false;
        }
    }
}
