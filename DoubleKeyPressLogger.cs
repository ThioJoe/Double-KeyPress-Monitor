using System;
using System.IO;
using System.Text.Json;
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
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "double_press_log.json");
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
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    // Don't bother encoding & character
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                };
                // Serialize the single entry to JSON
                string jsonEntry = JsonSerializer.Serialize(logEntry, options);

                // Use lock for thread-safe file access
                lock (_lock)
                {
                    // Append the JSON entry as a new line
                    File.AppendAllText(LogFilePath, jsonEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                // Basic error handling: Log to debug output or console
                System.Diagnostics.Debug.WriteLine($"Error logging double press: {ex.Message}");
                // Consider more robust error handling for a production app
            }
        }
    }
}