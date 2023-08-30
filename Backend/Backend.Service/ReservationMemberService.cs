using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Backend.Service.Common;
using System.Threading.Tasks;
using System;

namespace Backend.Service
{
    public class ReservationMemberService : IReservationMemberService
    {
        private readonly IReservationMemberRepository reservationMemberRepository;
        public ReservationMemberService(IReservationMemberRepository reservationMemberRepository)
        {
            this.reservationMemberRepository = reservationMemberRepository;
        }

        public async Task<PageList<ReservationMember>> GetAllAsync(Sorting sorting, Paging paging, ReservationMemberFilter reservationMemberFilter)
        {
            return await reservationMemberRepository.GetAllAsync(sorting, paging, reservationMemberFilter);
        }

        public async Task<ReservationMember> GetByIdAsync(Guid id)
        {
            return await reservationMemberRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(ReservationMember reservationMember)
        {
            return await reservationMemberRepository.CreateAsync(reservationMember);
        }

        public async Task<int> UpdateAsync(ReservationMember reservationMember)
        {
            return await reservationMemberRepository.UpdateAsync(reservationMember);
        }

        public async Task<int> ToggleActivateAsync(Guid id)
        {
            return await reservationMemberRepository.ToggleActivateAsync(id);
        }
    }
}
