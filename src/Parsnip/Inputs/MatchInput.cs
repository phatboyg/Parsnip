namespace Parsnip.Inputs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;


    [DebuggerDisplay("{DebugDisplay}")]
    public class MatchInput<TInput> :
        IEnumerable<Match>
    {
        readonly IList<MatchCursor<TInput>> _matches;
        readonly Parser<TInput, Match> _parser;
        int _count;
        Cursor<TInput> _input;

        public MatchInput(Parser<TInput, Match> parser, Cursor<TInput> input)
        {
            _count = int.MaxValue;
            _parser = parser;
            _input = input;
            _matches = new List<MatchCursor<TInput>>();
        }

        string DebugDisplay
        {
            get
            {
                return string.Format("MatchInput<{0}>, Count: {1}, Total: {2}",
                    typeof(TInput).Name, _matches.Count, _count == int.MaxValue ? "~" : _count.ToString());
            }
        }

        public IEnumerator<Match> GetEnumerator()
        {
            return new MatchEnumerator<TInput>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGet(int index, out MatchCursor<TInput> match)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (index < _matches.Count)
            {
                lock(_matches)
                    match = _matches[index];
                return true;
            }

            return TryParseUntil(index, out match);
        }

        bool TryParseUntil(int index, out MatchCursor<TInput> match)
        {
            if (index >= _count)
            {
                match = default(MatchCursor<TInput>);
                return false;
            }

            lock (_matches)
            {
                for (int i = _matches.Count; i <= index; i++)
                {
                    Result<TInput, Match> result = _parser.Parse(_input);
                    if (result.HasValue)
                    {
                        _input = result.Next;
                        _matches.Add(new MatchCursor<TInput>(this, index, result.Value));
                        continue;
                    }

                    _count = i;
                    break;
                }
            }

            if (index < _matches.Count)
            {
                match = _matches[index];
                return true;
            }

            match = default(MatchCursor<TInput>);
            return false;
        }
    }
}