using Backend.Common;
using Backend.Model;
using System;
using System.Threading.Tasks;

namespace Backend.Repository.Common
{
    public interface IMemberRepository
    {
        Task<PageList<Member>> GetAllAsync(Sorting sorting, Paging paging, MemberFilter memberFilter);
        Task<Member> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Member member);
        Task<int> UpdateAsync(Member member);
        Task<int> ToggleActivateAsync(Guid id);
    }
}
