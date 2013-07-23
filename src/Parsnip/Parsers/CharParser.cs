namespace Parsnip.Parsers
{
    using System;
    using System.Linq.Expressions;


    public class CharParser<TInput> :
        Parser<TInput, char>
    {
        readonly Parser<TInput, char> _parser;
        readonly Expression<Func<char, bool>> _predicateExpression;

        public CharParser(Parser<TInput, char> parser, Expression<Func<char, bool>> predicateExpression)
        {
            _predicateExpression = predicateExpression;

            Func<char, bool> predicate = _predicateExpression.Compile();

            _parser = from c in parser where predicate(c) select c;
        }

        public Result<TInput, char> Parse(Cursor<TInput> input)
        {
            return _parser.Parse(input);
        }

        public Type ResultType
        {
            get { return typeof(char); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this, x => _parser.Accept(x));
        }
    }
}