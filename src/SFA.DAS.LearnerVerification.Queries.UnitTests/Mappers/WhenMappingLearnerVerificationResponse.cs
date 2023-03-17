using AutoFixture;
using FluentAssertions;
using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Queries.Mappers;
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

        [TestCase(Domain.FailureFlag.VRF1, LearnerDetailMatchingError.GivenDoesntMatchGiven)]
        [TestCase(Domain.FailureFlag.VRF2, LearnerDetailMatchingError.GivenDoesntMatchFamily)]
        [TestCase(Domain.FailureFlag.VRF3, LearnerDetailMatchingError.GivenDoesntMatchPreviousFamily)]
        [TestCase(Domain.FailureFlag.VRF4, LearnerDetailMatchingError.FamilyDoesntMatchGiven)]
        [TestCase(Domain.FailureFlag.VRF5, LearnerDetailMatchingError.FamilyDoesntMatchFamily)]
        [TestCase(Domain.FailureFlag.VRF6, LearnerDetailMatchingError.FamilyDoesntMatchPreviousFamily)]
        [TestCase(Domain.FailureFlag.VRF7, LearnerDetailMatchingError.DateOfBirthDoesntMatchDateOfBirth)]
        [TestCase(Domain.FailureFlag.VRF8, LearnerDetailMatchingError.GenderDoesntMatchGender)]
        public void ThenAllValidFailureFlagsAreMapped(Domain.FailureFlag flag, LearnerDetailMatchingError expectedMatchingError)
        {
            List<Domain.FailureFlag> failures = new() { flag };
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

        [TestCase((Domain.FailureFlag)(-1))]
        public void AndAFailureFlagIsInvalidThenArgumentExceptionIsThrown(Domain.FailureFlag flag)
        {
            List<Domain.FailureFlag> failures = new() { flag };
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
                .With(x => x.FailureFlags, new List<Domain.FailureFlag>())
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
                .With(x => x.FailureFlags, new List<Domain.FailureFlag>())
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