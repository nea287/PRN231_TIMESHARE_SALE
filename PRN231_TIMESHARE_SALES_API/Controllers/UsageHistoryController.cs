using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsageHistoryController : ControllerBase
    {
        private readonly IUsageHistoryService _usageService;

        public UsageHistoryController(IUsageHistoryService usageService)
        {
            _usageService = usageService;
        }

        [HttpGet("GetUsageHistoryById/{id}")]
        public ResponseResult<UsageHistoryViewModel> GetUsageHistoryById(int id)
        {
            return _usageService.GetUsageHistoryById(id);
        }

        [HttpGet("GetListUsageHistory")]
        public DynamicModelResponse.DynamicModelsResponse<UsageHistoryViewModel> GetListUsageHistory(
            [FromQuery] UsageHistoryViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _usageService.GetUsageHistories(filter, paging);
        }

        [HttpPost("CreateUsageHistory")]
        public ResponseResult<UsageHistoryViewModel> CreateUsageHistory([FromBody] UsageHistoryRequestModel request)
        {
            return _usageService.CreateUsageHistory(request);
        }

        [HttpPut("UpdateUsageHistoryById/{id}")]
        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistoryById(
            [FromBody] UsageHistoryRequestModel request, int id)
        {
            return _usageService.UpdateUsageHistoryById(request, id);
        }

        [HttpPut("UpdateUsageHistory/{cusId}/{depId}")]
        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistory(
            [FromBody] UsageHistoryRequestModel request, int cusId, int depId)
        {
            return _usageService.UpdateUsageHistory(request, cusId, depId);
        }   

        [HttpDelete("DeleteUsageHistoryById/{id}")]
        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistoryById(int id)
        {
            return _usageService.DeleteUsageHistoryById(id);
        }
        
        [HttpDelete("DeleteUsageHistory/{cusId}/{depId}")]
        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistory(int cusId, int depId)
        {
            return _usageService.DeleteUsageHistory(cusId, depId);
        }
    }
}