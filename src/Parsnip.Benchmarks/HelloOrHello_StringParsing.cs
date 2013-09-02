namespace Parsnip.Benchmarks
{
    using Parsers;


    public class HelloOrHello_StringParsing :
        StringParsing
    {
        readonly Parser<string, string> _parser;

        public HelloOrHello_StringParsing()
        {
            var sp = new StringParser();

            _parser = (from hello in sp.String("Hello") select hello)
                .Longest(from hello in sp.String("Hello")
                         from comma in sp.Char(',')
                         select hello);
        }


        public void Parse(string subject)
        {
            Result<string, string> result = _parser.ParseString(subject);
        }
    }
}