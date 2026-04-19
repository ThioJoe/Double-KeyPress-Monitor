// Monitor-Double-Keypresses/MainForm.cs
//using IWshRuntimeLibrary;
using Monitor_Double_Keypresses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

#nullable enable

namespace DoubleKeyPressDetector
{
    public partial class MainForm : Form
    {
        public class Argument(string arg, string description)
        {
            public string Arg { get; private set; } = arg;
            public string Description { get; private set; } = description;
        }

        public static class AvailableArgs
        {
            // Define static properties for each specific argument
            public static Argument Threshold { get; } = new Argument("-threshold", "Set the delay threshold in milliseconds for detecting double key presses.");

            public static Argument Minimized { get; } = new Argument("-minimized", "Start the application minimized in the taskbar.");

            public static Argument StartOnLaunch { get; } = new Argument("-start-on-launch", "Start monitoring for double key presses immediately on launch.");

            public static Argument SoundAlias { get; } = new Argument("-sound-alias", "Specify a custom system sound alias or file path to play when a double keypress is detected.");

            public static Argument IgnoreKeys { get; } = new Argument("-ignore-keys", "Specify a comma-separated list of virtual key codes to ignore for double-press detection.");

            public static List<Argument> All { get; } = new List<Argument>
            {
                Threshold,
                Minimized,
                StartOnLaunch,
                SoundAlias,
                IgnoreKeys
            };
        }

        // Get the app version number from the assembly details
        public string AppVersion
        {
            get
            {
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return version != null ? version.ToString() : "";
            }
        }


        // -----------------------------------------------------------------------------

