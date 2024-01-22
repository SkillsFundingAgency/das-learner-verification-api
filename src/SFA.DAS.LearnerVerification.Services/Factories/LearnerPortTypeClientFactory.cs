using LearningRecordsService;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Services.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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

        public LearnerPortTypeClient Create(BasicHttpsBinding binding)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new LearnerPortTypeClient(binding, new EndpointAddress(_lrsApiSettings.LearnerServiceBaseUrl));

            //var cert = _certificateProvider.GetCertificates(new[] {"797AC6AE3BBA3279168560C727EE1D2BE44DB0BF"});

            //client.ClientCredentials.ClientCertificate.Certificate = cert.FirstOrDefault();

            client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindByThumbprint, "797AC6AE3BBA3279168560C727EE1D2BE44DB0BF");
            //client.ClientCredentials.ClientCertificate.Certificate = _certificateProvider.GetClientCertificate();
            //client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
            //        new X509ServiceCertificateAuthentication()
            //        {
            //            CertificateValidationMode = X509CertificateValidationMode.None,
            //            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
            //        };


            if (client.ClientCredentials.ClientCertificate.Certificate != null)
            {
                _logger.LogError($"Certificate: {client.ClientCredentials.ClientCertificate.Certificate}");
                _logger.LogError($"Certificate thumbprint: {client.ClientCredentials.ClientCertificate.Certificate.Thumbprint}");
                _logger.LogError($"Certificate serial no: {client.ClientCredentials.ClientCertificate.Certificate.SerialNumber}");
            }
            return client;
        }

        public LearnerPortTypeClient Create(WSHttpBinding binding)
        {
            throw new NotImplementedException();
        }
    }
}