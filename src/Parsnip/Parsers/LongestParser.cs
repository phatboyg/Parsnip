namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class LongestParser<TInput, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TResult>[] _parsers;

        public LongestParser(Parser<TInput, TResult>[] parsers)
        {
            _parsers = parsers;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            int longestOffset = input.Offset;
            Result<TInput, TResult> longestResult = new Unmatched<TInput, TResult>(input);
            for (int index = 0; index < _parsers.Length; index++)
            {
                Result<TInput, TResult> result = _parsers[index].Parse(input);
                if (result.HasValue && result.Next.Offset > longestOffset)
                    longestResult = result;
            }

            return longestResult;
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x =>
                {
                    foreach (var parser in _parsers)
                        parser.Accept(x);
                });
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }
    }
}