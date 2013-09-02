namespace Parsnip
{
    using Inputs;


    public static class InputExtensions
    {
        /// <summary>
        /// Parse an array of elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parser"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Result<T[], TResult> ParseArray<T, TResult>(this Parser<T[], TResult> parser, T[] array)
        {
            Input<T[]> input = new ArrayInput<T>(array);

            return parser.Parse(input);
        }

        /// <summary>
        /// Parse a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Result<string, T> ParseString<T>(this Parser<string, T> parser, string text)
        {
            Input<string> input = new StringInput(text);

            return parser.Parse(input);
        }

        /// <summary>
        /// Parse a string as an array of characters
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Result<char[], char> ParseString(this Parser<char[], char> parser, string text)
        {
            Input<char[]> input = new ArrayInput<char>(text.ToCharArray());

            return parser.Parse(input);
        }
    }
}