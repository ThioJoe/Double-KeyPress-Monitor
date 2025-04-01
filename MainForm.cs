using Monitor_Double_Keypresses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DoubleKeyPressDetector
{
    public partial class MainForm : Form
    {
        public Dictionary<string, string> argsInfo = new Dictionary<string, string>
        {
            { "-threshold", "Set the delay threshold in milliseconds for detecting double key presses." },
            { "-minimized", "Start the application minimized in the taskbar." },
            { "-start-on-launch", "Start monitoring for double key presses immediately on launch."  }
        };

        public MainForm(string[] args)
        {
            InitializeComponent();
            // Initial UI state
            buttonStop.Enabled = false;
            labelStatus.ForeColor = Color.Red;
            this.Text = "Double Key Press Detector"; // Set form title

            // Optional: Load settings from command line arguments
            // Supported arguments: -threshold -minimized -start-on-launch
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {
                        case "-threshold":
                            if (i + 1 < args.Length && int.TryParse(args[i + 1], out int threshold))
                            {
                                numericUpDownThreshold.Value = threshold;
                            }
                            break;
                        case "-minimized":
                            this.WindowState = FormWindowState.Minimized;
                            break;
                        case "-start-on-launch":
                            StartMonitor();
                            break;
                    }
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

            // Optional: Update UI or provide feedback (careful with threading)
            // If you need to update UI controls from here, use BeginInvoke:
            // this.BeginInvoke(new Action(() => { /* Update UI elements */ }));
            // For example:
            // this.BeginInvoke(new Action(() => {
            //    listBoxLog.Items.Insert(0, $"Double Press: VK={e.VirtualKeyCode:X2}, Delay={e.DelayMilliseconds}ms");
            // }));
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
            CustomSystemSounds.PlayCustomSound(textBoxSoundAlias.Text);
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            // Display message box with argument information
            string message = "Command line arguments supported:\n\n";
            foreach (var kvp in argsInfo)
            {
                message += $"{kvp.Key}: {kvp.Value}\n\n";
            }
            MessageBox.Show(message, "Command Line Arguments", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        // Auto-generated InitializeComponent - ensure controls match

    }
}