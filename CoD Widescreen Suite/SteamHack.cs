using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoD_Widescreen_Suite
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

        // We can inject the Steam Overlay, but this does not yet do that.

        public static bool HasSteamDll(string gamePath)
        {
            return File.Exists(Path.Combine(gamePath, "steam.dll"));
        }

        public static void EnsureSteamDll(string gamePath)
        {
            if (HasSteamDll(gamePath))
                return;

            string steamPath;

            if (gamePath.IndexOf("steam", StringComparison.OrdinalIgnoreCase) >= 0 && gamePath.IndexOf("steamapps", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                steamPath = DirectoryExtensions.DirectoryExtension.GetParentDirectory(gamePath, 3);
            }
            else steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam", "steam.dll");

            if (!Directory.Exists(steamPath))
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam", "steam.dll");
            
            if (!Directory.Exists(steamPath))
            {
                // Could not find steam anywhere!
                return;
            }

            var steamDllPath = Path.Combine(steamPath, "steam.dll");

            if (!File.Exists(steamDllPath))
            {
                // Steam.dll does not exist in the Steam directory!!
                return;
            }


            File.Copy(steamDllPath, Path.Combine(gamePath, "steam.dll"));
        }

    }
}
