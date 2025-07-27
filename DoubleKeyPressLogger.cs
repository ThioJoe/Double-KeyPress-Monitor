using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms; // Required for Keys enum

namespace DoubleKeyPressDetector
{
    // Represents the data for a single logged event
    public class DoublePressLogEntry
    {
        public string KeyName { get; set; }
        public int VirtualKeyCode { get; set; }
        public long TimeDelayMilliseconds { get; set; }
        public DateTime Timestamp { get; set; }
        public string DevicePath { get; set; }
    }

    public static class DoubleKeyPressLogger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "double_press_log.log");
        private static readonly object _lock = new object(); // For thread safety

        public static void LogEvent(int vkCode, long delayMs, string devicePath)
        {
            string keyName = Enum.GetName(typeof(Keys), vkCode) ?? $"VK_{vkCode:X2}";

            DoublePressLogEntry logEntry = new DoublePressLogEntry
            {
                KeyName = keyName,
                VirtualKeyCode = vkCode,
                TimeDelayMilliseconds = delayMs,
                Timestamp = DateTime.Now,
                DevicePath = devicePath
            };

            // Use a non-blocking approach for logging if possible,
            // or at least minimize lock duration. For simplicity here, using lock.
            ThreadPool.QueueUserWorkItem(_ => WriteLog(logEntry));
        }

        private static void WriteLog(DoublePressLogEntry logEntry)
        {
            try
            {
                // Ensure the log file exists
                if (!File.Exists(LogFilePath))
                {
                    CreateLogIfNecessary();
                }

                string formattedTime = logEntry.Timestamp.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss.fff tt");

                StringBuilder logLine = new StringBuilder();
                logLine.Append($"KeyName: {logEntry.KeyName}\t\t");
                logLine.Append($"TimeDelayMilliseconds: {logEntry.TimeDelayMilliseconds}\t\t");
                logLine.Append($"VirtualKeyCode: {logEntry.VirtualKeyCode}\t\t");
                logLine.Append($"Timestamp: {formattedTime}\t\t");
                logLine.Append($"DevicePath: {logEntry.DevicePath}");

                // Use lock for thread-safe file access
                lock (_lock)
                {
                    // Append the entry as a new line
                    File.AppendAllText(LogFilePath, logLine + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                // Basic error handling: Log to debug output or console
                System.Diagnostics.Debug.WriteLine($"Error logging double press: {ex.Message}");
            }
        }

        public static void OpenLogFile()
        {
            try
            {
                // Open the log file with the default text editor
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = LogFilePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur when trying to open the file
                MessageBox.Show($"Error opening log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateLogIfNecessary()
        {
            // Ensure the log file exists
            if (!File.Exists(LogFilePath))
            {
                try
                {
                    // Create the file if it doesn't exist
                    using (File.Create(LogFilePath)) { }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during file creation
                    MessageBox.Show($"Error creating log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}