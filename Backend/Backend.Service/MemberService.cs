using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Backend.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;
        public MemberService(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public async Task<PageList<Member>> GetAllAsync(Sorting sorting, Paging paging, MemberFilter memberFilter)
        {
            return await memberRepository.GetAllAsync(sorting, paging, memberFilter);
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            return await memberRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Member member)
        {
            return await memberRepository.CreateAsync(member);
        }

        public async Task<int> UpdateAsync(Member member)
        {
            return await memberRepository.UpdateAsync(member);
        }

        public async Task<int> ToggleActivateAsync(Guid id)
        {
            return await memberRepository.ToggleActivateAsync(id);
        }
    }
}
