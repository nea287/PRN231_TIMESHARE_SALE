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
    public interface IAvailableTimeService
    {
        public ResponseResult<AvailableTimeViewModel> GetAvailableTime(int id);
        public DynamicModelResponse.DynamicModelsResponse<AvailableTimeViewModel> GetAvailableTimes(PagingRequest paging, AvailableTimeViewModel filter);
        public ResponseResult<AvailableTimeViewModel> CreateAvailableTime(AvailableTimeRequestModel request);
        public ResponseResult<AvailableTimeViewModel> UpdateAvailableTime(AvailableTimeRequestModel request, int id);
        public ResponseResult<AvailableTimeViewModel> DeleteAvailableTime(int id);
    }
}
