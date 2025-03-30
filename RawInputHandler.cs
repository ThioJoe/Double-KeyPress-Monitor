using DoubleKeyPressDetector; // Namespace for Logger and Log Entry
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static DoubleKeyPressDetector.WinEnums.RAWKEYBOARD; // Assuming WinEnums is in this namespace

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
        private static int _lastVkCode = 0;
        private static readonly Stopwatch _stopwatch = new Stopwatch(); // For timing key presses
        private static long _lastKeyPressTimestamp = 0;
        public static int DoublePressThresholdMs { get; set; } = 10; // Default threshold

        // Event to signal a double press was detected
        public static event EventHandler<DoublePressEventArgs>? DoublePressDetected;
        // --- End Double Press Logic ---


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
                        _lastVkCode = 0;
                        _lastKeyPressTimestamp = 0;
                        _stopwatch.Restart(); // Start/Restart stopwatch when monitoring starts
                    }
                    else
                    {
                        _stopwatch.Stop(); // Stop stopwatch when monitoring stops
                    }
                }
            }
        }


        private delegate IntPtr WndProcDelegate(IntPtr hwnd, WinEnums.WM_MESSAGE msg, UIntPtr wParam, IntPtr lParam);
        private static WndProcDelegate? wndProcDelegate;
        private static IntPtr originalWndProc = IntPtr.Zero;
        private const int GWLP_WNDPROC = -4;
        private static IntPtr targetWindowHandle = IntPtr.Zero; // Store the target window handle

        public static readonly uint rawInputHeaderSize = (uint)Marshal.SizeOf<RAWINPUTHEADER>();
        public static readonly uint rawInputSize = (uint)Marshal.SizeOf<RAWINPUT>();

        public static bool InitializeRawInput(IntPtr hwnd, Label? labelToUpdate = null, int thresholdMs = 10)
        {
            if (RawInputWatcherActive) return true; // Already active

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

                        // Check if it's keyboard input and a key-down event
                        if (raw.header.dwType == (uint)WinEnums.RAWINPUTHEADER._dwType.RIM_TYPEKEYBOARD &&
                            raw.keyboard.Flags == (ushort)_Flags.RI_KEY_MAKE) // Check for RI_KEY_MAKE explicitly
                        {
                            int currentVkCode = raw.keyboard.VKey;
                            long currentTimestamp = _stopwatch.ElapsedMilliseconds;
                            long delay = currentTimestamp - _lastKeyPressTimestamp;

                            // ---- Double Press Check ----
                            if (currentVkCode == _lastVkCode && delay <= DoublePressThresholdMs)
                            {
                                // Double press detected! Raise the event.
                                DoublePressDetected?.Invoke(null, new DoublePressEventArgs(currentVkCode, delay));
                                Console.WriteLine($"Double Press: VK={currentVkCode:X2}, Delay={delay}ms"); // Debug output
                            }
                            // ---- End Check ----

                            // Update last key info AFTER checking
                            _lastVkCode = currentVkCode;
                            _lastKeyPressTimestamp = currentTimestamp;

                            // Optional: Print details like in your original example
                            // PrintKeyInfo(raw.keyboard);
                        }
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

                // VERY IMPORTANT for INPUTSINK: Let Windows know the message was processed
                // Call DefRawInputProc for background messages, otherwise CallWindowProc for foreground.
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
                WatcherActiveLabelReference.BeginInvoke(new Action(() => {
                    WatcherActiveLabelReference.Text = $"RawInput: {status}";
                    WatcherActiveLabelReference.ForeColor = color;
                }));
            }
            Console.WriteLine($"RawInput Status: {status}"); // Console fallback
        }

        // (Optional) Keep the PrintKeyInfo method if you want console output
        private static void PrintKeyInfo(RAWKEYBOARD kb)
        {
            int msgCode = (int)kb.Message; // Message might be 0 if RIDEV_NOLEGACY is used
            string msgName = msgCode == 0 ? "N/A (NOLEGACY)" : (Enum.GetName(typeof(WinEnums.WM_MESSAGE), msgCode) ?? $"0x{msgCode:X}");

            bool isE0 = (kb.Flags & (ushort)_Flags.RI_KEY_E0) != 0;
            bool isE1 = (kb.Flags & (ushort)_Flags.RI_KEY_E1) != 0;
            string prefix = isE0 ? "E0" : isE1 ? "E1" : "00";

            string makeCodeHex = prefix + kb.MakeCode.ToString("X2");
            string vKeyHex = kb.VKey.ToString("X2");
            string flags = ((_Flags)kb.Flags).ToString(); // Cast flags back to enum for string representation

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


        // --- Structs and Enums (Consider moving to WinEnums.cs) ---
        // Keep these aligned with your WinEnums.cs file
        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public WinEnums.RAWINPUTDEVICE._dwFlags dwFlags;
            public IntPtr hwndTarget;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            public uint dwType; // Use uint directly
            public uint dwSize;
            public IntPtr hDevice;
            public IntPtr wParam; // Corresponds to WM_INPUT wParam (RIM_INPUT or RIM_INPUTSINK)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWKEYBOARD
        {
            public ushort MakeCode;
            public ushort Flags; // Use ushort directly
            public ushort Reserved;
            public ushort VKey;
            public uint Message; // WM_KEYDOWN, WM_KEYUP etc. (0 if RIDEV_NOLEGACY)
            public uint ExtraInformation;

            [Flags]
            public enum _Flags : ushort // Keep internal enum consistent if WinEnums isn't used directly here
            {
                RI_KEY_MAKE = 0,
                RI_KEY_BREAK = 1,
                RI_KEY_E0 = 2,
                RI_KEY_E1 = 4,
                RI_KEY_TERMSRV_SET_LED = 8,
                RI_KEY_TERMSRV_SHADOW = 0x10
            }
        }

        // RAWINPUT struct needs layout based on type
        [StructLayout(LayoutKind.Explicit)]
        public struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;

            [FieldOffset(16)] // Check offset carefully based on RAWINPUTHEADER size (IntPtr is 8 bytes on x64)
            public RAWMOUSE mouse;

            [FieldOffset(16)]
            public RAWKEYBOARD keyboard;

            [FieldOffset(16)]
            public RAWHID hid;
        }

        // Dummy structs for RAWMOUSE and RAWHID if not fully defined elsewhere
        [StructLayout(LayoutKind.Sequential)]
        public struct RAWMOUSE { /* Define fields if needed */ public ushort usFlags; /* ... */ }
        [StructLayout(LayoutKind.Sequential)]
        public struct RAWHID { /* Define fields if needed */ public uint dwSizeHid; /* ... */ }


    } // End class RawInputHandler

    // EventArgs class for the double press event
    public class DoublePressEventArgs : EventArgs
    {
        public int VirtualKeyCode { get; }
        public long DelayMilliseconds { get; }

        public DoublePressEventArgs(int vkCode, long delay)
        {
            VirtualKeyCode = vkCode;
            DelayMilliseconds = delay;
        }
    }
}