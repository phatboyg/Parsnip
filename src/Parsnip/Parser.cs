namespace Parsnip
{
    using System;


    /// <summary>
    /// A parser determines if an input matches the requirements and if so returns the result
    /// </summary>
    /// <typeparam name="TInput">The parser input type</typeparam>
    /// <typeparam name="TResult">The parser result type</typeparam>
    public interface Parser<TInput, out TResult> :
        Parser
    {
        /// <summary>
        /// Parse input from the cursor and return the result
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>The result of the parser</returns>
        Result<TInput, TResult> Parse(Cursor<TInput> input);
    }

    public interface Parser :
        AcceptParserVisitor
    {
        Type ResultType { get; }
    }
}