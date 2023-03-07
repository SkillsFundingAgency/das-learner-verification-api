using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [Ignore("Unit tests still in development")]
    public class WhenGettingClientCertificate
    {
        private Mock<LrsApiWcfSettings> _mockLrsApiWcfSettings;

        private CertificateProvider _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CertificateProvider(_mockLrsApiWcfSettings.Object, Mock.Of<ILogger<CertificateProvider>>());
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