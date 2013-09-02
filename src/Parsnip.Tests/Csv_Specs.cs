namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Defining_a_csv_parser
    {
        const string text = @"1,Sun,""Moon"",12.34
21,""Jupiter"",Planet,Large,Round";

        [TestFixtureSetUp]
        public void Setup()
        {
            var sp = new StringParser();

            var delimiter = from d in sp.Char(',')
                            select d;

            var escapeEscape = from escapeChar in sp.Chars('\\', '\\')
                               select '\\';

            var escapeQuote = from escapeChar in sp.Chars('\\', '\"')
                              select '\"';


            var newLine = from s in sp.Chars('\r', '\n')
                         .Or(sp.Char('\r').One().Or(sp.Char('\n').One()))
                              select '\n';


            Parser<string, char> validChars = from c in sp.Char(x => x != ',')
                             select c;

            Parser<string, string> rawString = from cs in validChars.Except(delimiter.Or(newLine)).ZeroOrMore().String()
                                               select cs;

            var quotedChars = escapeEscape.Or(escapeQuote).Or(sp.Char(x => x != '\"'));

            var quotedString = from open in sp.Char('\"')
                               from cs in quotedChars.ZeroOrMore().String()
                               from close in sp.Char('\"')
                               select cs;


            var value = quotedString.Or(rawString);


            Parser<string, string[]> values = from vs in value.Split(delimiter, newLine)
                         select vs;


            Result<string, string[]> rs = values.ParseString(text);

            Assert.IsTrue(rs.HasValue);
            Assert.AreEqual(4, rs.Value.Length);

            Assert.AreEqual("1", rs.Value[0]);
            Assert.AreEqual("Sun", rs.Value[1]);
            Assert.AreEqual("Moon", rs.Value[2]);
            Assert.AreEqual("12.34", rs.Value[3]);



            var lines = from ls in values.Split(newLine)
                        select ls;

            var l = lines.ParseString(text);

            Assert.IsTrue(l.HasValue);

            Assert.AreEqual(2, l.Value.Length);

            Assert.AreEqual(4, l.Value[0].Length);
            Assert.AreEqual(5, l.Value[1].Length);


//
//
//
//
//            var value = from v in (valueChars.Except(delimiter.Or(newLine)).ZeroOrMore()).String()
//                        .Or(quotedString)
//                        select v;
//
//            var line = from es in sp.Split(value, delimiter)
//                       select es;
//
//
//            var result = line.ParseString(text);
//
//
//
//
//
//
//            Assert.IsTrue(result.HasValue);
//
//            Assert.AreEqual(1, result.Value.Length);
//
//            Assert.AreEqual("FUG", result.Value[0]);




        }

        [Test]
        public void Should_be_easy_enough()
        {


            
        }
    }
}
