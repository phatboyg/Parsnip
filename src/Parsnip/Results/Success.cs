namespace Parsnip.Results
{
    public struct Success<TInput, T> : 
        Result<TInput, T>
    {
        readonly bool _hasValue;
        readonly T _value;
        readonly Cursor<TInput> _next;

        public Success(T value, Cursor<TInput> next)
        {
            _value = value;
            _next = next;
            _hasValue = true;
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public T Value
        {
            get { return _value; }
        }

        public Cursor<TInput> Next
        {
            get { return _next; }
        }
    }
}