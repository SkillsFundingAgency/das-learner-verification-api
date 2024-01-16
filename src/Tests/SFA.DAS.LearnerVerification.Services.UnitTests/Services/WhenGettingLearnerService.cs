using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Services.Factories;
using SFA.DAS.LearnerVerification.Services.Services;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Services.Wrappers;

namespace SFA.DAS.LearnerVerification.Services.UnitTests.Services
{
    [TestFixture]
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
                .Setup(x => x.Create(It.IsAny<BasicHttpsBinding>()))
                .Returns(new LearnerPortTypeClient());

            _sut = new LearnerVerificationServiceClientProvider(_mockClientTypeFactory.Object, Mock.Of<ILogger<LearnerVerificationServiceClientProvider>>());

            //Act
            _clientWrapper = _sut.Get();
        }
        [TearDown]
        public void CleanUp()
        {
            _clientWrapper.DisposeAsync();

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
                .Verify(factory => factory.Create(It.Is<BasicHttpsBinding>(
                    binding => binding.Security.Transport.ClientCredentialType == HttpClientCredentialType.Certificate &&
                    binding.Security.Mode == BasicHttpsSecurityMode.Transport &&
                    binding.UseDefaultWebProxy == true
                    )), Times.Once);
        }
    }
}