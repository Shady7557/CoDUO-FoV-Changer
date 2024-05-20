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



        public static bool StartGame(string exePath, string optionalArgs = "", string forcedArgs = "", string ipPortToConnect = "")
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
                    if (useSteam && string.IsNullOrWhiteSpace(ipPortToConnect))
                    {
                        try
                        {
                            Log.WriteLine("Path contained 'steamapps' and Steam is running. Should launch with steam, trying!");

                            if (!string.IsNullOrWhiteSpace(optionalArgs))
                            {
                                Log.WriteLine("Writing launch parameters to config temporarily.");

                                oldCfg = GameConfig.GetGameConfig(gameType);



                                GameConfig.ApplyLaunchParametersToConfig(sb
                                    .Clear()
                                    .Append(forcedArgs)
                                    .Append(hasForcedArgs ? Environment.NewLine : string.Empty)
                                    .Append(optionalArgs)
                                    .ToString(), gameType);

                                Log.WriteLine("Wrote launch parameters.");
                            }

                            //steam://launch/{appid}/dialog

                            //thanks to a guy named Lone who helped me figure out how to launch a game where you can select the option via Steam.
                            //Now that Steam lets you permanently select your desired option,
                            //this either immediately launches the game via Steam or Steam prompts you to select version.

                            var steamLaunchUrl = sb
                                .Clear()
                                .Append("steam://launch/26")
                                .Append(gameType == GameConfig.GameType.CoDMP ? "20" : "40")
                                .Append("/dialog")
                                .ToString();

                            launchFileName = steamLaunchUrl;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Log.WriteLine(ex.ToString());
                        }
                    }
                    else if (useSteam)
                    {
                        Console.WriteLine("ipPort is not empty and useSteam is true, we must apply the hacky workaround.");
                        SteamHack.EnsureSteamDll(Path.GetDirectoryName(exePath));
                    }


                    var argSb = (!useSteam && !string.IsNullOrEmpty(optionalArgs)) ? sb.Clear().Append(forcedArgs).Append(hasForcedArgs ? Environment.NewLine : string.Empty).Append(optionalArgs) : sb.Clear();

                    if (!string.IsNullOrWhiteSpace(ipPortToConnect))
                        argSb.Append(" +connect ").Append(ipPortToConnect);
                    

                    var startInfo = new ProcessStartInfo
                    {
                        Arguments = argSb.Length > 0 ? argSb.ToString() : string.Empty,
                        FileName = launchFileName,
                        WorkingDirectory = Path.GetDirectoryName(exePath),
                    };

                    Process.Start(startInfo);
                }
                finally { Pool.Free(ref sb); }



                if (!string.IsNullOrWhiteSpace(oldCfg))
                {
                    //TODO: Improve checking - 8 seconds alone is not enough.
                    //For example, if Steam was not open and this has to launch Steam first, it often overwrites the config before game is open.
                    //It is likely safe to overwrite the config while the game is open [needs verification].
                    //Idea: Wait up to 45 seconds, then write cfg. Else, write cfg once game process is available.
                    //URGENT: If old cfg exists when FoV changer is shut down, write old cfg back to disk!

                    // Some of this is now done (see below), but it's a bit ugly and I don't love it.

                    Task.Run(() =>
                    {
                        var waitedTimes = 0;
                        var maxWaitTimes = 10;

                        Log.WriteLine("Waiting for game process to start before writing old config back to disk.");



                        while (!ProcessExtension.IsAnyCoDProcessRunning())
                        {
                            if (waitedTimes >= maxWaitTimes)
                            {
                                Log.WriteLine("Waited too long for game process to start, writing old config to disk.");
                                break;
                            }

                            Thread.Sleep(7500);
                            waitedTimes++;
                        }

                        // Wait to be sure we don't write it too soon to actually be loaded by the game.

                        Thread.Sleep(8000);
                        GameConfig.WriteGameConfig(Settings.Instance.BaseGamePath, oldCfg, gameType);

                        Log.WriteLine("Wrote old config to disk!");
                    });
                }

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
