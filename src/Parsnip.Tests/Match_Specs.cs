namespace Parsnip.Tests
{
    using Inputs;
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Matching_input
    {
        [Test]
        public void Should_work()
        {
            string source = "ABC123";

            var sp = new StringParser();

            Parser<string, string> letters = from x in sp.String("ABC")
                                             select x;

            Parser<string, string> numbers = from x in sp.String("123")
                                             select x;

            var input = new StringInput(source);

            var lr = letters.Parse(input);
            Assert.IsTrue(lr.HasValue, "Should have letter value");
            
            var nr = numbers.Parse(lr.Next);
            Assert.IsTrue(nr.HasValue, "Should have number value");


        }
    }
}