using Monitor_Double_Keypresses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

            public static List<Argument> All { get; } = new List<Argument>
            {
                Threshold,
                Minimized,
                StartOnLaunch,
                SoundAlias
            };
        }

        // -----------------------------------------------------------------------------

        public MainForm(string[] args)
        {
            InitializeComponent();
            // Initial UI state
            buttonStop.Enabled = false;
            labelStatus.ForeColor = Color.Red;
            this.Text = "Double Key Press Detector"; // Set form title
            bool startNow = false;

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
                }

                // After all the arguments are processed, start monitoring if requested
                if (startNow)
                {
                    StartMonitor();
                }

            }
        }

        private void StartMonitor()
        {
            int threshold = (int)numericUpDownThreshold.Value;

            // Pass the labelStatus for UI feedback from RawInputHandler
            bool started = RawInputHandler.InitializeRawInput(this.Handle, labelStatus, threshold);

            if (started)
            {
                // Subscribe to the event *after* successful initialization
                RawInputHandler.DoublePressDetected += RawInputHandler_DoublePressDetected;

                // Update UI
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                numericUpDownThreshold.Enabled = false; // Disable changing threshold while running
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
            // Call the logger
            DoubleKeyPressLogger.LogEvent(e.VirtualKeyCode, e.DelayMilliseconds, e.DevicePath);
            // Play sound if specified
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
            foreach (var arg in AvailableArgs.All)
            {
                message += $"{arg.Arg}: {arg.Description}\n\n";
            }
            MessageBox.Show(message, "Command Line Arguments", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            // Open the log file in the default text editor
             DoubleKeyPressLogger.OpenLogFile();

        }

    }
}