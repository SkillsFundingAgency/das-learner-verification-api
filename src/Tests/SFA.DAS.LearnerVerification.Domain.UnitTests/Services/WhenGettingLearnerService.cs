using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Services;
using System.Runtime;
using System.ServiceModel;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [Ignore("Unit tests still in development")]
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
            _mockClientTypeFactory
                .Setup(x => x.Create(It.IsAny<BasicHttpBinding>()))
                .Returns(It.IsAny<LearnerPortTypeClient>());

            //Act
            var service = _sut.GetServiceAsync();

            //Assert
        }
    }
}