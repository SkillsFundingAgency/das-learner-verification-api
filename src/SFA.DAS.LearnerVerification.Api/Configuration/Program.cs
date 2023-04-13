using SFA.DAS.LearnerVerification.Services;
using SFA.DAS.LearnerVerification.Queries;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.LearnerVerification.Api.Configuration
{
    [ExcludeFromCodeCoverage]
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

            app.Run();
        }
    }
}