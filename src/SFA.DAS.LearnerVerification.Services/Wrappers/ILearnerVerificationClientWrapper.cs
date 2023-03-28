using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Services.Wrappers;

public interface ILearnerVerificationClientWrapper : IAsyncDisposable
{
    Task<verifyLearnerResponse> verifyLearnerAsync(VerifyLearnerRqst VerifyLearner);
}