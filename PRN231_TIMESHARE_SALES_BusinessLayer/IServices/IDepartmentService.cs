using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
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
    public interface IDepartmentService
    {
        ResponseResult<DepartmentViewModel> CreateDepartment(DepartmentRequestModel request);
        ResponseResult<DepartmentViewModel> DeleteDepartment(int id);
        ResponseResult<DepartmentViewModel> GetDepartment(int id);
        DynamicModelResponse.DynamicModelsResponse<DepartmentViewModel> GetDepartments(DepartmentViewModel filter, PagingRequest paging, DepartmentFilter orderFilter);
        ResponseResult<DepartmentViewModel> UpdateDepartment(DepartmentRequestModel request, int id);
    }
}
