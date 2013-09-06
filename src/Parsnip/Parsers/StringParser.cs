namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class StringParser :
        Parser<string, char>
    {
        public Result<string, char> Parse(Cursor<string> input)
        {
            if (input.Count > input.Offset)
                return new Success<string, char>(input.Data[input.Offset], input.Skip(1));

            return new Unmatched<string, char>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Type ResultType
        {
            get { return typeof(char); }
        }

        public override string ToString()
        {
            return "[0]";
        }
    }
}