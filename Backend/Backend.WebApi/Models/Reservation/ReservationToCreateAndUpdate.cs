using System;

namespace Backend.WebApi.Models.Reservation
{
    public class ReservationToCreateAndUpdate
    {
        public DateTime? Time { get; set; }
        public Guid? CourtId { get; set; }
        public Guid? MemberId { get; set; }

        public ReservationToCreateAndUpdate(DateTime? time, Guid? courtId, Guid? memberId)
        {
            Time = time;
            CourtId = courtId;
            MemberId = memberId;
        }
    }
}