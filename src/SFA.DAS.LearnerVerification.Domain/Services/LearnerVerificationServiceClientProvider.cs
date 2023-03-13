using System.ServiceModel;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Wrappers;

namespace SFA.DAS.LearnerVerification.Domain.Services
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

                return new LearnerVerificationClientWrapper(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error attempting to get service");
                throw;
            }
        }
    }
}