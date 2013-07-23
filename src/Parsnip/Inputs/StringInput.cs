namespace Parsnip.Inputs
{
    using System;


    public class StringInput :
        Input<string>
    {
        readonly string _data;
        readonly Cursor<string> _start;

        public StringInput(string data)
        {
            _data = data ?? "";

            _start = new StringCursor(this, 0, _data.Length);
        }

        public int Offset
        {
            get { return _start.Offset; }
        }

        public int Count
        {
            get { return _start.Count; }
        }

        public string Data
        {
            get { return _data; }
        }

        public Input<string> Input
        {
            get { return this; }
        }

        public bool TryGet(int offset, int count, out Cursor<string> cursor)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "must be >= 0");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "must be >= 0");

            if (offset + count <= _start.Count)
            {
                cursor = new StringCursor(this, offset, count);
                return true;
            }

            cursor = default(Cursor<string>);
            return false;
        }
    }
}