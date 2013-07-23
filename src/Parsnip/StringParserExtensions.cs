namespace Parsnip
{
    using System;
    using Inputs;
    using Parsers;


    public static class StringParserExtensions
    {
        public static Result<string, T> Parse<T>(this Parser<string, T> parser, string input)
        {
            Input<string> stringInput = new StringInput(input);

            return parser.Parse(stringInput);
        }

        public static Result<char[], char> Parse(this Parser<char[], char> parser, string input)
        {
            Input<char[]> charInput = new ArrayInput<char>(input.ToCharArray());

            return parser.Parse(charInput);
        }

        public static Parser<string, string> String(this Parser<string, char> parser, string constant)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (string.IsNullOrEmpty(constant))
                throw new ArgumentNullException("constant");

            return new ConstantStringParser(constant);
        }

        public static Parser<string, string> String(this Parser<string, string> parser, string constant,
            StringComparison stringComparison)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");
            if (string.IsNullOrEmpty(constant))
                throw new ArgumentNullException("constant");

            return new ConstantStringParser(constant, stringComparison);
        }
    }
}