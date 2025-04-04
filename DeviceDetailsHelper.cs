using System;
using System.Runtime.InteropServices;
using System.ComponentModel; // Required for Win32Exception
using System.Diagnostics; // For Debug.WriteLine

#nullable enable

public static class DeviceDetailsHelper
{
    // Constants from winuser.h
    private const int RIDI_DEVICENAME = 0x20000007;
    private const int RIDI_DEVICEINFO = 0x2000000b;

    private const int RIM_TYPEMOUSE = 0;
    private const int RIM_TYPEKEYBOARD = 1;
    private const int RIM_TYPEHID = 2;

    // Nested structures remain the same (Sequential is fine here)
    [StructLayout(LayoutKind.Sequential)]
    public struct RID_DEVICE_INFO_MOUSE
    {
        public uint dwId;
        public uint dwNumberOfButtons;
        public uint dwSampleRate;
        [MarshalAs(UnmanagedType.Bool)] // Marshal as 4-byte BOOL
        public bool fHasHorizontalWheel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RID_DEVICE_INFO_KEYBOARD
    {
        public uint dwType;
        public uint dwSubType;
        public uint dwKeyboardMode;
        public uint dwNumberOfFunctionKeys;
        public uint dwNumberOfIndicators;
        public uint dwNumberOfKeysTotal;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RID_DEVICE_INFO_HID
    {
        public uint dwVendorId;
        public uint dwProductId;
        public uint dwVersionNumber;
        public ushort usUsagePage;
        public ushort usUsage;
    }

    // Structure for RID_DEVICE_INFO using Explicit layout for the union
    [StructLayout(LayoutKind.Explicit)]
    public struct RID_DEVICE_INFO
    {
        [FieldOffset(0)]
        public uint cbSize; // Size of the structure, in bytes. Must be set before call.

        [FieldOffset(4)] // Follows cbSize (DWORD = 4 bytes)
        public uint dwType; // RIM_TYPEMOUSE, RIM_TYPEKEYBOARD, or RIM_TYPEHID

        // The union starts after dwType (Offset 4 + 4 = 8)
        [FieldOffset(8)]
        public RID_DEVICE_INFO_MOUSE mouse;

        [FieldOffset(8)] // Overlaps mouse field
        public RID_DEVICE_INFO_KEYBOARD keyboard;

        [FieldOffset(8)] // Overlaps mouse and keyboard fields
        public RID_DEVICE_INFO_HID hid;
    }

    // Calculate the size explicitly to ensure correctness with Explicit layout
    // Size = Offset of union + Size of the LARGEST member in the union
    private static readonly uint RID_DEVICE_INFO_SIZE = CalculateRidDeviceInfoSize();

    private static uint CalculateRidDeviceInfoSize()
    {
        // Start with the offset of the union members
        uint baseOffset = 8;

        // Calculate sizes of individual union members
        uint mouseSize = (uint)Marshal.SizeOf<RID_DEVICE_INFO_MOUSE>();
        uint keyboardSize = (uint)Marshal.SizeOf<RID_DEVICE_INFO_KEYBOARD>();
        uint hidSize = (uint)Marshal.SizeOf<RID_DEVICE_INFO_HID>();

        // Find the maximum size among the union members
        uint maxSize = Math.Max(mouseSize, Math.Max(keyboardSize, hidSize));

        // Total size is the base offset plus the max size of the union part
        uint totalSize = baseOffset + maxSize;

        // Optional: Verify against Marshal.SizeOf on the explicit struct. They *should* match
        // if the explicit layout is correct, but calculating manually is safer.
        // Debug.WriteLine($"Calculated RID_DEVICE_INFO Size: {totalSize}");
        // Debug.WriteLine($"Marshal.SizeOf<RID_DEVICE_INFO>: {Marshal.SizeOf<RID_DEVICE_INFO>()}");

        return totalSize;
    }


    // P/Invoke declaration for GetRawInputDeviceInfo remains the same
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern uint GetRawInputDeviceInfo(
        IntPtr hDevice,
        uint uiCommand,
        IntPtr pData,
        ref uint pcbSize); // Note: pcbSize is IN/OUT for RIDI_DEVICENAME, but mostly IN for RIDI_DEVICEINFO

    /// <summary>
    /// Gets the device name string for a raw input device handle.
    /// </summary>
    /// <param name="hDevice">Handle to the raw input device.</param>
    /// <returns>The device name string, or null if an error occurs.</returns>
    // See: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdeviceinfow
    public static string? GetDeviceName(IntPtr hDevice)
    {
        uint sizeInChars = 0;
        uint result = GetRawInputDeviceInfo(hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref sizeInChars);


        if (result == unchecked((uint)-1) && sizeInChars == 0) // Check for error AND size being 0 (size might be updated even on error)
        {
            Debug.WriteLine($"Error getting device name size: {new Win32Exception(Marshal.GetLastWin32Error()).Message}");
            return null;
        }
        else if (result == unchecked((uint)-1))
        {
            Debug.WriteLine($"Buffer size not large enough. Error getting device name size: {new Win32Exception(Marshal.GetLastWin32Error()).Message}");
            return null;
        }
        else if (sizeInChars == 0)
        {
            Debug.WriteLine("Device name size is zero, possibly no name available.");
            return string.Empty; ;
        }

        IntPtr buffer = IntPtr.Zero;
        try
        {
            // The 'pcbSize' parameter returns the number of characters needed, NOT the size in bytes. So allocate enough bytes for that.
            // Not certain if it includes the null terminator, but better to be safe by adding 2 (2 bytes per char for Unicode).
            buffer = Marshal.AllocHGlobal((int)(sizeInChars * Marshal.SystemDefaultCharSize + 2));
            if (buffer == IntPtr.Zero) throw new OutOfMemoryException("Failed to allocate memory for device name.");

            uint pcbSize = sizeInChars; // Use a separate variable for the IN/OUT parameter
            result = GetRawInputDeviceInfo(hDevice, RIDI_DEVICENAME, buffer, ref pcbSize);

            if (result == unchecked((uint)-1))
            {
                Debug.WriteLine($"Error getting device name: {new Win32Exception(Marshal.GetLastWin32Error()).Message}");
                return null;
            }
            // Optional check: if (result != pcbSize) { /* Log warning? */ }
            string deviceName = Marshal.PtrToStringAuto(buffer);
            return deviceName; // Use Auto for wide/ansi chars
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in GetDeviceName: {ex.Message}");
            return null;
        }
        finally
        {
            if (buffer != IntPtr.Zero) Marshal.FreeHGlobal(buffer);
        }
    }

    /// <summary>
    /// Gets structured device information using Explicit layout for the union.
    /// </summary>
    /// <param name="hDevice">Handle to the raw input device.</param>
    /// <returns>A RID_DEVICE_INFO struct, or null if an error occurs.</returns>
    public static RID_DEVICE_INFO? GetDeviceInfo(IntPtr hDevice)
    {
        // Use the explicitly calculated size
        uint structSize = RID_DEVICE_INFO_SIZE;
        uint pcbSize = structSize; // Size for the ref parameter

        IntPtr buffer = IntPtr.Zero;
        try
        {
            buffer = Marshal.AllocHGlobal((int)structSize);
            if (buffer == IntPtr.Zero) throw new OutOfMemoryException("Failed to allocate memory for device info.");

            // *** CRITICAL STEP ***
            // Write ONLY the cbSize field into the beginning of the buffer.
            // Do NOT marshal the whole (potentially incorrectly sized by runtime) C# struct.
            Marshal.WriteInt32(buffer, 0, (int)structSize); // Write cbSize at offset 0

            // Call the API. For RIDI_DEVICEINFO, pcbSize (the ref uint) should match the input cbSize field.
            // The API expects the buffer to be exactly the size specified in the buffer's cbSize field.
            uint result = GetRawInputDeviceInfo(hDevice, RIDI_DEVICEINFO, buffer, ref pcbSize);

            // Check for errors. According to docs, result is bytes copied.
            // If result is 0 or -1, an error occurred.
            if (result == 0 || result == unchecked((uint)-1))
            {
                // Capture error *before* potential exception in Marshal.PtrToStructure
                int win32Error = Marshal.GetLastWin32Error();
                string errorMessage = new Win32Exception(win32Error).Message;
                // ERROR_INVALID_PARAMETER (87) often points to cbSize mismatch.
                // ERROR_INSUFFICIENT_BUFFER (122) can sometimes occur too.
                Debug.WriteLine($"Error getting device info (Code: {win32Error}): {errorMessage}. Struct Size Sent: {structSize}, API Result Bytes: {result}");
                return null;
            }

            // Check if the returned size matches expected size. Can indicate unexpected data.
            if (result != structSize)
            {
                Debug.WriteLine($"Warning: GetRawInputDeviceInfo for INFO returned {result} bytes, expected {structSize}.");
                // May still be usable, but proceed with caution. Let's attempt marshalling anyway.
            }


            // Marshal the data from the buffer back into our C# struct
            RID_DEVICE_INFO deviceInfo = Marshal.PtrToStructure<RID_DEVICE_INFO>(buffer);

            // Optional secondary check: Verify the cbSize *read back* from the struct
            if (deviceInfo.cbSize != structSize)
            {
                Debug.WriteLine($"Warning: Marshalled struct cbSize ({deviceInfo.cbSize}) mismatch. Expected {structSize}.");
                // This could indicate marshalling issues or API returning different structure version.
            }

            RID_DEVICE_INFO prunedInfo = new RID_DEVICE_INFO(); // Copy for further processing with only the applicable device type

            if (deviceInfo.dwType != uint.MaxValue)
            {
                Console.WriteLine($"Device Info cbSize: {deviceInfo.cbSize} (Expected: {RID_DEVICE_INFO_SIZE})");
                Console.WriteLine($"Device Type: {deviceInfo.dwType}"); // Read the type

                prunedInfo.cbSize = deviceInfo.cbSize; // Copy the cbSize for consistency
                prunedInfo.dwType = deviceInfo.dwType; // Copy the type

                // *** Use dwType to decide which field to access ***
                switch (deviceInfo.dwType)
                {
                    case RIM_TYPEKEYBOARD:
                        prunedInfo.keyboard = deviceInfo.keyboard;
                        prunedInfo.mouse = new RID_DEVICE_INFO_MOUSE(); // Zero out other fields
                        prunedInfo.hid = new RID_DEVICE_INFO_HID();
                        break;

                    case RIM_TYPEMOUSE:
                        prunedInfo.mouse = deviceInfo.mouse;
                        prunedInfo.keyboard = new RID_DEVICE_INFO_KEYBOARD(); // Zero out other fields
                        prunedInfo.hid = new RID_DEVICE_INFO_HID();
                        break;

                    case RIM_TYPEHID:
                        prunedInfo.hid = deviceInfo.hid;
                        prunedInfo.keyboard = new RID_DEVICE_INFO_KEYBOARD(); // Zero out other fields
                        prunedInfo.mouse = new RID_DEVICE_INFO_MOUSE();
                        break;

                    default:
                        Console.WriteLine("  Type: Unknown or Other");
                        break;
                }
                return prunedInfo;
            }
            else
            {
                Debug.WriteLine($"Could not retrieve structured device info. Value is max value ({deviceInfo.dwType}) and indicates a problem.");
                return deviceInfo;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in GetDeviceInfo: {ex.Message}");
            return null;
        }
        finally
        {
            if (buffer != IntPtr.Zero) Marshal.FreeHGlobal(buffer);
        }
    }


    public static void PrintDeviceInfo(IntPtr hDevice)
    {
        if (hDevice == IntPtr.Zero)
        {
            Console.WriteLine("Device handle is zero.");
            return;
        }

        Console.WriteLine($"\n--- Device Info for handle: {hDevice} ---");

        string? deviceName = GetDeviceName(hDevice);
        Console.WriteLine($"Device Name: {deviceName ?? "N/A"}");

        RID_DEVICE_INFO? deviceInfo = GetDeviceInfo(hDevice);

        if (deviceInfo.HasValue)
        {
            RID_DEVICE_INFO info = deviceInfo.Value;
            Console.WriteLine($"Device Info cbSize: {info.cbSize} (Expected: {RID_DEVICE_INFO_SIZE})"); // Added for debug
            Console.WriteLine($"Device Type: {info.dwType}");

            switch (info.dwType)
            {
                case RIM_TYPEKEYBOARD:
                    Console.WriteLine("  Type: Keyboard");
                    Console.WriteLine($"  Subtype: {info.keyboard.dwSubType}");
                    Console.WriteLine($"  Mode: {info.keyboard.dwKeyboardMode}");
                    Console.WriteLine($"  Function Keys: {info.keyboard.dwNumberOfFunctionKeys}");
                    Console.WriteLine($"  Indicators: {info.keyboard.dwNumberOfIndicators}");
                    Console.WriteLine($"  Total Keys: {info.keyboard.dwNumberOfKeysTotal}");
                    break;

                case RIM_TYPEMOUSE:
                    Console.WriteLine("  Type: Mouse");
                    Console.WriteLine($"  ID: {info.mouse.dwId}");
                    Console.WriteLine($"  Buttons: {info.mouse.dwNumberOfButtons}");
                    Console.WriteLine($"  Sample Rate: {info.mouse.dwSampleRate}");
                    Console.WriteLine($"  Horizontal Wheel: {info.mouse.fHasHorizontalWheel}");
                    break;

                case RIM_TYPEHID:
                    Console.WriteLine("  Type: HID");
                    Console.WriteLine($"  Vendor ID: 0x{info.hid.dwVendorId:X4}");
                    Console.WriteLine($"  Product ID: 0x{info.hid.dwProductId:X4}");
                    Console.WriteLine($"  Version: {info.hid.dwVersionNumber}");
                    Console.WriteLine($"  Usage Page: 0x{info.hid.usUsagePage:X2}");
                    Console.WriteLine($"  Usage: 0x{info.hid.usUsage:X2}");
                    break;

                default:
                    Console.WriteLine("  Type: Unknown or Other");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Could not retrieve structured device info.");
        }
        Console.WriteLine("--- End Device Info ---");

        // Per your [2025-03-28] note, if this is the main output of a console app:
        // Console.WriteLine("\nPress Enter to exit...");
        // Console.ReadLine();
    }
}