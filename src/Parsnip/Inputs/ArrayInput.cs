namespace Parsnip.Inputs
{
    using System.Diagnostics;


    [DebuggerDisplay("{DebugDisplay}")]
    public class ArrayInput<T> :
        Input<T[]>
    {
        readonly T[] _data;
        readonly Cursor<T[]> _start;

        public ArrayInput(T[] data)
        {
            _data = data ?? new T[0];

            _start = new ArrayCursor<T>(this, 0, _data.Length);
        }

        protected string DebugDisplay
        {
            get { return string.Format("{0}<{1}>: Count = {2}", typeof(ArrayInput<>).Name, typeof(T).Name, _start.Count); }
        }

        int Cursor<T[]>.Offset
        {
            get { return _start.Offset; }
        }

        int Cursor<T[]>.Count
        {
            get { return _start.Count; }
        }

        T[] Cursor<T[]>.Data
        {
            get { return _data; }
        }

        Input<T[]> Cursor<T[]>.Input
        {
            get { return this; }
        }

        bool Input<T[]>.TryGet(int offset, int count, out Cursor<T[]> cursor)
        {
            if (offset + count <= _start.Count)
            {
                cursor = new ArrayCursor<T>(this, offset, count);
                return true;
            }

            cursor = default(Cursor<T[]>);
            return false;
        }

        bool Input<T[]>.CanGet(int offset, int count)
        {
            return offset + count <= _start.Count;
        }
    }
}