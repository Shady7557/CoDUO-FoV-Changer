﻿using CurtLog;
using StringExtension;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer.Util
{
    internal static class SteamUtil
    {
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

        public static string GetSteamDirectory(string gamePath = "")
        {
            var steamPath = string.Empty;

            if (!string.IsNullOrWhiteSpace(gamePath))
            {
                if (gamePath.Contains("steam", StringComparison.OrdinalIgnoreCase) && gamePath.Contains("steamapps", StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        steamPath = DirectoryExtensions.DirectoryExtension.GetParentDirectory(gamePath, i);

                        if (steamPath.EndsWith("steam", StringComparison.OrdinalIgnoreCase))
                            break;

                    }
                }
            }

            if (string.IsNullOrWhiteSpace(steamPath) || !Directory.Exists(steamPath))
            {
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam");

                if (!Directory.Exists(steamPath))
                    steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam");

            }

            return steamPath;
        }

        public static string GetSteamExecutablePath(string gamePath = "")
        {
            var steamPath = GetSteamDirectory(gamePath);

            return Path.Combine(steamPath, "Steam.exe");
        }

        public static bool TryGetSteamDirectory(out string steamPath, string gamePath = "")
        {
            steamPath = GetSteamDirectory(gamePath);

            return !string.IsNullOrWhiteSpace(steamPath) && Directory.Exists(steamPath);
        }

        public static bool TryGetSteamExecutablePath(out string steamExePath, string gamePath = "")
        {
            steamExePath = GetSteamExecutablePath(gamePath);

            return !string.IsNullOrWhiteSpace(steamExePath) && File.Exists(steamExePath);
        }

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

            var steamPath = GetSteamDirectory();

            if (string.IsNullOrWhiteSpace(steamPath) || !Directory.Exists(steamPath))
                return; // Could not find steam anywhere!

            var steamDllPath = Path.Combine(steamPath, "steam.dll");

            if (!File.Exists(steamDllPath))
            {
                // Steam.dll does not exist in the Steam directory!!
                return;
            }


            File.Copy(steamDllPath, Path.Combine(gamePath, "steam.dll"));
        }

        public static async Task EnsureSteamOverlay(int processId, GameConfig.GameType gameType, int injectionDelayMs = 0)
        {
            if (gameType != GameConfig.GameType.CoDMP && gameType != GameConfig.GameType.CoDUOMP)
                return;

            var steamPath = GetSteamDirectory();

            if (string.IsNullOrWhiteSpace(steamPath) || !Directory.Exists(steamPath))
            {
                var logMsg = "Could not find Steam, final path: " + steamPath;

                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

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

            if (!steamProcId.HasValue)
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
                Arguments = StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(360)
                .Append("-pid ")
                .Append(processId)
                .Append("-steampid ")
                .Append(steamProcId.Value)
                .Append(" -manuallyclearframes 0 ")
                .Append("-gameid ")
                .Append(steamAppId))
            };

            var res = await Injector.Instance.Inject(processId, overlayDllPath, injectionDelayMs);

            if (res != DllInjectionResult.Success)
            {
                var logMsg = "Failed to inject overlay DLL: " + overlayDllPath + " into process ID: " + processId + ". Result: " + res;
                Console.WriteLine(logMsg);
                Log.WriteLine(logMsg);

                return;
            }

            Process.Start(procInfo);
        }

    }
}
