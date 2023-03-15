using System.ComponentModel;

namespace SFA.DAS.LearnerVerification.Domain
{
    public enum LearnerVerificationResponseCode
    {
        [Description("Successful Match - This result will occur where each of the incoming parameters matches exactly with those of a Learner held on LRS.")]
        WSVRC001,

        [Description("Successful Linked Match - A match was found on the same basis as described for ‘Successful Match’, but the ULN Register Record identified is that of a Linked Learner.")]
        WSVRC002,

        [Description("Similar Match - This result will occur where the incoming ULN was found but the associated GivenName and/or FamilyName were similar but were not a successful match.")]
        WSVRC003,

        [Description("Similar Linked Match - A match was found on the same basis as described for ‘Similar Match’, but the ULN Register Record identified is that of a Linked Learner.")]
        WSVRC004,

        [Description("Learner Does Not Match - This result will occur where the incoming ULN was found on the LRS Portal but any or all of the associated fields do not successfully match or meet the criteria for a similar match.")]
        WSVRC005,

        [Description("ULN Not Found - This result will occur where the incoming ULN does not exist on LRS. ")]
        WSVRC006
    }
}