using AutoFixture;
using FluentAssertions;
using Moq;
using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;

namespace SFA.DAS.LearnerVerification.Queries.UnitTests.VerifyLearner
{
    public class WhenVerifyingLearner
    {
        private Fixture _fixture;
        private Mock<ILearnerValidationService> _learnerValidationService;
        private VerifyLearnerQueryHandler _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _learnerValidationService = new Mock<ILearnerValidationService>();
            _sut = new VerifyLearnerQueryHandler(_learnerValidationService.Object);
        }

        [Test]
        public async Task ThenValidLearnerVerificationIsReturned()
        {
            //Arrange
            var query = _fixture.Create<VerifyLearnerQuery>();
            var expectedResult = _fixture.Create<LearnerVerificationResponse>();

            _learnerValidationService
                .Setup(x => x.ValidateLearner(query.UkPrn, query.Uln, query.FirstName, query.LastName, query.Gender, query.DateOfBirth))
                .ReturnsAsync(expectedResult);

            //Act
            var actualResult = await _sut.Handle(query);

            //Assert
            actualResult.Should().BeOfType<Queries.VerifyLearner.LearnerVerification>();
            actualResult.ResponseType.Should().NotBe(null);
        }
    }
}