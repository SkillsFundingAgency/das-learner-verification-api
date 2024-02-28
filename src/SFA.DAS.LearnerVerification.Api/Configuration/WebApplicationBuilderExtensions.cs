using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Api.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class WebApplicationBuilderExtensions
    {
        public static void SetupConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddAzureTableStorage(options =>
            {
                options.ConfigurationKeys = new[] { "SFA.DAS.LearnerVerification" };
                options.StorageConnectionString = builder.Configuration["ConfigurationStorageConnectionString"];
                options.EnvironmentName = builder.Configuration["EnvironmentName"];
                options.PreFixConfigurationKeys = false;
            });

            var applicationSettings = new ApplicationSettings();
            builder.Configuration.Bind(nameof(ApplicationSettings), applicationSettings);
            builder.Services.AddSingleton(x => applicationSettings);
        }

        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetry();
        }
    }
}