using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IStaffOfProjectService
    {

        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetStaffOfProjects(StaffOfProjectFilter filter, PagingRequest paging);
        public ResponseResult<(AccountViewModel, ProjectViewModel)> CreateStaffOfProject(int accId, int proId);
        public ResponseResult<AccountViewModel> GetStaffOfProject(int accId, int proId);
    }
}
