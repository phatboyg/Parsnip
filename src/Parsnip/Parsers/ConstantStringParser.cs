namespace Parsnip.Parsers
{
    using System;
    using Results;


    public class ConstantStringParser :
        Parser<string, string>
    {
        readonly StringComparison _comparisonType;
        readonly string _constant;

        public ConstantStringParser(string constant, StringComparison comparisonType = StringComparison.Ordinal)
        {
            _constant = constant;
            _comparisonType = comparisonType;
        }

        public Result<string, string> Parse(Cursor<string> input)
        {
            int compareLength = input.Count > _constant.Length ? _constant.Length : input.Count;
            if (input.Count >= _constant.Length)
            {
                if (string.Compare(_constant, 0, input.Data, input.Offset, compareLength, _comparisonType) == 0)
                {
                    Cursor<string> next;
                    input.Input.TryGet(input.Offset + _constant.Length, input.Count - _constant.Length, out next);
                    return new Success<string, string>(_constant, next);
                }
            }

            return new Unmatched<string, string>(input);
        }

        public Type ResultType
        {
            get { return typeof(string); }
        }

        public void Accept(ParserVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Format("== \"{0}\"", _constant);
        }
    }
}