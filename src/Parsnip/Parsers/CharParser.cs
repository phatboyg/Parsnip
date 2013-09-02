namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class CharParser<TInput> :
        Parser<TInput, char>
    {
        readonly Parser<TInput, char> _parser;
        readonly Func<char, bool> _predicate;

        public CharParser(Parser<TInput, char> parser, Func<char, bool> predicate)
        {
            _parser = parser;
            _predicate = predicate;
        }

        public Result<TInput, char> Parse(Cursor<TInput> input)
        {
            Result<TInput, char> result = _parser.Parse(input);
            if (result.HasValue)
            {
                char c = result.Value;
                if (_predicate(c))
                    return new Success<TInput, char>(c, result.Next);
            }

            return new Unmatched<TInput, char>(input);
        }

        public Type ResultType
        {
            get { return typeof(char); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }
    }
}