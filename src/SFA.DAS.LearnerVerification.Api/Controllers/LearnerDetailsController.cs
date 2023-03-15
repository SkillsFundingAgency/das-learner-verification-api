using Microsoft.AspNetCore.Mvc;
using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Infrastructure.Queries;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;

namespace SFA.DAS.LearnerVerification.InnerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class LearnerDetailsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public LearnerDetailsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [Route("verify")]
        [HttpGet]
        public async Task<IActionResult> VerifyLearnerDetails(string ukprn, string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            var request = new VerifyLearnerQuery(ukprn, uln, firstName, lastName, gender, dateOfBirth);
            var response = await _queryDispatcher.Send<VerifyLearnerQuery, LearnerVerificationResponse>(request);

            return Ok(response);
        }
    }
}