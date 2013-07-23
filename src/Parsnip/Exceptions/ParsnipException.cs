namespace Parsnip
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class ParsnipException :
        Exception
    {
        public ParsnipException()
        {
        }

        public ParsnipException(string message)
            : base(message)
        {
        }

        public ParsnipException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ParsnipException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}