using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenValidatingLearner
    {
        private Mock<ILearnerServiceClientProvider<LearnerPortTypeClient>> _mockClientProvider;

        private LearnerValidationService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>());
        }

        [Test]
        public void ThenVerificationResponseIsReturned()
        {
            //Arrange
            //TODO: Mock client provider behaviour

            //Act
            //var service = _sut.ValidateLearner(); //TODO:

            //Assert
            //TODO:
        }
    }
}