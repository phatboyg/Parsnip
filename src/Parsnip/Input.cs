namespace Parsnip
{
    /// <summary>
    /// An input source of the specified type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Input<T> :
        Cursor<T>
    {
        /// <summary>
        /// Attempts to get the input at the specified offset
        /// </summary>
        /// <param name="offset">The offset into the input buffer</param>
        /// <param name="count">The number of input elements requested</param>
        /// <param name="cursor">The cursor for the requested input</param>
        /// <returns></returns>
        bool TryGet(int offset, int count, out Cursor<T> cursor);

        /// <summary>
        /// Determines if the input can support the specified offset and count
        /// </summary>
        /// <param name="offset">The offset into the input buffer</param>
        /// <param name="count">The number of input elements requested</param>
        /// <returns>True if the input is available, otherwise false</returns>
        bool CanGet(int offset, int count);
    }
}