using CurtLog;
using ShadyPool;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class GameStarter
    {
        public static bool StartGame(string exePath, string optionalArgs = "", string forcedArgs = "")
        {
            if (string.IsNullOrWhiteSpace(exePath))
                throw new ArgumentNullException(nameof(exePath));

            try
            {
                if (!File.Exists(exePath))
                    return false;

                var exeDirectory = Path.GetDirectoryName(exePath);

                var hasForcedArgs = !string.IsNullOrWhiteSpace(forcedArgs);


                var launchFileName = Path.GetFileName(exePath);

                var oldCfg = string.Empty;
                var gameType = (launchFileName.IndexOf("coduo", StringComparison.OrdinalIgnoreCase) >= 0) ? GameConfig.GameType.CoDUOMP : GameConfig.GameType.CoDMP;

                var useSteam = GameConfig.ShouldUseSteam(exePath);

                var sb = Pool.Get<StringBuilder>();

                try
                {

                    try 
                    {
                        if (useSteam)
                            SteamHack.EnsureSteamDll(exeDirectory);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                   

                    var argSb = sb
                        .Clear()
                        .Append(forcedArgs)
                        .Append(" ")
                        .Append(optionalArgs);

                   
                    var startInfo = new ProcessStartInfo
                    {
                        Arguments = argSb.Length > 0 ? argSb.ToString() : string.Empty,
                        FileName = exePath,
                        WorkingDirectory = launchFileName,
                    };

                    var gameProc = Process.Start(startInfo);

                    try 
                    {
                        if (useSteam)
                            SteamHack.EnsureSteamOverlay(gameProc.Id, gameType);
                    }
                    catch(Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                    
                }
                finally { Pool.Free(ref sb); }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Failed to start game: " + ex.Message + Environment.NewLine + " Please refer to the log for more info.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.WriteLine("Failed to start process game process: " + Environment.NewLine + ex.ToString());
            }

            return false;
        }
    }
}
