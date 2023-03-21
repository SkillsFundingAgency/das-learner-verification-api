﻿using SFA.DAS.LearnerVerification.Infrastructure.Queries;

namespace SFA.DAS.LearnerVerification.Queries.VerifyLearner
{
    public class VerifyLearnerQuery : IQuery
    {
        public VerifyLearnerQuery(string uln, string firstName, string lastName, string? gender, DateTime? dateOfBirth)
        {
            Uln = uln;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Uln { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}