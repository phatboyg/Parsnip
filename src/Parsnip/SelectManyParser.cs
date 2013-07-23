namespace Parsnip
{
    using System;
    using System.Linq.Expressions;
    using Results;


    public class SelectManyParser<TInput, T, TSelect, TResult> :
        Parser<TInput, TResult>
    {
        readonly Parser<TInput, T> _parser;
        readonly Func<T, TSelect, TResult> _project;
        readonly Expression<Func<T, TSelect, TResult>> _projectExpression;
        readonly Func<T, Parser<TInput, TSelect>> _select;
        readonly Expression<Func<T, Parser<TInput, TSelect>>> _selectExpression;

        public SelectManyParser(Parser<TInput, T> parser, Expression<Func<T, Parser<TInput, TSelect>>> selectExpression,
            Expression<Func<T, TSelect, TResult>> projectExpression)
        {
            _parser = parser;
            _selectExpression = selectExpression;
            _projectExpression = projectExpression;

            _select = _selectExpression.Compile();
            _project = _projectExpression.Compile();
        }

        public Result<TInput, TResult> Parse(Cursor<TInput> input)
        {
            Result<TInput, T> parsed = _parser.Parse(input);
            if (parsed.HasValue)
            {
                T value = parsed.Value;
                Result<TInput, TSelect> selected = _select(value).Parse(parsed.Next);
                if (selected.HasValue)
                {
                    TResult result = _project(value, selected.Value);
                    return new Success<TInput, TResult>(result, selected.Next);
                }
            }

            return new Unmatched<TInput, TResult>(parsed.Next);
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
            return string.Format("from {0} to {1}", typeof(TInput), typeof(TResult));
        }
    }
}