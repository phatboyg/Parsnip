namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Querying_using_linq
    {
        [Test]
        public void Should_be_possible()
        {
            const string subject = "Hello, World.";

            var constant = "Hello";

            var helloParser = new ConstantStringParser(constant);

            var query = from hello in helloParser
                        select hello;

            var parsed = query.ParseString(subject);

            Assert.IsTrue(parsed.HasValue);
            Assert.AreEqual(constant, parsed.Value);
        }
    }
}
