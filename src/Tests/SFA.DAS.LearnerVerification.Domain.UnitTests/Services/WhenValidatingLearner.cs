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
                        VerifiedLearner = new MIAPVerifiedLearner()
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
            //TODO: Add more assertions
            // verify verifyLearnerAsync() is called with the correct args
        }
    }
}