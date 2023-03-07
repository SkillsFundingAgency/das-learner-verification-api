using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.InnerApi.Configuration;
using SFA.DAS.LearnerVerification.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureTableStorage(options =>
{
    options.ConfigurationKeys = new[] { "SFA.DAS.Funding.ApprenticeshipEarnings" };
    options.StorageConnectionString = builder.Configuration["ConfigurationStorageConnectionString"];
    options.EnvironmentName = builder.Configuration["EnvironmentName"];
    options.PreFixConfigurationKeys = false;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var applicationSettings = new ApplicationSettings();
builder.Configuration.Bind(nameof(ApplicationSettings), applicationSettings);
builder.Services.AddSingleton(x => applicationSettings);
builder.Services
    .AddQueryServices()
    .AddDomainServices();
builder.Services.AddHealthChecks();
builder.Services.AddLogging();

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