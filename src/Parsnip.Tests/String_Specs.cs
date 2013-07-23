namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Parsing_an_array_of_characters
    {
        [Test]
        public void Should_be_convertable_to_a_string()
        {
            const string subject = "Hello World";

            var sp = new StringParser();

            var parser = from x in sp.Chars('H', 'e', 'l', 'l', 'o').String()
                         select x;

            var result = parser.Parse(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("Hello", result.Value);
        }
    }
}
