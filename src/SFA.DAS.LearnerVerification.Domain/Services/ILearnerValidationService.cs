namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public interface ILearnerValidationService
    {
        Task<LearnerVerificationResponse> ValidateLearner(string ukprn, string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth);
    }
}