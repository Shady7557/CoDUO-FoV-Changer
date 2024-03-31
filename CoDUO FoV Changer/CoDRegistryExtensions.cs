using CoDUO_FoV_Changer;
using Microsoft.Win32;
using System;
using System.Text;

namespace CoDRegistryExtensions
{
    public class CodRex
    {

        //I don't know what's going on here and I am having a crisis trying to code this primarily for UO but also remembering minimal VCoD support exists in this same app which complicates ALL.

        private static string _registryPath = string.Empty;
        private static string _registryPathVS = string.Empty;

        private static string _registryPathCoD = string.Empty;
        private static string _registryPathCoDVS = string.Empty;

        private static string _gameVersion = string.Empty;

        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        public static object GetRegistryValue(string valueName, bool virtualStore = false, bool vCod = false) //should use enums somewhere instead of this messy nonsense.
        {
            if (string.IsNullOrWhiteSpace(valueName))
                throw new ArgumentNullException(nameof(valueName));

            return Registry.GetValue(vCod ? (virtualStore ? RegistryPathCoDVirtualStore : RegistryPathCoD) : virtualStore ? RegistryPathVirtualStore : RegistryPath, valueName, string.Empty);
        }

        public static bool TryGetRegistryValue<T>(string valueName, out T value, bool virtualStore = false, bool vCod = false)
        {
            return (value = (T)GetRegistryValue(valueName, virtualStore, vCod)) != null;
        }

        public static string GameVersion
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_gameVersion))
                {
                    if (!TryGetRegistryValue("Version", out _gameVersion))
                        TryGetRegistryValue("Version", out _gameVersion, true);
                }
                return _gameVersion;
            }
        }

        public static string RegistryPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_registryPath))
                    _registryPath = _stringBuilder.Clear().Append(Environment.Is64BitOperatingSystem ? @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive" : @"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive").ToString();


                return _registryPath;
            }
        }

        public static string RegistryPathVirtualStore
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_registryPathVS))
                    _registryPathVS = _stringBuilder.Clear().Append(@"HKEY_USERS\").Append(Program.CurrentUserSID).Append(@"\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\").Append(Environment.Is64BitOperatingSystem ? @"WOW6432Node\" : string.Empty).Append(@"Activision\Call of Duty United Offensive").ToString();
                
                return _registryPathVS;
            }
        }

        public static string RegistryPathCoD
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_registryPathCoD))
                    _registryPathCoD = _stringBuilder.Clear().Append(Environment.Is64BitOperatingSystem ? @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty" : @"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty").ToString();

                return _registryPathCoD;
            }
        }

        public static string RegistryPathCoDVirtualStore
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_registryPathCoDVS))
                    _registryPathCoDVS = _stringBuilder.Clear().Append(@"HKEY_USERS\").Append(Program.CurrentUserSID).Append(@"\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\").Append(Environment.Is64BitOperatingSystem ? @"WOW6432Node\" : string.Empty).Append(@"Activision\Call of Duty").ToString();
                
                return _registryPathCoDVS;
            }
        }

    }
}
