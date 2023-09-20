using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    internal class Validator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            
            X509Chain chain = new X509Chain();
            if (!chain.Build(certificate))
            {
                Console.WriteLine($"{chain.ChainStatus.FirstOrDefault().StatusInformation}. Press y to proceed...");
                if (Console.ReadKey().KeyChar != 'y')
                    throw new SecurityTokenValidationException("Service certification is not valid.");
            }
        }
    }
}
