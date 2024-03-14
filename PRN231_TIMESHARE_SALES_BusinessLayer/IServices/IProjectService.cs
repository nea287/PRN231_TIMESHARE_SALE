using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
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
    public interface IProjectService
    {
        public ResponseResult<ProjectViewModel> CreateProject(ProjectRequestModel request);
        public ResponseResult<ProjectViewModel> UpdateProject(ProjectRequestModel request, int id);
        public ResponseResult<ProjectViewModel> DeleteProject(int id);
        public ResponseResult<ProjectViewModel> GetProject(int id);
        public DynamicModelResponse.DynamicModelsResponse<ProjectViewModel> GetProjects(ProjectViewModel filter, PagingRequest paging, ProjectOrderFilter orderFilter);
    }
}
