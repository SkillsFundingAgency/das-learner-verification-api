namespace SFA.DAS.LearnerVerification.Domain
{
    public class LearnerVerificationResponse
    {
        public LearnerVerificationResponseCode ResponseCode { get; set; }
        public IEnumerable<FailureFlag> FailureFlags { get; set; }
    }
}