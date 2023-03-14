using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class CertificateProvider : ICertificateProvider
    {
        private readonly ILogger<CertificateProvider> _logger;
        private readonly LrsApiWcfSettings _lrsApiSettings;
        private X509Certificate2 _x509Certificate;

        public CertificateProvider(ILogger<CertificateProvider> logger, ApplicationSettings appSettings)
        {
            _lrsApiSettings = appSettings.LrsApiWcfSettings;
            _logger = logger;
        }

        public X509Certificate2 GetClientCertificate()
        {
            if (string.IsNullOrEmpty(_lrsApiSettings.KeyVaultUrl))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.KeyVaultUrl), "KeyVault url is not specified. That is required to run the app.");
            }

            if (string.IsNullOrEmpty(_lrsApiSettings.CertName))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.CertName), "Cert name added to KeyVault is not specified. That is required to run the app.");
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
                var client = new CertificateClient(new Uri(_lrsApiSettings.KeyVaultUrl), new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = _lrsApiSettings.AzureADManagedIdentityClientId
                }));

                //var client = new CertificateClient(new Uri(_lrsApiSettings.KeyVaultUrl), new DefaultAzureCredential(new DefaultAzureCredentialOptions
                //{
                //    ManagedIdentityClientId = _lrsApiSettings.AzureADManagedIdentityClientId
                //}));

                //_x509Certificate = client.DownloadCertificate(_lrsApiSettings.CertName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up client certificate");
                throw;
            }
        }
    }
}