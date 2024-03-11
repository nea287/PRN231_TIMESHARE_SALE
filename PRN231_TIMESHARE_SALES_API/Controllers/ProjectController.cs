using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "RequiredAdminOrStaff")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetProject/{id}")]
        public ResponseResult<ProjectViewModel> GetProject(int id)
        {
            return _projectService.GetProject(id);
        }

        [HttpGet("GetListProject")]
        public DynamicModelResponse.DynamicModelsResponse<ProjectViewModel> GetListProject(
            [FromQuery] ProjectViewModel filter,[FromQuery] PagingRequest paging)
        {
            return _projectService.GetProjects(filter, paging);
        }

        [HttpPost("CreateProject")]
        public ResponseResult<ProjectViewModel> CreateProject([FromBody] ProjectRequestModel request)
        {
            return _projectService.CreateProject(request);
        }

        [HttpPut("UpdateProject/{id}")]
        public ResponseResult<ProjectViewModel> UpdateProject(
            [FromBody] ProjectRequestModel request, int id)
        {
            return _projectService.UpdateProject(request, id);
        }

        [HttpDelete("DeleteProject/{id}")]
        public ResponseResult<ProjectViewModel> DeleteProject(int id)
        {
            return _projectService.DeleteProject(id);
        }
    }
}
