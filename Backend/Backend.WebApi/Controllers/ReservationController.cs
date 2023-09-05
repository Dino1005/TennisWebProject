using Backend.Common;
using Backend.Service.Common;
using Backend.WebApi.Models.Member;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Model;
using Backend.WebApi.Models.Reservation;

namespace Backend.WebApi.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] ReservationFilter reservationFilter)
        {
            try
            {
                PageList<Reservation> reservations = await reservationService.GetAllAsync(sorting, paging, reservationFilter);
                return Request.CreateResponse(HttpStatusCode.OK, reservations);
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
                Reservation reservation = await reservationService.GetByIdAsync(id);
                if (reservation != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, reservation);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Reservation with that id does not exist");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(ReservationToCreateAndUpdate reservation)
        {
            try
            {
                Reservation reservationToCreate = new Reservation(Guid.NewGuid(), (DateTime)reservation.Time, (Guid)reservation.CourtId, (Guid)reservation.MemberId);
                int affectedRows = await reservationService.CreateAsync(reservationToCreate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Reservation added succesfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Reservation was not added");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, ReservationToCreateAndUpdate reservation)
        {
            try
            {
                Reservation reservationById = await reservationService.GetByIdAsync(id);
                if (reservationById == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Reservation with that id was not found");
                }

                DateTime time = reservation.Time ?? reservationById.Time;
                Guid courtId = reservation.CourtId ?? reservationById.CourtId;
                Guid memberId = reservation.MemberId ?? reservationById.MemberId;
                Reservation reservationToUpdate = new Reservation(id, time, courtId, memberId);

                int affectedRows = await reservationService.UpdateAsync(reservationById);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Reservation was updated");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Reservation was not updated");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/reservation/toggle/{id}")]
        public async Task<HttpResponseMessage> ToggleActivateAsync(Guid id)
        {
            try
            {
                int affectedRows = await reservationService.ToggleActivateAsync(id);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Reservation status changed");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Reservation status was not changed");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}