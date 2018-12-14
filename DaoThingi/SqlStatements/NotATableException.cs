using System;
using System.Runtime.Serialization;

namespace DaoThingi.Database
{
    [Serializable]
    internal class NotATableException : Exception
    {
        public NotATableException()
        {
        }

        public NotATableException(string message) : base(message)
        {
        }

        public NotATableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotATableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}