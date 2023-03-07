using AutoFixture;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using System.ServiceModel;
using FluentAssertions;
using SFA.DAS.LearnerVerification.Domain.Services;
using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    [Ignore("Unit tests still in development")]
    public class WhenCreatingANewLearnerPortTypeClient
    {
        private LearnerPortTypeClient _client;
        private Fixture _fixture;
        private LrsApiWcfSettings _settings;
        private LearnerPortTypeClientFactory _sut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fixture = new Fixture();
            _settings = new LrsApiWcfSettings
            {
                LearnerServiceBaseUrl = "http://valid.test.url/"
            };
            _sut = new LearnerPortTypeClientFactory(_settings, Mock.Of<ICertificateProvider>());

            var binding = _fixture.Create<BasicHttpBinding>();

            //Act
            _client = _sut.Create(binding);
        }

        [Test]
        public void ThenALearnerPortTypeClientHasTheCorrectUrl()
        {
            _client.Endpoint.Address.ToString().Should().Be(_settings.LearnerServiceBaseUrl);
        }

        [Test]
        public void ThenALearnerPortTypeClientIsCreated()
        {
            _client.Should().NotBeNull();
            _client.Should().BeOfType<LearnerPortTypeClient>();
        }
    }
}