using System;

namespace Backend.WebApi.Models.ReservationMember
{
    public class ReservationMemberToCreateAndUpdate
    {
        public Guid? ReservationId { get; set; }
        public Guid? MemberId { get; set; }

        public ReservationMemberToCreateAndUpdate(Guid? reservationId, Guid? memberId)
        {
            ReservationId = reservationId;
            MemberId = memberId;
        }
    }
}