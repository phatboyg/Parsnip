namespace Parsnip
{
    /// <summary>
    /// A match is part of a successful result and can be used to apply the contents of the match
    /// to an arbitrary target
    /// </summary>
    public interface Match
    {
        /// <summary>
        /// Apply the match to a target
        /// </summary>
        /// <param name="target">Any target object, the implementation of Match will deal with the how?</param>
        void Apply<T>(T target)
            where T : class;
    }


    /// <summary>
    /// A match is part of a successful result which can be used to retrieve a typed value of the match and
    /// apply it to another type of object
    /// </summary>
    /// <typeparam name="TMatch"></typeparam>
    public interface Match<out TMatch> :
        Result<TMatch>
    {
        /// <summary>
        /// Returns true if the match is present, regardless of whether or not it has a value. For example,
        /// if the input matched, but the input specified a "null" value, IsPresent would be true but HasValue
        /// would be false.
        /// </summary>
        bool IsPresent { get; }
    }


    public interface Match<TInput, out TMatch> :
        Result<TInput, TMatch>,
        Match<TMatch>
    {
    }
}