namespace SFA.DAS.LearnerVerification.Queries.Exceptions
{
    [Serializable]
    public sealed class QueryDispatcherException : Exception
    {
        public QueryDispatcherException()
        {
        }

        public QueryDispatcherException(string message)
            : base(message)
        {
        }

        public QueryDispatcherException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}