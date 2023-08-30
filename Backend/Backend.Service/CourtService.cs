using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Backend.Service.Common;
using System;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository courtRepository;
        public CourtService(ICourtRepository courtRepository)
        {
            this.courtRepository = courtRepository;
        }

        public async Task<PageList<Court>> GetAllAsync(Sorting sorting, Paging paging, CourtFilter courtFilter)
        {
            return await courtRepository.GetAllAsync(sorting, paging, courtFilter);
        }

        public async Task<Court> GetByIdAsync(Guid id)
        {
            return await courtRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Court court)
        {
            return await courtRepository.CreateAsync(court);
        }

        public async Task<int> UpdateAsync(Court court)
        {
            return await courtRepository.UpdateAsync(court);
        }

        public async Task<int> ToggleActivateAsync(Guid id)
        {
            return await courtRepository.ToggleActivateAsync(id);
        }
    }
}
