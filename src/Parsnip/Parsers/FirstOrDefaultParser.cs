namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class FirstOrDefaultParser<TInput, T> :
        Parser<TInput, T>
    {
        readonly T _defaultValue;
        readonly Parser<TInput, T> _parser;

        public FirstOrDefaultParser(Parser<TInput, T> parser, T defaultValue = default(T))
        {
            _parser = parser;
            _defaultValue = defaultValue;
        }

        Result<TInput, T> Parser<TInput, T>.Parse(Cursor<TInput> input)
        {
            Result<TInput, T> result = _parser.Parse(input);
            if (result.HasValue)
                return result;

            return new Success<TInput, T>(_defaultValue, input);
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