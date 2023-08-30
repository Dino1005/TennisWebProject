using Backend.Common;
using Backend.Model;
using System;
using System.Threading.Tasks;

namespace Backend.Service.Common
{
    public interface IReservationMemberService
    {
        Task<PageList<ReservationMember>> GetAllAsync(Sorting sorting, Paging paging, ReservationMemberFilter reservationMemberFilter);
        Task<ReservationMember> GetByIdAsync(Guid id);
        Task<int> CreateAsync(ReservationMember reservationMember);
        Task<int> UpdateAsync(ReservationMember reservationMember);
        Task<int> ToggleActivateAsync(Guid id);
    }
}
