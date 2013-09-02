namespace Parsnip.Benchmarks
{
    using Parsers;


    public class Hello__WorldStringParsing :
        StringParsing
    {
        readonly Parser<string, string> _parser;

        public Hello__WorldStringParsing()
        {
            var sp = new StringParser();

            _parser = from hello in sp.String("Hello")
                      from comma in sp.Char(',')
                      from ws in sp.Whitespace()
                      from world in sp.String("World")
                      select hello;
        }


        public void Parse(string subject)
        {
            Result<string, string> result = _parser.ParseString(subject);
        }
    }
}