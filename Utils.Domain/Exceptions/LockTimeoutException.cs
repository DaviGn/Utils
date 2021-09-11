using System;
using System.Runtime.Serialization;

namespace Utils.Domain.Exceptions
{
    public class LockTimeoutException : ApplicationException
    {
        public LockTimeoutException()
        {
        }

        public LockTimeoutException(string message) : base(message)
        {
        }

        public LockTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LockTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
