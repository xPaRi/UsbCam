using System;
using System.Drawing;
using System.Reflection;
using IDEA.UniLib.Extensions;
using Microsoft.Win32;

namespace UsbCam;

/// <summary>
/// Uživatelské nastavení aplikace uložené v registru (HKCU\Software\{Company}\{App}\{Major.Minor}.x.x).
/// </summary>
internal sealed class AppSettings : IDisposable
{
    public const int MIN_PERIOD = 1;
    public const int MAX_PERIOD = 86400;

    public const int MAX_CIRCLE_DIAMETER = 999;

    private static readonly string DefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    private readonly RegistryKey _Key = Assembly.GetEntryAssembly().GetUserAppRegistryKey();

    /// <summary>Moniker poslední použité kamery.</summary>
    public string Device
    {
        get => _Key.GetData("Device", string.Empty);
        set => _Key.SetData("Device", value);
    }

    public bool AlwaysOnTop
    {
        get => _Key.GetData("AlwaysOnTop", false);
        set => _Key.SetData("AlwaysOnTop", value);
    }

    public bool DarkMode
    {
        get => _Key.GetData("DarkMode", false);
        set => _Key.SetData("DarkMode", value);
    }

    /// <summary>Klíč sady pomůcek (PenSet.Key).</summary>
    public string PenSet
    {
        get => _Key.GetData("PenSet", string.Empty);
        set => _Key.SetData("PenSet", value);
    }

    /// <summary>Název hodnoty System.Windows.Forms.PictureBoxSizeMode.</summary>
    public string SizeMode
    {
        get => _Key.GetData("SizeMode", string.Empty);
        set => _Key.SetData("SizeMode", value);
    }

    /// <summary>Složka pro jednorázové uložení snímku.</summary>
    public string FileTarget
    {
        get => _Key.GetData("FileTarget", DefaultFolder);
        set => _Key.SetData("FileTarget", value);
    }

    /// <summary>Složka pro periodické snímání.</summary>
    public string PeriodicalTarget
    {
        get => _Key.GetData("PeriodicalTarget", DefaultFolder);
        set => _Key.SetData("PeriodicalTarget", value);
    }

    /// <summary>Interval periodického snímání [sec].</summary>
    public int PeriodicalTargetInterval
    {
        get => Math.Clamp(_Key.GetData("PeriodicalTargetInterval", 10), MIN_PERIOD, MAX_PERIOD);
        set => _Key.SetData("PeriodicalTargetInterval", Math.Clamp(value, MIN_PERIOD, MAX_PERIOD));
    }

    public int Angle
    {
        get => Math.Clamp(_Key.GetData("Angle", 0), 0, 359);
        set => _Key.SetData("Angle", value);
    }

    public int AngleMark
    {
        get => Math.Clamp(_Key.GetData("AngleMark", 0), 0, 359);
        set => _Key.SetData("AngleMark", value);
    }

    public int CircleDiameter
    {
        get => Math.Clamp(_Key.GetData("CircleDiameter", 0), 0, MAX_CIRCLE_DIAMETER);
        set => _Key.SetData("CircleDiameter", value);
    }

    public Rectangle GetWindowBounds(Rectangle defaultBounds) => _Key.GetData("Form1", defaultBounds);

    public void SetWindowBounds(Rectangle bounds) => _Key.SetData("Form1", bounds);

    public void Dispose() => _Key.Dispose();
}
