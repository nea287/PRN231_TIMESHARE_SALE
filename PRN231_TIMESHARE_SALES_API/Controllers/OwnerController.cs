using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("GetOwner/{id}")]
        public ResponseResult<OwnerViewModel> GetOwner(int id)
        {
            return _ownerService.GetOwner(id);
        }

        [HttpGet("GetListOwner")]
        public DynamicModelResponse.DynamicModelsResponse<OwnerViewModel> GetListOwner(
            [FromQuery] OwnerViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _ownerService.GetOwners(filter, paging);
        }

        [HttpPost("CreateOwner")]
        public ResponseResult<OwnerViewModel> CreateOwner([FromBody] OwnerRequestModel request)
        {
            return _ownerService.CreateOwner(request);
        }

        [HttpPut("UpdateOwner/{id}")]
        public ResponseResult<OwnerViewModel> UpdateOwner(
            [FromBody] OwnerRequestModel request, int id)
        {
            return _ownerService.UpdateOwner(request, id);
        }

        [HttpDelete("DeleteOwner/{id}")]
        public ResponseResult<OwnerViewModel> DeleteOwner(int id)
        {
            return _ownerService.DeleteOwner(id);
        }
    }
}
