namespace Parsnip.Tests
{
    using NUnit.Framework;
    using Results;


    [TestFixture]
    public class Using_the_success_result
    {
        [Test]
        public void Should_have_a_value()
        {
            const string expected = "Hello, World!";

            var success = new Success<string, string>(expected, Cursor.Empty<string>());

            Assert.IsTrue(success.HasValue);

            Assert.AreEqual(expected, success.Value);
        }

        [Test]
        public void Should_have_default_value()
        {
            var success = new Success<string, string>();

            Assert.IsFalse(success.HasValue);

            Assert.AreEqual(default(string), success.Value);
        }
    }
}
