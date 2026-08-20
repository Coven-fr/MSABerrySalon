using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Coven.AIA.Core.Utilities
{
    public static class TextUtilities
    {
        public static List<string> SplitToList(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();                       

            return text
                .Split(',')
                .Select(s => Normalize(s))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();
        }

        public static string ExtractHeader(ref string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            int index = text.IndexOf("||");

            if (index < 0)
                return "";

            string header = text.Substring(0, index);
            text = text.Substring(index + 2);

            return header;
        }

        public static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.ToLower().Trim();

            text = RemoveAccents(text);

            text = text.Replace(" ", string.Empty);

            text = Regex.Replace(text, @"[^0-9a-zA-Z\u00C0-\u00FF]+", "");

            return text;
        }

        static string RemoveAccents(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
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