using CoDRegistryExtensions;
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

namespace CoD_Widescreen_Suite
{
    internal class PathScanner
    {
        private static readonly StringBuilder _stringBuilder = new StringBuilder();

        private static List<string> GetPotentialPathsFromSubkey(string subKey, RegistryKey regKey, bool recursive = false)
        {
            var paths = new List<string>();
            try
            {
                using (var key = regKey.OpenSubKey(subKey))
                {
                    if (key != null)
                    {
                        if (recursive)
                        {
                            var subKeyNames = key.GetSubKeyNames();
                            if (subKeyNames != null && subKeyNames.Length > 0)
                            {
                                for (int i = 0; i < subKeyNames.Length; i++)
                                {
                                    var name = subKeyNames[i];
                                    var fullName = _stringBuilder.Clear().Append(subKey).Append(@"\").Append(name).ToString();
                                    using (var key2 = regKey.OpenSubKey(fullName))
                                    {
                                        if (key2 == null)
                                            continue;
                                        
                                        var valueNames2 = key2.GetValueNames();

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
                                                    if (File.Exists(val))
                                                        paths.Add(val);
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(val))
                                                            continue;
                                                        
                                                        if (File.Exists(val))
                                                            paths.Add(val);
                                                    }
                                                }
                                            }
                                        }
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
                                    if (File.Exists(val))
                                        paths.Add(val);
                                    else
                                    {
                                        var val2 = key.GetValue(val) as string;
                                        if (string.IsNullOrEmpty(val2))
                                            continue;

                                        if (!string.IsNullOrEmpty(val2) && File.Exists(val2))
                                            paths.Add(val2);
                                    }
                                }
                            }
                        }
                    }
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

                var procNames = PathInfos.GAME_PROCESS_NAMES;

                foreach (var processName in procNames)
                {
                    try 
                    {
                        var fileName = ProcessExtension.GetFileNameFromProcessName(processName);

                        if (!string.IsNullOrWhiteSpace(fileName))
                            return fileName;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
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
                var procs = Process.GetProcesses();
                for (int i = 0; i < procs.Length; i++)
                {
                    try
                    {
                        var proc = procs[i];
                        if ((proc?.MainWindowTitle ?? string.Empty).IndexOf("CoD:United Offensive Multiplayer", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            var fileName = ProcessExtension.GetFileNameFromProcess(proc);
                            if (!string.IsNullOrWhiteSpace(fileName))
                                return fileName;

                            break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
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
                    for (int i = 0; i < filesInStartup.Length; i++)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(filesInStartup[i]);
                            if (fileName.Equals("CoDUOMP.exe", StringComparison.OrdinalIgnoreCase) || fileName.Equals("CoDMP.exe", StringComparison.OrdinalIgnoreCase) || fileName.Equals("mohaa.exe", StringComparison.OrdinalIgnoreCase)) return Application.StartupPath;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Log.WriteLine(ex.ToString());
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
                var registryInstallPath = Registry.GetValue(CodRex.RegistryPath, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(registryInstallPath))
                    registryInstallPath = Registry.GetValue(CodRex.RegistryPathVirtualStore, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(registryInstallPath))
                    registryInstallPath = Registry.GetValue(CodRex.RegistryPathCoD, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(registryInstallPath))
                    registryInstallPath = Registry.GetValue(CodRex.RegistryPathCoDVirtualStore, "InstallPath", string.Empty)?.ToString() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(registryInstallPath))
                    return registryInstallPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }
      

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
