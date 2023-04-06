using LearningRecordsService;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Services.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.LearnerVerification.Services.Factories
{
    public class LearnerPortTypeClientFactory : IClientTypeFactory<LearnerPortTypeClient>
    {
        private readonly ICertificateProvider _certificateProvider;
        private readonly ILogger<LearnerPortTypeClientFactory> _logger;
        private readonly LrsApiWcfSettings _lrsApiSettings;

        public LearnerPortTypeClientFactory(ICertificateProvider certificateProvider, ApplicationSettings appSettings, ILogger<LearnerPortTypeClientFactory> logger)
        {
            _certificateProvider = certificateProvider;
            _logger = logger;
            _lrsApiSettings = appSettings.LrsApiWcfSettings;
            if (string.IsNullOrEmpty(_lrsApiSettings.LearnerServiceBaseUrl))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.LearnerServiceBaseUrl), $"{nameof(_lrsApiSettings.LearnerServiceBaseUrl)} is not specified. This is required to run the app.");
            }
        }

        public LearnerPortTypeClient Create(BasicHttpBinding binding)
        {
            var client = new LearnerPortTypeClient(binding, new EndpointAddress(_lrsApiSettings.LearnerServiceBaseUrl));
            client.ClientCredentials.ClientCertificate.Certificate = _certificateProvider.GetClientCertificate();
            _logger.LogInformation($"Certificate: {client.ClientCredentials.ClientCertificate.Certificate}");
            return client;
        }
    }
}