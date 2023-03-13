using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using SFA.DAS.LearnerVerification.Domain.Factories;
using LearningRecordsService;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain
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