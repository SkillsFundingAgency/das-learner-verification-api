using AutoFixture;
using FluentAssertions;
using LearningRecordsService;
using SFA.DAS.LearnerVerification.Domain.Mappers;

namespace SFA.DAS.LearnerVerification.Domain.UnitTests.Mappers
{
    [TestFixture]
    public class WhenMappingMIAPVerifiedLearner
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [TestCase("VRF1", FailureFlag.VRF1)]
        [TestCase("VRF2", FailureFlag.VRF2)]
        [TestCase("VRF3", FailureFlag.VRF3)]
        [TestCase("VRF4", FailureFlag.VRF4)]
        [TestCase("VRF5", FailureFlag.VRF5)]
        [TestCase("VRF6", FailureFlag.VRF6)]
        [TestCase("VRF7", FailureFlag.VRF7)]
        [TestCase("VRF8", FailureFlag.VRF8)]
        public void ThenAllValidFailureFlagsAreMapped(string flag, FailureFlag expectedFailureFlag)
        {
            string[] failures = { flag };
            var verificationResponse = _fixture
                .Build<MIAPVerifiedLearner>()
                .With(x => x.FailureFlag, failures)
                .With(x => x.ResponseCode, "WSVRC001")
                .Create();

            //Act
            var result = verificationResponse.Map();

            //Assert
            result.Should().NotBeNull();
            result.FailureFlags.Should().NotBeNull();
            result.FailureFlags.Should().HaveCount(1);
            result.FailureFlags.First().Should().Be(expectedFailureFlag);
        }

        [TestCase("")]
        [TestCase("invalidFlag")]
        [TestCase("VRF0")]
        public void AndAFailureFlagIsInvalidThenArgumentExceptionIsThrown(string flag)
        {
            string[] failures = { flag };
            var verificationResponse = _fixture
                .Build<MIAPVerifiedLearner>()
                .With(x => x.FailureFlag, failures)
                .With(x => x.ResponseCode, "WSVRC001")
                .Create();

            //Act
            Action act = () => { _ = verificationResponse.Map(); };

            //Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Value {flag} could not be parsed to FailureFlag. (Parameter 'FailureFlag')");
        }

        [TestCase("WSVRC001", LearnerVerificationResponseCode.WSVRC001)]
        [TestCase("WSVRC002", LearnerVerificationResponseCode.WSVRC002)]
        [TestCase("WSVRC003", LearnerVerificationResponseCode.WSVRC003)]
        [TestCase("WSVRC004", LearnerVerificationResponseCode.WSVRC004)]
        [TestCase("WSVRC005", LearnerVerificationResponseCode.WSVRC005)]
        [TestCase("WSVRC006", LearnerVerificationResponseCode.WSVRC006)]
        public void ThenAllValidResponseCodesAreMapped(string responseCode, LearnerVerificationResponseCode learnerVerificationResponseCode)
        {
            var verificationResponse = _fixture
                .Build<MIAPVerifiedLearner>()
                .With(x => x.FailureFlag, Array.Empty<string>())
                .With(x => x.ResponseCode, responseCode)
                .Create();

            //Act
            var result = verificationResponse.Map();

            //Assert
            result.Should().NotBeNull();
            result.ResponseCode.Should().Be(learnerVerificationResponseCode);
        }

        [TestCase("")]
        [TestCase("invalidResponseCode")]
        [TestCase("WSVR000")]
        public void AndAResponseCodeIsInvalidThenArgumentExceptionIsThrown(string responseCode)
        {
            var verificationResponse = _fixture
                .Build<MIAPVerifiedLearner>()
                .With(x => x.FailureFlag, Array.Empty<string>())
                .With(x => x.ResponseCode, responseCode)
                .Create();

            //Act
            Action act = () => { _ = verificationResponse.Map(); };

            //Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Value {responseCode} could not be parsed to LearnerVerificationResponseCode. (Parameter 'ResponseCode')");
        }
    }
}