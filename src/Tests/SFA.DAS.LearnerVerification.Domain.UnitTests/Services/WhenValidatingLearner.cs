using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Domain.Wrappers;
using SFA.DAS.LearnerVerification.Infrastructure.Configuration;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenValidatingLearner
    {
        private MIAPVerifiedLearner _learner;
        private Mock<ILearnerVerificationClientWrapper> _mockClientWrapper;
        private Mock<ILearnerVerificationServiceClientProvider> _mockClientProvider;
        private ApplicationSettings _settings;
        private LearnerValidationService _sut;

        private readonly string _expectedFamilyName = "Dwyer";
        private readonly string _expectedGivenName = "Andy";

        public WhenValidatingLearner()
        {
            _mockClientWrapper = new();
            _mockClientProvider = new();
            _settings = new();
        }

        [SetUp]
        public void Setup()
        {
            //Arrange
            _mockClientWrapper
                .Setup(x => x.verifyLearnerAsync(It.IsAny<VerifyLearnerRqst>()))
                .ReturnsAsync(new verifyLearnerResponse()
                {
                    VerifyLearnerResponse = new VerifyLearnerResp()
                    {
                        VerifiedLearner = new MIAPVerifiedLearner
                        {
                            FamilyName = _expectedFamilyName,
                            GivenName = _expectedGivenName
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
            AddSettings(password, "test");

            //Act
            Action act = () => { _ = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>(), _settings); };

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
            AddSettings("password", username);

            //Act
            Action act = () => { _ = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>(), _settings); };

            //Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("UserName is not specified. This is required to run the app. (Parameter 'UserName')");
        }

        [Test]
        public async Task ThenVerificationResponseIsReturnedAsync()
        {
            //Arrange
            AddValidSettings();
            _sut = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>(), _settings);

            //Act
            _learner = await _sut.ValidateLearner("012345678", "012345678", "Ron", "Swanson", "F", DateTime.UtcNow.AddYears(-18));

            //Assert
            _learner.Should().BeOfType<MIAPVerifiedLearner>();
            _learner.FamilyName.Should().Be(_expectedFamilyName);
            _learner.GivenName.Should().Be(_expectedGivenName);
        }

        [Test]
        public async Task ThenVerificationRequestIsBuiltCorrectly()
        {
            //Arrange
            AddValidSettings();
            _sut = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>(), _settings);
            var ukprn = "012345678";
            var uln = "912345678";
            var firstName = "April";
            var lastName = "Ludgate";
            var gender = "F";
            var dateOfBirth = DateTime.UtcNow.AddYears(-20);

            //Act
            _learner = await _sut.ValidateLearner(ukprn, uln, firstName, lastName, gender, dateOfBirth);

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