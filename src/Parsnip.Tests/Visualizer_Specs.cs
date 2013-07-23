namespace Parsnip.Tests
{
    using System;
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Visualizing_a_parser
    {
        [Test]
        public void Should_support_a_combined_parser()
        {
            var anyParser = new AnyParser<int>();

            Parser<int[], int> parser = from x in anyParser
                                        where x == 1
                                        select x;

            var visualizer = new ParserVisualizer();
            parser.Accept(visualizer);
            string text = visualizer.ToString();

            Console.WriteLine(text);

            string expected = @"  (Int32) *
(Int32) Where x => (x == 1)";
            Assert.AreEqual(expected, text);
        }

        [Test]
        public void Should_support_a_combined_parser_of_the_same_type()
        {
            var anyParser = new AnyParser<int>();

            Parser<int[], int> parser = from x in anyParser
                                        from y in anyParser
                                        where x == 1
                                        select x;

            var visualizer = new ParserVisualizer();
            parser.Accept(visualizer);
            string text = visualizer.ToString();

            Console.WriteLine(text);

            string expected = @"  (Int32) *
(Int32) Where x => (x == 1)";
            Assert.AreEqual(expected, text);
        }

        [Test]
        public void Should_support_a_single_parser()
        {
            var parser = new ConstantStringParser("Hello");

            var visualizer = new ParserVisualizer();
            parser.Accept(visualizer);
            string text = visualizer.ToString();

            string expected = @"(String) == ""Hello""";
            Assert.AreEqual(expected, text);
        }
    }
}