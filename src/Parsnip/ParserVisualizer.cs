namespace Parsnip
{
    using System;
    using System.Collections.Generic;


    public class ParserVisualizer :
        ParserVisitor
    {
        readonly int _depth;
        readonly string _padding;
        readonly Stack<string> _parsers;

        public ParserVisualizer()
            : this(0)
        {
        }

        ParserVisualizer(int depth)
        {
            _depth = depth;
            _padding = new string(' ', depth);
            _parsers = new Stack<string>();
        }

        void ParserVisitor.Visit<T>(Parser<T[], T> parser)
        {
            _parsers.Push(string.Format("{0}({1}) {2}", _padding, parser.ResultType.Name, parser));
        }

        void ParserVisitor.Visit<TInput>(Parser<TInput[], TInput> parser, Action<ParserVisitor> visitChildren)
        {
            _parsers.Push(string.Format("{0}({1}) {2}", _padding, parser.ResultType.Name, parser));

            var childVisualizer = new ParserVisualizer(_depth + 2);
            visitChildren(childVisualizer);
            _parsers.Push(childVisualizer.ToString());
        }

        void ParserVisitor.Visit<TInput, TResult>(Parser<TInput, TResult> parser)
        {
            _parsers.Push(string.Format("{0}({1}) {2}", _padding, parser.ResultType.Name, parser));
        }

        void ParserVisitor.Visit<TInput, TResult>(Parser<TInput, TResult> parser, Action<ParserVisitor> visitChildren)
        {
            _parsers.Push(string.Format("{0}({1}) {2}", _padding, parser.ResultType.Name, parser));

            var childVisualizer = new ParserVisualizer(_depth + 2);
            visitChildren(childVisualizer);
            _parsers.Push(childVisualizer.ToString());
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _parsers);
        }
    }
}