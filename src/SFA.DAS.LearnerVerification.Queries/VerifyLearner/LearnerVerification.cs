using SFA.DAS.LearnerVerification.Types;

namespace SFA.DAS.LearnerVerification.Queries.VerifyLearner
{
    public class LearnerVerification
    {
        public LearnerVerificationResponseType ResponseType { get; set; }
        public IEnumerable<LearnerDetailMatchingError>? MatchingErrors { get; set; }
    }
}