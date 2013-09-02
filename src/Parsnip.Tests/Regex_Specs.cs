namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class When_using_a_regular_expression
    {
        [Test]
        public void Should_match_digits()
        {
            const string subject = "120v";

            var sp = new ConstantStringParser("X");

            var parser = from x in sp.Regex(@"\d+")
                         select x;

            var result = parser.ParseString(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("120", result.Value);
            Assert.AreEqual(3, result.Next.Offset);
        }

        [Test]
        public void Should_not_match_digits()
        {
            const string subject = "v120";

            var sp = new ConstantStringParser("X");

            var parser = from x in sp.Regex(@"\d+")
                         select x;

            var result = parser.ParseString(subject);

            Assert.IsFalse(result.HasValue);
            Assert.AreEqual(0, result.Next.Offset);
        }
    }
}
