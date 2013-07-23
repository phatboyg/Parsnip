namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class IntegerParser :
        Parser<string, string>
    {
        readonly Parser<string, char[]> _parser;

        public IntegerParser(Parser<string, char> parser)
        {
            _parser = parser.Char(x => char.IsDigit(x)).Or(parser.Char('-')).OneOrMore();
        }

        public Result<string, string> Parse(Cursor<string> input)
        {
            Result<string, char[]> result = _parser.Parse(input);
            if (result.HasValue)
                return new Success<string, string>(new string(result.Value), result.Next);

            return new Unmatched<string, string>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Type ResultType
        {
            get { return typeof(char); }
        }
    }
}