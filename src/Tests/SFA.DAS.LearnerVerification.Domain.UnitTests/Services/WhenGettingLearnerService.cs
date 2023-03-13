using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Services;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Domain.Wrappers;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenGettingLearnerService
    {
        private ILearnerVerificationClientWrapper _clientWrapper;
        private Mock<IClientTypeFactory<LearnerPortTypeClient>> _mockClientTypeFactory;
        private LearnerVerificationServiceClientProvider _sut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _mockClientTypeFactory = new();
            _mockClientTypeFactory
                .Setup(x => x.Create(It.IsAny<BasicHttpBinding>()))
                .Returns(new LearnerPortTypeClient());

            _sut = new LearnerVerificationServiceClientProvider(_mockClientTypeFactory.Object, Mock.Of<ILogger<LearnerVerificationServiceClientProvider>>());

            //Act
            _clientWrapper = _sut.Get();
        }

        [Test]
        public void ThenCorrectServiceIsReturned()
        {
            _clientWrapper.Should().NotBeNull();
            _clientWrapper.Should().BeOfType<LearnerVerificationClientWrapper>();
        }

        [Test]
        public void ThenLearnerServiceClientFactoryIsCalledWithCorrectArguments()
        {
            _mockClientTypeFactory
                .Verify(factory => factory.Create(It.Is<BasicHttpBinding>(
                    binding => binding.Security.Transport.ClientCredentialType == HttpClientCredentialType.Certificate &&
                    binding.Security.Mode == BasicHttpSecurityMode.Transport &&
                    binding.UseDefaultWebProxy == true
                    )), Times.Once);
        }
    }
}