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

        public async Task<MIAPVerifiedLearner> ValidateLearner(string uln, string firstName, string lastName)
        {
            try
            {
                var service = _lrsClientProvider.GetService();

                ///TODO: Should we be using the 'Find by ULN endpoint' instead here? (reccomended by LRS)
                var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
                {
                    LearnerToVerify = new MIAPLearnerToVerify() { ULN = uln, GivenName = firstName, FamilyName = lastName }
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