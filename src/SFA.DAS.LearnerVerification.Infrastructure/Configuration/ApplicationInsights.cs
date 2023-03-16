using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApplicationInsights
    {
        public bool EnableAppInsights { get; set; }
        public string? ConnectionString { get; set; }
    }
}