using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Model;
using Backend.Service.Common;
using Backend.Common;
using Backend.WebApi.Models.Court;

namespace Backend.WebApi.Controllers
{
    public class CourtController : ApiController
    {
        private readonly ICourtService courtService;
        public CourtController(ICourtService courtService)
        {
            this.courtService = courtService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAsync([FromUri] Sorting sorting, [FromUri] Paging paging, [FromUri] CourtFilter courtFilter)
        {
            try
            {
                PageList<Court> courts = await courtService.GetAllAsync(sorting, paging, courtFilter);
                return Request.CreateResponse(HttpStatusCode.OK, courts);
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
                Court court = await courtService.GetByIdAsync(id);
                if (court != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, court);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Court with that id does not exist");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(CourtToCreateAndUpdate court)
        {
            try
            {
                Court courtToCreate = new Court(Guid.NewGuid(), court.Name);
                int affectedRows = await courtService.CreateAsync(courtToCreate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Court {court.Name} added succesfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Court was not added");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, CourtToCreateAndUpdate court)
        {
            try
            {
                Court courtById = await courtService.GetByIdAsync(id);
                if (courtById == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Court with that id was not found");
                }

                string name = court.Name ?? courtById.Name;
                Court courtToUpdate = new Court(id, name);

                int affectedRows = await courtService.UpdateAsync(courtToUpdate);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Court was updated to {name}");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Court was not updated");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/court/toggle/{id}")]
        public async Task<HttpResponseMessage> ToggleActivateAsync(Guid id)
        {
            try
            {
                int affectedRows = await courtService.ToggleActivateAsync(id);
                if (affectedRows > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Court status changed");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Court status was not changed");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}