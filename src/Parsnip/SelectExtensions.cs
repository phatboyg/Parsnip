namespace Parsnip
{
    using System;
    using System.Linq.Expressions;
    using Parsers;


    public static class SelectExtensions
    {
        public static Parser<TInput, TResult> Select<TInput, T, TResult>(this Parser<TInput, T> parser, Func<T, TResult> projection)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (projection == null)
                throw new ArgumentNullException("projection");

            return new SelectParser<TInput, T, TResult>(parser, projection);
        }

        public static Parser<TContent, TResult> SelectMany<TContent, T, TValue, TResult>(
            this Parser<TContent, T> parser, Expression<Func<T, Parser<TContent, TValue>>> selection,
            Expression<Func<T, TValue, TResult>> projection)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (selection == null)
                throw new ArgumentNullException("selection");
            if (projection == null)
                throw new ArgumentNullException("projection");

            return new SelectManyParser<TContent, T, TValue, TResult>(parser, selection, projection);
        }
    }
}