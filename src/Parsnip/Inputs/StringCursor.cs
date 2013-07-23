namespace Parsnip.Inputs
{
    public class StringCursor :
        Cursor<string>
    {
        readonly int _count;
        readonly string _data;
        readonly Input<string> _input;
        readonly int _offset;

        public StringCursor(Input<string> input, int offset, int count)
        {
            _input = input;
            _offset = offset;
            _count = count;
            _data = _input.Data;
        }

        int Cursor<string>.Offset
        {
            get { return _offset; }
        }

        int Cursor<string>.Count
        {
            get { return _count; }
        }

        string Cursor<string>.Data
        {
            get { return _data; }
        }

        Input<string> Cursor<string>.Input
        {
            get { return _input; }
        }
    }
}