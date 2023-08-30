using Backend.Common;
using Backend.Model;
using Backend.Service.Common;
using Backend.WebApi.Models.Member;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.WebApi.Controllers
{
    public class MemberController : ApiController
    {
        private readonly IMemberService memberService;
        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] MemberFilter memberFilter)
        {
            try
            {
                PageList<Member> members = await memberService.GetAllAsync(sorting, paging, memberFilter);
                return Request.CreateResponse(HttpStatusCode.OK, members);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            try
            {
                Member member = await memberService.GetByIdAsync(id);
                if (member != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, member);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Member with that id does not exist");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(MemberToCreateAndUpdate member)
        {
            try
            {
                Member memberToCreate = new Member(Guid.NewGuid(), member.FirstName, member.LastName, (DateTime)member.DoB);
                int affectedRows = await memberService.CreateAsync(memberToCreate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Member added succesfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Member was not added");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, MemberToCreateAndUpdate member)
        {
            try
            {
                Member memberById = await memberService.GetByIdAsync(id);
                if (memberById == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Member with that id was not found");
                }

                string firstName = member.FirstName ?? memberById.FirstName;
                string lastName = member.LastName ?? memberById.LastName;
                DateTime doB = member.DoB ?? memberById.DoB;
                Member memberToUpdate = new Member(id, firstName, lastName, doB);

                int affectedRows = await memberService.UpdateAsync(memberToUpdate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Member was updated");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Member was not updated");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/member/toggle/{id}")]
        public async Task<HttpResponseMessage> ToggleActivateAsync(Guid id)
        {
            try
            {
                int affectedRows = await memberService.ToggleActivateAsync(id);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Member status changed");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Member status was not changed");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}