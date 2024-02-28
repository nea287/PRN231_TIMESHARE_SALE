using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IFeedbackService
    {
        ResponseResult<FeedbackViewModel> CreateFeedback(FeedbackRequestModel request);
        ResponseResult<FeedbackViewModel> DeleteFeedback(int id);
        ResponseResult<FeedbackViewModel> GetFeedback(int id);
        DynamicModelResponse.DynamicModelsResponse<FeedbackViewModel> GetFeedbacks(FeedbackViewModel filter, PagingRequest paging);
        ResponseResult<FeedbackViewModel> UpdateFeedback(FeedbackRequestModel request, int id);
    }
}
