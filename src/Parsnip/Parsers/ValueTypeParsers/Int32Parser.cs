namespace Parsnip.Parsers.ValueTypeParsers
{
    using System;
    using Results;


    public class Int32Parser :
        Parser<string, int>
    {
        readonly Parser<string, string> _parser;

        public Int32Parser(Parser<string, string> parser)
        {
            _parser = parser;
        }

        public Result<string, int> Parse(Cursor<string> input)
        {
            var result = _parser.Parse(input);
            if (result.HasValue)
            {
                Int32 value;
                if (Int32.TryParse(result.Value, out value))
                    return new Success<string, Int32>(value, result.Next);
            }

            return new Unmatched<string, Int32>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Type ResultType
        {
            get { return typeof(Int32); }
        }
    }
}