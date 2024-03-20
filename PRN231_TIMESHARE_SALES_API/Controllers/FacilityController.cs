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
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("GetFacility/{id}")]
        public ResponseResult<FacilityViewModel> GetFacility(int id)
        {
            return _facilityService.GetFacility(id);
        }

        [HttpGet("GetListFacility")]
        public DynamicModelResponse.DynamicModelsResponse<FacilityViewModel> GetListFacility(
            [FromQuery] FacilityViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _facilityService.GetFacilitys(filter, paging);
        }

        [HttpPost("CreateFacility")]
        public ResponseResult<FacilityViewModel> CreateFacility([FromBody] FacilityRequestModel request)
        {
            return _facilityService.CreateFacility(request);
        }

        [HttpPut("UpdateFacility/{id}")]
        public ResponseResult<FacilityViewModel> UpdateFacility(
            [FromBody] FacilityRequestModel request, int id)
        {
            return _facilityService.UpdateFacility(request, id);
        }

        [HttpDelete("DeleteFacility/{id}")]
        public ResponseResult<FacilityViewModel> DeleteFacility(int id)
        {
            return _facilityService.DeleteFacility(id);
        }
        [HttpGet("GetFacilityType")]
        public EnumViewModel GetFacilityType() => _facilityService.GetFacilityType();
    }
}
