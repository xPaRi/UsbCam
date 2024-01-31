using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;
using Point = System.Drawing.Point;
using PointConverter = System.Drawing.PointConverter;

namespace IDEA.UniLib.Extensions
{
    /// <summary>
    /// Rozšíření typu Microsoft.Win32.Registry
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public static class RegistryExt
    {
        #region Helpers

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
        /// <returns>Klíč.</returns>
        public static RegistryKey GetUserAppRegistryKey(this Assembly assembly, params string[] subKeyNames)
        {
            var softwareKey = Registry.CurrentUser.CreateSubKey("Software");
            var companyKey = softwareKey.CreateSubKey(assembly.GetCompany());
            var appKey = companyKey.CreateSubKey(assembly.GetConfiguration());
            var versionKey = appKey.CreateSubKey($"{assembly.GetVersion().Major}.{assembly.GetVersion().Minor}.x.x");

            //Projdeme a sestavíme případnou cestu k seznamu podklíčů
            return subKeyNames.Aggregate(versionKey, (current, subKeyName) => current.CreateSubKey(subKeyName));
        }

        #endregion //Helpers

        #region TypeConverter

        /// <summary>
        /// Uloží do registry hodnotu parsovanou na řetězec.
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        /// <param name="converter">Konvertor pro převod.</param>
        public static void SetData(RegistryKey registryKey, string name, object data, TypeConverter converter)
        {
            registryKey.SetValue(name, converter.ConvertToInvariantString(data) ?? string.Empty, RegistryValueKind.String);
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu podle typu konvertoru, pak stačí jen přetypovat.
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <param name="converter">Konvertor pro převod.</param>
        public static object GetData(RegistryKey registryKey, string name, object defaultData, TypeConverter converter)
        {
            var data = Convert.ToString(registryKey.GetValue(name, string.Empty, RegistryValueOptions.DoNotExpandEnvironmentNames));

            if (string.IsNullOrEmpty(data))
                return defaultData;

            return converter.IsValid(data) ? converter.ConvertFromInvariantString(data) ?? string.Empty : defaultData;
        }

        #endregion //TypeConverter

        #region String

        /// <summary>
        /// Uloží do registry data typu: String. 
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">Ukládaná hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, string data)
        {
            SetData(registryKey, name, data, new StringConverter());
        }

        /// <summary>
        /// Načte z registry data typu: String. 
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="defaultData"></param>
        public static string GetData(this RegistryKey registryKey, string name, string defaultData)
        {
            return (string)GetData(registryKey, name, defaultData, new StringConverter());
        }

        #endregion //String

        #region DateTime

        /// <summary>
        /// Uloží do registry data typu: DateTime 
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">Ukládaná hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, DateTime data)
        {
            SetData(registryKey, name, data, new DateTimeConverter());
        }

        /// <summary>
        /// Načte z registry data typu: DateTime 
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="defaultData"></param>
        public static DateTime GetData(this RegistryKey registryKey, string name, DateTime defaultData)
        {
            return (DateTime)GetData(registryKey, name, defaultData, new DateTimeConverter());
        }

        #endregion //DateTime

        #region Boolean

        /// <summary>
        /// Uloží do registry data typu: Boolean
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, bool data)
        {
            SetData(registryKey, name, data, new BooleanConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Boolean
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static bool GetData(this RegistryKey registryKey, string name, bool defaultData)
        {
            return (bool)GetData(registryKey, name, defaultData, new BooleanConverter());
        }

        #endregion //Boolean

        #region Int

        /// <summary>
        /// Uloží do registry data typu: Int32
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, int data)
        {
            SetData(registryKey, name, data, new Int32Converter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Int
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static int GetData(this RegistryKey registryKey, string name, int defaultData)
        {
            return (int)GetData(registryKey, name, defaultData, new Int32Converter());
        }

        #endregion //Int

        #region Float

        /// <summary>
        /// Uloží do registry data typu: Float
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, float data)
        {
            SetData(registryKey, name, data, new SingleConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Float
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static float GetData(this RegistryKey registryKey, string name, float defaultData)
        {
            return (float)GetData(registryKey, name, defaultData, new SingleConverter());
        }

        #endregion //Float

        #region Double

        /// <summary>
        /// Uloží do registry data typu: Double
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, double data)
        {
            SetData(registryKey, name, data, new DoubleConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Double
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static double GetData(this RegistryKey registryKey, string name, double defaultData)
        {
            return (double)GetData(registryKey, name, defaultData, new DoubleConverter());
        }

        #endregion //Double

        #region Decimal

        /// <summary>
        /// Uloží do registry data typu: Decimal
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název ukládané hodnoty.</param>
        /// <param name="data">ukládaná Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, decimal data)
        {
            SetData(registryKey, name, data, new DecimalConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Decimal
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Defaultní hodnota pro případ, že v registry nic není nebo dojde k nějaké chybě.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static decimal GetData(this RegistryKey registryKey, string name, decimal defaultData)
        {
            return (decimal)GetData(registryKey, name, defaultData, new DecimalConverter());
        }

        #endregion //Decimal

        #region Point

        /// <summary>
        /// Uloží do registry data typu: Point
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="data">Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, Point data)
        {
            SetData(registryKey, name, data, new PointConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Rectangle
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Pokud dojde k nějaké nesrovnalosti, vrátí tuto hodnotu.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static Point GetData(this RegistryKey registryKey, string name, Point defaultData)
        {
            return (Point)GetData(registryKey, name, defaultData, new PointConverter());
        }

        #endregion //Point

        #region Rectangle

        /// <summary>
        /// Uloží do registry data typu: Rectangle
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="data">Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, Rectangle data)
        {
            SetData(registryKey, name, data, new RectangleConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Rectangle
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Pokud dojde k nějaké nesrovnalosti, vrátí tuto hodnotu.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static Rectangle GetData(this RegistryKey registryKey, string name, Rectangle defaultData)
        {
            return (Rectangle)GetData(registryKey, name, defaultData, new RectangleConverter());
        }

        #endregion //Rectangle

        #region Rec
        /*

        /// <summary>
        /// Uloží do registry data typu: Rect
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="data">Hodnota.</param>
        public static void SetData(this RegistryKey registryKey, string name, Rect data)
        {
            SetData(registryKey, name, data, new RectConverter());
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: Rect
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultData">Pokud dojde k nějaké nesrovnalosti, vrátí tuto hodnotu.</param>
        /// <returns>Hodnota nebo 'defaultData'.</returns>
        public static Rect GetData(this RegistryKey registryKey, string name, Rect defaultData)
        {
            return (Rect)GetData(registryKey, name, defaultData, new RectConverter());
        }

        */
        #endregion //Rec

        #region IEnumerable<string>

        /// <summary>
        /// Uloží do registry data typu: IEnumerable of string
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="dataList">Seznam hodnot.</param>
        public static void SetData(this RegistryKey registryKey, string name, IEnumerable<string> dataList)
        {
            registryKey.SetValue(name, dataList.ToArray(), RegistryValueKind.MultiString);
        }

        /// <summary>
        /// Přečte a vrátí z registry hodnotu typu: IEnumerable of string
        /// </summary>
        /// <param name="registryKey">Klíč registry.</param>
        /// <param name="name">Název hodnoty.</param>
        /// <param name="defaultDataList">Pokud dojde k nějaké nesrovnalosti, vrátí tuto hodnotu.</param>
        /// <returns>Hodnota nebo 'defaultDataList'.</returns>
        public static IEnumerable<string> GetData(this RegistryKey registryKey, string name, IEnumerable<string> defaultDataList)
        {
            return (IEnumerable<string>)registryKey.GetValue(name, defaultDataList, RegistryValueOptions.DoNotExpandEnvironmentNames);
        }

        #endregion //IEnumerable<string>


    }
}
