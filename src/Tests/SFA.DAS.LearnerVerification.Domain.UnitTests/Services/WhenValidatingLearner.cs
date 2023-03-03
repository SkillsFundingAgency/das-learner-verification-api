using LearningRecordsService;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenValidatingLearner
    {
        private Mock<ILearnerServiceClientProvider<LearnerPortTypeClient>> _mockClientProvider;

        private LearnerValidationService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LearnerValidationService(_mockClientProvider.Object);
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