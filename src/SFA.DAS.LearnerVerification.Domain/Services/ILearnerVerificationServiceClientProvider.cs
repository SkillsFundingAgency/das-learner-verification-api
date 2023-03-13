using SFA.DAS.LearnerVerification.Domain.Wrappers;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public interface ILearnerVerificationServiceClientProvider
    {
        ILearnerVerificationClientWrapper Get();
    }
}