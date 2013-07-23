namespace Parsnip.Parsers
{
    using System;
    using System.Linq.Expressions;
    using Results;


    public class WhereParser<TInput, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, TResult> _parser;
        readonly Func<TResult, bool> _predicate;
        readonly Expression<Func<TResult, bool>> _predicateExpression;

        public WhereParser(Parser<TInput, TResult> parser, Expression<Func<TResult, bool>> predicateExpression)
        {
            _parser = parser;
            _predicateExpression = predicateExpression;
            _predicate = predicateExpression.Compile();
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, TResult> result = _parser.Parse(input);
            if (result.HasValue)
            {
                if (_predicate(result.Value))
                    return result;
            }

            return new Unmatched<TInput, TResult>(input);
        }

        public Type ResultType
        {
            get { return typeof(TResult); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }

        public override string ToString()
        {
            return string.Format("Where {0}", _predicateExpression);
        }
    }
}