using LearningRecordsService;
using Microsoft.Extensions.Logging;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerValidationService : ILearnerValidationService
    {
        private readonly ILogger<LearnerValidationService> _logger;
        private readonly ILearnerVerificationServiceClientProvider _lrsClientProvider;
        private readonly LrsApiWcfSettings _lrsApiSettings;

        public LearnerValidationService(ILearnerVerificationServiceClientProvider lrsClientProvider, ILogger<LearnerValidationService> logger, ApplicationSettings appSettings)
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

        public async Task<MIAPVerifiedLearner> ValidateLearner(string ukprn, string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            try
            {
                await using var service = _lrsClientProvider.Get();
                var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
                {
                    UKPRN = ukprn,
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

                return learnerVerificationResponse.VerifyLearnerResponse.VerifiedLearner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating learner");
                throw;
            }
        }
    }
}