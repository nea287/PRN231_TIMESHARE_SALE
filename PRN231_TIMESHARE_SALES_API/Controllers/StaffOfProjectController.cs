using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;

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
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetStaffOfProjects([FromQuery]StaffOfProjectFilter filter,
                [FromQuery]PagingRequest paging)
        {
            return _service.GetStaffOfProjects(filter, paging);
        }

        [HttpGet("GetStaffOfProject/{accountId}/{projectId}")]
        public ResponseResult<AccountViewModel> GetStaffOfProject(int accountId, int projectId)
        {
            return _service.GetStaffOfProject(accountId, projectId); 
        }
        [HttpPost("CreateStaffOfProject/{accountId}/{projectId}")]
        public ResponseResult<(AccountViewModel, ProjectViewModel)> CreateStaffOfProject(int accountId, int projectId)
        {
            return _service.CreateStaffOfProject(accountId, projectId);
        }
    }
}
