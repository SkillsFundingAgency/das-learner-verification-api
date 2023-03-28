using SFA.DAS.LearnerVerification.Services;
using SFA.DAS.LearnerVerification.Types;

namespace SFA.DAS.LearnerVerification.Queries.Mappers
{
    public static class LearnerVerificationResponseMapper
    {
        public static VerifyLearner.LearnerVerification Map(this LearnerVerificationResponse learnerVerificationResponse)
        {
            var responseType = learnerVerificationResponse.ResponseCode switch
            {
                LearnerVerificationResponseCode.WSVRC001 => LearnerVerificationResponseType.SuccessfulMatch,
                LearnerVerificationResponseCode.WSVRC002 => LearnerVerificationResponseType.SuccessfulLinkedMatch,
                LearnerVerificationResponseCode.WSVRC003 => LearnerVerificationResponseType.SimilarMatch,
                LearnerVerificationResponseCode.WSVRC004 => LearnerVerificationResponseType.SimilarLinkedMatch,
                LearnerVerificationResponseCode.WSVRC005 => LearnerVerificationResponseType.LearnerDoesNotMatch,
                LearnerVerificationResponseCode.WSVRC006 => LearnerVerificationResponseType.UlnNotFound,
                _ => throw new ArgumentException($"Value {learnerVerificationResponse.ResponseCode} could not be mapped to a {nameof(LearnerVerificationResponseCode)}.", nameof(learnerVerificationResponse.ResponseCode)),
            };

            List<LearnerDetailMatchingError> matchingErrors = new();
            if (learnerVerificationResponse.FailureFlags != null)
            {
                foreach (var flag in learnerVerificationResponse.FailureFlags)
                {
                    switch (flag)
                    {
                        case Services.FailureFlag.VRF1:
                            matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchGiven);
                            break;

                        case Services.FailureFlag.VRF2:
                            matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchFamily);
                            break;

                        case Services.FailureFlag.VRF3:
                            matchingErrors.Add(LearnerDetailMatchingError.GivenDoesntMatchPreviousFamily);
                            break;

                        case Services.FailureFlag.VRF4:
                            matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchGiven);
                            break;

                        case Services.FailureFlag.VRF5:
                            matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchFamily);
                            break;

                        case Services.FailureFlag.VRF6:
                            matchingErrors.Add(LearnerDetailMatchingError.FamilyDoesntMatchPreviousFamily);
                            break;

                        case Services.FailureFlag.VRF7:
                            matchingErrors.Add(LearnerDetailMatchingError.DateOfBirthDoesntMatchDateOfBirth);
                            break;

                        case Services.FailureFlag.VRF8:
                            matchingErrors.Add(LearnerDetailMatchingError.GenderDoesntMatchGender);
                            break;

                        default:
                            throw new ArgumentException($"Value {flag} could not be mapped to a {nameof(LearnerDetailMatchingError)}.", nameof(learnerVerificationResponse.FailureFlags));
                    }
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