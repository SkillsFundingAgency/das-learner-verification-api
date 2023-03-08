using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Services;
using System.ServiceModel;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenGettingLearnerService
    {
        private LearnerPortTypeClient? _client;
        private Mock<IClientTypeFactory<LearnerPortTypeClient>> _mockClientTypeFactory;
        private LearnerServiceClientProvider<LearnerPortTypeClient> _sut;

        [SetUp]
        public async Task SetupAsync()
        {
            //Arrange
            _mockClientTypeFactory = new();
            _mockClientTypeFactory
                .Setup(x => x.Create(It.IsAny<BasicHttpBinding>()))
                .Returns(new LearnerPortTypeClient());

            _sut = new LearnerServiceClientProvider<LearnerPortTypeClient>(_mockClientTypeFactory.Object, Mock.Of<ILogger<LearnerServiceClientProvider<LearnerPortTypeClient>>>());

            //Act
            _client = _sut.GetService();
        }

        [Test]
        public void ThenCorrectServiceIsReturned()
        {
            _client.Should().NotBeNull();
            _client.Should().BeOfType<LearnerPortTypeClient>();
        }

        [Test]
        public void ThenLearnerServiceClientFactoryIsCalledWithCorrectArguments()
        {
            _mockClientTypeFactory
                .Verify(x => x.Create(It.Is<BasicHttpBinding>(
                    x => x.Security.Transport.ClientCredentialType == HttpClientCredentialType.Certificate &&
                    x.Security.Mode == BasicHttpSecurityMode.Transport &&
                    x.UseDefaultWebProxy == true
                    )), Times.Once);
        }
    }
}