using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi.Models.Member
{
    public class MemberToCreateAndUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        public MemberToCreateAndUpdate(string firstName, string lastName, DateTime? doB)
        {
            FirstName = firstName;
            LastName = lastName;
            DoB = doB;
        }
    }
}