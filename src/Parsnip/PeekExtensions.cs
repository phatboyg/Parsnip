namespace Parsnip
{
    using System;
    using Parsers;


    public static class PeekExtensions
    {
        public static PeekParser<TInput, TResult> Peek<TInput, TResult>(this Parser<TInput, TResult> peek)
        {
            if (peek == null)
                throw new ArgumentNullException("peek");

            return new PeekParser<TInput, TResult>(peek);
        }
    }
}