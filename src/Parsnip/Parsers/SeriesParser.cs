namespace Parsnip.Parsers
{
    using System;
    using System.Collections.Generic;
    using Results;


    public class SeriesParser<TInput, T> :
        Parser<TInput, T[]>
    {
        readonly bool _atLeastOne;
        readonly Parser<TInput, T> _parser;

        public SeriesParser(Parser<TInput, T> parser, bool atLeastOne)
        {
            _parser = parser;
            _atLeastOne = atLeastOne;
        }

        public Result<TInput, T[]> Parse(Cursor<TInput> input)
        {
            Cursor<TInput> next = input;
            var results = new List<T>();

            Result<TInput, T> r = _parser.Parse(next);
            if (_atLeastOne && !r.HasValue)
                return new Unmatched<TInput, T[]>(input);

            while (r.HasValue)
            {
                if (next == r.Next)
                    break;

                results.Add(r.Value);
                next = r.Next;

                r = _parser.Parse(next);
            }

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