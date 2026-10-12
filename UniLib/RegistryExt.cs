using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

namespace IDEA.UniLib.Extensions;

/// <summary>
/// Rozšíření typu Microsoft.Win32.Registry
/// </summary>
public static class RegistryExt
{
    /// <summary>
    /// Klíč k uživatelskému registru pro zadanou assembly.
    /// používá se zpravidla k uložení stavu aplikace.
    /// </summary>
    /// <remarks>
    /// Cesta je typicky:
    /// HKEY_CURRENT_USER\Software\'AssemblyCompany'\'AssemblyConfiguration'\'AssemblyVersion.Major'.'AssemblyVersion.Minor'.x.x
    /// </remarks>
    /// <param name="assembly">Zadaná assembly.</param>
    /// <param name="subKeyNames">Zadání případného podklíče.</param>
    /// <returns>Klíč; volající je zodpovědný za Dispose.</returns>
    public static RegistryKey GetUserAppRegistryKey(this Assembly assembly, params string[] subKeyNames)
    {
        var path = string.Join("\\", new[]
        {
            "Software",
            assembly.GetCompany(),
            assembly.GetConfiguration(),
            $"{assembly.GetVersion().Major}.{assembly.GetVersion().Minor}.x.x"
        }.Concat(subKeyNames));

        return Registry.CurrentUser.CreateSubKey(path);
    }

    /// <summary>
    /// Uloží do registry hodnotu převedenou na řetězec.
    /// </summary>
    public static void SetData(RegistryKey registryKey, string name, object data, TypeConverter converter)
    {
        registryKey.SetValue(name, converter.ConvertToInvariantString(data) ?? string.Empty, RegistryValueKind.String);
    }

    /// <summary>
    /// Přečte z registry hodnotu podle typu konvertoru, pak stačí jen přetypovat.
    /// </summary>
    /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
    public static object GetData(RegistryKey registryKey, string name, object defaultData, TypeConverter converter)
    {
        var data = Convert.ToString(registryKey.GetValue(name, string.Empty, RegistryValueOptions.DoNotExpandEnvironmentNames));

        if (string.IsNullOrEmpty(data))
            return defaultData;

        return converter.IsValid(data) ? converter.ConvertFromInvariantString(data) ?? string.Empty : defaultData;
    }

    public static void SetData(this RegistryKey registryKey, string name, string data) => SetData(registryKey, name, data, new StringConverter());

    public static string GetData(this RegistryKey registryKey, string name, string defaultData) => (string)GetData(registryKey, name, defaultData, new StringConverter());

    public static void SetData(this RegistryKey registryKey, string name, bool data) => SetData(registryKey, name, data, new BooleanConverter());

    public static bool GetData(this RegistryKey registryKey, string name, bool defaultData) => (bool)GetData(registryKey, name, defaultData, new BooleanConverter());

    public static void SetData(this RegistryKey registryKey, string name, int data) => SetData(registryKey, name, data, new Int32Converter());

    public static int GetData(this RegistryKey registryKey, string name, int defaultData) => (int)GetData(registryKey, name, defaultData, new Int32Converter());

    public static void SetData(this RegistryKey registryKey, string name, Rectangle data) => SetData(registryKey, name, data, new RectangleConverter());

    public static Rectangle GetData(this RegistryKey registryKey, string name, Rectangle defaultData) => (Rectangle)GetData(registryKey, name, defaultData, new RectangleConverter());
}
