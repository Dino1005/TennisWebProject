using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Backend.Service.Common;
using System.Threading.Tasks;
using System;

namespace Backend.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<PageList<Reservation>> GetAllAsync(Sorting sorting, Paging paging, ReservationFilter reservationFilter)
        {
            return await reservationRepository.GetAllAsync(sorting, paging, reservationFilter);
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            return await reservationRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Reservation reservation)
        {
            return await reservationRepository.CreateAsync(reservation);
        }

        public async Task<int> UpdateAsync(Reservation reservation)
        {
            return await reservationRepository.UpdateAsync(reservation);
        }

        public async Task<int> ToggleActivateAsync(Guid id)
        {
            return await reservationRepository.ToggleActivateAsync(id);
        }
    }
}
