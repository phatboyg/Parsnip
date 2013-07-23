namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Parsing_any_element_from_an_array
    {
        [Test]
        public void Should_return_the_first_element()
        {
            const string subject = "Hello, World.";

            var anyParser = new AnyParser<char>();

            Parser<char[], char> query = from any in anyParser
                                         select any;

            Result<char[], char> parsed = query.Parse(subject);

            Assert.IsTrue(parsed.HasValue);
            Assert.AreEqual('H', parsed.Value);

            Assert.AreEqual(1, parsed.Next.Offset);
        }

        [Test]
        public void Should_return_the_first_element_of_value_type()
        {
            var subject = new[] {1, 2, 3, 4, 5, 6};

            var anyParser = new AnyParser<int>();

            Parser<int[], int> query = from any in anyParser
                                         select any;

            Result<int[], int> parsed = query.Parse(subject);

            Assert.IsTrue(parsed.HasValue);
            Assert.AreEqual(1, parsed.Value);

            Assert.AreEqual(1, parsed.Next.Offset);
        }

        [Test]
        public void Should_return_a_series_of_elements()
        {
            var subject = new[] {1, 2, 3, 4, 5, 6};

            var anyParser = new AnyParser<int>();

            Parser<int[], int[]> query = from one in anyParser
                                       from two in anyParser
                                       from three in anyParser
                                       from four in anyParser
                                       select new[] {one, two, three, four};

            Result<int[], int[]> parsed = query.Parse(subject);

            Assert.IsTrue(parsed.HasValue);
            Assert.AreEqual(4, parsed.Value.Length);
            Assert.AreEqual(1, parsed.Value[0]);
            Assert.AreEqual(2, parsed.Value[1]);
            Assert.AreEqual(3, parsed.Value[2]);
            Assert.AreEqual(4, parsed.Value[3]);

            Assert.AreEqual(4, parsed.Next.Offset);
        }
    }
}