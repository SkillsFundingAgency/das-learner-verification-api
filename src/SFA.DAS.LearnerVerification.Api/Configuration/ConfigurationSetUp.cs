using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Api.Configuration
{
    public static class ConfigurationSetUp
    {
        public static void SetupConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddAzureTableStorage(options =>
            {
                options.ConfigurationKeys = new[] { "SFA.DAS.LearnerVerification.Api" };
                options.StorageConnectionString = builder.Configuration["ConfigurationStorageConnectionString"];
                options.EnvironmentName = builder.Configuration["EnvironmentName"];
                options.PreFixConfigurationKeys = false;
            });

            var applicationSettings = new ApplicationSettings();
            builder.Configuration.Bind(nameof(ApplicationSettings), applicationSettings);
            builder.Services.AddSingleton(x => applicationSettings);
        }
    }
}