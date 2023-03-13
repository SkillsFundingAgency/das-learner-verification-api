using SFA.DAS.LearnerVerification.Api.Configuration;
using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Queries;

namespace SFA.DAS.LearnerVerification.Api
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.SetupConfiguration();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddQueryServices()
                .AddDomainServices();
            builder.Services.AddHealthChecks();
            builder.Services.AddLogging(); //TODO: Ensure Logger is configured properly

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