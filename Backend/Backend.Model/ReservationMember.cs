using System;

namespace Backend.Model
{
    public class ReservationMember
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public Guid MemberId { get; set; }
        public Reservation Reservation { get; set; }
        public Member Member { get; set; }

        public ReservationMember(Guid id, Guid reservationId, Guid memberId)
        {
            Id = id;
            ReservationId = reservationId;
            MemberId = memberId;
        }

        public ReservationMember(Guid id, Guid reservationId, Guid memberId, Reservation reservation, Member member)
        {
            Id = id;
            ReservationId = reservationId;
            MemberId = memberId;
            Reservation = reservation;
            Member = member;
        }
    }
}
