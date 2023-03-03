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
        }

        public LearnerPortTypeClient Create(BasicHttpBinding binding)
        {
            var client = new LearnerPortTypeClient(binding, new EndpointAddress(_lrsApiSettings.LearnerServiceBaseUrl));
            client.ClientCredentials.ClientCertificate.Certificate = _certificateProvider.GetClientCertificate();
            return client;
        }
    }
}