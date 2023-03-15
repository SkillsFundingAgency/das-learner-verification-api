using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Queries;
using SFA.DAS.LearnerVerification.Queries.Mappers;
using SFA.DAS.LearnerVerification.Types;

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
                query.UkPrn,
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