namespace Parsnip
{
    using System;
    using Parsers;


    public static class SeriesExtensions
    {
        /// <summary>
        /// Returns a parsed element as a singular array instead of a single element.
        /// </summary>
        public static Parser<TInput, T[]> One<TInput, T>(this Parser<TInput, T> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new OneParser<TInput, T>(parser);
        }

        /// <summary>
        /// Returns a series of parsed elements as an array as long as there are one or more elements.
        /// </summary>
        public static Parser<TInput, T[]> OneOrMore<TInput, T>(this Parser<TInput, T> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new SeriesParser<TInput, T>(parser, true);
        }

        /// <summary>
        /// Returns a series of parsed elements as an array.
        /// </summary>
        public static Parser<TInput, T[]> ZeroOrMore<TInput, T>(this Parser<TInput, T> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new SeriesParser<TInput, T>(parser, false);
        }

        /// <summary>
        /// Take exactly the number of elements specified from the document starting from the current position of the cursor.
        /// </summary>
        public static Parser<TInput, T[]> Take<TInput, T>(this Parser<TInput, T> parser, int count)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "Count must be >= 0");

            return new TakeParser<TInput, T>(parser, count);
        }
    }
}