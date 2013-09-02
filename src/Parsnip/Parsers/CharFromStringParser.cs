namespace Parsnip.Parsers
{
    using System;
    using Results;


    /// <summary>
    /// This is a fast parser, able to quickly check if the character at the next string position matches
    /// the predicate.
    /// </summary>
    public class CharFromStringParser :
        Parser<string, char>
    {
        readonly Func<char, bool> _predicate;

        public CharFromStringParser(Func<char, bool> predicate)
        {
            _predicate = predicate;
        }

        public Result<string, char> Parse(Cursor<string> input)
        {
            if (input.Count > 0)
            {
                char c = input.Data[input.Offset];
                if (_predicate(c))
                    return new Success<string, char>(c, input.Skip(1));
            }

            return new Unmatched<string, char>(input);
        }

        public Type ResultType
        {
            get { return typeof(char); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}