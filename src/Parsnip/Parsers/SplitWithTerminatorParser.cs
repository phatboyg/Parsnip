namespace Parsnip.Parsers
{
    using System;
    using System.Collections.Generic;
    using Results;


    public class SplitWithTerminatorParser :
        Parser<string, string[]>
    {
        readonly Parser<string, string> _element;
        readonly Parser<string, char> _separator;
        readonly Parser<string, char> _terminator;

        public SplitWithTerminatorParser(Parser<string, string> element, Parser<string, char> separator,
            Parser<string, char> terminator)
        {
            _element = element;
            _separator = separator;
            _terminator = terminator;
        }

        public Result<string, string[]> Parse(Cursor<string> input)
        {
            Cursor<string> next = input;
            var results = new List<string>();

            Result<string, string> element = _element.Parse(next);
            while (element.HasValue)
            {
                if (next == element.Next)
                    break;

                results.Add(element.Value);
                next = element.Next;

                Result<string, char> terminator = _terminator.Parse(next);
                if (terminator.HasValue)
                    break;

                Result<string, char> separator = _separator.Parse(next);
                if (!separator.HasValue)
                    break;
                next = separator.Next;

                element = _element.Parse(next);
            }

            return new Success<string, string[]>(results.ToArray(), next);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x =>
                {
                    _element.Accept(x);
                    _separator.Accept(x);
                    _terminator.Accept(x);
                });
        }

        public Type ResultType
        {
            get { return typeof(string[]); }
        }
    }

    public class SplitParser<TResult> :
        Parser<string, TResult[]>
    {
        readonly Parser<string, TResult> _element;
        readonly Parser<string, char> _separator;

        public SplitParser(Parser<string, TResult> element, Parser<string, char> separator)
        {
            _element = element;
            _separator = separator;
        }

        public Result<string, TResult[]> Parse(Cursor<string> input)
        {
            Cursor<string> next = input;
            var results = new List<TResult>();

            Result<string, TResult> element = _element.Parse(next);
            while (element.HasValue)
            {
                if (next == element.Next)
                    break;

                results.Add(element.Value);
                next = element.Next;

                Result<string, char> separator = _separator.Parse(next);
                if (!separator.HasValue)
                    break;
                next = separator.Next;

                element = _element.Parse(next);
            }

            return new Success<string, TResult[]>(results.ToArray(), next);
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x =>
                {
                    _element.Accept(x);
                    _separator.Accept(x);
                });
        }

        public Type ResultType
        {
            get { return typeof(string[]); }
        }
    }
}