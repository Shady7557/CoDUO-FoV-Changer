# CoDUO FoV Changer Translation Guide

This guide explains how to create translations for the CoDUO FoV Changer application.

## How to Create a Translation

1. Navigate to the localization folder:
   - Windows: `%AppData%\CoDUO FoV Changer\Localization\`
   - Or find the `Localization` folder in your installation directory

2. Copy `en-US.json` and rename it to your locale code (e.g., `de-DE.json` for German, `es-ES.json` for Spanish)

3. Open the file in a text editor and translate the strings

4. Restart the application - your language will automatically appear in the Language menu

## JSON File Structure

The localization file has two main sections:

### Strings Section

All translatable text lives in a single flat `Strings` dictionary using descriptive keys.
Keys use dot notation for organization:

| Type | Key Pattern | Example |
|------|-------------|---------|
| Single-state control | `Control.{Form}.{Control}` | `Control.MainForm.StartGameButton` |
| Multi-state control | `Control.{Form}.{Control}.{State}` | `Control.MainForm.StatusLabel.Success` |
| Error messages | `Error.*` | `Error.FailedToStartGame` |
| Info messages | `Info.*` | `Info.AutoDetectedPath` |
| Confirmations | `Confirm.*` | `Confirm.UpdateNow` |
| Hotkeys | `Hotkey.*` | `Hotkey.SetFoVUp` |
| Tooltips | `Tooltip.*` | `Tooltip.MainForm.StatusLabel.NotFound` |
| Time units | `Time.*` | `Time.Seconds` |
| UI strings | `UI.*` | `UI.MinimizedToTray.Title` |

#### Multi-State Controls

Some controls display different text depending on their state. These use named state keys:

```json
"Control.MainForm.StatusLabel.NotRunning": "Status: Not running.",
"Control.MainForm.StatusLabel.Success": "Status: Successfully wrote to game memory.",
"Control.MainForm.StatusLabel.RequiresElevation": "Status: Requires elevation (run as Admin).",

"Control.MainForm.CheckUpdatesLabel.Checking": "Checking for updates...",
"Control.MainForm.CheckUpdatesLabel.NoUpdatesFound": "No updates found. Click to check again.",
"Control.MainForm.CheckUpdatesLabel.UpdatesAvailable": "Updates available!",

"Control.ServersForm.FavoritesButton.ShowFavorites": "Show Favorites",
"Control.ServersForm.FavoritesButton.ShowAll": "Show All"
```

#### Time Units

Time strings use `{0}` as a placeholder for the numeric value:

```json
"Time.Seconds": "{0} seconds",
"Time.Minutes": "{0} minutes",
"Time.Hours": "{0} hours"
```

#### UI Strings

General UI text that doesn't belong to a specific control:

```json
"UI.MinimizedToTray.Title": "Minimized to Tray",
"UI.MinimizedToTray.Message": "{0} is minimized. Click to restore full-size.",
"UI.ServersForm.Title": "Server List",
"UI.ServersForm.TitleFavorites": "Server List (Favorites)",
"UI.Pinging": "Pinging...",
"UI.LocateInstallDirectory": "Locate your Call of Duty installation directory",
"UI.CopyColumn": "Copy {0}"
```

### Metadata Section

Information about your translation:

```json
"Metadata": {
  "LanguageName": "German",
  "LanguageNameNative": "Deutsch",
  "CultureCode": "de-DE",
  "Version": "1.0",
  "Author": "Your Name"
}
```

## Placeholder Syntax

Strings may contain placeholders that get replaced with dynamic values at runtime:

- `{0}` - First argument (e.g., error message, file path)
- `{1}` - Second argument
- `{2}` - Third argument
- `\n` - New line

Example:
```json
"Confirm.ConnectToServer": "Would you like to connect to this server?\n{0} ({1}:{2})"
```

This becomes: "Would you like to connect to this server?\nServer Name (192.168.1.1:28960)"

**Important:** Keep the placeholders in the same order as the original English text!

## Tips for Translators

1. **Do not translate placeholder markers** like `{0}`, `{1}` - keep them exactly as they are
2. **Keep special characters** like `\n` (newline) in the same positions
3. **Test your translation** in the app after making changes
4. **Update the Metadata** section with your language info and your name as author
5. **Keep punctuation consistent** with the original where appropriate
6. **The `Controls` section** in the JSON file should remain as an empty object `{}` - all control text is in `Strings`

## Common Culture Codes

| Language | Code |
|----------|------|
| English (US) | en-US |
| French (France) | fr-FR |
| German (Germany) | de-DE |
| Spanish (Spain) | es-ES |
| Italian (Italy) | it-IT |
| Portuguese (Brazil) | pt-BR |
| Russian | ru-RU |
| Polish | pl-PL |
| Chinese (Simplified) | zh-CN |
| Japanese | ja-JP |
| Korean | ko-KR |

## Submitting Your Translation

If you'd like to contribute your translation to the project:

1. Fork the repository on GitHub
2. Add your translation file to the `Localization` folder
3. Submit a pull request

Your contribution will help make CoDUO FoV Changer accessible to more players!

## Questions?

If you have questions about translating specific strings or need help, please open an issue on the GitHub repository.
