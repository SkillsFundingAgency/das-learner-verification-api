using AutoFixture;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using System.ServiceModel;
using FluentAssertions;
using SFA.DAS.LearnerVerification.Domain.Services;
using LearningRecordsService;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenCreatingANewLearnerPortTypeClient
    {
        private LearnerPortTypeClient _client;
        private Fixture _fixture;
        private ApplicationSettings _settings;
        private BasicHttpBinding _basicHttpBinding;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _client = new();
            _fixture = new Fixture();
            _basicHttpBinding = _fixture.Create<BasicHttpBinding>();
            _settings = new ApplicationSettings();
        }

        private void AddLearnerServiceBaseUrl(string? url)
        {
            _settings.LrsApiWcfSettings = new LrsApiWcfSettings
            {
                LearnerServiceBaseUrl = url
            };
        }

        private void AddValidLearnerServiceBaseUrl()
        {
            _settings.LrsApiWcfSettings = new LrsApiWcfSettings
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
            Action act = () => { _ = new LearnerPortTypeClientFactory(Mock.Of<ICertificateProvider>(), _settings); };

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
            var _sut = new LearnerPortTypeClientFactory(Mock.Of<ICertificateProvider>(), _settings);

            //Act
            _client = _sut.Create(_basicHttpBinding);

            //Assert
            _client.Endpoint.Address.ToString().Should().Be(_settings.LrsApiWcfSettings.LearnerServiceBaseUrl);
        }

        [Test]
        public void ThenALearnerPortTypeClientIsCreated()
        {
            //Arrange
            AddValidLearnerServiceBaseUrl();
            var _sut = new LearnerPortTypeClientFactory(Mock.Of<ICertificateProvider>(), _settings);

            //Act
            _client = _sut.Create(_basicHttpBinding);

            //Assert
            _client.Should().NotBeNull();
            _client.Should().BeOfType<LearnerPortTypeClient>();
        }
    }
}