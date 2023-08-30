using System;

namespace Backend.Common
{
    public class ReservationMemberFilter
    {
        public Guid? ReservationId { get; set; }
        public Guid? MemberId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
