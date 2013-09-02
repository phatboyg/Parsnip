namespace Parsnip
{
    public interface AcceptParserVisitor
    {
        /// <summary>
        /// A parser must accept a visitor so that the composition of the parser
        /// can be discovered. All parsers should call the visitor's visit methods
        /// for itself and any contained parsers in order of composition
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        void Accept(ParserVisitor visitor);
    }
}