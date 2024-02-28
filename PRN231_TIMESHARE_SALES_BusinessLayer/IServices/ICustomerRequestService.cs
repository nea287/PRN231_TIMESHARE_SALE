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
    public interface ICustomerRequestService
    {
        ResponseResult<CustomerRequestViewModel> CreateCustomerRequest(CustomerRequestRequestModel request);
        ResponseResult<CustomerRequestViewModel> DeleteCustomerRequest(int id);
        ResponseResult<CustomerRequestViewModel> GetCustomerRequest(int id);
        DynamicModelResponse.DynamicModelsResponse<CustomerRequestViewModel> GetCustomerRequests(CustomerRequestViewModel filter, PagingRequest paging);
        ResponseResult<CustomerRequestViewModel> UpdateCustomerRequest(CustomerRequestRequestModel request, int id);
    }
}
