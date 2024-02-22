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
    public interface IUsageHistoryService
    {
        public ResponseResult<UsageHistoryViewModel> GetUsageHistoryById(int id);
        public DynamicModelResponse.DynamicModelsResponse<UsageHistoryViewModel> GetUsageHistories(UsageHistoryViewModel filter, PagingRequest paging);
        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistoryById(UsageHistoryRequestModel request, int id);
        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistory(UsageHistoryRequestModel request, int customerId, int depId);
        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistoryById(int id);
        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistory(int customerId, int depId);
        public ResponseResult<UsageHistoryViewModel> CreateUsageHistory(UsageHistoryRequestModel request);

    }
}
