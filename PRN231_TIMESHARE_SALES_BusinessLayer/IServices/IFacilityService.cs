using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IFacilityService
    {
        ResponseResult<FacilityViewModel> CreateFacility(FacilityRequestModel request);
        ResponseResult<FacilityViewModel> DeleteFacility(int id);
        ResponseResult<FacilityViewModel> GetFacility(int id);
        DynamicModelResponse.DynamicModelsResponse<FacilityViewModel> GetFacilitys(FacilityViewModel filter, PagingRequest paging);
        ResponseResult<FacilityViewModel> UpdateFacility(FacilityRequestModel request, int id);
        public EnumViewModel GetFacilityType();
    }
}
