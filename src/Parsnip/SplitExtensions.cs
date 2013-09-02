namespace Parsnip
{
    using System;
    using Parsers;


    public static class SplitExtensions
    {
        /// <summary>
        /// Returns a series of parsed elements as an array as long as there are one or more elements.
        /// </summary>
        public static Parser<string, string[]> Split(this
            Parser<string, string> element, Parser<string, char> separator, Parser<string, char> terminator)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (separator == null)
                throw new ArgumentNullException("separator");
            if (terminator == null)
                throw new ArgumentNullException("terminator");

            return new SplitWithTerminatorParser(element, separator, terminator);
        }

        /// <summary>
        /// Returns a series of parsed elements as an array as long as there are one or more elements.
        /// </summary>
        public static Parser<string, TResult[]> Split<TResult>(this
            Parser<string, TResult> element, Parser<string, char> separator)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (separator == null)
                throw new ArgumentNullException("separator");

            return new SplitParser<TResult>(element, separator);
        }
    }
}