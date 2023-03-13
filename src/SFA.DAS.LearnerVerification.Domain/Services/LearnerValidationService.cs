using LearningRecordsService;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerValidationService : ILearnerValidationService
    {
        private readonly ILogger<LearnerValidationService> _logger;
        private readonly ILearnerServiceClientProvider<LearnerPortTypeClient> _lrsClientProvider;

        public LearnerValidationService(ILearnerServiceClientProvider<LearnerPortTypeClient> lrsClientProvider, ILogger<LearnerValidationService> logger)
        {
            _lrsClientProvider = lrsClientProvider;
            _logger = logger;
        }

        public async Task<MIAPVerifiedLearner> ValidateLearner(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            try
            {
                await using var service = _lrsClientProvider.GetServiceAsync();
                var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
                {
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