namespace Parsnip
{
    using Inputs;


    public static class ArrayParserExtensions
    {
        public static Result<T[], TResult> Parse<T, TResult>(this Parser<T[], TResult> parser, T[] input)
        {
            Input<T[]> arrayInput = new ArrayInput<T>(input);

            return parser.Parse(arrayInput);
        }
    }
}