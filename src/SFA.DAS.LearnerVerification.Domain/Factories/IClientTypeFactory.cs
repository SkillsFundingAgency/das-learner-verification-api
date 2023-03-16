using System.ServiceModel;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public interface IClientTypeFactory<out T>
    {
        T Create(BasicHttpBinding binding);
    }
}