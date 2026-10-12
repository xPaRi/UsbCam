using System;
using System.Windows.Forms;

namespace UsbCam;

internal static class Program
{
    /// <summary>
    /// Aktuální režim zobrazení (true = tmavý).
    /// </summary>
    public static bool DarkMode { get; private set; }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using (var settings = new AppSettings())
        {
            SetDarkMode(settings.DarkMode, false);
        }

        Application.Run(new Form1());
    }

    /// <summary>
    /// Nastaví tmavý / světlý režim celé aplikace a uloží jej do registru.
    /// </summary>
    public static void SetDarkMode(bool darkMode, bool save = true)
    {
        DarkMode = darkMode;

#pragma warning disable WFO5001 // Dark mode je v .NET 10 stále označen jako experimentální.
        Application.SetColorMode(darkMode ? SystemColorMode.Dark : SystemColorMode.Classic);
#pragma warning restore WFO5001

        if (save)
        {
            using (var settings = new AppSettings())
            {
                settings.DarkMode = darkMode;
            }
        }
    }
}
