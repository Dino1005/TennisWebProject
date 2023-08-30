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
        public Court Court { get; set; }

        public Reservation(Guid id, DateTime time, Guid courtId)
        {
            Id = id;
            Time = time;
            CourtId = courtId;
        }

        public Reservation(Guid id, DateTime time, Guid courtId, Court court)
        {
            Id = id;
            Time = time;
            CourtId = courtId;
            Court = court;
        }
    }
}
