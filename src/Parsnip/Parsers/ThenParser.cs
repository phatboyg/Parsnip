namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class ThenParser<TInput, TFirst, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TFirst> _first;
        readonly Parser<TInput, TResult> _second;

        public ThenParser(Parser<TInput, TFirst> first, Parser<TInput, TResult> second)
        {
            _first = first;
            _second = second;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, TFirst> result = _first.Parse(input);
            if (result.HasValue)
            {
                var nextResult = _second.Parse(result.Next);
                if(nextResult.HasValue)
                    return nextResult;
            }

            return new Unmatched<TInput, TResult>(input);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x =>
                {
                    _first.Accept(visitor);
                    _second.Accept(visitor);
                });
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }
    }
}