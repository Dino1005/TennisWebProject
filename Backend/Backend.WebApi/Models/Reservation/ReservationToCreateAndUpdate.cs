using System;

namespace Backend.WebApi.Models.Reservation
{
    public class ReservationToCreateAndUpdate
    {
        public DateTime? Time { get; set; }
        public Guid? CourtId { get; set; }

        public ReservationToCreateAndUpdate(DateTime time, Guid courtId)
        {
            Time = time;
            CourtId = courtId;
        }
    }
}