using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Services.Factories;
using SFA.DAS.LearnerVerification.Services.Wrappers;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public class LearnerVerificationServiceClientProvider : ILearnerVerificationServiceClientProvider
    {
        private readonly IClientTypeFactory<LearnerPortTypeClient> _factory;
        private readonly ILogger<LearnerVerificationServiceClientProvider> _logger;

        public LearnerVerificationServiceClientProvider(IClientTypeFactory<LearnerPortTypeClient> factory, ILogger<LearnerVerificationServiceClientProvider> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public ILearnerVerificationClientWrapper Get()
        {
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.UseDefaultWebProxy = true;
  
                var service = _factory.Create(binding);
               
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                    
                };
                //service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
                //service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CustomCertificateValidator = new Validator();

                return new LearnerVerificationClientWrapper(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error attempting to get client in {nameof(LearnerVerificationServiceClientProvider)}.");
                throw;
            }
        }
    }
}