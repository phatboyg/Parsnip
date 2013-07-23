namespace Parsnip
{
    using System;


    public interface ParserVisitor
    {
        void Visit<TInput>(Parser<TInput[], TInput> parser);
        void Visit<TInput>(Parser<TInput[], TInput> parser, Action<ParserVisitor> visitChildren);

        void Visit<TInput, TResult>(Parser<TInput, TResult> parser);
        void Visit<TInput, TResult>(Parser<TInput, TResult> parser, Action<ParserVisitor> visitChildren);
    }
}