namespace Parsnip
{
    using System;
    using Parsers;


    public static class LongestExtensions
    {
        /// <summary>
        /// Returns the parser with the longest/deepest/greediest result from the input
        /// </summary>
        /// <typeparam name="TInput">The input type</typeparam>
        /// <typeparam name="T">The intermediate type of the input</typeparam>
        /// <typeparam name="TResult">The result type of the parser</typeparam>
        /// <param name="parser">The input parser</param>
        /// <param name="parsers">The parsers to compare</param>
        /// <returns></returns>
        public static Parser<TInput, TResult> Longest<TInput, T, TResult>(this Parser<TInput, T> parser,
            params Parser<TInput, TResult>[] parsers)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (parsers == null)
                throw new ArgumentNullException("parsers");

            return new LongestParser<TInput, TResult>(parsers);
        }
    }
}