using System.Text;

namespace WordAnalyzer.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceNonAlphabeticalCharacters(this string s, char replacement)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    sb.Append(c);
                else
                    sb.Append(replacement);

            return sb.ToString();
        }
    }
}
