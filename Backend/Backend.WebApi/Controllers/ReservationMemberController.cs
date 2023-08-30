using Backend.Common;
using Backend.Model;
using Backend.Service.Common;
using Backend.WebApi.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Backend.WebApi.Models.ReservationMember;

namespace Backend.WebApi.Controllers
{
    public class ReservationMemberController : ApiController
    {
        private readonly IReservationMemberService reservationMemberService;
        public ReservationMemberController(IReservationMemberService reservationMemberService)
        {
            this.reservationMemberService = reservationMemberService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] ReservationMemberFilter reservationMemberFilter)
        {
            try
            {
                PageList<ReservationMember> reservationMembers = await reservationMemberService.GetAllAsync(sorting, paging, reservationMemberFilter);
                return Request.CreateResponse(HttpStatusCode.OK, reservationMembers);
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
                ReservationMember reservationMember = await reservationMemberService.GetByIdAsync(id);
                if (reservationMember != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, reservationMember);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "ReservationMember with that id does not exist");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(ReservationMemberToCreateAndUpdate reservationMember)
        {
            try
            {
                ReservationMember reservationMemberToCreate = new ReservationMember(Guid.NewGuid(), (Guid)reservationMember.ReservationId, (Guid)reservationMember.MemberId);
                int affectedRows = await reservationMemberService.CreateAsync(reservationMemberToCreate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "ReservationMember added succesfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ReservationMember was not added");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, ReservationMemberToCreateAndUpdate reservationMember)
        {
            try
            {
                ReservationMember reservationMemberById = await reservationMemberService.GetByIdAsync(id);
                if (reservationMemberById == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ReservationMember with that id was not found");
                }

                Guid reservationId = reservationMember.ReservationId ?? reservationMemberById.ReservationId;
                Guid memberId = reservationMember.MemberId ?? reservationMemberById.MemberId;
                ReservationMember reservationMemberToUpdate = new ReservationMember(id, reservationId, memberId);

                int affectedRows = await reservationMemberService.UpdateAsync(reservationMemberById);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"ReservationMember was updated");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ReservationMember was not updated");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/reservationMember/toggle/{id}")]
        public async Task<HttpResponseMessage> ToggleActivateAsync(Guid id)
        {
            try
            {
                int affectedRows = await reservationMemberService.ToggleActivateAsync(id);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "ReservationMember status changed");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ReservationMember status was not changed");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}