using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using SFA.DAS.LearnerVerification.Services.Factories;
using LearningRecordsService;
using SFA.DAS.LearnerVerification.Services.Services;
using System.ServiceModel.Description;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.LearnerVerification.Services
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICertificateProvider, CertificateProvider>()
                .AddTransient<ILearnerValidationService, LearnerValidationService>()
                .AddTransient<IClientTypeFactory<LearnerPortTypeClient>, LearnerPortTypeClientFactory>()
                .AddTransient<ILearnerVerificationServiceClientProvider, LearnerVerificationServiceClientProvider>();

            return serviceCollection;
        }
    }
}