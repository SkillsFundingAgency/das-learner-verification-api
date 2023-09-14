using LearningRecordsService;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Services.Mappers;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;
using System.ServiceModel;
using Newtonsoft.Json;

namespace SFA.DAS.LearnerVerification.Services.Services
{
    public class LearnerValidationService : ILearnerValidationService
    {
        private readonly ILearnerVerificationServiceClientProvider _lrsClientProvider;
        private readonly ILogger<LearnerValidationService> _logger;
        private readonly LrsApiWcfSettings _lrsApiSettings;

        public LearnerValidationService(ILearnerVerificationServiceClientProvider lrsClientProvider, ApplicationSettings appSettings, ILogger<LearnerValidationService> logger)
        {
            _lrsClientProvider = lrsClientProvider;
            _logger = logger;
            _lrsApiSettings = appSettings.LrsApiWcfSettings;

            if (string.IsNullOrEmpty(_lrsApiSettings.OrgPassword))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.OrgPassword), $"{nameof(_lrsApiSettings.OrgPassword)} is not specified. This is required to run the app.");
            }

            if (string.IsNullOrEmpty(_lrsApiSettings.UserName))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.UserName), $"{nameof(_lrsApiSettings.UserName)} is not specified. This is required to run the app.");
            }
        }

        public async Task<LearnerVerificationResponse> ValidateLearner(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            try
            {
                await using var service = _lrsClientProvider.Get();
                
                var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
                {
                    OrganisationRef = _lrsApiSettings.OrganisationRef,
                    OrgPassword = _lrsApiSettings.OrgPassword,
                    UserName = _lrsApiSettings.UserName,
                    LearnerToVerify = new MIAPLearnerToVerify()
                    {
                        ULN = uln,
                        GivenName = firstName,
                        FamilyName = lastName,
                        Gender = gender,
                        DateOfBirth = dateOfBirth?.ToString("yyyy-MM-dd")
                    }
                });

                _logger.LogError($"Response: {JsonConvert.SerializeObject(learnerVerificationResponse)}");

                return learnerVerificationResponse.VerifyLearnerResponse.VerifiedLearner.Map();
            }
            catch (FaultException<MIAPAPIException> ex)
            {
                _logger.LogError(ex.Detail.Description, $"Error ({ex.Detail.ErrorCode}) occured whilst attempting to verify learner details with ULN {uln}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured whilst attempting to verify learner details with ULN {uln}.");
                throw;
            }
        }
    }
}