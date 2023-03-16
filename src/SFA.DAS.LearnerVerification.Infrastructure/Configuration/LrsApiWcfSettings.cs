using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class LrsApiWcfSettings
    {
        public string? AzureADManagedIdentityClientId { get; set; }
        public string? CertName { get; set; }
        public string? KeyVaultUrl { get; set; }
        public string? LearnerServiceBaseUrl { get; set; }
        public string? OrganisationRef { get; set; }
        public string? UserName { get; set; }
        public string? OrgPassword { get; set; }
    }
}