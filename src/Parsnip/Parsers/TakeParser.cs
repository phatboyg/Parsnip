namespace Parsnip.Parsers
{
    using System;
    using System.Collections.Generic;
    using Results;


    public class TakeParser<TInput, T> :
        Parser<TInput, T[]>
    {
        readonly int _count;
        readonly Parser<TInput, T> _parser;

        public TakeParser(Parser<TInput, T> parser, int count)
        {
            _parser = parser;
            _count = count;
        }

        public Result<TInput, T[]> Parse(Cursor<TInput> input)
        {
            Cursor<TInput> next = input;
            var results = new List<T>();
            for (int i = 0; i < _count; i++)
            {
                Result<TInput, T> r = _parser.Parse(next);
                if (r.HasValue)
                {
                    if (next == r.Next)
                        break;

                    results.Add(r.Value);
                    next = r.Next;
                }
            }

            if (results.Count < _count)
                return new Unmatched<TInput, T[]>();

            return new Success<TInput, T[]>(results.ToArray(), next);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }

        public Type ResultType
        {
            get { return typeof(T[]); }
        }
    }
}