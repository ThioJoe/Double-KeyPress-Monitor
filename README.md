# Double Key Press Monitor

A Windows application that detects and logs unintended double keypresses from your keyboard, which is sometimes a problem for mechanical keyboards.

➤ **Note:** This tool is designed to quantify known or suspected key chatter. If you haven't noticed any annoying double-typing in your daily use, this app won't reveal any hidden issues.

<p align="center">
<img width="350" alt="Double Key Press Detector" src="https://github.com/user-attachments/assets/358059fb-f2b0-4efb-abfc-6a7cb58f1b77" />
</p>

## **Why?** 
This tool can be useful for:
- Determining which are the most problematic keys
- Understanding the severity of the problem in general (the frequency of double keypresses)
- Providing evidence for keyboard warranty replacements due to key chatter.

## Features
- Configurable delay threshold in milliseconds
- Optional ignore list for specific keys
- Customizeable audio indication on detection
- Detected events logged to local `double_press_log.log` file
- Button to generate a command to launch the app with current settings

## Download and Installation
- Navigate to the repository [Releases](https://github.com/ThioJoe/Double-KeyPress-Monitor/releases) page.
- Under the "Assets" section of the latest release, download `Double-KeyPress-Monitor.exe`
- Run the exe. No installation is required.

## Usage
* Define the threshold (ms).
   *   Set this value just below the fastest delay you can intentionally produce when pressing a key rapidly.
* Enter Virtual Key (VK) codes to ignore as a comma-separated list
  * For example: `46, 8` for Delete and Backspace.
  * Hexadecimal formats (Such as `0x2E`) are also supported.
* Provide a sound alias or absolute/relative file path to play an alert on detection.
  * Leave blank to default to the Windows 'Speech Misrecognition' sound.
* Click "Start Monitoring" to begin monitoring and "Stop Monitoring" to stop.

## Command Line Arguments
These arguments determine how the app launches:
- `-start-on-launch`: Start monitoring immediately upon the app launching. (Equivalent to clicking "Start Monitoring")
- `-minimized`: Start the application minimized in the taskbar.

These arguments change initial settings that are also configurable in the GUI:
- `-threshold <ms>`: Set the delay threshold in milliseconds for detecting double key presses.
- `-sound-alias "<alias_or_path>"`: Specify a custom system sound alias or file path to play when a double keypress is detected.
- `-ignore-keys "<vk_codes>"`: Specify a comma-separated list of virtual key codes to ignore.

## Compilation
1. Open `Monitor-Double-Keypresses.sln` using Visual Studio 2022 or 2026.
2. Uses .NET Framework 4.8, no third party packages required.
4. Build the solution (`Build > Build Solution`) as either Release or Debug.
