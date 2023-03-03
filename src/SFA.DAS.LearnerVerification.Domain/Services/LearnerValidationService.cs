using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Services
{
    public class LearnerValidationService : ILearnerValidationService
    {
        private readonly ILearnerServiceClientProvider<LearnerPortTypeClient> _lrsClientProvider;

        public LearnerValidationService(ILearnerServiceClientProvider<LearnerPortTypeClient> lrsClientProvider)
        {
            _lrsClientProvider = lrsClientProvider;
        }

        public async Task<MIAPVerifiedLearner> ValidateLearner(string uln, string firstName, string lastName)
        {
            try
            {
                var service = _lrsClientProvider.GetServiceAsync();
                ///TODO: Should we be using the 'Find by ULN endpoint' instead here? (reccomended by LRS)
                var learnerVerificationResponse = await service.verifyLearnerAsync(new VerifyLearnerRqst()
                {
                    LearnerToVerify = new MIAPLearnerToVerify() { ULN = uln, GivenName = firstName, FamilyName = lastName }
                });

                await service.CloseAsync(); //TODO: Is this necessary?

                return learnerVerificationResponse.VerifyLearnerResponse.VerifiedLearner;
            }
            catch (Exception ex)
            {
                //TODO: think about logging?
                throw;
            }
        }
    }
}