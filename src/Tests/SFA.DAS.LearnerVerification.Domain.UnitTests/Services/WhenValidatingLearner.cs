using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;
using SFA.DAS.LearnerVerification.Domain.Wrappers;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    public class WhenValidatingLearner
    {
        private MIAPVerifiedLearner _learner;
        private Mock<ILearnerVerificationClientWrapper> _mockClientWrapper;
        private Mock<ILearnerVerificationServiceClientProvider> _mockClientProvider;
        private LearnerValidationService _sut;

        private readonly string _expectedFamilyName = "Dwyer";
        private readonly string _expectedGivenName = "Andy";

        public WhenValidatingLearner()
        {
            _mockClientWrapper = new();
            _mockClientProvider = new();
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

            _sut = new LearnerValidationService(_mockClientProvider.Object, Mock.Of<ILogger<LearnerValidationService>>());
        }

        [Test]
        public async Task ThenVerificationResponseIsReturnedAsync()
        {
            //Act
            _learner = await _sut.ValidateLearner("012345678", "Ron", "Swanson", "F", DateTime.UtcNow.AddYears(-18));

            //Assert
            _learner.Should().BeOfType<MIAPVerifiedLearner>();
            _learner.FamilyName.Should().Be(_expectedFamilyName);
            _learner.GivenName.Should().Be(_expectedGivenName);
        }

        [Test]
        public async Task ThenVerificationRequestIsBuiltCorrectly()
        {
            //Arrange
            var uln = "912345678";
            var firstName = "April";
            var lastName = "Ludgate";
            var gender = "F";
            var dateOfBirth = DateTime.UtcNow.AddYears(-20);

            //Act
            _learner = await _sut.ValidateLearner(uln, firstName, lastName, gender, dateOfBirth);

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