        public MainForm(string[] args)
        {
            InitializeComponent();
            // Initial UI state
            buttonStop.Enabled = false;
            labelStatus.ForeColor = Color.Red;
            bool startNow = false;

            // Set form title
            string titleText = $"Double Key Press Monitor";
            if (AppVersion != "") { titleText += $" - {AppVersion}"; };
            this.Text = titleText; 

            // Optional: Load settings from command line arguments
            // Supported arguments: -threshold -minimized -start-on-launch
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (string.Equals(args[i], AvailableArgs.Threshold.Arg, StringComparison.OrdinalIgnoreCase))
                    {
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int threshold))
                        {
                            numericUpDownThreshold.Value = threshold;
                        }
                    }
                    else if (string.Equals(args[i], AvailableArgs.Minimized.Arg, StringComparison.OrdinalIgnoreCase))
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                    else if (string.Equals(args[i], AvailableArgs.StartOnLaunch.Arg, StringComparison.OrdinalIgnoreCase))
                    {
                        startNow = true;
                    }
                    else if (string.Equals(args[i], AvailableArgs.SoundAlias.Arg, StringComparison.OrdinalIgnoreCase))
                    {
                        if (i + 1 < args.Length)
                        {
                            textBoxSoundAlias.Text = args[i + 1];
                        }
                    }
                    else if (string.Equals(args[i], AvailableArgs.IgnoreKeys.Arg, StringComparison.OrdinalIgnoreCase))
                    {
                        if (i + 1 < args.Length)
                        {
                            List<int> ignoredKeys = ValidateAndSplitIgnoredKeysString(args[i + 1]);
                            textBoxIgnore.Text = string.Join(", ", ignoredKeys);
                        }
                    }
                }

                // After all the arguments are processed, start monitoring if requested
                if (startNow)
                {
                    StartMonitor();
                }
            }
        }

        // 
        private List<int> ValidateAndSplitIgnoredKeysString(string rawStringIgnoredKeys)
        {
            List<int> ignoredKeys = [];
            List<string> invalidParts = [];

            // Trim quotes and spaces
            string[] parts = rawStringIgnoredKeys.Trim().Trim('"').Trim('\'')
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                string trimmedPart = part.Trim();
                if (int.TryParse(trimmedPart, out int keyCode))
                {
                    ignoredKeys.Add(keyCode);
                }
                else if (trimmedPart.StartsWith("0x", StringComparison.OrdinalIgnoreCase) &&
                         int.TryParse(trimmedPart.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out keyCode))
                {
                    ignoredKeys.Add(keyCode);
                }
                else
                {
                    invalidParts.Add(trimmedPart);
                }
            }

            if (invalidParts.Count > 0)
            { 
                string invalidPartsMsg = string.Join("\n    ", invalidParts);
                MessageBox.Show($"The following values in the ignored keys box are invalid:\n    {invalidPartsMsg}", "Warning: Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ignoredKeys;
        }

        private void StartMonitor()
        {
            DoubleKeyPressLogger.CreateLogIfNecessary();

            int threshold = (int)numericUpDownThreshold.Value;

            // The minimum on the UpDown control should already be 1, but just in case, make sure the resulting threshold is always 1 or more
            if (threshold <=0)
            {
                threshold = 1;
                numericUpDownThreshold.Value = threshold;
            }

            List<int> ignoredKeys = ValidateAndSplitIgnoredKeysString(textBoxIgnore.Text);

            // Pass the labelStatus for UI feedback from RawInputHandler
            bool started = RawInputHandler.InitializeRawInput(this.Handle, labelStatus, threshold, ignoredKeys);

            if (started)
            {
                // Subscribe to the event *after* successful initialization
                RawInputHandler.DoublePressDetected += RawInputHandler_DoublePressDetected;

                // Update UI
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                numericUpDownThreshold.Enabled = false; // Disable changing threshold while running
                textBoxSoundAlias.Enabled = false; // Disable sound alias input while running
                textBoxIgnore.Enabled = false; // Disable ignore keys input while running
                labelStatus.Text = "Status: Running"; // Update status label directly here too
                labelStatus.ForeColor = Color.Green;
            }
            else
            {
                MessageBox.Show("Failed to start RawInput monitoring. Check console/debug output.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelStatus.Text = "Status: Error";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private void StopMonitor()
        {
            // Unsubscribe *before* cleanup
            RawInputHandler.DoublePressDetected -= RawInputHandler_DoublePressDetected;

            RawInputHandler.CleanupInputWatcher();

            // Update UI
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            numericUpDownThreshold.Enabled = true;
            textBoxSoundAlias.Enabled = true; // Re-enable sound alias input
            textBoxIgnore.Enabled = true; // Re-enable ignore keys input
            labelStatus.Text = "Status: Stopped"; // Update status label
            labelStatus.ForeColor = Color.Red;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartMonitor();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopMonitor();
        }

        // Event handler called by RawInputHandler when a double press occurs
        private void RawInputHandler_DoublePressDetected(object sender, DoublePressEventArgs e)
        {
            // Queue this entire operation to the thread pool to prevent blocking the raw input message loop
            System.Threading.ThreadPool.QueueUserWorkItem(_ => ProcessDoublePressEvent(e));
        }

        private void ProcessDoublePressEvent(DoublePressEventArgs e)
        {
            // Call the logger (which already uses thread pool internally)
            DoubleKeyPressLogger.LogEvent(e.VirtualKeyCode, e.DelayMilliseconds, e.DevicePath, e.PreviousPressTimestamp, e.CurrentPressTimestamp);
            
            // Play sound if specified (also on background thread now)
            if (checkBoxPlaySound.Checked == true)
            {
                CustomSystemSounds.PlayCustomSoundFromTextbox(textBoxSoundAlias.Text);
            }
        }

        // Ensure cleanup when the form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Check if monitoring is active before attempting cleanup
            if (RawInputHandler.RawInputWatcherActive)
            {
                // Unsubscribe and cleanup
                RawInputHandler.DoublePressDetected -= RawInputHandler_DoublePressDetected;
                RawInputHandler.CleanupInputWatcher();
            }
            base.OnFormClosing(e);
        }

        private void buttonPreviewSound_Click(object sender, EventArgs e)
        {
            PlaySound();
        }

        private void PlaySound()
        {
            // Play Windows "Speech Misrecognition" sound
            CustomSystemSounds.PlayCustomSoundFromTextbox(textBoxSoundAlias.Text);
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            // Display message box with argument information
            string message = "Command line arguments supported:\n\n";
            // Loop through and show each argument
            foreach (var arg in AvailableArgs.All)
            {
                message += $"{arg.Arg}: {arg.Description}\n\n";
            }

            MessageBox.Show(message, "Command Line Arguments", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            DoubleKeyPressLogger.CreateLogIfNecessary();
            // Open the log file in the default text editor
            DoubleKeyPressLogger.OpenLogFile();

        }

        private void buttonSoundHelp_Click(object sender, EventArgs e)
        {
            string message = "You can enter a Windows sound alias, or sound file to play when a double keypress is detected.\n\n" +
                "Supported Values:\n" +
                "- Full absolute path to a sound file\n" +
                "- Just a File Name: (It will look in C:\\Windows\\Media)\n" +
                "- Relative path to a sound file (Relative to C:\\Windows\\Media)\n" +
                "- The \"Alias\" of the sound, a list of which can be found in the registry at: HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\n\n" +
                "If you leave this blank, the 'Speech Misrecognition' system sound will be played.";

            MessageBox.Show(message, "Sound Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonIgnoreHelp_Click(object sender, EventArgs e)
        {
            string message = "You can specify keys to ignore for double-press detection by entering their Virtual Key Codes here (either hex or decimal), separated by commas.\n\n" +
                "For example, to ignore the Delete (46) and Backspace (8) keys, enter either:\n46,8   or   0x2E,0x08\n\n" +
                "This is useful for keys that are often pressed rapidly as part of normal usage, so you can set a higher threshold for other keys without false positives.\n\n" +
                "You can see a key's Virtual Key Code value in the log file.\n\n" +
                "Some Common Virtual Key Codes:\n" +
                "   Backspace: 8\n" +
                "   Delete: 46\n" +
                "   Space: 32";


            MessageBox.Show(message, "Sound Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonCreateShortcut_Click(object sender, EventArgs e)
        {
            string executablePath = Application.ExecutablePath;

            // Set the current settings as arguments
            var args = new List<string>();

            // Threshold
            if (numericUpDownThreshold.Value > 0)
            {
                args.Add(AvailableArgs.Threshold.Arg);
                args.Add(numericUpDownThreshold.Value.ToString());
            } 
            else
            {
                MessageBox.Show("Warning: Invalid threshold value. Must be positive. Will use default.");
            }

            // Sound Alias
            if (!string.IsNullOrEmpty(textBoxSoundAlias.Text))
            {
                args.Add(AvailableArgs.SoundAlias.Arg);
                args.Add($"\"{textBoxSoundAlias.Text}\"");
            }

            // Ignored Keys
            if (!string.IsNullOrEmpty(textBoxIgnore.Text))
            {
                List<int> validatedIgnoreKeys = ValidateAndSplitIgnoredKeysString(textBoxIgnore.Text);
                string validatedIgnoreKeysString = string.Join(", ", validatedIgnoreKeys);

                args.Add(AvailableArgs.IgnoreKeys.Arg);
                args.Add($"\"{validatedIgnoreKeysString}\"");
            }

            // Minimized and start on launch (always added)
            args.Add(AvailableArgs.Minimized.Arg);
            args.Add(AvailableArgs.StartOnLaunch.Arg);

            // Combine and copy to clipboard
            string fullCommand = $"\"{executablePath}\" {string.Join(" ", args)}";
            if (!string.IsNullOrEmpty(fullCommand))
            {
                Clipboard.SetText(fullCommand);
                ShowLabelForTime(labelCopyCheck, 300);
            }
        }

        private async void ShowLabelForTime(Label label, int milliseconds)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke((Action)(() => ShowLabelForTime(label, milliseconds)));
                return;
            }

            label.Visible = true;
            await Task.Delay(milliseconds);
            label.Visible = false;
        }

        private void buttonCopyCommandHelp_Click(object sender, EventArgs e)
        {
            string message = "Copies a command to launch the app with the current active settings.\n\n" +
                "You can then paste it as the target for a shortcut or some other script, for example.\n\n" +
                "Tip: To make it start with windows, you can put the shortcut in: \n%APPDATA%\\Microsoft\\Windows\\Start Menu\\Programs\\Startup \n\n" +
                "Note:  The \"-minimized\" and \"-start-on-launch\" args is always added. Just remove those if you don't want that.";

            MessageBox.Show(message, "Sound Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonRemoveLastEntryHelp_Click(object sender, EventArgs e)
        {
            string message = "If the app registered a double keypress you know was intentional, press this button to remove the most recent line from the log file.";

            MessageBox.Show(message);
        }

        private void buttonRemoveLastEntry_Click(object sender, EventArgs e)
        {
            bool result = DoubleKeyPressLogger.RemoveLastEntry();

            if (result == true)
            {
                ShowLabelForTime(labelRemoveLastEntry, 300);
            }
        }

        private void buttonThresholdHelp_Click(object sender, EventArgs e)
        {
            string message = "" +
                "To catch the most erroneous keypresses, you should set the threshold time as high as you can, " +
                "but still less than the max speed you'd naturally be able to actually press keys.\n" +
                "\n" +
                "To test this, set the threshold to something very very high like 500ms, then mash a single key as fast as you can, " +
                "and look in the log for roughly the shortest delay you managed to produce between any of the keypresses. Then set " +
                "your threshold just below, or right around that delay. That should safely prevent any false-positives even in the case where you have to mash a button like in a game.\n\n" +
                "Remember:\nThe duplicate threshold delay applies to each individual key, so if you press 'A' then immediately 'B' it will obviously not count. " +
                "And it is still able to detect if an erroneous second 'A' keystroke occurs after the 'B' keystroke for example.\n\n";

            MessageBox.Show(message, "Threshold Tips", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}