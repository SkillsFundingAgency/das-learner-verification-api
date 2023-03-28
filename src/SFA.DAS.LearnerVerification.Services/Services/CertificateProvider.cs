using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public class CertificateProvider : ICertificateProvider
    {
        private readonly ILogger<CertificateProvider> _logger;
        private readonly ApplicationSettings _appSettings;
        private X509Certificate2 _x509Certificate;

        public CertificateProvider(ILogger<CertificateProvider> logger, ApplicationSettings appSettings)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public X509Certificate2 GetClientCertificate()
        {
            if (string.IsNullOrEmpty(_appSettings.KeyVaultUrl))
            {
                throw new ArgumentNullException(nameof(_appSettings.KeyVaultUrl), $"{nameof(_appSettings.KeyVaultUrl)} is not specified. That is required to run the app.");
            }

            if (string.IsNullOrEmpty(_appSettings.LrsApiWcfSettings.CertificateName))
            {
                throw new ArgumentNullException(nameof(_appSettings.LrsApiWcfSettings.CertificateName), $"{nameof(_appSettings.LrsApiWcfSettings.CertificateName)} for LRS Web Service is not specified. That is required to run the app.");
            }

            if (_x509Certificate == null)
            {
                SetupClientCertificate();
            }

            return _x509Certificate;
        }

        private void SetupClientCertificate()
        {
            try
            {
                var client = new CertificateClient(new Uri(_appSettings.KeyVaultUrl), new DefaultAzureCredential());
                _x509Certificate = client.DownloadCertificate(_appSettings.LrsApiWcfSettings.CertificateName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up client certificate");
                throw;
            }
        }
    }
}