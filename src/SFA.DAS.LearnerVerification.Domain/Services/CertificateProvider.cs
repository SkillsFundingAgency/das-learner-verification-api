﻿using System.Security.Cryptography.X509Certificates;
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

            if (string.IsNullOrEmpty(_lrsApiSettings.CertificateName))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.CertificateName), "Certificate name added to KeyVault is not specified. That is required to run the app.");
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
                var keyVaultUrl = GetKeyVaultUrl();
                var client = new CertificateClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
                _x509Certificate = client.DownloadCertificate(_lrsApiSettings.CertificateName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up client certificate");
                throw;
            }
        }

        private static string GetKeyVaultUrl()
        {
            var keyVaultName = Environment.GetEnvironmentVariable("keyVaultName");
            var keyVaultUrl = $"https://{keyVaultName}.vault.azure.net";
            return keyVaultUrl;
        }
    }
}