using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Wrappers;

public interface ILearnerVerificationClientWrapper : IAsyncDisposable
{
    Task<verifyLearnerResponse> verifyLearnerAsync(VerifyLearnerRqst VerifyLearner);
}