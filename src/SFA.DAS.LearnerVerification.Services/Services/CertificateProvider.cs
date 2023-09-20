using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public class CertificateProvider : ICertificateProvider
    {
        private readonly ILogger<CertificateProvider> _logger;
        private readonly ApplicationSettings _appSettings;
        private X509Certificate2 _x509Certificate;
        private X509Certificate2 clientCertificate;

        public CertificateProvider(ILogger<CertificateProvider> logger, ApplicationSettings appSettings)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public X509Certificate2 GetClientCertificate()
        {
            if (string.IsNullOrEmpty(_appSettings.LearnerVerificationKeyVaultUrl))
            {
                throw new ArgumentNullException(nameof(_appSettings.LearnerVerificationKeyVaultUrl), $"{nameof(_appSettings.LearnerVerificationKeyVaultUrl)} is not specified. That is required to run the app.");
            }

            if (string.IsNullOrEmpty(_appSettings.LrsApiWcfSettings.LRSCertificateName))
            {
                throw new ArgumentNullException(nameof(_appSettings.LrsApiWcfSettings.LRSCertificateName), $"{nameof(_appSettings.LrsApiWcfSettings.LRSCertificateName)} for LRS Web Service is not specified. That is required to run the app.");
            }

            if (clientCertificate == null)
            {
                SetupClientCertificate();
            }

            return clientCertificate;
        }

        private void SetupClientCertificate()
        {
        //    X509KeyStorageFlags keyStorageFlags = X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable;
        //    if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        //    {
        //        keyStorageFlags |= X509KeyStorageFlags.EphemeralKeySet;
        //    }

        //    DownloadCertificateOptions options = new DownloadCertificateOptions(_appSettings.LrsApiWcfSettings.LRSCertificateName)
        //    {
        //        KeyStorageFlags = keyStorageFlags
        //    };
            try
            {
                    //var client = new CertificateClient(new Uri(_appSettings.LearnerVerificationKeyVaultUrl), new DefaultAzureCredential());

                    //_x509Certificate = client.DownloadCertificate(options);

                
                //// Create an instance of SecretClient to retrieve the certificate's secret
                var secretClient = new SecretClient(new Uri(_appSettings.LearnerVerificationKeyVaultUrl), new DefaultAzureCredential());

                //// Retrieve the certificate's secret
                KeyVaultSecret certificateSecret = secretClient.GetSecret(_appSettings.LrsApiWcfSettings.LRSCertificateName);
                //// Create an X.509 certificate from the certificate's secret value
                byte[] certificateBytes = Convert.FromBase64String(certificateSecret.Value);
                clientCertificate = new X509Certificate2(certificateBytes, (string)null, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up client certificate");
                throw;
            }
        }
    }
}