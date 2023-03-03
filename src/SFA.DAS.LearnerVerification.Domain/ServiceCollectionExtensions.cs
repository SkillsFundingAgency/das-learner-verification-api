using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using SFA.DAS.LearnerVerification.Domain.Factories;
using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(scan =>
                {
                    scan.FromAssembliesOf(typeof(CertificateProvider))
                        .AddClasses(classes => classes.AssignableTo(typeof(ILearnerServiceClientProvider<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();
                })
                .AddScoped<ICertificateProvider, CertificateProvider>()
                .AddScoped<ILearnerValidationService, LearnerValidationService>()
                .AddScoped<IClientTypeFactory<LearnerPortTypeClient>, LearnerPortTypeClientFactory>()
                .AddScoped<LrsApiWcfSettings>(); //todo I think this needs to be populated with config data

            return serviceCollection;
        }
    }
}
