using LearningRecordsService;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public class LearnerPortTypeClientFactory : IClientTypeFactory<LearnerPortTypeClient>
    {
        private readonly ICertificateProvider _certificateProvider;
        private readonly LrsApiWcfSettings _lrsApiSettings;

        public LearnerPortTypeClientFactory(LrsApiWcfSettings lrsApiSettings, ICertificateProvider certificateProvider)
        {
            _lrsApiSettings = lrsApiSettings;
            _certificateProvider = certificateProvider;

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