using Moq;
using SFA.DAS.LearnerVerification.Domain.Factories;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Factories
{
    public class WhenGettingClientCertificate
    {
        private Mock<LrsApiWcfSettings> _mockLrsApiWcfSettings;

        private CertificateProvider _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CertificateProvider(_mockLrsApiWcfSettings.Object);
        }

        [Test]
        public void ThenX509Certificate2IsReturned()
        {
            //Arrange

            //Act
            var certificate = _sut.GetClientCertificate();

            //Assert
            //TODO:
        }
    }
}