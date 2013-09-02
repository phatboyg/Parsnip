namespace Parsnip.Benchmarks
{
    using Parsers;


    public class Hello__StringParsing :
        StringParsing
    {
        readonly Parser<string, string> _parser;

        public Hello__StringParsing()
        {
            var sp = new StringParser();

            _parser = from x in sp.String("Hello")
                      from y in sp.Char(',')
                      from z in sp.Whitespace()
                      select x;
        }


        public void Parse(string subject)
        {
            Result<string, string> result = _parser.ParseString(subject);
        }
    }
}