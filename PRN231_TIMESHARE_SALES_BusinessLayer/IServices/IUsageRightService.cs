using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IUsageRightService
    {
        public ResponseResult<UsageRightViewModel> GetUsageRight(int id);
        public DynamicModelResponse.DynamicModelsResponse<UsageRightViewModel> GetUsageRights(PagingRequest paging, UsageRightViewModel filter);
        public ResponseResult<UsageRightViewModel> CreateUsageRight(UsageRightRequestModel request);
        public ResponseResult<UsageRightViewModel> UpdateUsageRight(UsageRightRequestModel request, int id);
        public ResponseResult<UsageRightViewModel> DeleteUsageRight(int id);
        public EnumViewModel GetUsageRightStatus();
    }
}
