namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class SelectParser<TInput, T, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, T> _parser;
        readonly Func<T, TResult> _projector;

        public SelectParser(Parser<TInput, T> parser, Func<T, TResult> projector)
        {
            _parser = parser;
            _projector = projector;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, T> parsed = _parser.Parse(input);
            if (parsed.HasValue)
            {
                T value = parsed.Value;

                TResult result = _projector(value);
                return new Success<TInput, TResult>(result, parsed.Next);
            }

            return new Unmatched<TInput, TResult>(parsed.Next);
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }
    }
}