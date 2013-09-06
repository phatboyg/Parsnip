namespace Parsnip.Tests
{
    using System.Linq;
    using NUnit.Framework;
    using Parsers;


    [TestFixture]
    public class Parsing_a_command_line
    {
        [Test]
        public void Should_bind_directly_to_the_model()
        {
            const string commandLine = @"-file:filename.txt";

            var parser = new CommandLineParser();

            var result = parser.Definition.ParseString(commandLine);

            Assert.IsTrue(result.HasValue);

            Assert.AreEqual("file", result.Value.Id);
            Assert.AreEqual("filename.txt", result.Value.Value);

        }

        struct Definition
        {
            public string Id;
            public string Value;

            public Definition(string id, string value)
            {
                Id = id;
                Value = value;
            }
        }


        class CommandLineParser
        {
            public CommandLineParser()
            {
                var p = new StringParser();

                Id = from ws in p.SkipWhitespace()
                     from c in p.Char(char.IsLetter)
                     from cs in p.Char(char.IsLetterOrDigit).ZeroOrMore()
                     select new string(c, 1) + new string(cs);

                Key = from ws in p.SkipWhitespace()
                     from c in p.Char(char.IsLetter)
                     from cs in (p.Char(char.IsLetterOrDigit).Or(p.Char('.'))).ZeroOrMore()
                     select new string(c, 1) + new string(cs);

//                Value = from v in (p.Char(x => !char.IsWhiteSpace(x))).ZeroOrMore()
//                        select new string(v);
                Value = from v in (p.Char().Except(p.Whitespace())).ZeroOrMore()
                        select new string(v);

                Definition = (from ws in p.SkipWhitespace()
                              from c in p.Char('-', '/')
                              from id in Id
                              from eq in p.Char(':', '=')
                              from v in Value
                              select new Definition(id, v));
            }

            public Parser<string, Definition> Definition { get; private set; }

            protected Parser<string, string> Value { get; private set; }

            protected Parser<string, string> Key { get; private set; }

            protected Parser<string, string> Id { get; private set; }
        }

        class Model
        {
            public string File { get; set; }
        }

//        class ModelMap :
//            BindingMap<Model>
//        {
//            public ModelMap()
//            {
//                Map(x => x.File, x =>
//                    {
//                        x.Required();
//                        x.Help("The input file name (may be a relative or full path name");
//                        x.Key("f"); // this is the default, property name all lowercase
//                    });
//            }
//        }
    }
}
