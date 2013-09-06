namespace Parsnip.Inputs
{
    using System.Diagnostics;


    [DebuggerDisplay("{DebugDisplay}")]
    public class MatchCursor<TInput>
    {
        readonly int _index;
        readonly MatchInput<TInput> _input;
        readonly Match _match;

        public MatchCursor(MatchInput<TInput> input, int index, Match match)
        {
            _index = index;
            _input = input;
            _match = match;
        }

        string DebugDisplay
        {
            get
            {
                return string.Format("MatchCursor<{0}>, Index: {1}",
                    typeof(TInput).Name, _index);
            }
        }

        public Match Match
        {
            get { return _match; }
        }

        public int Index
        {
            get { return _index; }
        }

        public MatchInput<TInput> Input
        {
            get { return _input; }
        }

        public bool TryGetNext(out MatchCursor<TInput> cursor)
        {
            return _input.TryGet(_index + 1, out cursor);
        }
    }
}