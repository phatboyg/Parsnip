namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class CharArrayParser<TInput> :
        Parser<TInput, char[]>
    {
        readonly char[] _chars;
        readonly Parser<TInput, char> _parser;

        public CharArrayParser(Parser<TInput, char> parser, char[] chars)
        {
            _parser = parser;
            _chars = chars;
        }

        Result<TInput, char[]> Parser<TInput, char[]>.Parse(Cursor<TInput> input)
        {
            Cursor<TInput> next = input;
            int i;
            for (i = 0; i < _chars.Length; i++)
            {
                Result<TInput, char> r = _parser.Parse(next);
                if (r.HasValue)
                {
                    if (next == r.Next)
                        break;

                    if (_chars[i] != r.Value)
                        break;

                    next = r.Next;
                }
            }

            if (i < _chars.Length)
                return new Unmatched<TInput, char[]>();

            return new Success<TInput, char[]>(_chars, next);
        }

        void AcceptParserVisitor.Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }

        Type Parser.ResultType
        {
            get { return typeof(char[]); }
        }
    }
}