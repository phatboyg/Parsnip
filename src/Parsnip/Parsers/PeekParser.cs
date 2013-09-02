namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class PeekParser<TInput, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TResult> _parser;

        public PeekParser(Parser<TInput, TResult> parser)
        {
            _parser = parser;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, TResult> result = _parser.Parse(input);
            if (result.HasValue)
                return new Success<TInput, TResult>(result.Value, input);

            return new Unmatched<TInput, TResult>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => { _parser.Accept(x); });
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }
    }
}