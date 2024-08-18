using CoDUO_FoV_Changer;
using CoDUO_FoV_Changer.Util;
using ControlExtensions;
using CurtLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Localization
{
    public class LocalizationManager
    {
        public static string LocalizationPath
        {
            get
            {
                return PathInfos.LocalizationPath;
            }
        }

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

        public Dictionary<string, Dictionary<int, string>> LocalizedStrings { get; private set; } = new Dictionary<string, Dictionary<int, string>>();
        public static LocalizationManager Instance { get; private set; }

        public LocalizationManager() 
        {
            Instance = this;
        }

        public static LocalizationManager CreateInstance()
        {
            return Instance = new LocalizationManager();
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
            var filePath = Path.Combine(LocalizationPath, $"{culture.Name}.json");

            if (LocalizedStrings == null)
                return;

            var jsonContent = JsonConvert.SerializeObject(LocalizedStrings, Formatting.Indented);
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
                LocalizedStrings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, string>>>(jsonContent);
            }
            // we don't need an else here to fall back onto en-US because if a culture is picked and does not have translations,
            // it will simply use english by default.
        }

        public int GetControlIndexCount(Control control)
        {
            if (LocalizedStrings == null || !LocalizedStrings.TryGetValue(GetControlAndFormName(control), out var localized))
                return 0;

            return localized.Count;
        }

        public void LoadLocalization(string cultureCode, bool restartApp = true)
        {
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

        public string GetLocalizedString(Control control, int index = 0)
        {
            if (string.IsNullOrWhiteSpace(control?.Name))
                throw new ArgumentNullException(nameof(control));

            if (LocalizedStrings == null || !LocalizedStrings.TryGetValue(GetControlAndFormName(control), out var localized))
                return control.Text;

            if (!localized.TryGetValue(index, out var localizedString))
                return control.Text;

            return localizedString;
        }

        public void ApplyLocalization(Control control, int index = 0)
        {
            if (string.IsNullOrWhiteSpace(control?.Text))
                return;

            var localized = GetLocalizedString(control, index);

            if (control.Text != localized)
            {
                if (control.InvokeRequired)
                    control.BeginInvoke((MethodInvoker)delegate { control.Text = localized; });
                else control.Text = localized;
            }
            else
            {
                // Else, we don't have localization for this one yet. Not even English.

                var changes = false;


                // Get the control and form name
                var controlFormName = GetControlAndFormName(control);

                // Check if the control form name exists in the LocalizedStrings dictionary
                if (!LocalizedStrings.TryGetValue(controlFormName, out var localizedStrings))
                {
                    // If it doesn't exist, create a new dictionary for the control form name
                    localizedStrings = new Dictionary<int, string>();
                    LocalizedStrings[controlFormName] = localizedStrings;
                    changes = true;
                }

                // Check if the localized string for the control and index exists
                if (!LocalizedStrings[controlFormName].TryGetValue(index, out var localizedString))
                {
                    // If it doesn't exist, add the control text to the localized strings dictionary
                    LocalizedStrings[controlFormName][index] = control.Text;
                    changes = true;
                }

                // If there were changes made, save the localization
                if (changes)
                    SaveLocalization(Culture);
            }


        }

        private static string GetControlAndFormName(Control control)
        {
            return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(360).Append(control.FindForm().Name).Append(".").Append(control.Name));
        }

    }
}
