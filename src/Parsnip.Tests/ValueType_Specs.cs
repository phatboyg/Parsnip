namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Parsing_a_string_to_an_int32
    {
        [Test]
        public void Should_match_a_series_of_integers()
        {
            string text = "123 456 789";

            var p = new StringParser();

            var parser = from a in p.Int32()
                         from ws in p.Whitespace()
                         from b in p.Int32()
                         from ws2 in p.Whitespace()
                         from c in p.Int32()
                         select new {a, b, c};

            var result = parser.ParseString(text);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(123, result.Value.a);
            Assert.AreEqual(456, result.Value.b);
            Assert.AreEqual(789, result.Value.c);
        }

        [Test]
        public void Should_match_valid_numerics()
        {
            string text = "12345";

            var p = new StringParser();

            Parser<string, int> parser = from n in p.Int32()
                                         select n;

            Result<string, int> result = parser.ParseString(text);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(12345, result.Value);
        }
    }
}