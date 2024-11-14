

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OnlineStory.Contract.Extensions;

public static class VietnameseSlugExtensions
{
    public static string ToSlug(this string phrase)
    {
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string slug = phrase.Normalize(NormalizationForm.FormD).Trim().ToLower();

        slug = regex.Replace(slug, String.Empty)
          .Replace('\u0111', 'd').Replace('\u0110', 'D')
          .Replace(",", "-").Replace(".", "-").Replace("!", "")
          .Replace("(", "").Replace(")", "").Replace(";", "-")
          .Replace("/", "-").Replace("%", "ptram").Replace("&", "va")
          .Replace("?", "").Replace('"', '-').Replace(' ', '-');

        return slug;
        //var normalizedString = phrase.RemoveDiacritics().ToLowerInvariant();
        //normalizedString = Regex.Replace(normalizedString, @"[^a-z0-9\s-]", ""); // Remove invalid characters
        //normalizedString = Regex.Replace(normalizedString, @"\s+", " ").Trim(); // Convert multiple spaces to one space
        //normalizedString = Regex.Replace(normalizedString, @"\s", "-"); // Convert spaces to hyphens
        //return normalizedString;
    }
    public static string RemoveDiacritics(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

}
