using Localization;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer.Forms
{
    /// <summary>
    /// Helper class for displaying localized MessageBox dialogs.
    /// Uses string keys from the localization system with optional placeholder support.
    /// </summary>
    public static class LocalizedMessageBox
    {
        /// <summary>
        /// Shows a MessageBox with a localized message.
        /// </summary>
        /// <param name="key">The localization key (e.g., "Error.FailedToStart")</param>
        /// <param name="args">Optional format arguments for placeholders {0}, {1}, etc.</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult Show(string key, params object[] args)
        {
            return MessageBox.Show(LocalizationManager.L(key, args), Application.ProductName);
        }

        /// <summary>
        /// Shows a MessageBox with specified buttons and icon.
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="buttons">The MessageBox buttons</param>
        /// <param name="icon">The MessageBox icon</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult Show(string key, MessageBoxButtons buttons, MessageBoxIcon icon, params object[] args)
        {
            return MessageBox.Show(LocalizationManager.L(key, args), Application.ProductName, buttons, icon);
        }

        /// <summary>
        /// Shows an error MessageBox (OK button, Error icon).
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult ShowError(string key, params object[] args)
        {
            return Show(key, MessageBoxButtons.OK, MessageBoxIcon.Error, args);
        }

        /// <summary>
        /// Shows an informational MessageBox (OK button, Information icon).
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult ShowInfo(string key, params object[] args)
        {
            return Show(key, MessageBoxButtons.OK, MessageBoxIcon.Information, args);
        }

        /// <summary>
        /// Shows a confirmation MessageBox (YesNo buttons, Question icon).
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult ShowConfirm(string key, params object[] args)
        {
            return Show(key, MessageBoxButtons.YesNo, MessageBoxIcon.Question, args);
        }

        /// <summary>
        /// Shows a warning MessageBox (OK button, Warning icon).
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult ShowWarning(string key, params object[] args)
        {
            return Show(key, MessageBoxButtons.OK, MessageBoxIcon.Warning, args);
        }

        /// <summary>
        /// Shows a confirmation MessageBox with Yes/No/Cancel buttons.
        /// </summary>
        /// <param name="key">The localization key</param>
        /// <param name="args">Optional format arguments for placeholders</param>
        /// <returns>The DialogResult from the MessageBox.</returns>
        public static DialogResult ShowConfirmWithCancel(string key, params object[] args)
        {
            return Show(key, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, args);
        }
    }
}
