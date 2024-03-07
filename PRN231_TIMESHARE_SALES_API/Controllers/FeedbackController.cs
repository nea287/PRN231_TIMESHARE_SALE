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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("GetFeedback/{id}")]
        public ResponseResult<FeedbackViewModel> GetFeedback(int id)
        {
            return _feedbackService.GetFeedback(id);
        }

        [HttpGet("GetListFeedback")]
        public DynamicModelResponse.DynamicModelsResponse<FeedbackViewModel> GetListFeedback(
            [FromQuery] FeedbackViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _feedbackService.GetFeedbacks(filter, paging);
        }

        [HttpPost("CreateFeedback")]
        public ResponseResult<FeedbackViewModel> CreateFeedback([FromBody] FeedbackRequestModel request)
        {
            return _feedbackService.CreateFeedback(request);
        }

        [HttpPut("UpdateFeedback/{id}")]
        public ResponseResult<FeedbackViewModel> UpdateFeedback(
            [FromBody] FeedbackRequestModel request, int id)
        {
            return _feedbackService.UpdateFeedback(request, id);
        }

        [HttpDelete("DeleteFeedback/{id}")]
        public ResponseResult<FeedbackViewModel> DeleteFeedback(int id)
        {
            return _feedbackService.DeleteFeedback(id);
        }
    }
}
