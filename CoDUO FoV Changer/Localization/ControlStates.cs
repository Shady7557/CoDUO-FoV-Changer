namespace Localization
{
    /// <summary>
    /// States for MainForm.StatusLabel - displays current FoV write status.
    /// </summary>
    public enum StatusLabelState
    {
        NotRunning = 0,
        Success = 1,
        RequiresElevation = 2
    }

    /// <summary>
    /// States for MainForm.CheckUpdatesLabel - displays update check status.
    /// </summary>
    public enum CheckUpdatesLabelState
    {
        Checking = 0,
        NoUpdatesFound = 1,
        UpdatesAvailable = 2
    }

    /// <summary>
    /// States for ServersForm.FavoritesButton - toggles between showing all servers or favorites only.
    /// </summary>
    public enum FavoritesButtonState
    {
        ShowFavorites = 0,
        ShowAll = 1
    }
}
