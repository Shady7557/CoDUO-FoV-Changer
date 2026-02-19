using Localization;
using System;
using System.Windows.Forms;

namespace ControlExtensions
{
    public static class ControlLocalizationExtensions
    {
        public static void ApplyLocalization(this Control control, int index = 0)
        {
            if (LocalizationManager.Instance is null)
                return;

            LocalizationManager.Instance.ApplyLocalization(control, index);
        }

        /// <summary>
        /// Applies localization to a control using a strongly-typed state enum.
        /// Constructs a Strings key using the pattern: Control.{FormName}.{ControlName}.{StateName}
        /// </summary>
        /// <typeparam name="TEnum">The enum type representing the control's states.</typeparam>
        /// <param name="control">The control to localize.</param>
        /// <param name="state">The state to apply.</param>
        public static void ApplyLocalization<TEnum>(this Control control, TEnum state) where TEnum : Enum
        {
            if (LocalizationManager.Instance is null || control is null)
                return;

            var form = control.FindForm();
            if (form is null)
                return;

            var key = $"Control.{form.Name}.{control.Name}.{state}";
            var localized = LocalizationManager.Instance.GetString(key);

            if (control.InvokeRequired)
                control.BeginInvoke((MethodInvoker)delegate { control.Text = localized; });
            else
                control.Text = localized;
        }
    }
}
