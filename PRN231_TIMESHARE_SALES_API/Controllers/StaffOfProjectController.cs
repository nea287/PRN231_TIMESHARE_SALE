using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffOfProjectController : ControllerBase
    {
        private readonly IStaffOfProjectService _service;

        public StaffOfProjectController(IStaffOfProjectService service)
        {
            _service = service;
        }
        [HttpGet("GetStaffOfProjects")]
        public DynamicModelResponse.DynamicModelsResponse<StaffOfProjectsViewModel> GetStaffOfProjects([FromQuery] StaffOfProjectsViewModel filter,
                [FromQuery]PagingRequest paging)
        {
            return _service.GetStaffOfProjects(filter, paging);
        }

        [HttpGet("GetStaffOfProject")]
        public ResponseResult<StaffOfProjectsViewModel> GetStaffOfProject([FromQuery] StaffOfProjectRequestModel request)
        {
            return _service.GetStaffOfProject(request); 
        }
        [HttpPost("CreateStaffOfProject")]
        public ResponseResult<StaffOfProjectsViewModel> CreateStaffOfProject([FromBody] StaffOfProjectRequestModel request)
        {
            return _service.CreateStaffOfProject(request);
        }

        [HttpDelete("DeleteStaffOfProject")]
        public ResponseResult<StaffOfProjectsViewModel> DeleteStaffOfProject([FromQuery] StaffOfProjectRequestModel request)
        {
            return _service.DeleteStaffOfProject(request);
        }

        [HttpPut("UpdateStaffOfProject/{staffId}/{projectId}")]
        public ResponseResult<StaffOfProjectsViewModel> UpdateStaffOfProject(int staffId, int projectId, [FromBody] StaffOfProjectRequestModel request)
        {
            return _service.UpdateStaffOfProject(staffId, projectId, request);
        }
    }
}
