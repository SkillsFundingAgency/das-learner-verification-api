using SFA.DAS.LearnerVerification.Infrastructure.Queries;

namespace SFA.DAS.LearnerVerification.Queries.VerifyLearner
{
    public class VerifyLearnerQuery : IQuery
    {
        public VerifyLearnerQuery(string uln, string firstName, string lastName)
        {
            Uln = uln;
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Uln { get; set; }
    }
}