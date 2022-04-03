using CurtLog;
using Microsoft.Win32;
using ProcessExtensions;
using ShadyPool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class PathScanner
    {
        private static string _registryPath = string.Empty;
        private static string _registryPathVS = string.Empty;

        private static string _registryPathCoD = string.Empty;
        private static string _registryPathCoDVS = string.Empty;

        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        public static string RegistryPath
        {
            get
            {
                if (string.IsNullOrEmpty(_registryPath))
                {
                    _registryPath = _stringBuilder.Clear().Append(Environment.Is64BitOperatingSystem ? @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive" : @"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive").ToString();

                }
                return _registryPath;
            }
        }

        public static string RegistryPathVirtualStore
        {
            get
            {
                if (string.IsNullOrEmpty(_registryPathVS))
                {
                    _registryPathVS = _stringBuilder.Clear().Append(@"HKEY_USERS\").Append(Program.CurrentUserSID).Append(@"\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\").Append(Environment.Is64BitOperatingSystem ? @"WOW6432Node\" : string.Empty).Append(@"Activision\Call of Duty United Offensive").ToString();
                }
                return _registryPathVS;
            }
        }

        public static string RegistryPathCoD
        {
            get
            {
                if (string.IsNullOrEmpty(_registryPathCoD))
                {
                    _registryPathCoD = _stringBuilder.Clear().Append(Environment.Is64BitOperatingSystem ? @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty" : @"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty").ToString();

                }
                return _registryPathCoD;
            }
        }

        public static string RegistryPathCoDVirtualStore
        {
            get
            {
                if (string.IsNullOrEmpty(_registryPathCoDVS))
                {
                    _registryPathCoDVS = _stringBuilder.Clear().Append(@"HKEY_USERS\").Append(Program.CurrentUserSID).Append(@"\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\").Append(Environment.Is64BitOperatingSystem ? @"WOW6432Node\" : string.Empty).Append(@"Activision\Call of Duty").ToString();
                }
                return _registryPathCoDVS;
            }
        }

        private static List<string> GetPotentialPathsFromSubkey(string subKey, RegistryKey regKey, bool recursive = false)
        {
            var paths = new List<string>();
            try
            {
                using (var key = regKey.OpenSubKey(subKey))
                {
                    if (key != null)
                    {
                        Console.WriteLine(subKey + " was not null, so that's good");
                        if (recursive)
                        {
                            Console.WriteLine("recursive");
                            var subKeyNames = key.GetSubKeyNames();
                            if (subKeyNames != null && subKeyNames.Length > 0)
                            {
                                Console.WriteLine("subKeyNames: " + subKeyNames.Length);
                                for (int i = 0; i < subKeyNames.Length; i++)
                                {
                                    var name = subKeyNames[i];
                                    var fullName = _stringBuilder.Clear().Append(subKey).Append(@"\").Append(name).ToString();
                                    using (var key2 = regKey.OpenSubKey(fullName))
                                    {
                                        if (key2 == null)
                                        {
                                            Console.WriteLine("key2 is null for: " + fullName);
                                            continue;
                                        }
                                        var valueNames2 = key2.GetValueNames();
                                        Console.WriteLine("key2 " + fullName + " has valueNames2 length: " + valueNames2.Length);
                                        if (valueNames2 != null && valueNames2.Length > 1)
                                        {
                                            for (int j = 0; j < valueNames2.Length; j++)
                                            {
                                                var valName = valueNames2[j];
                                                var val = (key2?.GetValue(valName) ?? null) as string;
                                                var fullVal = valName + ": " + val;
                                                Console.WriteLine(fullVal);
                                                if (!string.IsNullOrEmpty(val) && (val.IndexOf("CoDUOMP", StringComparison.OrdinalIgnoreCase) >= 0 || val.IndexOf("CoDMP", StringComparison.OrdinalIgnoreCase) >= 0))
                                                {
                                                    Console.WriteLine("indexof coduomp/codmp was >= 0: " + fullVal);
                                                    if (File.Exists(val))
                                                    {
                                                        Console.WriteLine("file exists for val: " + fullVal);
                                                        paths.Add(val);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("file doesn't exist for val: " + fullVal);
                                                        if (string.IsNullOrEmpty(val))
                                                        {
                                                            Console.WriteLine("val is null/empty (after trying to cast as string)");
                                                            continue;
                                                        }
                                                        if (File.Exists(val))
                                                        {
                                                            Console.WriteLine("file DOES exist for val: " + fullVal);
                                                            paths.Add(val);
                                                        }
                                                        else Console.WriteLine("file does not exist for val: " + fullVal);
                                                    }
                                                }
                                            }
                                        }
                                        else Console.WriteLine("getvaluenames null (recursive): " + fullName);
                                    }
                                }
                            }
                        }


                        var valueNames = key.GetValueNames();
                        if (valueNames != null && valueNames.Length > 1)
                        {
                            for (int i = 0; i < valueNames.Length; i++)
                            {
                                var val = valueNames[i];
                                if (val.IndexOf("CoDUOMP", StringComparison.OrdinalIgnoreCase) >= 0 || val.IndexOf("CoDMP", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    Console.WriteLine("indexof coduomp/codmp was >= 0: " + val);
                                    if (File.Exists(val))
                                    {
                                        Console.WriteLine("file exists for val: " + val);
                                        paths.Add(val);
                                    }
                                    else
                                    {
                                        Console.WriteLine("file doesn't exist for val: " + val);
                                        var val2 = key.GetValue(val) as string;
                                        if (string.IsNullOrEmpty(val2))
                                        {
                                            Console.WriteLine("val2 is null/empty (after trying to cast as string)");
                                            continue;
                                        }
                                        if (!string.IsNullOrEmpty(val2) && File.Exists(val2))
                                        {
                                            Console.WriteLine("file does exist for val2: " + val2);
                                            paths.Add(val2);
                                        }
                                        else Console.WriteLine("file does not exist or is empty for val2: " + val2);
                                    }
                                }
                            }
                        }
                        else Console.WriteLine("getvaluenames null: " + subKey);
                    }
                    else Console.WriteLine("key was null: " + subKey);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            return paths;
        }



        public static string ScanForGamePath()
        {
         

            try
            {

                var uoFileName = ProcessExtension.GetFileNameFromProcessName("CoDUOMP");
                if (!string.IsNullOrEmpty(uoFileName)) return uoFileName;

                var codFileName = ProcessExtension.GetFileNameFromProcessName("CoDMP");
                if (!string.IsNullOrEmpty(codFileName)) return codFileName;

                var mohaaFileName = ProcessExtension.GetFileNameFromProcessName("mohaa");
                if (!string.IsNullOrEmpty(mohaaFileName)) return mohaaFileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            try
            {
                var procs = Process.GetProcesses();
                for (int i = 0; i < procs.Length; i++)
                {
                    var proc = procs[i];
                    if ((proc?.MainWindowTitle ?? string.Empty).IndexOf("CoD:United Offensive Multiplayer", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var fileName = ProcessExtension.GetFileNameFromProcess(proc);
                        if (!string.IsNullOrEmpty(fileName)) return fileName;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            try
            {
                var filesInStartup = Directory.GetFiles(Application.StartupPath);
                if (filesInStartup?.Length > 0)
                {
                    for (int i = 0; i < filesInStartup.Length; i++)
                    {
                        var fileName = Path.GetFileName(filesInStartup[i]);
                        if (fileName.Equals("CoDUOMP.exe", StringComparison.OrdinalIgnoreCase) || fileName.Equals("CoDMP.exe", StringComparison.OrdinalIgnoreCase) || fileName.Equals("mohaa.exe", StringComparison.OrdinalIgnoreCase)) return Application.StartupPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            var registryInstallPath = Registry.GetValue(RegistryPath, "InstallPath", string.Empty)?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(registryInstallPath)) registryInstallPath = Registry.GetValue(RegistryPathVirtualStore, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(registryInstallPath)) return registryInstallPath;

            var paths1 = GetPotentialPathsFromSubkey(@"Software\Classes\VirtualStore\MACHINE\SOFTWARE\NVIDIA Corporation\Global\NVTweak\NvCplAppNamesStored", Registry.CurrentUser);
            var paths2 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\AppSwitched", Registry.CurrentUser);
            var paths3 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\ShowJumpView", Registry.CurrentUser);
            var paths4 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store", Registry.CurrentUser);
            var paths5 = GetPotentialPathsFromSubkey(@"System\GameConfigStore\Children", Registry.CurrentUser, true);

            var allPaths = Pool.GetList<string>();
            try 
            {
                allPaths.AddRange(paths1);
                allPaths.AddRange(paths2);
                allPaths.AddRange(paths3);
                allPaths.AddRange(paths4);
                allPaths.AddRange(paths5);

                if (allPaths.Count < 1)
                {
                    var noRegPathStr = "allPaths was empty, so we were unable to grab any install info from registry.";
                    Console.WriteLine(noRegPathStr);
                    Log.WriteLine(noRegPathStr);
                    return string.Empty;
                }
                else
                {
                    if (allPaths.Count == 1) return allPaths[0];

                    var distinctPaths = allPaths.Distinct();
                    var allPathStr = "distinctPaths contains: " + distinctPaths.Count().ToString("N0") + " paths: " + Environment.NewLine + string.Join(", ", distinctPaths);
                    Console.WriteLine(allPathStr);
                    Log.WriteLine(allPathStr);
                    return distinctPaths.Count() == 1 ? distinctPaths.FirstOrDefault() : distinctPaths.OrderByDescending(p => File.GetLastAccessTimeUtc(p)).FirstOrDefault();
                }
            }
            finally { Pool.FreeList(ref allPaths); }
        }
    }
}
