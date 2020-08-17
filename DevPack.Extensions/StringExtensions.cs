using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevPack.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpecialCharacteres(this string value)
        {
            return new string(RemoveSpecialCharacteresInternal(value).ToArray());
        }

        private static IEnumerable<char> RemoveSpecialCharacteresInternal(string value)
        {
            foreach (var c in value.Normalize(NormalizationForm.FormD))
            {
                var unicodeCategory = char.GetUnicodeCategory(c);

                if ((unicodeCategory == UnicodeCategory.LowercaseLetter ||
                     unicodeCategory == UnicodeCategory.UppercaseLetter) &&
                    !c.Equals('\u00AA') && //ª
                    !c.Equals('\u00BA'))   //º
                {
                    yield return c;
                }
                else if (unicodeCategory == UnicodeCategory.DecimalDigitNumber ||
                         unicodeCategory == UnicodeCategory.SpaceSeparator)
                {
                    yield return c;
                }
            }
        }

        public static string RemoveAccents(this string value)
        {
            return new string(RemoveAccentsInternal(value).ToArray());
        }

        private static IEnumerable<char> RemoveAccentsInternal(string value)
        {
            foreach (var c in value.Normalize(NormalizationForm.FormD))
            {
                if (char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    yield return c;
            }
        }
    }
}
