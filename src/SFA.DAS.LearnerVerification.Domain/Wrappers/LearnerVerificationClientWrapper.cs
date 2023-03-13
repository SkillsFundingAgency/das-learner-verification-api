using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Wrappers
{
    public class LearnerVerificationClientWrapper : ILearnerVerificationClientWrapper
    {
        private readonly LearnerPortTypeClient _client;
        
        public LearnerVerificationClientWrapper(LearnerPortTypeClient client)
        {
            _client = client;
        }

        public Task<verifyLearnerResponse> verifyLearnerAsync(VerifyLearnerRqst VerifyLearner)
        {
            return _client.verifyLearnerAsync(VerifyLearner);
        }

        public async ValueTask DisposeAsync()
        {
            await (_client as IAsyncDisposable).DisposeAsync();
        }
    }
}
