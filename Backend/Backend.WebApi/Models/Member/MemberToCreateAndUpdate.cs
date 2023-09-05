using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi.Models.Member
{
    public class MemberToCreateAndUpdate
    {
        public string FirebaseUid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        public MemberToCreateAndUpdate(string firebaseUid, string firstName, string lastName, DateTime? doB)
        {
            FirebaseUid = firebaseUid;
            FirstName = firstName;
            LastName = lastName;
            DoB = doB;
        }
    }
}