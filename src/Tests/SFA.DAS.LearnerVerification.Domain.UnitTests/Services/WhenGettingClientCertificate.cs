using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [TestFixture]
    public class WhenGettingClientCertificate
    {
        private ApplicationSettings _settings;
        private CertificateProvider _sut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _settings = new ApplicationSettings
            {
                LrsApiWcfSettings = new LrsApiWcfSettings()
            };
            _sut = new CertificateProvider(Mock.Of<ILogger<CertificateProvider>>(), _settings);
        }

        [TestCase(null)]
        [TestCase("")]
        public void AndKeyVaultConfigIsNotSetThenThrowException(string url)
        {
            //Arrange
            _settings.LrsApiWcfSettings.KeyVaultUrl = url;

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
            _settings.LrsApiWcfSettings.KeyVaultUrl = "not null test url";
            _settings.LrsApiWcfSettings.CertificateName = certName;

            //Act
            Action act = () => _sut.GetClientCertificate();

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Cert name added to KeyVault is not specified. That is required to run the app. (Parameter 'CertName')");
        }
    }
}