using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Queries
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(scan =>
                {
                    scan.FromExecutingAssembly()
                        .AddClasses(classes => classes.AssignableTo(typeof(IQuery)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();

                    scan.FromAssembliesOf(typeof(VerifyLearnerQueryHandler))
                        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();
                })
                .AddScoped<IQueryDispatcher, QueryDispatcher>();

            return serviceCollection;
        }
    }
}