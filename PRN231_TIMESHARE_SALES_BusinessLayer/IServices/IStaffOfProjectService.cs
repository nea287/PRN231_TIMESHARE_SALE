using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IStaffOfProjectService
    {

        public DynamicModelResponse.DynamicModelsResponse<StaffOfProjectsViewModel> GetStaffOfProjects(StaffOfProjectsViewModel filter, PagingRequest paging);
        public ResponseResult<StaffOfProjectsViewModel> CreateStaffOfProject(StaffOfProjectRequestModel request);
        public ResponseResult<StaffOfProjectsViewModel> GetStaffOfProject(StaffOfProjectRequestModel request);
        public ResponseResult<StaffOfProjectsViewModel> UpdateStaffOfProject(int staffId, int projectId, StaffOfProjectRequestModel request);
        public ResponseResult<StaffOfProjectsViewModel> DeleteStaffOfProject(StaffOfProjectRequestModel request);
       
    }
}
