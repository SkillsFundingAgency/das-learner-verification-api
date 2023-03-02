using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Domain
{
    [ExcludeFromCodeCoverage]
    public class LrsApiWcfSettings
    {
        public string AzureADManagedIdentityClientId { get; set; }
        public string CertName { get; set; }
        public string KeyVaultUrl { get; set; }
        public string LearnerServiceBaseUrl { get; set; }
    }
}