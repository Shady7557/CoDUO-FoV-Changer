using CurtLog;
using System;
using System.Diagnostics;
using System.IO;

namespace CoDUO_FoV_Changer
{
    internal class SteamHack
    {
        // No, not that kind of hack.
        // This is not a "hack". It's a hacky workaround in the technical sense.
        // We are not hacking anything. There's no cheating. Please read the code.

        // This is a workaround for launching CoD1 and CoD:UO through Steam *without* using the Steam client's launch option.
        // You must still own the game. Steam must still be installed.
        // What this does:
        // Ensure that "steam.dll", from the Steam installation path, exists in the game's directory.
        // If it doesn't, it will copy it there.
        // Then, it will launch the game.

        // The problem is that we cannot launch with launch parameters through Steam's dialogue because of an annoying pop-up they recently added.
        // If we launch the executable directly, when Steam.dll is present in the game directory, the game realizes this and launches it normally.
        // The only downside is that the Steam overlay is no longer present.

        // This also now injects the Steam overlay into the game process, to ensure there's no
        // apparent difference between launching the game through this app or through Steam itself.

        public static bool HasSteamDll(string gamePath)
        {
            if (string.IsNullOrWhiteSpace(gamePath))
                throw new ArgumentNullException(nameof(gamePath));

            return File.Exists(Path.Combine(gamePath, "steam.dll"));
        }

        public static void EnsureSteamDll(string gamePath)
        {
            if (HasSteamDll(gamePath))
                return;

            string steamPath;

            if (gamePath.IndexOf("steam", StringComparison.OrdinalIgnoreCase) >= 0 && gamePath.IndexOf("steamapps", StringComparison.OrdinalIgnoreCase) >= 0)
                steamPath = DirectoryExtensions.DirectoryExtension.GetParentDirectory(gamePath, 4);
            else steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam");

            if (!Directory.Exists(steamPath))
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam");
            

            if (!Directory.Exists(steamPath))
                return; // Could not find steam anywhere!

            var steamDllPath = Path.Combine(steamPath, "steam.dll");

            if (!File.Exists(steamDllPath))
            {
                // Steam.dll does not exist in the Steam directory!!
                return;
            }


            File.Copy(steamDllPath, Path.Combine(gamePath, "steam.dll"));
        }

        public static void EnsureSteamOverlay(int processId, GameConfig.GameType gameType)
        {
            var ensureLogMsg = nameof(EnsureSteamOverlay) + " called for process with ID " + processId + ", " + nameof(gameType) + ": " + gameType;

            Console.WriteLine(ensureLogMsg);
            Log.WriteLine(ensureLogMsg);

            if (gameType != GameConfig.GameType.CoDMP && gameType != GameConfig.GameType.CoDUOMP)
                return;

            var process = Process.GetProcessById(processId);

            var executablePath = process.MainModule.FileName;

            string steamPath;

            if (executablePath.IndexOf("steam", StringComparison.OrdinalIgnoreCase) >= 0 && executablePath.IndexOf("steamapps", StringComparison.OrdinalIgnoreCase) >= 0)
                steamPath = DirectoryExtensions.DirectoryExtension.GetParentDirectory(executablePath, 4);
            else steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam");

            if (!Directory.Exists(steamPath))
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam");


            if (!Directory.Exists(steamPath))
            {
                var logMsg = "Could not find Steam, final path: " + steamPath;

                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            var steamPathLog = "Steam path: " + steamPath;

            Console.WriteLine(steamPathLog);
            Log.WriteLine(steamPathLog);


            var overlayExecutablePath = Path.Combine(steamPath, "GameOverlayUI.exe");

            if (!File.Exists(overlayExecutablePath))
            {
                var logMsg = "GameOverlayUI.exe does not exist in the Steam directory. Path: " + overlayExecutablePath;

                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            var overlayDllPath = Path.Combine(steamPath, "GameOverlayRenderer.dll");

            if (!File.Exists(overlayDllPath))
            {
                var logMsg = "GameOverlayRenderer.dll does not exist in the Steam directory. Path: " + overlayDllPath;

                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            var steamProcesses = Process.GetProcessesByName("Steam");

            if (steamProcesses == null || steamProcesses.Length < 1)
            {
                var logMsg = "Could not find Steam process.";
                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            var steamProcId = steamProcesses[0]?.Id;

            if (steamProcId is null)
            {
                var logMsg = "Could not get Steam process ID.";

                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            var steamAppId = gameType == GameConfig.GameType.CoDMP ? "2620" : "2640";

            var procInfo = new ProcessStartInfo
            {
                FileName = overlayExecutablePath,
                WorkingDirectory = steamPath,
                Arguments = "-pid " + processId + " -steampid " + steamProcId.Value + " -manuallyclearframes 0 -gameid " + steamAppId,
            };


            var injectLog = "Injecting overlay DLL: " + overlayDllPath + " into process ID: " + processId;

            Console.WriteLine(injectLog);
            Log.WriteLine(injectLog);

            Injector.Instance.Inject(processId, overlayDllPath);

            var injectedLog = "Ran Injector.Jnject for Overlay DLL: " + overlayDllPath + " into process ID: " + processId;

            Console.WriteLine(injectedLog);
            Log.WriteLine(injectedLog);

            var procLog = "Starting Steam overlay executable: " + procInfo.FileName + " " + procInfo.Arguments;

            Console.WriteLine(procLog);
            Log.WriteLine(procLog);

            var overlayProcess = Process.Start(procInfo);

            var startedLog = "Started Steam overlay process: " + overlayProcess.Id;

            Console.WriteLine(startedLog);
            Log.WriteLine(startedLog);
         

        }

    }
}
