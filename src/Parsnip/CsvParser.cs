namespace Parsnip
{
    using System;
    using Parsers;


    public class CsvParser :
        Parser<string, string[][]>,
        Parser<string, string[]>
    {
        readonly Parser<string, char> _delimiter;
        readonly Parser<string, char> _escapeEscape;
        readonly Parser<string, char> _escapeQuote;
        readonly Parser<string, char> _newLine;
        readonly Parser<string, char> _quotedChars;
        readonly Parser<string, string> _quotedString;
        readonly Parser<string, string> _rawString;
        readonly Parser<string, char> _validChars;
        readonly Parser<string, string> _value;
        readonly Parser<string, string[]> _singleLineParser;
        Parser<string, string[][]> _parser;

        public CsvParser()
        {
            var sp = new StringParser();

            _delimiter = from d in sp.Char(',')
                         select d;

            _escapeEscape = from escapeChar in sp.Chars('\\', '\\')
                            select '\\';

            _escapeQuote = from escapeChar in sp.Chars('\\', '\"')
                           select '\"';


            _newLine = from s in sp.Chars('\r', '\n')
                                   .Or(sp.Char('\r').One().Or(sp.Char('\n').One()))
                       select '\n';


            _validChars = from c in sp.Char(x => x != ',')
                          select c;

            _rawString = from cs in _validChars.Except(_delimiter.Or(_newLine)).ZeroOrMore().String()
                         select cs;

            _quotedChars = _escapeEscape.Or(_escapeQuote).Or(sp.Char(x => x != '\"'));

            _quotedString = from open in sp.Char('\"')
                            from cs in _quotedChars.ZeroOrMore().String()
                            from close in sp.Char('\"')
                            select cs;


            _value = _quotedString.Or(_rawString);


            _singleLineParser = from vs in _value.Split(_delimiter, _newLine)
                      select vs;

            _parser = from ls in _singleLineParser.Split(_newLine)
                      select ls;
        }

        Result<string, string[][]> Parser<string, string[][]>.Parse(Cursor<string> input)
        {
            return _parser.Parse(input);
        }

        Result<string, string[]> Parser<string, string[]>.Parse(Cursor<string> input)
        {
            return _singleLineParser.Parse(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            _parser.Accept(visitor);
        }

        public Type ResultType
        {
            get { return typeof(string[][]); }
        }
    }
}