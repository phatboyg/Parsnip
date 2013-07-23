namespace Parsnip
{
    using System;
    using Parsers;
    using Parsers.ValueTypeParsers;


    public static class ValueTypeExtensions
    {
        public static Parser<string, string> Integer(this Parser<string, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new IntegerParser(parser);
        }

        public static Parser<string, Int32> Int32(this Parser<string, char> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            return new Int32Parser(parser.Integer());
        }
    }
}