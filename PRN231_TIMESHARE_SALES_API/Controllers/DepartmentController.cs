﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartment/{id}")]
        public ResponseResult<DepartmentViewModel> GetDepartment(int id)
        {
            return _departmentService.GetDepartment(id);
        }

        [HttpGet("GetListDepartment")]
        public DynamicModelResponse.DynamicModelsResponse<DepartmentViewModel> GetListDepartment(
            [FromQuery] DepartmentViewModel filter, [FromQuery] PagingRequest paging)
        {
            return _departmentService.GetDepartments(filter, paging);
        }

        [HttpPost("CreateDepartment")]
        public ResponseResult<DepartmentViewModel> CreateDepartment([FromBody] DepartmentRequestModel request)
        {
            return _departmentService.CreateDepartment(request);
        }

        [HttpPut("UpdateDepartment/{id}")]
        public ResponseResult<DepartmentViewModel> UpdateDepartment(
            [FromBody] DepartmentRequestModel request, int id)
        {
            return _departmentService.UpdateDepartment(request, id);
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public ResponseResult<DepartmentViewModel> DeleteDepartment(int id)
        {
            return _departmentService.DeleteDepartment(id);
        }
    }
}