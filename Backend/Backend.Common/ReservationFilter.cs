using System;

namespace Backend.Common
{
    public class ReservationFilter
    {
        public DateTime? Time { get; set; }
        public Guid? CourtId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
