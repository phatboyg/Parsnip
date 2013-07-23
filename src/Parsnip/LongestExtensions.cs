namespace Parsnip
{
    using System;
    using Parsers;


    public static class LongestExtensions
    {
        public static Parser<TInput, TResult> Longest<TInput, T, TResult>(this Parser<TInput, T> parser,
            params Parser<TInput, TResult>[] parsers)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (parsers == null)
                throw new ArgumentNullException("parsers");

            return new LongestParser<TInput, T, TResult>(parser, parsers);
        }
    }
}