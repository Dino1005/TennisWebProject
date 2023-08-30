using Backend.Common;
using Backend.Model;
using System;
using System.Threading.Tasks;

namespace Backend.Service.Common
{
    public interface IReservationService
    {
        Task<PageList<Reservation>> GetAllAsync(Sorting sorting, Paging paging, ReservationFilter reservationFilter);
        Task<Reservation> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Reservation reservation);
        Task<int> UpdateAsync(Reservation reservation);
        Task<int> ToggleActivateAsync(Guid id);
    }
}
