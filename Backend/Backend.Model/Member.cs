using System;

namespace Backend.Model
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public Member(Guid id, string firstName, string lastName, DateTime doB)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DoB = doB;
        }
    }
}
