using System.ServiceModel;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public interface IClientTypeFactory<T>
    {
        T Create(BasicHttpBinding binding);
    }
}