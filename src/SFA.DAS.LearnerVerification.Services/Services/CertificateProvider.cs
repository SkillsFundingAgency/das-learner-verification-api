﻿using System.Security.Cryptography.X509Certificates;
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
            if (string.IsNullOrEmpty(_appSettings.LearnerVerificationKeyVaultUrl))
            {
                throw new ArgumentNullException(nameof(_appSettings.LearnerVerificationKeyVaultUrl), $"{nameof(_appSettings.LearnerVerificationKeyVaultUrl)} is not specified. That is required to run the app.");
            }

            if (string.IsNullOrEmpty(_appSettings.LrsApiWcfSettings.LRSCertificateName))
            {
                throw new ArgumentNullException(nameof(_appSettings.LrsApiWcfSettings.LRSCertificateName), $"{nameof(_appSettings.LrsApiWcfSettings.LRSCertificateName)} for LRS Web Service is not specified. That is required to run the app.");
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
                var options = new DownloadCertificateOptions(_appSettings.LrsApiWcfSettings.LRSCertificateName)
                {
                    KeyStorageFlags = X509KeyStorageFlags.EphemeralKeySet
                };

                var client = new CertificateClient(new Uri(_appSettings.LearnerVerificationKeyVaultUrl), new DefaultAzureCredential());

                _x509Certificate = client.DownloadCertificate(options).Value;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up client certificate");
                throw;
            }
        }
    }
}