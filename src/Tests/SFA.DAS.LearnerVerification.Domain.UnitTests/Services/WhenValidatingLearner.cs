using FluentAssertions;
using LearningRecordsService;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.LearnerVerification.Domain.Services;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Services
{
    [Ignore("Unit tests still in development")]
    public class WhenValidatingLearner
    {
        private MIAPVerifiedLearner _learner;
        private Mock<LearnerPortType> _mockClient;
        private Mock<ILearnerServiceClientProvider<LearnerPortTypeClient>> _mockClientProvider;
        private LearnerValidationService _sut;

        public WhenValidatingLearner()
        {
            _mockClient = new();
            _mockClientProvider = new();
        }

        [SetUp]
        public void Setup()
        {
            //Arrange
            _mockClient
                .Setup(x => x.verifyLearnerAsync(It.IsAny<verifyLearnerRequest>()))
                .ReturnsAsync(new verifyLearnerResponse()
                {
                    VerifyLearnerResponse = new VerifyLearnerResp()
                    {
                        VerifiedLearner = new MIAPVerifiedLearner()
                    }
                });

            //TODO: finish this set up (not currently working)
            //_mockClientProvider
            //    .Setup(x => x.GetService())
            //    .Returns(_mockClient.Object);

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