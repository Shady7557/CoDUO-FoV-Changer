using CurtLog;
using Microsoft.Win32;
using ProcessExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class PathScanner
    {
        private static string _registryPath = string.Empty;
        public static string RegistryPath
        {
            get
            {
                if (string.IsNullOrEmpty(_registryPath))
                {
                    var path = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive";
                    if (!Environment.Is64BitOperatingSystem) path = path.Replace(@"Wow6432Node\", string.Empty);
                    if (Registry.LocalMachine.OpenSubKey(path.Replace(@"HKEY_LOCAL_MACHINE\", string.Empty)) == null) path = path.Replace("United Offensive", string.Empty); //if UO key is null, use cod1 (if it exists)
                    _registryPath = path;
                }
                return _registryPath;
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
                                    var fullName = subKey + @"\" + name;
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

                var uoFileName = ProcessExtension.GetFileNameFromProcess("CoDUOMP");
                if (!string.IsNullOrEmpty(uoFileName)) return uoFileName;

                var codFileName = ProcessExtension.GetFileNameFromProcess("CoDMP");
                if (!string.IsNullOrEmpty(codFileName)) return codFileName;

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
                        if (fileName.Equals("CoDUOMP.exe", StringComparison.OrdinalIgnoreCase) || fileName.Equals("CoDMP.exe", StringComparison.Ordinal)) return Application.StartupPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            var registryInstallPath = Registry.GetValue(RegistryPath, "InstallPath", string.Empty)?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(registryInstallPath)) return registryInstallPath;

            var paths1 = GetPotentialPathsFromSubkey(@"Software\Classes\VirtualStore\MACHINE\SOFTWARE\NVIDIA Corporation\Global\NVTweak\NvCplAppNamesStored", Registry.CurrentUser);
            var paths2 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\AppSwitched", Registry.CurrentUser);
            var paths3 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\ShowJumpView", Registry.CurrentUser);
            var paths4 = GetPotentialPathsFromSubkey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store", Registry.CurrentUser);
            var paths5 = GetPotentialPathsFromSubkey(@"System\GameConfigStore\Children", Registry.CurrentUser, true);
            var allPaths = new List<string>();
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
                var distinctPaths = allPaths.Distinct();
                var allPathStr = "distinctPaths contains: " + distinctPaths.Count().ToString("N0") + " paths: " + Environment.NewLine + string.Join(", ", distinctPaths);
                Console.WriteLine(allPathStr);
                Log.WriteLine(allPathStr);
                return distinctPaths.OrderByDescending(p => File.GetLastAccessTimeUtc(p)).FirstOrDefault();
            }
        }
    }
}
