namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class AnyParser<TResult> :
        Parser<TResult[], TResult>
    {
        public Result<TResult[], TResult> Parse(Cursor<TResult[]> input)
        {
            if (input.Count > input.Offset)
                return new Success<TResult[], TResult>(input.Data[input.Offset], input.Skip(1));

            return new Unmatched<TResult[], TResult>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }

        public override string ToString()
        {
            return "*";
        }
    }
}