using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Queries;
using SFA.DAS.LearnerVerification.Api.Configuration;

namespace SFA.DAS.LearnerVerification.Api.Configuration
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.SetupConfiguration();
            builder.ConfigureLogging();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services
                .AddQueryServices()
                .AddDomainServices();
            builder.Services.AddHealthChecks();

            var app = builder.Build();
            app.MapHealthChecks("/ping");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Logger.LogInformation("App starting");
            app.Run();
        }
    }
}