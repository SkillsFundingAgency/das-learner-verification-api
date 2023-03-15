using System.ComponentModel;

namespace SFA.DAS.LearnerVerification.Domain
{
    public enum FailureFlag
    {
        [Description("Incoming GivenName does not satisfy the name matching criteria against LRS GivenName")]
        VRF1,

        [Description("Incoming GivenName does not satisfy the name matching criteria against LRS FamilyName")]
        VRF2,

        [Description("Incoming GivenName does not satisfy the name matching criteria against LRS PreviousFamilyName")]
        VRF3,

        [Description("Incoming FamilyName does not satisfy the name matching criteria against LRS GivenName ")]
        VRF4,

        [Description("Incoming FamilyName does not satisfy the name matching criteria against LRS FamilyName")]
        VRF5,

        [Description("Incoming FamilyName does not satisfy the name matching criteria against LRS PreviousFamilyName ")]
        VRF6,

        [Description("Incoming DateOfBirth does not match LRS DateOfBirth ")]
        VRF7,

        [Description("Incoming Gender does not match LRS Gender")]
        VRF8
    }
}