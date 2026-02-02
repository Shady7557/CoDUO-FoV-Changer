# CoDUO FoV Changer Translation Guide

This guide explains how to create translations for the CoDUO FoV Changer application.

## How to Create a Translation

1. Navigate to the localization folder:
   - Windows: `%AppData%\CoDUO FoV Changer\Localization\`
   - Or find the `Localization` folder in your installation directory

2. Copy `en-US.json` and rename it to your locale code (e.g., `de-DE.json` for German, `es-ES.json` for Spanish)

3. Open the file in a text editor and translate the strings

4. Restart the application and select your language from the Language menu

## JSON File Structure

The localization file has three main sections:

### Controls Section
Contains UI control text. Keys are in format `FormName.ControlName`.
Some controls have multiple states (0, 1, 2...) for different UI states.

```json
"Controls": {
  "MainForm.StatusLabel": {
    "0": "Status: Not running",
    "1": "Status: Success!",
    "2": "Status: Game requires elevation!"
  }
}
```

### Strings Section
Contains all other strings (messages, tooltips, confirmations, etc.).
Keys use dot notation for organization:

- `Error.*` - Error messages
- `Info.*` - Informational messages
- `Confirm.*` - Confirmation dialogs
- `Hotkey.*` - Hotkey-related messages
- `Tooltip.*` - Tooltip strings

```json
"Strings": {
  "Error.FailedToStartGame": "Failed to start game: {0}\nPlease refer to the log for more info.",
  "Info.AutoDetectedPath": "Automatically detected game path:\n{0}",
  "Confirm.UpdateNow": "Are you sure you want to update now?"
}
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
