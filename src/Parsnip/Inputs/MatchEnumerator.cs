namespace Parsnip.Inputs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;


    public class MatchEnumerator<TInput> :
        IEnumerator<Match>
    {
        readonly MatchInput<TInput> _input;
        MatchCursor<TInput> _cursor;

        public MatchEnumerator(MatchInput<TInput> input)
        {
            _input = input;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_cursor == null)
                return _input.TryGet(0, out _cursor);

            MatchCursor<TInput> nextCursor;
            if (_cursor.TryGetNext(out nextCursor))
            {
                _cursor = nextCursor;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _cursor = null;
        }

        public Match Current
        {
            get
            {
                if (_cursor == null)
                    throw new InvalidOperationException("The enumerator has not been moved to a valid position");
                return _cursor.Match;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}