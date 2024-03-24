using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Authorize(Policy = "RequiredAdminOrStaff")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("GetDashboardByMonth")]
        public IActionResult GetDashboardByMonth([FromQuery] MonthRequestModel request)
        {
            return Ok(_dashboardService.GetDashboardByMonth(request).Result);
        }

        [HttpGet("GetDashboardByDate")]
        public IActionResult GetDashboardByDate([FromQuery] DateRequestModel request)
        {
            return Ok(_dashboardService.GetDashboardByDate(request).Result);
        }

        [HttpGet("GetDashboardByYear")]
        public Task<DashboardResponse<int>> GetDashboardByYear([FromQuery] YearRequestModel request)
        {
            return _dashboardService.GetDashboardByYear(request);
        }
        [HttpGet("GetRevenueContructionTypeName")]
        public DashboardResponse<string> GetRevenueContructionTypeName([FromQuery]DepartmentConstructionType? request) 
            => _dashboardService.GetRevenueContructionTypeName(request);

        [HttpGet("GetRevenueProjectName")]
        public DashboardResponse<string> GetRevenueProjectName([FromQuery]string? projectName = null) => _dashboardService.GetRevenueProjectName(projectName);
    }

}
