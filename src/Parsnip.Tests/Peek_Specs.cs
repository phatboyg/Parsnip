namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Peeking_ahead_in_the_input_stream
    {
        [Test]
        public void Should_not_advance_the_input_itself()
        {
            string subject = "0123456789";

            var p = new StringParser();

            var parser = from ws in p.String("01")
                         from peeked in p.String("234").Peek()
                         from value in p.String("234")
                         select new {peeked, value};

            var result = parser.ParseString(subject);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(result.Value.peeked, result.Value.value);
        }

        [Test]
        public void Should_not_match_if_the_peeked_value_not_matched()
        {
            string subject = "0123456789";

            var p = new StringParser();

            var parser = from ws in p.String("01")
                         from peeked in p.String("789").Peek()
                         from value in p.String("234")
                         select new {peeked, value};

            var result = parser.ParseString(subject);

            Assert.IsFalse(result.HasValue, "Pattern should not matched");

            Assert.AreEqual(2, result.Next.Offset);
        }
    }
}