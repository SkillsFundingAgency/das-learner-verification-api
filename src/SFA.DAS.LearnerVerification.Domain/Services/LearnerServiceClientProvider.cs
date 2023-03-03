using System.ServiceModel;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Domain.Factories;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerServiceClientProvider<T> : ILearnerServiceClientProvider<T>
    {
        private readonly IClientTypeFactory<T> _factory;
        private readonly ILogger<LearnerServiceClientProvider<T>> _logger;

        public LearnerServiceClientProvider(IClientTypeFactory<T> factory, ILogger<LearnerServiceClientProvider<T>> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public T GetServiceAsync()
        {
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                binding.UseDefaultWebProxy = true;

                var service = _factory.Create(binding);

                return service;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error attempting to get service");
                throw;
            }
        }
    }
}