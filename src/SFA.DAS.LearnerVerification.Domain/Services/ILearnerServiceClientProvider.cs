namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public interface ILearnerServiceClientProvider<T>
    {
        T GetServiceAsync();
    }
}