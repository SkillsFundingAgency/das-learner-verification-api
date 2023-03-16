using LearningRecordsService;
using SFA.DAS.LearnerVerification.Domain.Mappers;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerValidationService : ILearnerValidationService
    {
        private readonly ILearnerVerificationServiceClientProvider _lrsClientProvider;
        private readonly LrsApiWcfSettings? _lrsApiSettings;

        public LearnerValidationService(ILearnerVerificationServiceClientProvider lrsClientProvider, ApplicationSettings appSettings)
        {
            _lrsClientProvider = lrsClientProvider;
            _lrsApiSettings = appSettings.LrsApiWcfSettings;

            if (string.IsNullOrEmpty(_lrsApiSettings?.OrgPassword))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.OrgPassword), $"{nameof(_lrsApiSettings.OrgPassword)} is not specified. This is required to run the app.");
            }

            if (string.IsNullOrEmpty(_lrsApiSettings?.UserName))
            {
                throw new ArgumentNullException(nameof(_lrsApiSettings.UserName), $"{nameof(_lrsApiSettings.UserName)} is not specified. This is required to run the app.");
            }
        }

        public async Task<LearnerVerificationResponse> ValidateLearner(string ukprn, string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            await using var service = _lrsClientProvider.Get();
            var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
            {
                UKPRN = ukprn,
                //OrganisationRef = _lrsApiSettings.OrgPassword, //TODO: Verify if necessary
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

            return learnerVerificationResponse.VerifyLearnerResponse.VerifiedLearner.Map();
        }
    }
}