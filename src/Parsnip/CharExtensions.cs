namespace Parsnip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Parsers;


    public static class CharExtensions
    {
        /// <summary>
        /// Parse a single character matching ch
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="ch">The character to match</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser, char ch)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharParser<TInput>(parser, c => c == ch);
        }

        public static Parser<string, char> Char(this Parser<string, char> parser, char ch)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharFromStringParser(c => c == ch);
        }

        public static Parser<char[], char> Char(this Parser<char[], char> parser, char ch)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharFromArrayParser(c => c == ch);
        }

        /// <summary>
        /// Parse a single character matching the expression
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="predicate">The character matching expression</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser,
            Func<char, bool> predicate)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharParser<TInput>(parser, predicate);
        }

        /// <summary>
        /// Parse a single character matching any chars
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">The character set to match</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser, params char[] chars)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            List<char> allowed = chars.ToList();

            return new CharParser<TInput>(parser, c => allowed.Contains(c));
        }

        /// <summary>
        /// Parse a single character matching any chars
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">The character set to match</param>
        /// <returns>The character parser</returns>
        public static Parser<string, char> Char(this Parser<string, char> parser, params char[] chars)
        {
            if (chars.Length == 0)
                return new CharFromStringParser(x => true);

            List<char> allowed = chars.ToList();

            return new CharFromStringParser(c => allowed.Contains(c));
        }

        /// <summary>
        /// Parse a single character matching any chars
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">The character set to match</param>
        /// <returns>The character parser</returns>
        public static Parser<char[], char> Char(this Parser<char[], char> parser, params char[] chars)
        {
            if (chars.Length == 0)
                return new CharFromArrayParser(x => true);

            List<char> allowed = chars.ToList();

            return new CharFromArrayParser(c => allowed.Contains(c));
        }

        /// <summary>
        /// Parse a single character matching any chars
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">The character set to match</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser, IEnumerable<char> chars)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            List<char> allowed = chars.ToList();
            if (allowed.Count == 0)
                throw new ArgumentException("At least one character must be specified");

            return new CharParser<TInput>(parser, c => allowed.Contains(c));
        }

        /// <summary>
        /// Parse an array of characters matching the input array
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">A sequence of characters to be matched, in order</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char[]> Chars<TInput>(this Parser<TInput, char> parser, params char[] chars)
        {
            if (chars.Length == 0)
                throw new ArgumentException("At least one character must be specified");

            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharArrayParser<TInput>(parser, chars);
        }

        /// <summary>
        /// Parse an array of characters matching the input string
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="chars">A sequence of characters to be matched, in order</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char[]> Chars<TInput>(this Parser<TInput, char> parser, string chars)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharArrayParser<TInput>(parser, chars.ToCharArray());
        }

        public static Parser<TInput, char> Whitespace<TInput>(this Parser<TInput, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Char(' ', '\t', '\r', '\n', '\x000C', '\x000B', '\x00A0', '\xFEFF');
        }

        public static Parser<TInput, char[]> SkipWhitespace<TInput>(this Parser<TInput, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Char(' ', '\t', '\r', '\n', '\x000C', '\x000B', '\x00A0', '\xFEFF')
                         .ZeroOrMore();
        }

        public static Parser<TInput, char> NewLine<TInput>(this Parser<TInput, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Char('\r').FirstOrDefault().Char('\n').Or(
                parser.Char('\x2028').Or(parser.Char('\x2029')));
        }

        public static Parser<string, char[]> NewLine(this Parser<string, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Chars('\r', '\n')
                         .Or(parser.Char('\r').One().Or(parser.Char('\n').One()));
        }

        public static Parser<char[], char[]> NewLine(this Parser<char[], char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Chars('\r', '\n').Or(parser.Char('\r', '\n').One());
        }

        public static Parser<TInput, string> String<TInput>(this Parser<TInput, char[]> parser)
        {
            return new CharToStringParser<TInput>(parser);
        }
    }
}