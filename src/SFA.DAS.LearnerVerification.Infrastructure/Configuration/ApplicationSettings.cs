using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApplicationSettings
    {
        public LrsApiWcfSettings LrsApiWcfSettings { get; set; }
        public string? LearnerVerificationKeyVaultUrl { get; set; }
    }
}