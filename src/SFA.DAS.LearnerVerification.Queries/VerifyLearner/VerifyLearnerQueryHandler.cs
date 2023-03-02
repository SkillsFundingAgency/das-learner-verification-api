using SFA.DAS.Funding.ApprenticeshipEarnings.Queries.GetAcademicYearEarnings;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Infrastructure.Queries;

namespace SFA.DAS.LearnerVerification.Queries.VerifyLearner
{
    public class VerifyLearnerQueryHandler : IQueryHandler<VerifyLearnerQuery, VerifyLearnerQueryResponse>
    {
        private readonly ILearnerValidationService _learnerValidationService;

        public VerifyLearnerQueryHandler(ILearnerValidationService learnerValidationService)
        {
            _learnerValidationService = learnerValidationService;
        }

        public async Task<VerifyLearnerQueryResponse> Handle(VerifyLearnerQuery query, CancellationToken cancellationToken = default)
        {
            var learnerValidationResponse = await _learnerValidationService.ValidateLearner(query.Uln, query.FirstName, query.LastName);

            return new VerifyLearnerQueryResponse
            {
                IsValid = true //TODO: populate properly
            };
        }
    }
}