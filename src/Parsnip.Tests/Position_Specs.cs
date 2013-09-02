namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class When_an_unamtched_result_is_returned
    {
        [Test]
        public void Should_return_the_position_that_did_not_match()
        {
            var sp = new StringParser();

            var parser = from x in sp.String("Hello World")
                         select x;

            var result = parser.ParseString("Hello");

            Assert.IsFalse(result.HasValue);
            
            Assert.AreEqual(0, result.Next.Offset);
        }

        [Test]
        public void Should_return_the_greediest_position_that_did_not_match()
        {
            var sp = new StringParser();

            var parser = from x in sp.String("Hello")
                         from y in sp.String("World")
                         select y;

            var result = parser.ParseString("Hello");

            Assert.IsFalse(result.HasValue);
            
            Assert.AreEqual(5, result.Next.Offset);
        }

        [Test]
        public void Should_return_the_greediest_position_that_did_not_match_including_whitespace()
        {
            var sp = new StringParser();

            var parser = from x in sp.String("Hello")
                         from _ in sp.Whitespace()
                         from y in sp.String("World")
                         select y;

            var result = parser.ParseString("Hello\t W0rld");

            Assert.IsFalse(result.HasValue);
            
            Assert.AreEqual(7, result.Next.Offset);
        }
    }
}
