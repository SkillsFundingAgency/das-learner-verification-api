using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Domain.Factories
{
    public class CertificateProvider : ICertificateProvider
    {
        private readonly LrsApiWcfSettings _lrsApiSettings;
        private X509Certificate2 _x509Certificate;

        public CertificateProvider(LrsApiWcfSettings lrsApiSettings)
        {
            _lrsApiSettings = lrsApiSettings;
            SetupClientCertificate();
        }

        public X509Certificate2 GetClientCertificate()
        {
            if (string.IsNullOrEmpty(_lrsApiSettings.KeyVaultUrl))
            {
                throw new Exception("KeyVault url is not specified. That is required to run the app");
            }

            if (string.IsNullOrEmpty(_lrsApiSettings.CertName))
            {
                throw new Exception("Cert name added to KeyVault is not specified. That is required to run the app");
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

                _x509Certificate = client.DownloadCertificate(_lrsApiSettings.CertName);
            }
            catch (Exception ex)
            {
                //TODO: Logging?
                throw;
            }
        }
    }
}