using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Queries.Mappers;

namespace SFA.DAS.LearnerVerification.Queries.VerifyLearner
{
    public class VerifyLearnerQueryHandler : IQueryHandler<VerifyLearnerQuery, LearnerVerification>
    {
        private readonly ILearnerValidationService _learnerValidationService;

        public VerifyLearnerQueryHandler(ILearnerValidationService learnerValidationService)
        {
            _learnerValidationService = learnerValidationService;
        }

        public async Task<LearnerVerification> Handle(VerifyLearnerQuery query, CancellationToken cancellationToken = default)
        {
            var learnerValidationResponse = await _learnerValidationService.ValidateLearner(
                query.Uln,
                query.FirstName,
                query.LastName,
                query.Gender,
                query.DateOfBirth
                );

            return learnerValidationResponse.Map();
        }
    }
}