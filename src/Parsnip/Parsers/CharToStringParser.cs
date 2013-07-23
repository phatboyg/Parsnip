namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class CharToStringParser<TInput> :
        Parser<TInput, string>
    {
        readonly Parser<TInput, char[]> _parser;

        public CharToStringParser(Parser<TInput, char[]> parser)
        {
            _parser = parser;
        }

        public Result<TInput, string> Parse(Cursor<TInput> input)
        {
            Result<TInput, char[]> result = _parser.Parse(input);
            if (result.HasValue)
                return new Success<TInput, string>(new string(result.Value), result.Next);

            return new Unmatched<TInput, string>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }

        public Type ResultType
        {
            get { return typeof(string); }
        }
    }
}