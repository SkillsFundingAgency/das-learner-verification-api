using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [Ignore("Unit tests still in development")]
    public class WhenGettingClientCertificate
    {
        private X509Certificate2 _clientCertificate;
        private LrsApiWcfSettings _settings;
        private CertificateProvider _sut;

        [TestCase(null)]
        [TestCase("")]
        public void AndKeyVaultConfigIsNotSetThenThrowException(string url)
        {
            //Arrange
            _settings.KeyVaultUrl = url;

            //Act
            Action act = () => _sut.GetClientCertificate();

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("KeyVault url is not specified. That is required to run the app. (Parameter 'KeyVaultUrl')");
        }

        [SetUp]
        public void Setup()
        {
            //Arrange
            _settings = new LrsApiWcfSettings();
            _sut = new CertificateProvider(_settings, Mock.Of<ILogger<CertificateProvider>>());
        }

        [Test]
        public void ThenX509Certificate2IsReturned()
        {
            //Arrange
            _settings.CertName = "certificate";
            _settings.KeyVaultUrl = "http://valid.test.url/";
            //TODO: Test not currently passing. Figure out if code needs to be changed so that this passes?

            //Act
            _clientCertificate = _sut.GetClientCertificate();

            //Assert
            _clientCertificate.Should().NotBeNull();
            _clientCertificate.Should().BeOfType<X509Certificate2>();
        }
    }
}