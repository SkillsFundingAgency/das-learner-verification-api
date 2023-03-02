using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public interface ICertificateProvider
    {
        X509Certificate2 GetClientCertificate();
    }
}