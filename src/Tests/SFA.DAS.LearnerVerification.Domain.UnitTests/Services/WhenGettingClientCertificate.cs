using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using System.Security.Cryptography.X509Certificates;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenGettingClientCertificate
    {
        private LrsApiWcfSettings _settings;
        private CertificateProvider _sut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _settings = new LrsApiWcfSettings();
            _sut = new CertificateProvider(_settings, Mock.Of<ILogger<CertificateProvider>>());
        }

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

        [TestCase(null)]
        [TestCase("")]
        public void AndCertNameConfigIsNotSetThenThrowException(string certName)
        {
            //Arrange
            _settings.KeyVaultUrl = "not null test url";
            _settings.CertName = certName;

            //Act
            Action act = () => _sut.GetClientCertificate();

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Cert name added to KeyVault is not specified. That is required to run the app. (Parameter 'CertName')");
        }
    }
}