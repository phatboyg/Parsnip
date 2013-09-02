namespace Parsnip
{
    using System;
    using Parsers;


    public static class StringParserExtensions
    {
        /// <summary>
        /// Match the specified string
        /// </summary>
        /// <param name="parser">The parser to match</param>
        /// <param name="constant">The string to match</param>
        /// <returns></returns>
        public static Parser<string, string> String(this Parser<string, char> parser, string constant)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (string.IsNullOrEmpty(constant))
                throw new ArgumentNullException("constant");

            return new ConstantStringParser(constant);
        }

        /// <summary>
        /// Match the specified string
        /// </summary>
        /// <param name="parser">The parser to match</param>
        /// <param name="constant">The string to match</param>
        /// <param name="stringComparison">The type of string comparison to perform</param>
        /// <returns></returns>
        public static Parser<string, string> String(this Parser<string, string> parser, string constant,
            StringComparison stringComparison)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (string.IsNullOrEmpty(constant))
                throw new ArgumentNullException("constant");

            return new ConstantStringParser(constant, stringComparison);
        }
    }
}