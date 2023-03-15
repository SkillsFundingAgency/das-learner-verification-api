using SFA.DAS.LearnerVerification.Domain;
using SFA.DAS.LearnerVerification.Types;

namespace SFA.DAS.LearnerVerification.Queries.Mappers
{
    internal static class LearnerVerificationResponseMapper
    {
        internal static VerifyLearner.LearnerVerification Map(this LearnerVerificationResponse learnerVerificationResponse)
        {
            var responseType = learnerVerificationResponse.ResponseCode switch
            {
                Domain.LearnerVerificationResponseCode.WSVRC001 => LearnerVerificationResponseType.SuccessfulMatch,
                Domain.LearnerVerificationResponseCode.WSVRC002 => LearnerVerificationResponseType.SuccessfulLinkedMatch,
                Domain.LearnerVerificationResponseCode.WSVRC003 => LearnerVerificationResponseType.SimilarMatch,
                Domain.LearnerVerificationResponseCode.WSVRC004 => LearnerVerificationResponseType.SimilarLinkedMatch,
                Domain.LearnerVerificationResponseCode.WSVRC005 => LearnerVerificationResponseType.LearnerDoesNotMatch,
                Domain.LearnerVerificationResponseCode.WSVRC006 => LearnerVerificationResponseType.UlnNotFound,
                _ => throw new ArgumentException($"Value {learnerVerificationResponse.ResponseCode} could not be mapped to a {nameof(LearnerVerificationResponseCode)}.", nameof(learnerVerificationResponse.ResponseCode)),
            };

            List<LearnerDetailMatchingError> matchingErrors = new();
            foreach (var flag in learnerVerificationResponse.FailureFlags)
            {
                switch (flag)
                {
                    case Domain.FailureFlag.VRF1:
                        matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchGiven);
                        break;

                    case Domain.FailureFlag.VRF2:
                        matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchFamily);
                        break;

                    case Domain.FailureFlag.VRF3:
                        matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchPreviousFamily);
                        break;

                    case Domain.FailureFlag.VRF4:
                        matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchGiven);
                        break;

                    case Domain.FailureFlag.VRF5:
                        matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchFamily);
                        break;

                    case Domain.FailureFlag.VRF6:
                        matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchPreviousFamily);
                        break;

                    case Domain.FailureFlag.VRF7:
                        matchingErrors.Add(LearnerDetailMatchingError.DateOfBirthDoesntMatchDateOfBirth);
                        break;

                    case Domain.FailureFlag.VRF8:
                        matchingErrors.Add(LearnerDetailMatchingError.GenderDoesntMatchGender);
                        break;

                    default:
                        throw new ArgumentException($"Value {flag} could not be mapped to a {nameof(LearnerDetailMatchingError)}.", nameof(learnerVerificationResponse.FailureFlags));
                }
            }

            return new VerifyLearner.LearnerVerification
            {
                ResponseType = responseType,
                MatchingErrors = matchingErrors
            };
        }
    }
}