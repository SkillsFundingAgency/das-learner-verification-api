namespace SFA.DAS.LearnerVerification.Types
{
    public enum LearnerDetailMatchingError
    {
        GivenDoesntMatchGiven,

        GivenDoesntMatchFamily,

        GivenDoesntMatchPreviousFamily,

        FamilyDoesntMatchGiven,

        FamilyDoesntMatchFamily,

        FamilyDoesntMatchPreviousFamily,

        DateOfBirthDoesntMatchDateOfBirth,

        GenderDoesntMatchGender
    }
}