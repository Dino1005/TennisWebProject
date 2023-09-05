using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Guid CourtId { get; set; }
        public Guid MemberId { get; set; }
        public Court Court { get; set; }
        public Member Member { get; set; }

        public Reservation(Guid id, DateTime time, Guid courtId, Guid memberId)
        {
            Id = id;
            Time = time;
            CourtId = courtId;
            MemberId = memberId;
        }

        public Reservation(Guid id, DateTime time, Guid courtId, Guid memberId, Court court, Member member)
        {
            Id = id;
            Time = time;
            CourtId = courtId;
            MemberId = memberId;
            Court = court;
            Member = member;
        }
    }
}
