using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.Services;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet("GetReservation/{id}")]
        public IActionResult GetReservation(int id)
        {
            return Ok(_service.GetReservation(id)); 
        }

        [HttpGet("GetListReservation")]
        public IActionResult GetListReservation([FromQuery] ReservationViewModel filter, [FromQuery] PagingRequest paging)
        {
            return Ok(_service.GetReservations(paging, filter));
        }

        [HttpPost("CreateReservation")]
        public IActionResult CreateReservation([FromBody] ReservationRequestModel request)
        {
            return Ok(_service.CreateReservation(request));
        }

        [HttpPut("UpdateReservation/{id}")]
        public IActionResult UpdateResevation([FromBody] ReservationRequestModel request, int id)
        {
            return Ok(_service.UpdateReservation(request, id));
        }

        [HttpDelete("DeleteReservation/{id}")]
        public IActionResult DeleleReservation(int id)
        {
            return Ok(_service.DeleteReservation(id));  
        }
    }
}
