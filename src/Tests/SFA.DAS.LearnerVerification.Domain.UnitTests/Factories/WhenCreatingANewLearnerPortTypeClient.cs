using AutoFixture;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using System.ServiceModel;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenCreatingANewLearnerPortTypeClient
    {
        private Fixture _fixture;
        private Mock<ICertificateProvider> _mockCertificateProvider;
        private Mock<LrsApiWcfSettings> _mockLrsApiWcfSettings;
        private LearnerPortTypeClientFactory _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _sut = new LearnerPortTypeClientFactory(_mockLrsApiWcfSettings.Object, _mockCertificateProvider.Object);
        }

        [Test]
        public void ThenALearnerPortTypeClientIsCreated()
        {
            //Arrange
            var binding = _fixture.Create<BasicHttpBinding>();

            //Act
            var client = _sut.Create(binding);

            //Assert
            //TODO:
        }
    }
}