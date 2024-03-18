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
    public interface IOwnerService
    {
        ResponseResult<OwnerViewModel> CreateOwner(OwnerRequestModel request);
        ResponseResult<OwnerViewModel> DeleteOwner(int id);
        ResponseResult<OwnerViewModel> GetOwner(int id);
        DynamicModelResponse.DynamicModelsResponse<OwnerViewModel> GetOwners(OwnerViewModel filter, PagingRequest paging);
        ResponseResult<OwnerViewModel> UpdateOwner(OwnerRequestModel request, int id);
        public EnumViewModel GetOwnerStatus();
    }
}
