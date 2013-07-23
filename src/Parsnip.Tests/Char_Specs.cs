namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Specifying_whitespace
    {
        [Test]
        public void Should_select_all_whitespace_before_value()
        {
            string subject = "   \t\r\n\tHello";

            var p = new StringParser();

            var parser = from ws in p.Whitespace()
                         from value in p.String("Hello")
                         select value;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);
        }

        [Test]
        public void Should_not_match_without_whitespace_filter()
        {
            string subject = "   \t\r\n\tHello";

            var helloParser = new ConstantStringParser("Hello");

            var parser = from value in helloParser
                         select value;

            var result = parser.Parse(subject);

            Assert.IsFalse(result.HasValue);
        }
    }
}
