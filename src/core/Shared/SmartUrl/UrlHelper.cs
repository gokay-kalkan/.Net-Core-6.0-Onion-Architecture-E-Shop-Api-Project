

using System.Text.RegularExpressions;

namespace Shared.SmartUrl
{
    public static class UrlHelper
    {
        public static string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            
            str = Regex.Replace(str, @"\s+", "-").Trim();

          
            str = str.Substring(0, str.Length <= 75 ? str.Length : 75).Trim();

          
            str = Regex.Replace(str, @"([-_]){2,}", "$1");

            return str;
        }
    }
}
