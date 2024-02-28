using System.Runtime.Serialization;

namespace SFA.DAS.LearnerVerification.Queries.Exceptions
{
    [Serializable]
    public sealed class QueryException : Exception
    {
        public QueryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private QueryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}