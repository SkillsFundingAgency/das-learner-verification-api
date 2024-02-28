using AutoFixture;
using FluentAssertions;
using SFA.DAS.LearnerVerification.Queries.Mappers;
using SFA.DAS.LearnerVerification.Services;
using SFA.DAS.LearnerVerification.Types;

namespace SFA.DAS.LearnerVerification.Queries.UnitTests.Mappers
{
    [TestFixture]
    public class WhenMappingLearnerVerificationResponse
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [TestCase(Services.FailureFlag.VRF1, LearnerDetailMatchingError.GivenDoesntMatchGiven)]
        [TestCase(Services.FailureFlag.VRF2, LearnerDetailMatchingError.GivenDoesntMatchFamily)]
        [TestCase(Services.FailureFlag.VRF3, LearnerDetailMatchingError.GivenDoesntMatchPreviousFamily)]
        [TestCase(Services.FailureFlag.VRF4, LearnerDetailMatchingError.FamilyDoesntMatchGiven)]
        [TestCase(Services.FailureFlag.VRF5, LearnerDetailMatchingError.FamilyDoesntMatchFamily)]
        [TestCase(Services.FailureFlag.VRF6, LearnerDetailMatchingError.FamilyDoesntMatchPreviousFamily)]
        [TestCase(Services.FailureFlag.VRF7, LearnerDetailMatchingError.DateOfBirthDoesntMatchDateOfBirth)]
        [TestCase(Services.FailureFlag.VRF8, LearnerDetailMatchingError.GenderDoesntMatchGender)]
        public void ThenAllValidFailureFlagsAreMapped(Services.FailureFlag flag, LearnerDetailMatchingError expectedMatchingError)
        {
            List<Services.FailureFlag> failures = new() { flag };
            var verificationResponse = _fixture
                .Build<LearnerVerificationResponse>()
                .With(x => x.FailureFlags, failures)
                .With(x => x.ResponseCode, LearnerVerificationResponseCode.WSVRC001)
                .Create();

            //Act
            var result = verificationResponse.Map();

            //Assert
            result.Should().NotBeNull();
            result.MatchingErrors.Should().NotBeNull();
            result.MatchingErrors.Should().HaveCount(1);
            result.MatchingErrors.First().Should().Be(expectedMatchingError);
        }

        [TestCase((Services.FailureFlag)(-1))]
        public void AndAFailureFlagIsInvalidThenArgumentExceptionIsThrown(Services.FailureFlag flag)
        {
            List<Services.FailureFlag> failures = new() { flag };
            var verificationResponse = _fixture
                .Build<LearnerVerificationResponse>()
                .With(x => x.FailureFlags, failures)
                .With(x => x.ResponseCode, LearnerVerificationResponseCode.WSVRC001)
                .Create();

            //Act
            Action act = () => { _ = verificationResponse.Map(); };

            //Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Value {flag} could not be mapped to a LearnerDetailMatchingError. (Parameter 'FailureFlags')");
        }

        [TestCase(LearnerVerificationResponseCode.WSVRC001, LearnerVerificationResponseType.SuccessfulMatch)]
        [TestCase(LearnerVerificationResponseCode.WSVRC002, LearnerVerificationResponseType.SuccessfulLinkedMatch)]
        [TestCase(LearnerVerificationResponseCode.WSVRC003, LearnerVerificationResponseType.SimilarMatch)]
        [TestCase(LearnerVerificationResponseCode.WSVRC004, LearnerVerificationResponseType.SimilarLinkedMatch)]
        [TestCase(LearnerVerificationResponseCode.WSVRC005, LearnerVerificationResponseType.LearnerDoesNotMatch)]
        [TestCase(LearnerVerificationResponseCode.WSVRC006, LearnerVerificationResponseType.UlnNotFound)]
        public void ThenAllValidResponseCodesAreMapped(LearnerVerificationResponseCode responseCode, LearnerVerificationResponseType expectedResponseType)
        {
            var verificationResponse = _fixture
                .Build<LearnerVerificationResponse>()
                .With(x => x.FailureFlags, new List<Services.FailureFlag>())
                .With(x => x.ResponseCode, responseCode)
                .Create();

            //Act
            var result = verificationResponse.Map();

            //Assert
            result.Should().NotBeNull();
            result.ResponseType.Should().Be(expectedResponseType);
        }

        [TestCase((LearnerVerificationResponseCode)(-1))]
        public void AndALearnerVerificationResponseCodeIsInvalidThenArgumentExceptionIsThrown(LearnerVerificationResponseCode responseCode)
        {
            var verificationResponse = _fixture
                .Build<LearnerVerificationResponse>()
                .With(x => x.FailureFlags, new List<Services.FailureFlag>())
                .With(x => x.ResponseCode, responseCode)
                .Create();

            //Act
            Action act = () => { _ = verificationResponse.Map(); };

            //Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Value {responseCode} could not be mapped to a LearnerVerificationResponseCode. (Parameter 'ResponseCode')");
        }
    }
}