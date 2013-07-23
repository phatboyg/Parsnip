namespace Parsnip.Parsers
{
    using System;
    using System.Text.RegularExpressions;
    using Results;


    public class RegexParser :
        Parser<string, string>
    {
        readonly string _pattern;
        readonly Regex _regex;

        public RegexParser(string pattern, RegexOptions options = RegexOptions.None)
        {
            _pattern = pattern;
            _regex = new Regex(pattern, options | RegexOptions.Compiled);
        }

        public string Pattern
        {
            get { return _pattern; }
        }

        public Result<string, string> Parse(Cursor<string> input)
        {
            if (input.Count <= 0)
                return new Unmatched<string, string>(input);

            Match match = _regex.Match(input.Data, input.Offset, input.Count);
            if (match.Success && match.Index == input.Offset)
                return new Success<string, string>(match.Value, input.Skip(match.Length));

            return new Unmatched<string, string>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Type ResultType
        {
            get { return typeof(string); }
        }
    }
}