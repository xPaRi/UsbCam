using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEA.UniLib.Extensions
{
    /// <summary>
    /// Rozšíření třídy Version.
    /// </summary>
    public static class VersionExt
    {
        /// <summary>
        /// Datum, kterému odpovídá zadaná verze.
        /// </summary>
        /// <remarks>
        /// Počet dní od 1.ledna 2000 + počet dvojsekund od půlnoci.
        /// </remarks>
        /// <param name="version">Zadaná verze</param>
        /// <returns></returns>
        public static DateTime VersionDateTime(this Version version) => 
            new DateTime(2000, 1, 1)
            .Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + TimeSpan.TicksPerSecond * 2 * version.Revision));

        /// <summary>
        /// Vrací verzi pro zadaný čas.
        /// </summary>
        /// <param name="dateTime">Zadaný čas.</param>
        /// <param name="major">Hodnota major verze.</param>
        /// <param name="minor">Hodnota minor verze.</param>
        /// <returns></returns>
        public static Version Version(this DateTime dateTime, int major = 1, int minor = 0)
        {
            var build = Math.Truncate((dateTime - new DateTime(2000, 1, 1)).TotalDays);
            var version = Math.Truncate((dateTime - dateTime.Date).TotalSeconds / 2d);

            return new Version(major, minor, (int)build, (int)version);
        }
    }
}
