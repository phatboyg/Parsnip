namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Matching_a_constant_string
    {
        [Test]
        public void Should_match_the_string_at_the_start_of_the_input()
        {
            const string hello = "Hello";

            var parser = new ConstantStringParser(hello);
            var parsed = parser.ParseString("Hello, World.");

            Assert.IsTrue(parsed.HasValue);
            Assert.AreEqual(hello, parsed.Value);
        }

        [Test]
        public void Should_continue_with_the_next_string_match()
        {
            const string hello = "Hello";

            var parser = new ConstantStringParser(hello);
            var parsed = parser.ParseString("Hello, World.");

            const string world = ", World";

            var nextParser = new ConstantStringParser(world);
            var nextParsed = nextParser.Parse(parsed.Next);

            Assert.IsTrue(nextParsed.HasValue);
            Assert.AreEqual(world, nextParsed.Value);
        }
    }
}
