using Microsoft.AspNetCore.Mvc;
using SFA.DAS.LearnerVerification.Queries;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;

namespace SFA.DAS.LearnerVerification.Api.Controllers
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
        public async Task<IActionResult> VerifyLearnerDetails(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            var request = new VerifyLearnerQuery(uln, firstName, lastName, gender, dateOfBirth);
            var response = await _queryDispatcher.Send<VerifyLearnerQuery, Queries.VerifyLearner.LearnerVerification>(request);

            return Ok(response);
        }
    }
}