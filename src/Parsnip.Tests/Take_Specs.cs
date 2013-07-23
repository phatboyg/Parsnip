namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Specifying_a_take_query_argument
    {
        [Test]
        public void Should_only_take_that_many_elements()
        {
            var subject = new[] {1, 2, 3, 4, 5};

            var anyParser = new AnyParser<int>();

            Parser<int[], int[]> query = (from x in anyParser
                                          select x).Take(2);

            Result<int[], int[]> result = query.Parse(subject);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(2, result.Value.Length);
        }
    }
}