namespace Parsnip.Inputs
{
    public class ArrayCursor<T> :
        Cursor<T[]>
    {
        readonly int _count;
        readonly T[] _data;
        readonly Input<T[]> _input;
        readonly int _offset;

        public ArrayCursor(Input<T[]> input, int offset, int count)
        {
            _input = input;
            _data = _input.Data;
            _offset = offset;
            _count = count;
        }

        T[] Cursor<T[]>.Data
        {
            get { return _data; }
        }

        Input<T[]> Cursor<T[]>.Input
        {
            get { return _input; }
        }

        int Cursor<T[]>.Offset
        {
            get { return _offset; }
        }

        int Cursor<T[]>.Count
        {
            get { return _count; }
        }
    }
}