using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Coven.AIA.Core.Utilities
{
    public static class TextUtilities
    {
        public static List<string> SplitToList(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string>();                       

            return value
                .Split(',')
                .Select(s => Normalize(s))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();
        }

        public static string SplitName(string value)
        {
            string text = value.ToString();

            text = Regex.Replace(text, "([A-Z]+)([A-Z][a-z])", "$1 $2");
            text = Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");

            return text;
        }

        public static string ExtractHeader(ref string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            int index = value.IndexOf("||");

            if (index < 0)
                return "";

            string header = value.Substring(0, index);
            value = value.Substring(index + 2);

            return header;
        }

        public static string Normalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.ToLower().Trim();

            value = RemoveAccents(value);

            value = value.Replace(" ", string.Empty);

            value = Regex.Replace(value, @"[^0-9a-zA-Z\u00C0-\u00FF]+", "");

            return value;
        }

        static string RemoveAccents(string value)
        {
            var normalized = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}