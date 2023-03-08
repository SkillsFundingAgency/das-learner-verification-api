namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public interface ILearnerServiceClientProvider<T>
    {
        T GetService();
    }
}