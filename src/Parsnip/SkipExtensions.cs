namespace Parsnip
{
    using System;


    public static class SkipExtensions
    {
        /// <summary>
        /// Skip until the except parser is not matched (note this is a combined parser, and not a primitive
        /// one) -- yea!
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parser"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        public static Parser<TInput, T[]> Skip<TInput, T, TResult>(this Parser<TInput, T> parser,
            Parser<TInput, TResult> until)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Except(until).ZeroOrMore();
        }

        public static Parser<TInput, TResult> SkipUntil<TInput, T, TResult>(this Parser<TInput, T> parser, Parser<TInput, TResult> until)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Except(until).ZeroOrMore().Then(until);
        }

        /// <summary>
        /// Skip until the except parser is not matched (note this is a combined parser, and not a primitive
        /// one) and return the result parser
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TUntil"></typeparam>
        /// <param name="parser"></param>
        /// <param name="until"></param>
        /// <param name="resultParser"></param>
        /// <returns></returns>
        public static Parser<TInput, TResult> SkipUntil<TInput, T, TUntil, TResult>(this Parser<TInput, T> parser,
            Parser<TInput, TUntil> until, Parser<TInput, TResult> resultParser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return parser.Except(until).ZeroOrMore().Then(resultParser);
        }
    }
}