namespace Parsnip.Results
{
    /// <summary>
    /// An unmatched parse result
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public struct Unmatched<TInput, TResult> :
        Result<TInput, TResult>
    {
        readonly Cursor<TInput> _next;

        public Unmatched(Cursor<TInput> next)
            : this()
        {
            _next = next;
        }

        public bool HasValue
        {
            get { return false; }
        }

        public TResult Value
        {
            get { return default(TResult); }
        }

        public Cursor<TInput> Next
        {
            get { return _next; }
        }
    }
}