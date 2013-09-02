namespace Parsnip.Benchmarks
{
    using Parsers;


    public class Hello_StringParsing :
        StringParsing
    {
        readonly Parser<string, string> _parser;

        public Hello_StringParsing()
        {
            var sp = new StringParser();

            _parser = from x in sp.String("Hello")
                      from y in sp.Char(',')
                      select x;
        }


        public void Parse(string subject)
        {
            Result<string, string> result = _parser.ParseString(subject);
        }
    }
}