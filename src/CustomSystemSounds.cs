using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace Monitor_Double_Keypresses;
public static class CustomSystemSounds
{
    // Import the PlaySound API function from winmm.dll
    // SetLastError = true is optional but good practice for P/Invoke if you wanted to check errors
    [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool PlaySound(string pszSound, IntPtr hmod, fdwSound fdwSound);

    // The registry alias for the "Speech Misrecognition" sound event
    // You can verify this key name exists under:
    // HKEY_CURRENT_USER\AppEvents\EventLabels\SpeechMisrecognition
    // And the actual sound mapping under:
    // HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\SpeechMisrecognition\.Current\
    //private const string SPEECH_MISRECOGNITION_ALIAS = "MisrecoSound";

    /// <summary>
    /// Plays the system sound currently configured for the "Speech Misrecognition" event.
    /// </summary>
    public static void PlayCustomSoundFromTextbox(string inputSound)
    {
        // Combine flags for playing a system event alias asynchronously
        // Using SND_ALIAS by itself often implies searching specific registry locations.
        // Sometimes you might need SND_ALIAS_ID if the alias is predefined numerically, but for event names, SND_ALIAS is standard.
        // Let's try the most common combination first: Alias + Async + NoDefault
        fdwSound playSoundFlags =
            fdwSound.SND_ALIAS         // Treat pszSound as an alias
            | fdwSound.SND_APPLICATION // Specify that it's an APPLICATION-SPECIFIC alias
            | fdwSound.SND_ASYNC       // Play asynchronously
            | fdwSound.SND_SYSTEM    // Treat as a system sound (respects system volume etc.)
            | fdwSound.SND_NODEFAULT   // Silence if the sound alias is not found
            //| fdwSound.SND_NOSTOP
            ;
        // HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\MisrecoSound
        // HKEY_CURRENT_USER\AppEvents\EventLabels\MisrecoSound
        const string SOUND_MISRECOGNITION_PATH = "C:\\Windows\\Media\\Speech Misrecognition.wav";

        // Trim quotes if present and trim whitespace
        inputSound.Trim().Trim('"');

        // First detect if it's a file path that exists
        if (Path.HasExtension(inputSound) && File.Exists(inputSound))
        {
            playSoundFlags |= fdwSound.SND_FILENAME;
        }
        // If it's a file name but not a full path, we'll assume it's relative to C:\Windows\Media
        else if (Path.HasExtension(inputSound))
        {
            string possibleFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Media", inputSound);
            if (File.Exists(possibleFilePath))
            {
                playSoundFlags |= fdwSound.SND_FILENAME;
                inputSound = possibleFilePath; // Update to the full path
            }
        }
        // If nothing is provided, we'll default to the Speech Misrecognition sound
        else if (string.IsNullOrEmpty(inputSound))
        {
            // Known to work with aliases at: HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default
            playSoundFlags |= fdwSound.SND_FILENAME;
            inputSound = SOUND_MISRECOGNITION_PATH; // Default to the speech misrecognition sound
        }
        // Otherwise we'll assume it's an alias name
        else
        {
            // Do nothing, we assume it's an alias and let PlaySound handle it
        }


        try
        {
            // Call PlaySound using the alias name and flags.
            // hmod must be IntPtr.Zero when using SND_ALIAS.
            PlaySound(inputSound, IntPtr.Zero, playSoundFlags);

            // Note: PlaySound returns true on success, false on failure.
            // Failure often means the sound alias wasn't found or configured.
            // With SND_NODEFAULT, it will typically fail silently in that case.
            // You could add error checking using Marshal.GetLastWin32Error() if needed.
        }
        catch (Exception ex)
        {
            // Log or handle potential exceptions (e.g., DllNotFoundException if winmm.dll is missing)
            Debug.WriteLine($"Error playing Speech Misrecognition sound: {ex.Message}");
            // Depending on your application, you might want to use Debug.WriteLine or a proper logging framework.
        }
    }

    // You could add other methods here for other specific system sounds
    // public static void PlayDeviceConnect()
    // {
    //     PlaySound("DeviceConnect", IntPtr.Zero, PlaySoundFlags);
    // }

    /// <summary>
    /// Flags for the PlaySound API function
    /// </summary>
    [Flags]
    public enum fdwSound : int
    {
        /// <summary>Play synchronously (default)</summary>
        SND_SYNC = 0x0000,

        /// <summary>Play asynchronously</summary>
        SND_ASYNC = 0x0001,

        /// <summary>Silence if sound not found</summary>
        SND_NODEFAULT = 0x0002,

        /// <summary>pszSound points to a memory file</summary>
        SND_MEMORY = 0x0004,

        /// <summary>Loop the sound until next sndPlaySound</summary>
        SND_LOOP = 0x0008,

        /// <summary>Don't stop any currently playing sound</summary>
        SND_NOSTOP = 0x0010,

        /// <summary>Purge non-static events for task</summary>
        SND_PURGE = 0x0040,

        /// <summary>Look for application specific association</summary>
        SND_APPLICATION = 0x0080,

        /// <summary>Don't wait if the driver is busy</summary>
        SND_NOWAIT = 0x00002000,

        /// <summary>Name is a registry alias</summary>
        /// Name is a registry alias (e.g., HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\Alias\)
        /// Or check registry: HKEY_CURRENT_USER\AppEvents\EventLabels
        SND_ALIAS = 0x00010000,

        /// <summary>Name is file name</summary>
        SND_FILENAME = 0x00020000,

        /// <summary>Name is resource name or atom</summary>
        SND_RESOURCE = 0x00040004,

        /// <summary>Generate a SoundSentry event with this sound</summary>
        SND_SENTRY = 0x00080000,

        /// <summary>Alias is a predefined ID</summary>
        SND_ALIAS_ID = 0x00110000,

        /// <summary>Treat this as a "ring" from a communications app - don't duck me</summary>
        SND_RING = 0x00100000,

        /// <summary>Treat this as a system sound</summary>
        SND_SYSTEM = 0x00200000
    }

}
