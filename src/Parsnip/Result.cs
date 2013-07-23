namespace Parsnip
{
    /// <summary>
    /// The result of a parser, akin to a maybe monad
    /// </summary>
    /// <typeparam name="TInput">The parser input type</typeparam>
    /// <typeparam name="TResult">The parser result type</typeparam>
    public interface Result<TInput, out TResult>
    {
        /// <summary>
        /// True if the result has a value
        /// </summary>
        bool HasValue { get; }
        
        /// <summary>
        /// The value of the result
        /// </summary>
        TResult Value { get; }

        /// <summary>
        /// The next position after the result (or before if the parser was not matched)
        /// </summary>
        Cursor<TInput> Next { get; }
    }
}