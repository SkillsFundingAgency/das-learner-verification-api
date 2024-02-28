using SFA.DAS.LearnerVerification.Services.Wrappers;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public interface ILearnerVerificationServiceClientProvider
    {
        ILearnerVerificationClientWrapper Get();
    }
}