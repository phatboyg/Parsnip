namespace Parsnip
{
    using System;
    using System.Linq.Expressions;
    using Parsers;


    public static class WhereExtensions
    {
        /// <summary>
        /// Provides LINQ where query support
        /// </summary>
        /// <typeparam name="TInput">The input type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="parser">The source parser for the where condition</param>
        /// <param name="predicate">The predicate expression for the where condition</param>
        /// <returns>The condition parser</returns>
        public static Parser<TInput, TResult> Where<TInput, TResult>(this Parser<TInput, TResult> parser,
            Expression<Func<TResult, bool>> predicate)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return new WhereParser<TInput, TResult>(parser, predicate);
        }
    }
}