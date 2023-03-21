using FluentAssertions;
using LearningRecordsService;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Domain.Wrappers;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [TestFixture]
    public class WhenValidatingLearner
    {
        private LearnerVerificationResponse _verificationResponse;
        private readonly Mock<ILearnerVerificationClientWrapper> _mockClientWrapper;
        private readonly Mock<ILearnerVerificationServiceClientProvider> _mockClientProvider;
        private readonly ApplicationSettings _settings;

        private readonly string _expectedFamilyName = "Dwyer";
        private readonly string _expectedGivenName = "Andy";
        private readonly string _expectedResponseCode = "WSVRC001";
        private readonly string[] _expectedFailures = Array.Empty<string>();

        public WhenValidatingLearner()
        {
            _mockClientWrapper = new();
            _mockClientProvider = new();
            _settings = new();
            _verificationResponse = new();
        }

        private void MockValidClientProvider()
        {
            _mockClientWrapper
                            .Setup(x => x.verifyLearnerAsync(It.IsAny<VerifyLearnerRqst>()))
                            .ReturnsAsync(new verifyLearnerResponse()
                            {
                                VerifyLearnerResponse = new VerifyLearnerResp()
                                {
                                    VerifiedLearner = new MIAPVerifiedLearner
                                    {
                                        FamilyName = _expectedFamilyName,
                                        GivenName = _expectedGivenName,
                                        ResponseCode = _expectedResponseCode,
                                        FailureFlag = _expectedFailures
                                    }
                                }
                            });

            _mockClientProvider
                .Setup(x => x.Get())
                .Returns(_mockClientWrapper.Object);
        }

        private void AddSettings(string? password, string? username)
        {
            _settings.LrsApiWcfSettings = new LrsApiWcfSettings
            {
                OrgPassword = password,
                UserName = username
            };
        }

        private void AddValidSettings()
        {
            _settings.LrsApiWcfSettings = new LrsApiWcfSettings
            {
                OrgPassword = "orgPassword",
                UserName = "userName"
            };
        }

        [TestCase(null)]
        [TestCase("")]
        public void AndOrgPasswordConfigIsNotSetThenThrowException(string password)
        {
            //Arrange
            MockValidClientProvider();
            AddSettings(password, "test");

            //Act
            Action act = () => { _ = new LearnerValidationService(_mockClientProvider.Object, _settings); };

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("OrgPassword is not specified. This is required to run the app. (Parameter 'OrgPassword')");
        }

        [TestCase(null)]
        [TestCase("")]
        public void AndUserNameConfigIsNotSetThenThrowException(string username)
        {
            //Arrange
            MockValidClientProvider();
            AddSettings("password", username);

            //Act
            Action act = () => { _ = new LearnerValidationService(_mockClientProvider.Object, _settings); };

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("UserName is not specified. This is required to run the app. (Parameter 'UserName')");
        }

        [Test]
        public async Task ThenVerificationResponseIsReturnedAsync()
        {
            //Arrange
            MockValidClientProvider();
            AddValidSettings();
            var _sut = new LearnerValidationService(_mockClientProvider.Object, _settings);

            //Act
            _verificationResponse = await _sut.ValidateLearner("012345678", "Ron", "Swanson", "F", DateTime.UtcNow.AddYears(-18));

            //Assert
            _verificationResponse.Should().NotBeNull();
            _verificationResponse.Should().BeOfType<LearnerVerificationResponse>();
        }

        [Test]
        public async Task ThenVerificationRequestIsBuiltCorrectly()
        {
            //Arrange
            MockValidClientProvider();
            AddValidSettings();
            var _sut = new LearnerValidationService(_mockClientProvider.Object, _settings);
            var ukprn = "012345678";
            var uln = "912345678";
            var firstName = "April";
            var lastName = "Ludgate";
            var gender = "F";
            var dateOfBirth = DateTime.UtcNow.AddYears(-20);

            //Act
            _verificationResponse = await _sut.ValidateLearner(uln, firstName, lastName, gender, dateOfBirth);

            //Assert
            _mockClientWrapper.Verify(x =>
                x.verifyLearnerAsync(It.Is<VerifyLearnerRqst>(y =>
                    y.LearnerToVerify.ULN == uln
                    && y.LearnerToVerify.GivenName == firstName
                    && y.LearnerToVerify.FamilyName == lastName
                    && y.LearnerToVerify.Gender == gender
                    && y.LearnerToVerify.DateOfBirth == dateOfBirth.ToString("yyyy-MM-dd")
            )));
        }
    }
}