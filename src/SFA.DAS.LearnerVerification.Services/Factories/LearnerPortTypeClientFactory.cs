using LearningRecordsService;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Services.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Services.Factories
{
    public class LearnerPortTypeClientFactory : IClientTypeFactory<LearnerPortTypeClient>
    {
        private readonly ICertificateProvider _certificateProvider;
        private readonly LrsApiWcfSettings _lrsApiSettings;

        public LearnerPortTypeClientFactory(ICertificateProvider certificateProvider, ApplicationSettings appSettings)
        {
            _certificateProvider = certificateProvider;
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
            return client;
        }
    }
}