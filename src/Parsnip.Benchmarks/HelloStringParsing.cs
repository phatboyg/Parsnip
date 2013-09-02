namespace Parsnip.Benchmarks
{
    using Parsers;


    public class HelloStringParsing :
        StringParsing
    {
        readonly Parser<string, string> _parser;

        public HelloStringParsing()
        {
            var sp = new StringParser();

            _parser = from x in sp.String("Hello")
                      select x;
        }


        public void Parse(string subject)
        {
            Result<string, string> result = _parser.ParseString(subject);
        }
    }
}