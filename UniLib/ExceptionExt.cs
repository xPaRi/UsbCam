using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEA.UniLib.Extensions
{
    /// <summary>
    /// Rozšíření třídy Exception.
    /// </summary>
    public static class ExceptionExt
    {
        /// <summary>
        /// Projde strom (innerMessage) popisu vyjímek a sestaví z nich řetězec vhodný 
        /// k zobrazení v MessageBoxu.
        /// </summary>
        /// <param name="ex">Kořenová vyjímka.</param>
        /// <param name="text">Úvodní text.</param>
        /// <returns>Popis vyjímek.</returns>
        public static string InnerMessages(this Exception ex, string text = "")
        {
            while (ex != null)
            {
                text += $"{(string.IsNullOrEmpty(text) ? string.Empty : "\n- ")}{ex.Message}";
                ex = ex.InnerException;
            }

            return text;
        }

    }
}
