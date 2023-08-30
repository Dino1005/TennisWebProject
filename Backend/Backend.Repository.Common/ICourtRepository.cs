using Backend.Common;
using Backend.Model;
using System;
using System.Threading.Tasks;

namespace Backend.Repository.Common
{
    public interface ICourtRepository
    {
        Task<PageList<Court>> GetAllAsync(Sorting sorting, Paging paging, CourtFilter courtFilter);
        Task<Court> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Court court);
        Task<int> UpdateAsync(Court court);
        Task<int> ToggleActivateAsync(Guid id);
    }
}
