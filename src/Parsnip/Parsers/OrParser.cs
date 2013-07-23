namespace Parsnip.Parsers
{
    using System;


    public class OrParser<TInput, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TResult> _first;
        readonly Parser<TInput, TResult> _second;

        public OrParser(Parser<TInput, TResult> first, Parser<TInput, TResult> second)
        {
            _first = first;
            _second = second;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, TResult> result = _first.Parse(input);
            if (result.HasValue)
                return result;

            return _second.Parse(input);
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


    public class OrParser<TInput, TResult, T1, T2> :
        Parser<TInput, TResult>
        where T1 : TResult
        where T2 : TResult
    {
        readonly Parser<TInput, T1> _first;
        readonly Parser<TInput, T2> _second;

        public OrParser(Parser<TInput, T1> first, Parser<TInput, T2> second)
        {
            _first = first;
            _second = second;
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, T1> result = _first.Parse(input);
            if (result.HasValue)
                return (Result<TInput, TResult>)result;

            return (Result<TInput, TResult>)_second.Parse(input);
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