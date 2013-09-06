namespace Parsnip.Tests
{
    using NUnit.Framework;


    [TestFixture]
    public class Defining_a_csv_parser
    {
        [Test]
        public void Should_be_able_to_parse_a_single_line()
        {
            Result<string, string[]> result = _singleLineParser.ParseString(Text);

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(4, result.Value.Length);

            Assert.AreEqual("1", result.Value[0]);
            Assert.AreEqual("Sun", result.Value[1]);
            Assert.AreEqual("Moon", result.Value[2]);
            Assert.AreEqual("12.34", result.Value[3]);
        }

        [Test]
        public void Should_be_able_to_parse_multiple_lines()
        {
            Result<string, string[][]> result = _parser.ParseString(Text);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual(2, result.Value.Length);

            Assert.AreEqual(4, result.Value[0].Length);
            Assert.AreEqual(5, result.Value[1].Length);
        }

        Parser<string, string[]> _singleLineParser;
        Parser<string, string[][]> _parser;

        const string Text = @"1,Sun,""Moon"",12.34
21,""Jupiter"",Planet,Large,Round";

        [TestFixtureSetUp]
        public void Setup()
        {
            var parser = new CsvParser();

            _singleLineParser = parser;
            _parser = parser;
        }
    }
}