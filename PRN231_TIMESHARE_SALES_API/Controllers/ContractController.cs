using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequiredAdminOrStaff")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private IContractService _service;

        public ContractController(IContractService service)
        {
            _service = service;
        }

        [HttpGet("GetContract/{id}")]
        public IActionResult GetContract(int id)
        {
            return Ok(_service.GetContract(id));
        }

        [HttpGet("GetListContract")]
        public IActionResult GetListContract([FromQuery] ContractViewModel filter, [FromQuery] PagingRequest paging)
        {
            return Ok(_service.GetContracts(paging, filter));
        }

        [HttpPost("CreateContract")]
        public IActionResult CreateContract([FromBody] ContractRequestModel request)
        {
            return Ok(_service.CreateContract(request));
        }

        [HttpPut("UpdateContract/{id}")]
        public IActionResult UpdateResevation([FromBody] ContractRequestModel request, int id)
        {
            return Ok(_service.UpdateContract(request, id));
        }

        [HttpDelete("DeleteContract/{id}")]
        public IActionResult DeleleContract(int id)
        {
            return Ok(_service.DeleteContract(id));
        }

        [HttpGet("GetConstractType")]
        public EnumViewModel GetConstractType() => _service.GetConstractType();
    }
}
