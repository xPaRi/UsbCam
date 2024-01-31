using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IDEA.UniLib.Extensions
{
    /// <summary>
    /// Rozšíření třídy Assembly.
    /// </summary>
    public static class AssemblyExt
    {
        /// <summary>
        /// Verze assembly.
        /// </summary>
        /// Hodnota je definována v *.csproj jako atribut AssemblyVersion.
        /// <param name="assembly">Zkoumaná assembly.</param>
        /// <returns>csproj: Version</returns>
        public static Version GetVersion(this Assembly assembly) => assembly?.GetName()?.Version ?? new Version();

        /// <summary>
        /// Unikátní identifikátor assembly, např. "B1E356CD-FFD5-46BC-80E5-66D2E3B3479B"
        /// Jednoznačně identifikuje aplikaci v Get&Go!
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v AssemblyInfo.cs jako [assembly: Guid("Nějaké GUID")]
        /// GUID je možné vytvořit ve VS / Tools / Create GUID
        /// </remarks>
        /// <param name="assembly">Zkoumaná assembly.</param>
        /// <returns>assembly: GUID</returns>
        public static string GetGuid(this Assembly assembly) => assembly?.GetCustomAttribute<GuidAttribute>()?.Value ?? string.Empty;

        /// <summary>
        /// Titulek assembly, např. "Správce příjemců změn v databázi"
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v AssemblyInfo jako [assembly: AssemblyTitle("Nějaký titulek")]
        /// </remarks>
        /// <param name="assembly">Zkoumaná assembly.</param>
        /// <returns>assembly: AssemblyTitle</returns>
        public static string GetTitle(this Assembly assembly) => assembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? string.Empty;

        /// <summary>
        /// Popis assembly, např. "Administruje seznam příjemců změn v databázi ISKO."
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v AssemblyInfo jako [assembly: AssemblyDescription("Nějaký description.")]
        /// </remarks>
        /// <param name="assembly">Zkoumaná assembly.</param>
        /// <returns>assembly: AssemblyDescription</returns>
        public static string GetDescription(this Assembly assembly) => assembly?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty;

        /// <summary>
        /// Copyright assembly, např. "Copyright ©2022 - 2023 IDEA-ENVI s.r.o. Všechna práva vyhrazena."
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v *.csproj jako atribut Copyright.
        /// </remarks>
        /// <returns>csproj: Copyright</returns>
        public static string GetCopyright(this Assembly assembly) => assembly?.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;

        /// <summary>
        /// Společnost, která vyprodukovala assembly, např. "IDEA-ENVI s.r.o."
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v *.csproj jako atribut Company.
        /// </remarks>
        /// <returns>csproj: Company</returns>
        public static string GetCompany(this Assembly assembly) => assembly?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;

        /// <summary>
        /// Název produktu assembly, např. "ISKO2"
        /// </summary>
        /// <remarks>
        /// Hodnota je definována v *.csproj jako atribut Product.
        /// </remarks>
        /// <returns>csproj: Product</returns>
        public static string GetProduct(this Assembly assembly) => assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? string.Empty;

        /// <summary>
        /// Název klíče vhodný pro uložení konfigurace.
        /// </summary>
        /// <remarks>
        /// Hodnota byla dříve definována v AssemblyInfo.cs jako [assembly: AssemblyConfiguration("Nějaký název konfigurace")],
        /// v .NET Core je to nepohodlné, takže vrací hodnotu assembly.Name
        /// </remarks>
        /// <returns>csproj: Product</returns>
        public static string GetConfiguration(this Assembly assembly) => GetShortName(assembly);

        /// <summary>
        /// Název assembly, např. "RecipientsOfDbChangesAdm"
        /// </summary>
        /// <remarks>
        /// Hodnota je načtena z assembly?.GetName().Name
        /// </remarks>
        /// <returns>assembly.GetName().Name</returns>
        public static string GetShortName(this Assembly assembly) => assembly?.GetName().Name ?? string.Empty;

    }
}
