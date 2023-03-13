using SFA.DAS.LearnerVerification.Types;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Queries.GetAcademicYearEarnings
{
    public class LearnerVerificationResponse
    {
        public LearnerVerificationResponseCode ResponseCode { get; set; }
        public IEnumerable<FailureFlag>? FailureFlags { get; set; }
    }
}