namespace Parsnip
{
    using System.Text.RegularExpressions;
    using Parsers;


    public static class RegexExtensions
    {
        public static Parser<string, string> Regex(this Parser<string, string> parser, string pattern,
            RegexOptions options = RegexOptions.None)
        {
            return new RegexParser(pattern, options);
        }
    }
}