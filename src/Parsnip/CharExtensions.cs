namespace Parsnip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
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

        /// <summary>
        /// Parse a single character matching the expression
        /// </summary>
        /// <typeparam name="TInput">The input parser type</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="predicateExpression">The character matching expression</param>
        /// <returns>The character parser</returns>
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser,
            Expression<Func<char, bool>> predicateExpression)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new CharParser<TInput>(parser, predicateExpression);
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
        public static Parser<TInput, char> Char<TInput>(this Parser<TInput, char> parser, IEnumerable<char> chars)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            List<char> allowed = chars.ToList();

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

        public static Parser<TInput, char[]> Whitespace<TInput>(this Parser<TInput, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Char(' ').Or(parser.Char('\t').Or(parser.Char('\r').Or(parser.Char('\n'))))
                         .ZeroOrMore();
        }

        public static Parser<TInput, char[]> NewLine<TInput>(this Parser<TInput, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Chars('\r', '\n')
                         .Or(parser.Char('\r').One().Or(parser.Char('\n').One()));
        }

        public static Parser<TInput, string> String<TInput>(this Parser<TInput, char[]> parser)
        {
            return new CharToStringParser<TInput>(parser);
        }
    }
}