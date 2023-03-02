using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public interface ILearnerValidationService
    {
        Task<MIAPVerifiedLearner> ValidateLearner(string uln, string firstName, string lastName);
    }
}