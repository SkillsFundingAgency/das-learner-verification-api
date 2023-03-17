using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.LearnerVerification.Infrastructure.Queries;
using SFA.DAS.LearnerVerification.InnerApi.Controllers;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;

namespace SFA.DAS.LearnerVerification.Api.UnitTests.Controllers
{
    public class WhenVerifyingLearnerDetails
    {
        private Fixture _fixture;
        private Mock<IQueryDispatcher> _queryDispatcher;
        private LearnerDetailsController _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _queryDispatcher = new Mock<IQueryDispatcher>();
            _sut = new LearnerDetailsController(_queryDispatcher.Object);
        }

        [Test]
        public async Task ThenApprenticeshipsAreReturned()
        {
            var ukprn = _fixture.Create<string>();
            var uln = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var expectedResult = _fixture.Create<Queries.VerifyLearner.LearnerVerification>();

            _queryDispatcher
                .Setup(x => x.Send<VerifyLearnerQuery, Queries.VerifyLearner.LearnerVerification>(
                    It.IsAny<VerifyLearnerQuery>()))
                .ReturnsAsync(expectedResult);

            var result = await _sut.VerifyLearnerDetails(ukprn, uln, firstName, lastName, null, null);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedResult);
        }
    }
}