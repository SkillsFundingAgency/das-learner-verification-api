namespace SFA.DAS.LearnerVerification.Services
{
    public class LearnerVerificationResponse
    {
        public LearnerVerificationResponseCode ResponseCode { get; set; }
        public IEnumerable<FailureFlag>? FailureFlags { get; set; }
    }
}