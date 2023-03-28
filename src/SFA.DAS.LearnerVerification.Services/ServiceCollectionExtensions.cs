using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using SFA.DAS.LearnerVerification.Services.Factories;
using LearningRecordsService;
using SFA.DAS.LearnerVerification.Services.Services;

namespace SFA.DAS.LearnerVerification.Services
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<ICertificateProvider, CertificateProvider>()
                .AddScoped<ILearnerValidationService, LearnerValidationService>()
                .AddScoped<IClientTypeFactory<LearnerPortTypeClient>, LearnerPortTypeClientFactory>()
                .AddScoped<ILearnerVerificationServiceClientProvider, LearnerVerificationServiceClientProvider>();

            return serviceCollection;
        }
    }
}