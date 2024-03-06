using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTimeController : ControllerBase
    {
        private IAvailableTimeService _service;

        public AvailableTimeController(IAvailableTimeService service)
        {
            _service = service;
        }

        [HttpGet("GetAvailableTime/{id}")]
        public IActionResult GetAvailableTime(int id)
        {
            return Ok(_service.GetAvailableTime(id));
        }

        [HttpGet("GetListAvailableTime")]
        public IActionResult GetListAvailableTime([FromQuery] AvailableTimeViewModel filter, [FromQuery] PagingRequest paging)
        {
            return Ok(_service.GetAvailableTimes(paging, filter));
        }

        [HttpPost("CreateAvailableTime")]
        public IActionResult CreateAvailableTime([FromBody] AvailableTimeRequestModel request)
        {
            return Ok(_service.CreateAvailableTime(request));
        }

        [HttpPut("UpdateAvailableTime/{id}")]
        public IActionResult UpdateResevation([FromBody] AvailableTimeRequestModel request, int id)
        {
            return Ok(_service.UpdateAvailableTime(request, id));
        }

        [HttpDelete("DeleteAvailableTime/{id}")]
        public IActionResult DeleleAvailableTime(int id)
        {
            return Ok(_service.DeleteAvailableTime(id));
        }
    }
}
