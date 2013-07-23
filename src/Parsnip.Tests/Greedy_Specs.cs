namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class When_two_parsers_overlap
    {
        [Test]
        public void Should_choose_the_greediest_one()
        {
            string subject = "Hello, World";

            var p = new StringParser();

            Parser<string, string> first = from x in p.String("Hello")
                                           select x;

            Assert.IsTrue(first.Parse(subject).HasValue, "First did not match");

            Parser<string, string> second = from x in p.String("Hello")
                                            from y in p.Char(',')
                                            from ws in p.Whitespace()
                                            from z in p.String("World")
                                            select x + z;

            Assert.IsTrue(second.Parse(subject).HasValue, "Second did not match");

            Parser<string, string> parser = p.Longest(first, second);

            Result<string, string> result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue, "Neither matched");

            Assert.AreEqual("HelloWorld", result.Value, "Longest parser should have matched");
        }

        [Test]
        public void Should_capture_sequential_items()
        {
            string subject = "Hello World";

            var p = new StringParser();

            Parser<string, string> parser = from x in p.String("Hello")
                                           from y in p.Whitespace().Then(p.String("World"))
                                           select x + y;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("HelloWorld", result.Value);
        }

        [Test]
        public void Should_skip_until_the_world_is_found()
        {
            string subject = "Hello World";

            var p = new StringParser();

            Parser<string, string> parser = from x in p.SkipUntil(p.String("World"))
                                           select x;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("World", result.Value);
        }

        [Test]
        public void Should_skip_until_the_wor_is_found()
        {
            string subject = "Hello World";

            var p = new StringParser();

            Parser<string, string> parser = from x in p.SkipUntil(p.String("World"), p.String("Wor"))
                                           select x;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("Wor", result.Value);
        }

        [Test]
        public void Should_skip_until_the_longest_is_found()
        {
            string subject = "Hello World";

            var p = new StringParser();

            Parser<string, string> parser = from x in p.SkipUntil(p.Longest(p.String("Hello"), p.String("Hello World")))
                                           select x;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("Hello World", result.Value);
        }

        [Test]
        public void Should_skip_until_the_world()
        {
            string subject = "Hello World";

            var p = new StringParser();

            var parser = from x in p.Skip(p.String("World"))
                                            from y in p.String("World")
                                           select new {x, y};

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("Hello ".ToCharArray(), result.Value.x);
            Assert.AreEqual("World", result.Value.y);
        }
    }
}