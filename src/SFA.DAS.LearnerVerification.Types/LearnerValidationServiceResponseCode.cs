using System.ComponentModel;

namespace SFA.DAS.LearnerVerification.Types
{
    public enum LearnerValidationServiceResponseCode
    {
        [Description("WSVRC001")]
        SuccessfulMatch,

        [Description("WSVRC002")]
        SuccessfulLinkedMatch,

        [Description("WSVRC003")]
        SimilarMatch,

        [Description("WSVRC004")]
        SimilarLinkedMatch,

        [Description("WSVRC005")]
        LearnerDoesNotMatch,

        [Description("WSVRC006")]
        UlnNotFound
    }
}