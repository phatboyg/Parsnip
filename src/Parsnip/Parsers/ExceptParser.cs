namespace Parsnip.Parsers
{
    using System;
    using Results;


    /// <summary>
    /// Parses uses the specified parser only if the except parser is unsuccessful
    /// </summary>
    /// <typeparam name="TInput">The input type</typeparam>
    /// <typeparam name="TExcept">Type except parser type</typeparam>
    /// <typeparam name="TResult">The parser type</typeparam>
    public class ExceptParser<TInput, TExcept, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TExcept> _except;
        readonly Parser<TInput, TResult> _parser;

        public ExceptParser(Parser<TInput, TResult> parser, Parser<TInput, TExcept> except)
        {
            _parser = parser;
            _except = except;
        }

        Result<TInput, TResult> Parser<TInput, TResult>.Parse(Cursor<TInput> input)
        {
            Result<TInput, TExcept> excepted = _except.Parse(input);
            if (excepted.HasValue)
                return new Unmatched<TInput, TResult>(input);

            return _parser.Parse(input);
        }

        void AcceptParserVisitor.Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x =>
                {
                    _parser.Accept(x);
                    _except.Accept(x);
                });
        }

        Type Parser.ResultType
        {
            get { return typeof(TResult); }
        }
    }
}