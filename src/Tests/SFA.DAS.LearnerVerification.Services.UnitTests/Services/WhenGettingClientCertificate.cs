using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Services.Services;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Services.UnitTests.Services
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
        public void AndKeyVaultConfigIsNotSetThenThrowException(string? url)
        {
            //Arrange
            _settings.LearnerVerificationKeyVaultUrl = url;

            //Act
            Action act = () => _sut.GetClientCertificate();

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("LearnerVerificationKeyVaultUrl is not specified. That is required to run the app. (Parameter 'LearnerVerificationKeyVaultUrl')");
        }

        [TestCase(null)]
        [TestCase("")]
        public void AndCertNameConfigIsNotSetThenThrowException(string? certName)
        {
            //Arrange
            _settings.LearnerVerificationKeyVaultUrl = "not null test url";
            _settings.LrsApiWcfSettings.LRSCertificateName = certName;

            //Act
            Action act = () => _sut.GetClientCertificate();

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("LRSCertificateName for LRS Web Service is not specified. That is required to run the app. (Parameter 'LRSCertificateName')");
        }
    }
}