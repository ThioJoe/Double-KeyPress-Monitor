// Monitor-Double-Keypresses/RawInputHandler.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using static DoubleKeyPressDetector.WinEnums.RAWKEYBOARD; // Assuming WinEnums is in this namespace
using System.Windows.Forms;

#nullable enable

namespace DoubleKeyPressDetector
{
    internal static class RawInputHandler
    {
        // --- Existing P/Invoke declarations from your example ---
        [DllImport("user32.dll")]
        private static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray)] RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

        [DllImport("user32.dll")]
        private static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr DefRawInputProc(IntPtr paRawInput, Int32 nInput, UInt32 cbSizeHeader);

        [DllImport("user32.dll")]
        static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam);
        // --- End P/Invoke ---

        private static bool _rawInputWatcherActive = false;
        private static Label? WatcherActiveLabelReference = null; // Optional UI feedback

        // --- Double Press Detection Logic ---
        private static readonly Stopwatch _stopwatch = new Stopwatch(); // For timing key presses
        public static int DoublePressThresholdMs { get; set; } = 10; // Default threshold
        public static List<int> IgnoredVkCodes { get; } = new List<int>(); // List of VK codes to ignore>

        // Event to signal a double press was detected
        public static event EventHandler<DoublePressEventArgs>? DoublePressDetected;

        // Arrays to store the last press timestamp and key state for each VK code
        private static readonly long[] _lastKeyTimestamps = new long[256];
        private static readonly bool[] _isKeyDown = new bool[256];

        // Delegate for the window procedure
        private delegate IntPtr WndProcDelegate(IntPtr hwnd, WinEnums.WM_MESSAGE msg, UIntPtr wParam, IntPtr lParam);
        private static WndProcDelegate? wndProcDelegate;
        private static IntPtr originalWndProc = IntPtr.Zero;
        private const int GWLP_WNDPROC = -4;
        private static IntPtr targetWindowHandle = IntPtr.Zero; // Store the target window handle

        public static readonly uint rawInputHeaderSize = (uint)Marshal.SizeOf<RAWINPUTHEADER>();
        public static readonly uint rawInputSize = (uint)Marshal.SizeOf<RAWINPUT>();


        public static bool RawInputWatcherActive
        {
            get => _rawInputWatcherActive;
            private set // Make setter private to control state internally
            {
                if (_rawInputWatcherActive != value)
                {
                    _rawInputWatcherActive = value;
                    UpdateLabel(value ? "Started" : "Stopped", value ? System.Drawing.Color.Green : System.Drawing.Color.Red);

                    if (value)
                    {
                        // Reset state when starting
                        _stopwatch.Restart(); // Start/Restart stopwatch when monitoring starts
                    }
                    else
                    {
                        _stopwatch.Stop(); // Stop stopwatch when monitoring stops
                    }
                }
            }
        }

        public static bool InitializeRawInput(IntPtr hwnd, Label? labelToUpdate = null, int thresholdMs = 10, List<int>? ignoredVKs = null)
        {
            if (RawInputWatcherActive) return true; // Already active

            IgnoredVkCodes.Clear();
            if (ignoredVKs != null)
            {
                IgnoredVkCodes.AddRange(ignoredVKs);
            }

            Array.Clear(_isKeyDown, 0, _isKeyDown.Length);
            Array.Clear(_lastKeyTimestamps, 0, _lastKeyTimestamps.Length);
            WatcherActiveLabelReference = labelToUpdate;
            targetWindowHandle = hwnd; // Store handle
            DoublePressThresholdMs = thresholdMs; // Set threshold

            // Only subclass once
            if (originalWndProc == IntPtr.Zero)
            {
                if (!SubclassWindow(hwnd))
                {
                    UpdateLabel("Subclass Failed", System.Drawing.Color.Red);
                    return false;
                }
            }

            RAWINPUTDEVICE[] rid =
            [
                new()
                {
                    usUsagePage = (ushort)WinEnums.HIDUsagePage.HID_USAGE_PAGE_GENERIC,
                    usUsage = (ushort)WinEnums.HIDGenericDesktopUsage.HID_USAGE_GENERIC_KEYBOARD,
                    dwFlags = WinEnums.RAWINPUTDEVICE._dwFlags.RIDEV_INPUTSINK | WinEnums.RAWINPUTDEVICE._dwFlags.RIDEV_NOLEGACY, // INPUTSINK crucial for background monitoring, NOLEGACY prevents WM_KEYDOWN etc.
                    hwndTarget = hwnd // Target the specific window
                },
            ];

            if (!RegisterRawInputDevices(rid, 1, (uint)Marshal.SizeOf<WinEnums.RAWINPUTDEVICE>()))
            {
                UpdateLabel("Register Failed", System.Drawing.Color.Red);
                RawInputWatcherActive = false; // Ensure state is correct
                                               // Attempt to unsubclass if registration fails after subclassing
                if (originalWndProc != IntPtr.Zero)
                {
                    UnSubclassWindow(hwnd);
                }
                return false;
            }

            RawInputWatcherActive = true; // Set state AFTER successful registration
            return true;
        }

        public static void CleanupInputWatcher()
        {
            if (!RawInputWatcherActive) return; // Only cleanup if active

            RAWINPUTDEVICE[] rid =
            [
                new()
                {
                    usUsagePage = (ushort)WinEnums.HIDUsagePage.HID_USAGE_PAGE_GENERIC,
                    usUsage = (ushort)WinEnums.HIDGenericDesktopUsage.HID_USAGE_GENERIC_KEYBOARD,
                    dwFlags = WinEnums.RAWINPUTDEVICE._dwFlags.RIDEV_REMOVE,
                    hwndTarget = IntPtr.Zero // Must be IntPtr.Zero on remove
                },
            ];

            // Attempt to unregister the device
            RegisterRawInputDevices(rid, 1, (uint)Marshal.SizeOf<WinEnums.RAWINPUTDEVICE>());
            Array.Clear(_isKeyDown, 0, _isKeyDown.Length);
            Array.Clear(_lastKeyTimestamps, 0, _lastKeyTimestamps.Length);

            // Unsubclass the window IF it was originally subclassed by this instance
            if (originalWndProc != IntPtr.Zero && targetWindowHandle != IntPtr.Zero)
            {
                UnSubclassWindow(targetWindowHandle);
                targetWindowHandle = IntPtr.Zero; // Clear handle after unsubclassing
            }


            RawInputWatcherActive = false; // Set state AFTER cleanup attempts
        }

        private static bool SubclassWindow(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) return false;
            if (originalWndProc != IntPtr.Zero) return true; // Already subclassed

            wndProcDelegate = WndProc; // Keep a reference
            IntPtr wndProcPtr = Marshal.GetFunctionPointerForDelegate(wndProcDelegate);
            originalWndProc = SetWindowLongPtr(hwnd, GWLP_WNDPROC, wndProcPtr);

            if (originalWndProc == IntPtr.Zero)
            {
                // Failed to subclass - check Marshal.GetLastWin32Error()
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine($"Failed to subclass window. Error code: {error}");
                return false;
            }
            return true;
        }

        private static void UnSubclassWindow(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero || originalWndProc == IntPtr.Zero) return;

            SetWindowLongPtr(hwnd, GWLP_WNDPROC, originalWndProc);
            originalWndProc = IntPtr.Zero; // Reset state
            wndProcDelegate = null; // Release delegate reference
        }


        private static IntPtr WndProc(IntPtr hWnd, WinEnums.WM_MESSAGE uMsg, UIntPtr wParam, IntPtr lParam)
        {
            // Only process input if active and the message is WM_INPUT
            if (RawInputWatcherActive && uMsg == WinEnums.WM_MESSAGE.WM_INPUT)
            {
                uint dwSize = 0;
                long currentTimestamp = _stopwatch.ElapsedMilliseconds;

                // First call to get the required size
                GetRawInputData(lParam, (uint)WinEnums.uiCommand.RID_INPUT, IntPtr.Zero, ref dwSize, rawInputHeaderSize);

                if (dwSize == 0)
                {
                    // No data or error, pass to original proc or DefRawInputProc
                    return CallOriginalWndProc(hWnd, uMsg, wParam, lParam, isInputSink: (wParam.ToUInt32() == (uint)WinEnums.WM_INPUT_wParam.RIM_INPUTSINK), rawInputHandle: lParam);
                }

                IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
                try
                {
                    uint bytesCopied = GetRawInputData(lParam, (uint)WinEnums.uiCommand.RID_INPUT, buffer, ref dwSize, rawInputHeaderSize);

                    if (bytesCopied == dwSize)
                    {
                        RAWINPUT raw = Marshal.PtrToStructure<RAWINPUT>(buffer);

                        if (raw.header.dwType == RAWINPUTHEADER._dwType.RIM_TYPEKEYBOARD
                            && raw.header.hDevice != IntPtr.Zero // Ignore artificial key semds like from SendInput
                            )
                        {
                            int currentVkCode = raw.keyboard.VKey;
                            if (currentVkCode < 256 && !IgnoredVkCodes.Contains(currentVkCode))
                            {
                                if (!raw.keyboard.Flags.HasFlag(RAWKEYBOARD._Flags.RI_KEY_BREAK)) // If it's key down
                                {
                                    // If it's not already down, aka not a held key repeat
                                    if (!_isKeyDown[currentVkCode])
                                    {
                                        _isKeyDown[currentVkCode] = true;

                                        // Don't bother checking if lastPressTimestamp is negative
                                        long lastPressTimestamp = _lastKeyTimestamps[currentVkCode];
                                        long delay = currentTimestamp - lastPressTimestamp;

                                        if (delay <= DoublePressThresholdMs)
                                        {
                                            IntPtr hDevice = raw.header.hDevice;
                                            string deviceHandleStr = hDevice.ToInt64().ToString("X16");
                                            DeviceDetailsHelper.RID_DEVICE_INFO? info = DeviceDetailsHelper.GetDeviceInfo(hDevice) ?? new DeviceDetailsHelper.RID_DEVICE_INFO();
                                            string? deviceName = DeviceDetailsHelper.GetDeviceName(hDevice) ?? "Unknown";

                                            DoublePressDetected?.Invoke(null, new DoublePressEventArgs(
                                                vkCode: currentVkCode,
                                                delay: delay,
                                                previousPressTimestamp: lastPressTimestamp,
                                                currentPressTimestamp: currentTimestamp,
                                                handleStrng: deviceHandleStr,
                                                devicePath: deviceName
                                            ));
                                        }
                                        _lastKeyTimestamps[currentVkCode] = currentTimestamp;
                                    }
                                }
                                else // Key Up
                                {
                                    _isKeyDown[currentVkCode] = false;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

                // Let Windows know the message was processed
                return CallOriginalWndProc(hWnd, uMsg, wParam, lParam, isInputSink: (wParam.ToUInt32() == (uint)WinEnums.WM_INPUT_wParam.RIM_INPUTSINK), rawInputHandle: lParam);
            }

            // For messages other than WM_INPUT, or if not active, pass to the original window procedure.
            return originalWndProc != IntPtr.Zero
                       ? CallWindowProc(originalWndProc, hWnd, (uint)uMsg, wParam, lParam)
                       : DefWindowProc(hWnd, (uint)uMsg, wParam, lParam); // Fallback if original isn't set
        }

        // Helper to decide whether to call DefRawInputProc or the original window proc
        private static IntPtr CallOriginalWndProc(IntPtr hWnd, WinEnums.WM_MESSAGE uMsg, UIntPtr wParam, IntPtr lParam, bool isInputSink, IntPtr rawInputHandle)
        {
            // If it's an INPUTSINK message, DefRawInputProc *must* be called for cleanup.
            // However, simply calling DefRawInputProc might prevent other hooks/apps from seeing the input.
            // A common pattern is to call DefRawInputProc and then CallWindowProc if needed,
            // but for just *monitoring*, calling DefRawInputProc should be sufficient for background input.
            if (isInputSink)
            {
                // Process it and indicate handled by returning 0 after DefRawInputProc
                DefRawInputProc(rawInputHandle, 1, rawInputHeaderSize);
                return IntPtr.Zero;
            }
            else
            {
                // For foreground messages (RIM_INPUT), pass to the original window procedure
                return originalWndProc != IntPtr.Zero
                      ? CallWindowProc(originalWndProc, hWnd, (uint)uMsg, wParam, lParam)
                      : DefWindowProc(hWnd, (uint)uMsg, wParam, lParam); // Fallback
            }
        }

        // --- Helper Methods ---
        private static void UpdateLabel(string status, System.Drawing.Color color)
        {
            if (WatcherActiveLabelReference != null && WatcherActiveLabelReference.IsHandleCreated)
            {
                // Ensure UI updates happen on the UI thread
                WatcherActiveLabelReference.BeginInvoke(new Action(() =>
                {
                    WatcherActiveLabelReference.Text = $"RawInput: {status}";
                    WatcherActiveLabelReference.ForeColor = color;
                }));
            }
            Console.WriteLine($"RawInput Status: {status}"); // Console fallback
        }

        private static void PrintKeyInfo(RAWKEYBOARD kb)
        {
            int msgCode = (int)kb.Message; // Message might be 0 if RIDEV_NOLEGACY is used
            string msgName = msgCode == 0 ? "N/A (NOLEGACY)" : (Enum.GetName(typeof(WinEnums.WM_MESSAGE), msgCode) ?? $"0x{msgCode:X}");

            bool isE0 = (kb.Flags & RAWKEYBOARD._Flags.RI_KEY_E0) != 0;
            bool isE1 = (kb.Flags & RAWKEYBOARD._Flags.RI_KEY_E1) != 0;
            string prefix = isE0 ? "E0" : isE1 ? "E1" : "00";

            string makeCodeHex = prefix + kb.MakeCode.ToString("X2");
            string vKeyHex = kb.VKey.ToString("X2");
            string flags = kb.Flags.ToString(); // Cast flags back to enum for string representation

            string keyName = "Unknown";
            try { keyName = ((Keys)kb.VKey).ToString(); } catch { }
            if (string.IsNullOrEmpty(keyName)) keyName = $"VK_{vKeyHex}";


            string t = "   ";
            Console.WriteLine($"RawInput Key Event:" +
                             $"\n{t}Key:       {keyName}" +
                             $"\n{t}ScanCode:  0x{makeCodeHex}" +
                             $"\n{t}VKey:      0x{vKeyHex}" +
                             $"\n{t}Flags:     {flags}" +
                             $"\n{t}Message:   {msgName}\n" // Message might not be relevant with RIDEV_NOLEGACY
                             );
        }

        //public static string RawDataToString(RAWINPUT raw)
        //{
        //    var rawData = new
        //    {
        //        Header = new
        //        {
        //            Type = raw.header.dwType,
        //            Size = raw.header.dwSize,
        //            Device = raw.header.hDevice.ToInt64(),
        //            Param = raw.header.wParam.ToInt64()
        //        },
        //        // Add the device type-specific data based on the input type
        //        Keyboard = new
        //        {
        //            MakeCode = raw.keyboard.MakeCode,
        //            Flags = raw.keyboard.Flags,
        //            Reserved = raw.keyboard.Reserved,
        //            VirtualKey = raw.keyboard.VKey,
        //            Message = raw.keyboard.Message,
        //            ExtraInfo = raw.keyboard.ExtraInformation
        //        },
        //        // Include mouse data if needed, since it can be present in the union
        //        Mouse = raw.header.dwType == (uint)WinEnums.RAWINPUTHEADER._dwType.RIM_TYPEMOUSE ? new
        //        {
        //            Flags = raw.mouse.usFlags
        //            // Add other mouse fields if defined in your RAWMOUSE struct
        //        } : null,
        //        // Include HID data if needed
        //        Hid = raw.header.dwType == WinEnums.RAWINPUTHEADER._dwType.RIM_TYPEHID ? new
        //        {
        //            SizeHid = raw.hid.dwSizeHid
        //            // Add other HID fields if defined in your RAWHID struct
        //        } : null,
        //        // Add the raw type enum name for easier reading
        //        TypeName = Enum.GetName(typeof(WinEnums.RAWINPUTHEADER._dwType), raw.header.dwType) ?? $"Unknown({raw.header.dwType})"
        //    };
        //    string rawString = JsonSerializer.Serialize(rawData);

        //    return rawString;
        //}

    } // End class RawInputHandler

    // EventArgs class for the double press event
    public class DoublePressEventArgs : EventArgs
    {
        public int VirtualKeyCode { get; }
        public long DelayMilliseconds { get; }
        public long PreviousPressTimestamp { get; }
        public long CurrentPressTimestamp { get; }
        public string Handle { get; }
        public string DevicePath { get; }

        public DoublePressEventArgs(int vkCode, long delay, long previousPressTimestamp, long currentPressTimestamp, string handleStrng, string devicePath)
        {
            VirtualKeyCode = vkCode;
            DelayMilliseconds = delay;
            PreviousPressTimestamp = previousPressTimestamp;
            CurrentPressTimestamp = currentPressTimestamp;
            Handle = handleStrng;
            DevicePath = devicePath;
        }
    }
}