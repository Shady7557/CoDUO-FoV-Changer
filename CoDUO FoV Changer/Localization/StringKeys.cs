namespace Localization
{
    /// <summary>
    /// Strongly-typed string keys for localization.
    /// Using these constants instead of raw strings provides compile-time safety.
    /// </summary>
    public static class StringKeys
    {
        #region Errors

        public const string ErrorFailedToStartGame = "Error.FailedToStartGame";
        public const string ErrorSteamNotRunningStartAttempt = "Error.SteamNotRunning.StartAttempt";
        public const string ErrorSteamNotRunningPleaseStart = "Error.SteamNotRunning.PleaseStart";
        public const string ErrorAppPathNotExist = "Error.AppPathNotExist";
        public const string ErrorAppPathNotExistUpdate = "Error.AppPathNotExistUpdate";
        public const string ErrorFailedHotkeysForm = "Error.FailedHotkeysForm";
        public const string ErrorFailedCDKeyForm = "Error.FailedCDKeyForm";
        public const string ErrorGenericError = "Error.GenericError";
        public const string ErrorUpdateFailed = "Error.UpdateFailed";
        public const string ErrorProcessListFailed = "Error.ProcessListFailed";
        public const string ErrorSelectedNotExecutable = "Error.SelectedNotExecutable";
        public const string ErrorUnableToDetermineGameName = "Error.UnableToDetermineGameName";
        public const string ErrorUnableToDetermineGameVersion = "Error.UnableToDetermineGameVersion";
        public const string ErrorFailedToGrabServerList = "Error.FailedToGrabServerList";
        public const string ErrorNoServersFound = "Error.NoServersFound";

        #endregion

        #region Info

        public const string InfoAutoDetectedPath = "Info.AutoDetectedPath";
        public const string InfoSetInstallPath = "Info.SetInstallPath";
        public const string InfoSelectExePrompt = "Info.SelectExePrompt";
        public const string InfoSelectedExecutable = "Info.SelectedExecutable";
        public const string InfoCheckingTooQuickly = "Info.CheckingTooQuickly";

        #endregion

        #region Confirmations

        public const string ConfirmUpdateNow = "Confirm.UpdateNow";
        public const string ConfirmCloseWithoutSaving = "Confirm.CloseWithoutSaving";
        public const string ConfirmConnectToServer = "Confirm.ConnectToServer";

        #endregion

        #region Hotkeys

        public const string HotkeySetFoVUp = "Hotkey.SetFoVUp";
        public const string HotkeySetFoVDown = "Hotkey.SetFoVDown";
        public const string HotkeySetModifier = "Hotkey.SetModifier";

        #endregion

        #region Tooltips

        public const string TooltipMinimizeCheckBox = "Tooltip.MainForm.MinimizeCheckBox";
        public const string TooltipDesktopRes = "Tooltip.MainForm.checkBoxDesktopRes";
        public const string TooltipStatusNotFound = "Tooltip.MainForm.StatusLabel.NotFound";
        public const string TooltipStatusRequiresElevation = "Tooltip.MainForm.StatusLabel.RequiresElevation";
        public const string TooltipStatusFailedWrite = "Tooltip.MainForm.StatusLabel.FailedWrite";

        #endregion
    }
}
