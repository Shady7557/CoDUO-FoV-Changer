using CoDUO_FoV_Changer;
using CoDUO_FoV_Changer.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Localization
{
    /// <summary>
    /// Container class for the JSON localization file structure.
    /// </summary>
    public class LocalizationData
    {
        /// <summary>
        /// Legacy control-based strings. Migrated to Strings on load.
        /// </summary>
        [JsonProperty("Controls")]
        public Dictionary<string, Dictionary<int, string>> Controls { get; set; } = new Dictionary<string, Dictionary<int, string>>();

        /// <summary>
        /// Key-based strings for all translatable text.
        /// Supports {0}, {1} placeholders for string.Format().
        /// </summary>
        [JsonProperty("Strings")]
        public Dictionary<string, string> Strings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Metadata about the localization file (language name, author, etc.).
        /// </summary>
        [JsonProperty("Metadata")]
        public LocalizationMetadata Metadata { get; set; } = new LocalizationMetadata();
    }

    /// <summary>
    /// Metadata about a localization file.
    /// </summary>
    public class LocalizationMetadata
    {
        [JsonProperty("LanguageName")]
        public string LanguageName { get; set; } = "English";

        [JsonProperty("LanguageNameNative")]
        public string LanguageNameNative { get; set; } = "English";

        [JsonProperty("CultureCode")]
        public string CultureCode { get; set; } = "en-US";

        [JsonProperty("Version")]
        public string Version { get; set; } = "1.0";

        [JsonProperty("Author")]
        public string Author { get; set; } = "";
    }

    public class LocalizationManager
    {
        /// <summary>
        /// The path to the localization directory.
        /// </summary>
        public static string LocalizationPath
        {
            get
            {
                return PathInfos.LocalizationPath;
            }
        }

        /// <summary>
        ///  The full file path to the currently selected localization file.
        /// </summary>
        public string SelectedLocalizationPath
        {
            get
            {
                return Path.Combine(LocalizationPath, $"{CultureCode}.json");
            }
        }

        public string CultureCode => Culture?.Name;

        /// <summary>
        /// Set via LoadLocalization
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// The full localization data including Controls, Strings, and Metadata.
        /// </summary>
        public LocalizationData Data { get; private set; } = new LocalizationData();

        /// <summary>
        /// Legacy control-based strings (backward compatible property).
        /// </summary>
        public Dictionary<string, Dictionary<int, string>> LocalizedStrings
        {
            get => Data.Controls;
            private set => Data.Controls = value;
        }

        /// <summary>
        /// Key-based strings for all translatable text.
        /// </summary>
        public Dictionary<string, string> Strings
        {
            get => Data.Strings;
            private set => Data.Strings = value;
        }

        public static LocalizationManager Instance { get; private set; }

        public LocalizationManager()
        {
            Instance = this;
        }

        public static LocalizationManager CreateInstance()
        {
            return Instance = new LocalizationManager();
        }

        /// <summary>
        /// Static convenience method for getting localized strings with placeholder support.
        /// Usage: LocalizationManager.L("Error.FailedToStart", ex.Message)
        /// </summary>
        /// <param name="key">The string key (e.g., "Error.FailedToStart")</param>
        /// <param name="args">Optional format arguments for placeholders {0}, {1}, etc.</param>
        /// <returns>The localized and formatted string, or the key itself if not found.</returns>
        public static string L(string key, params object[] args)
        {
            return Instance?.GetString(key, args) ?? (args.Length > 0 ? string.Format(key, args) : key);
        }

        /// <summary>
        /// Gets a localized string by key with optional placeholder substitution.
        /// </summary>
        /// <param name="key">The string key (e.g., "Error.FailedToStart")</param>
        /// <param name="args">Optional format arguments for placeholders {0}, {1}, etc.</param>
        /// <returns>The localized and formatted string.</returns>
        public string GetString(string key, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key ?? string.Empty;

            string localizedString;

            if (Strings == null || !Strings.TryGetValue(key, out localizedString))
            {
                // Auto-register missing key with the key itself as default value
                RegisterMissingString(key, key);
                localizedString = key;
            }

            try
            {
                return args.Length > 0 ? string.Format(localizedString, args) : localizedString;
            }
            catch (FormatException)
            {
                // If format fails (wrong number of placeholders), return unformatted
                return localizedString;
            }
        }

        /// <summary>
        /// Registers a missing string key with its default English value.
        /// </summary>
        /// <param name="key">The string key</param>
        /// <param name="defaultValue">The default English value</param>
        public void RegisterMissingString(string key, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            if (Strings == null)
                Strings = new Dictionary<string, string>();

            if (!Strings.ContainsKey(key))
            {
                Strings[key] = defaultValue;
                SaveLocalization(Culture);
            }
        }

        public static void LocalizeAllForms()
        {
            Console.WriteLine(nameof(LocalizeAllForms));
            var forms = Application.OpenForms;
            foreach (Form form in forms)
            {
                if (form is ExtendedForm extendedForm)
                    extendedForm.LoadAllLocalization();
            }
        }

        public void SaveLocalization(CultureInfo culture)
        {
            if (culture is null)
                throw new ArgumentNullException(nameof(culture));

            if (Data == null)
                return;

            // Update metadata
            Data.Metadata.CultureCode = culture.Name;

            var filePath = Path.Combine(LocalizationPath, $"{culture.Name}.json");

            var jsonContent = JsonConvert.SerializeObject(Data, Formatting.Indented);

            new DirectoryInfo(LocalizationPath).Create();

            File.WriteAllText(filePath, jsonContent);
        }

        public void LoadLocalization(CultureInfo culture)
        {
            Console.WriteLine(nameof(LoadLocalization) + " " + culture);
            Culture = culture;

            var filePath = SelectedLocalizationPath;

            if (File.Exists(filePath))
            {
                var jsonContent = File.ReadAllText(filePath);

                // Try to load new format first
                try
                {
                    var parsed = JObject.Parse(jsonContent);

                    // Check if it's the new format (has "Controls" key) or old format
                    if (parsed.ContainsKey("Controls"))
                    {
                        // New format
                        Data = JsonConvert.DeserializeObject<LocalizationData>(jsonContent);
                    }
                    else
                    {
                        // Old format - migrate to new structure
                        Data = new LocalizationData();
                        Data.Controls = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, string>>>(jsonContent);
                        Data.Metadata.CultureCode = culture.Name;

                        // Save in new format
                        SaveLocalization(culture);
                    }
                }
                catch (JsonException)
                {
                    // Fallback to empty data
                    Data = new LocalizationData();
                    Data.Metadata.CultureCode = culture.Name;
                }
            }
            else
            {
                // Initialize with empty data
                Data = new LocalizationData();
                Data.Metadata.CultureCode = culture.Name;
            }

            // Migrate any legacy Controls entries to Strings
            MigrateControlsToStrings();

            Settings.Instance.SelectedCultureCode = culture.Name;

            // we don't need an else here to fall back onto en-US because if a culture is picked and does not have translations,
            // it will simply use english by default.
        }

        /// <summary>
        /// Migrates legacy Controls dictionary entries into the Strings dictionary.
        /// Called during LoadLocalization as a safety net for old format files.
        /// </summary>
        private void MigrateControlsToStrings()
        {
            if (LocalizedStrings == null || LocalizedStrings.Count == 0)
                return;

            var migrated = false;

            foreach (var kvp in LocalizedStrings)
            {
                var controlKey = kvp.Key; // e.g. "MainForm.StatusLabel"

                if (kvp.Value.Count == 1 && kvp.Value.ContainsKey(0))
                {
                    // Single-state control
                    var stringsKey = $"Control.{controlKey}";
                    if (!Strings.ContainsKey(stringsKey))
                    {
                        Strings[stringsKey] = kvp.Value[0];
                        migrated = true;
                    }
                }
                else
                {
                    // Multi-state control
                    foreach (var stateKvp in kvp.Value)
                    {
                        var stringsKey = $"Control.{controlKey}.State{stateKvp.Key}";
                        if (!Strings.ContainsKey(stringsKey))
                        {
                            Strings[stringsKey] = stateKvp.Value;
                            migrated = true;
                        }
                    }
                }
            }

            if (migrated)
            {
                LocalizedStrings.Clear();
                SaveLocalization(Culture);
            }
        }

        /// <summary>
        /// Returns the count of multi-state Strings keys matching a control prefix.
        /// Used by ExtendedForm to skip multi-state controls during auto-localization.
        /// </summary>
        public int GetControlIndexCount(Control control)
        {
            if (Strings == null || control == null)
                return 0;

            var prefix = $"Control.{GetControlAndFormName(control)}.";
            var count = 0;

            foreach (var key in Strings.Keys)
            {
                if (key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    count++;
            }

            return count;
        }

        public void LoadLocalization(string cultureCode, bool restartApp = true)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
                throw new ArgumentNullException(nameof(cultureCode));

            Console.WriteLine(nameof(LoadLocalization) + " : " + cultureCode);
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var loaded = false;

            for (int i = 0; i < cultures.Length; i++)
            {
                var c = cultures[i];

                if (c.Name.Equals(cultureCode, StringComparison.OrdinalIgnoreCase))
                {
                    LoadLocalization(c);
                    loaded = true;
                    break;
                }
            }

            if (!loaded)
                throw new CultureNotFoundException(nameof(CultureCode), cultureCode);

            if (loaded && restartApp)
            {
                // improve restart handling later, we want to keep the arguments used to launch the app, for example.

                Application.Restart();

            }
        }

        /// <summary>
        /// Gets a localized string for a control using the Strings dictionary.
        /// Key pattern: Control.{FormName}.{ControlName}
        /// </summary>
        public string GetLocalizedString(Control control, int index = 0)
        {
            if (string.IsNullOrWhiteSpace(control?.Name))
                throw new ArgumentNullException(nameof(control));

            var controlFormName = GetControlAndFormName(control);
            var key = $"Control.{controlFormName}";

            if (Strings != null && Strings.TryGetValue(key, out var localizedString))
                return localizedString;

            // Fallback to legacy Controls dict
            if (LocalizedStrings != null && LocalizedStrings.TryGetValue(controlFormName, out var localized))
            {
                if (localized.TryGetValue(index, out var legacyString))
                    return legacyString;
            }

            return control.Text;
        }

        /// <summary>
        /// Applies localization to a single-state control using the Strings dictionary.
        /// Key pattern: Control.{FormName}.{ControlName}
        /// </summary>
        public void ApplyLocalization(Control control, int index = 0)
        {
            if (control is null)
                throw new ArgumentNullException(nameof(control));

            if (control.IsDisposed || control.Disposing || string.IsNullOrWhiteSpace(control?.Text))
                    return;

            var controlFormName = GetControlAndFormName(control);
            var key = $"Control.{controlFormName}";

            if (Strings != null && Strings.TryGetValue(key, out var localizedString))
            {
                if (control.Text != localizedString)
                {
                    if (control.InvokeRequired)
                        control.BeginInvoke((MethodInvoker)delegate { control.Text = localizedString; });
                    else control.Text = localizedString;
                }
                return;
            }

            // Fallback to legacy Controls dict
            var localized = GetLocalizedString(control, index);

            if (control.Text != localized)
            {
                if (control.InvokeRequired)
                    control.BeginInvoke((MethodInvoker)delegate { control.Text = localized; });
                else control.Text = localized;
            }
            else
            {
                // Auto-register this control's text for future localization
                if (!Strings.ContainsKey(key))
                {
                    Strings[key] = control.Text;
                    SaveLocalization(Culture);
                }
            }
        }

        /// <summary>
        /// Scans the Localization directory for available language files.
        /// Returns a list of tuples with culture code, language name, and native name.
        /// </summary>
        public List<(string CultureCode, string LanguageName, string NativeName)> GetAvailableLanguages()
        {
            var languages = new List<(string, string, string)>();

            try
            {
                var localizationDir = LocalizationPath;

                if (!Directory.Exists(localizationDir))
                    return languages;

                var files = Directory.GetFiles(localizationDir, "*.json");

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        var jsonContent = File.ReadAllText(files[i]);
                        var parsed = JObject.Parse(jsonContent);
                        var metadata = parsed["Metadata"];

                        if (metadata == null)
                            continue;

                        var cultureCode = metadata["CultureCode"]?.ToString() ?? Path.GetFileNameWithoutExtension(files[i]);
                        var languageName = metadata["LanguageName"]?.ToString() ?? cultureCode;
                        var nativeName = metadata["LanguageNameNative"]?.ToString() ?? languageName;

                        languages.Add((cultureCode, languageName, nativeName));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to read language file {files[i]}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to scan for languages: {ex.Message}");
            }

            return languages;
        }

        /// <summary>
        /// Ensures the default en-US.json localization file exists in the AppData directory.
        /// Copies from the application's build output directory if missing or outdated.
        /// </summary>
        public void EnsureDefaultLocalizationFiles()
        {
            try
            {
                var appDataDir = LocalizationPath;
                var shippedDir = Path.Combine(Application.StartupPath, "Localization");

                if (!Directory.Exists(shippedDir))
                    return;

                new DirectoryInfo(appDataDir).Create();

                var shippedFiles = Directory.GetFiles(shippedDir, "*.json");

                for (int i = 0; i < shippedFiles.Length; i++)
                {
                    var shippedFile = shippedFiles[i];
                    var fileName = Path.GetFileName(shippedFile);
                    var targetFile = Path.Combine(appDataDir, fileName);

                    if (!File.Exists(targetFile))
                    {
                        File.Copy(shippedFile, targetFile);
                        continue;
                    }

                    // Compare versions - copy if shipped version is newer
                    try
                    {
                        var shippedJson = JObject.Parse(File.ReadAllText(shippedFile));
                        var targetJson = JObject.Parse(File.ReadAllText(targetFile));

                        var shippedVersion = shippedJson["Metadata"]?["Version"]?.ToString() ?? "0";
                        var targetVersion = targetJson["Metadata"]?["Version"]?.ToString() ?? "0";

                        if (string.Compare(shippedVersion, targetVersion, StringComparison.OrdinalIgnoreCase) > 0)
                            File.Copy(shippedFile, targetFile, true);
                    }
                    catch (Exception)
                    {
                        // If we can't compare versions, leave existing file alone
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to ensure default localization files: {ex.Message}");
            }
        }

        private static string GetControlAndFormName(Control control)
        {
            return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(360).Append(control.FindForm().Name).Append(".").Append(control.Name));
        }

    }
}
