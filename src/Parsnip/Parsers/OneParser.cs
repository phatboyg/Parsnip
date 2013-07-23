namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class OneParser<TInput, T> :
        Parser<TInput, T[]>
    {
        readonly Parser<TInput, T> _parser;

        public OneParser(Parser<TInput, T> parser)
        {
            _parser = parser;
        }

        Result<TInput, T[]> Parser<TInput, T[]>.Parse(Cursor<TInput> input)
        {
            Result<TInput, T> result = _parser.Parse(input);
            if (result.HasValue)
                return new Success<TInput, T[]>(new[] {result.Value}, result.Next);

            return new Unmatched<TInput, T[]>(input);
        }

        void AcceptParserVisitor.Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }

        Type Parser.ResultType
        {
            get { return typeof(T[]); }
        }
    }
}