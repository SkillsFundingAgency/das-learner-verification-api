using LearningRecordsService;

namespace SFA.DAS.LearnerVerification.Domain.Mappers
{
    internal static class LearnerVerificationResponseMapper
    {
        internal static LearnerVerificationResponse Map(this MIAPVerifiedLearner verifiedLearner)
        {
            if (!Enum.TryParse(verifiedLearner.ResponseCode, true, out LearnerVerificationResponseCode parsedResponseCode))
            {
                throw new ArgumentException($"Value {verifiedLearner.ResponseCode} could not be parsed to {nameof(LearnerVerificationResponseCode)}.", nameof(verifiedLearner.ResponseCode));
            }

            List<FailureFlag> failures = new();
            foreach (var flag in verifiedLearner.FailureFlag)
            {
                if (!Enum.TryParse(flag, true, out FailureFlag parsedFailureFlag))
                {
                    throw new ArgumentException($"Value {flag} could not be parsed to {nameof(FailureFlag)}.", nameof(verifiedLearner.FailureFlag));
                }
                failures.Add(parsedFailureFlag);
            }

            return new LearnerVerificationResponse
            {
                ResponseCode = parsedResponseCode,
                FailureFlags = failures
            };
        }
    }
}