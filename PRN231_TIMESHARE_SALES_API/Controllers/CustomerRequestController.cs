using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequiredAdminOrStaff")]
    [ApiController]
    public class CustomerRequestController : ControllerBase
    {
        private readonly ICustomerRequestService _customerRequestService;

        public CustomerRequestController(ICustomerRequestService customerRequestService)
        {
            _customerRequestService = customerRequestService;
        }

        [HttpGet("GetCustomerRequest/{id}")]
        public ResponseResult<CustomerRequestViewModel> GetCustomerRequest(int id)
        {
            return _customerRequestService.GetCustomerRequest(id);
        }

        [HttpGet("GetListCustomerRequest")]
        public DynamicModelResponse.DynamicModelsResponse<CustomerRequestViewModel> GetListCustomerRequest(
            [FromQuery] CustomerRequestViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _customerRequestService.GetCustomerRequests(filter, paging);
        }

        [HttpPost("CreateCustomerRequest")]
        public ResponseResult<CustomerRequestViewModel> CreateCustomerRequest([FromBody] CustomerRequestRequestModel request)
        {
            return _customerRequestService.CreateCustomerRequest(request);
        }

        [HttpPut("UpdateCustomerRequest/{id}")]
        public ResponseResult<CustomerRequestViewModel> UpdateCustomerRequest(
            [FromBody] CustomerRequestRequestModel request, int id)
        {
            return _customerRequestService.UpdateCustomerRequest(request, id);
        }

        [HttpDelete("DeleteCustomerRequest/{id}")]
        public ResponseResult<CustomerRequestViewModel> DeleteCustomerRequest(int id)
        {
            return _customerRequestService.DeleteCustomerRequest(id);
        }
    }
}
