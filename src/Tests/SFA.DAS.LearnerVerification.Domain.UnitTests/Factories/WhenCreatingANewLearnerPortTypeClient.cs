using AutoFixture;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using System.ServiceModel;
using FluentAssertions;
using SFA.DAS.LearnerVerification.Domain.Services;
using LearningRecordsService;
using System.ServiceModel.Channels;
using System.Security.Policy;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenCreatingANewLearnerPortTypeClient
    {
        private LearnerPortTypeClient _client;
        private Fixture _fixture;
        private LrsApiWcfSettings _settings;
        private BasicHttpBinding _basicHttpBinding;
        private LearnerPortTypeClientFactory _sut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fixture = new Fixture();
            _basicHttpBinding = _fixture.Create<BasicHttpBinding>();
        }

        private void AddLearnerServiceBaseUrl(string? url)
        {
            _settings = new LrsApiWcfSettings
            {
                LearnerServiceBaseUrl = url
            };
        }

        private void AddValidLearnerServiceBaseUrl()
        {
            _settings = new LrsApiWcfSettings
            {
                LearnerServiceBaseUrl = "http://valid.test.url/"
            };
        }

        [TestCase(null)]
        [TestCase("")]
        public void AndKeyVaultConfigIsNotSetThenThrowException(string url)
        {
            //Arrange
            AddLearnerServiceBaseUrl(url);

            //Act
            Action act = () => { _ = new LearnerPortTypeClientFactory(_settings, Mock.Of<ICertificateProvider>()); };

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("LearnerServiceBaseUrl is not specified. This is required to run the app. (Parameter 'LearnerServiceBaseUrl')");
        }

        [Test]
        public void ThenALearnerPortTypeClientHasTheCorrectUrl()
        {
            //Arrange
            AddValidLearnerServiceBaseUrl();
            _sut = new LearnerPortTypeClientFactory(_settings, Mock.Of<ICertificateProvider>());

            //Act
            _client = _sut.Create(_basicHttpBinding);

            //Assert
            _client.Endpoint.Address.ToString().Should().Be(_settings.LearnerServiceBaseUrl);
        }

        [Test]
        public void ThenALearnerPortTypeClientIsCreated()
        {
            //Arrange
            AddValidLearnerServiceBaseUrl();
            _sut = new LearnerPortTypeClientFactory(_settings, Mock.Of<ICertificateProvider>());

            //Act
            _client = _sut.Create(_basicHttpBinding);

            //Assert
            _client.Should().NotBeNull();
            _client.Should().BeOfType<LearnerPortTypeClient>();
        }
    }
}