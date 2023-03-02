using LearningRecordsService;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenGettingLearnerService
    {
        private Mock<IClientTypeFactory<LearnerPortTypeClient>> _mockClientTypeFactory;

        private LearnerServiceClientProvider<LearnerPortTypeClient> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LearnerServiceClientProvider<LearnerPortTypeClient>(_mockClientTypeFactory.Object);
        }

        [Test]
        public void ThenCorrectServiceIsReturned()
        {
            //Arrange

            //Act
            var service = _sut.GetServiceAsync();

            //Assert
            //TODO:
        }
    }
}