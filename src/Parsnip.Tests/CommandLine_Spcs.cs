namespace Parsnip.Tests
{
    using NUnit.Framework;


    [TestFixture]
    public class Parsing_a_command_line
    {
        [Test]
        public void Should_bind_directly_to_the_model()
        {
            const string commandLine = @"-f ""filename.txt""";



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
