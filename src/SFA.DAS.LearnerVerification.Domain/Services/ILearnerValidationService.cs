using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public interface ILearnerValidationService
    {
        Task<MIAPVerifiedLearner> ValidateLearner(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth);
    }
}