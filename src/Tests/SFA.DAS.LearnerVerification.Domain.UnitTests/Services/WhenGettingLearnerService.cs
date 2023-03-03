using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenGettingLearnerService
    {
        private Mock<IClientTypeFactory<LearnerPortTypeClient>> _mockClientTypeFactory;

        private LearnerServiceClientProvider<LearnerPortTypeClient> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LearnerServiceClientProvider<LearnerPortTypeClient>(_mockClientTypeFactory.Object, Mock.Of<ILogger<LearnerServiceClientProvider<LearnerPortTypeClient>>>());
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