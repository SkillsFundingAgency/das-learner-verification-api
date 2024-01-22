using System.ServiceModel;

namespace SFA.DAS.LearnerVerification.Services.Factories
{
    public interface IClientTypeFactory<out T>
    {
        T Create(BasicHttpBinding binding);
    }
}