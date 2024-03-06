using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using Microsoft.AspNetCore.Cors;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsageRightController : ControllerBase
    {
        private IUsageRightService _service;

        public UsageRightController(IUsageRightService service)
        {
            _service = service;
        }

        [HttpGet("GetUsageRight/{id}")]
        public IActionResult GetUsageRight(int id)
        {
            return Ok(_service.GetUsageRight(id));
        }

        [HttpGet("GetListUsageRight")]
        public IActionResult GetListUsageRight([FromQuery] UsageRightViewModel filter, [FromQuery] PagingRequest paging)
        {
            return Ok(_service.GetUsageRights(paging, filter));
        }

        [HttpPost("CreateUsageRight")]
        public IActionResult CreateUsageRight([FromBody] UsageRightRequestModel request)
        {
            return Ok(_service.CreateUsageRight(request));
        }

        [HttpPut("UpdateUsageRight/{id}")]
        public IActionResult UpdateResevation([FromBody] UsageRightRequestModel request, int id)
        {
            return Ok(_service.UpdateUsageRight(request, id));
        }

        [HttpDelete("DeleteUsageRight/{id}")]
        public IActionResult DeleleUsageRight(int id)
        {
            return Ok(_service.DeleteUsageRight(id));
        }
    }
}
