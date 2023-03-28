namespace SFA.DAS.LearnerVerification.Services.Services
{
    public interface ILearnerValidationService
    {
        Task<LearnerVerificationResponse> ValidateLearner(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth);
    }
}