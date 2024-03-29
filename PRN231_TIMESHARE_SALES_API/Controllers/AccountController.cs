﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize(Policy = "RequiredAdminOrStaff")]
        [HttpGet("GetAccountById/{id}")]
        public ResponseResult<AccountViewModel> GetAccountById(int id)
        {
            return _accountService.GetAccountById(id);
        }
        [HttpGet("GetListAccount")]
        [AllowAnonymous]
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetListAccount(
            [FromQuery] AccountViewModel filter, [FromQuery] PagingRequest paging,
            [FromQuery] AccountOrderFilter orderFilter = AccountOrderFilter.AccountId)
        {
            return _accountService.GetAccounts(filter, paging, orderFilter);
        }

        [HttpPost("CreateAccount")]
        public ResponseResult<AccountViewModel> CreateAccount([FromBody] AccountRequestModel request)
        {
            return _accountService.CreateAccount(request);
        }

        [HttpPut("UpdateAccountById/{id}")]
        public ResponseResult<AccountViewModel> UpdateAccountById(
            [FromBody] AccountRequestModel request, int id)
        {
            return _accountService.UpdateAccountById(id, request);
        }

        [HttpPut("UpdateAccountByEmail/{email}")]
        public ResponseResult<AccountViewModel> UpdateAccountByEmail(
            [FromBody] AccountRequestModel request, string email)
        {
            return _accountService.UpdateAccountByEmail(email, request);
        }
        [Authorize(Policy = "RequiredAdminOrStaff")]
        [HttpDelete("DeleteAccountById/{id}")]
        public ResponseResult<AccountViewModel> DeleteAccountById(int id)
        {
            return _accountService.DeleteAccountById(id);
        }
        [Authorize(Policy = "RequiredAdminOrStaff")]
        [HttpDelete("DeleteAccountByEmail/{email}")]
        public ResponseResult<AccountViewModel> DeleteAccountByEmail(string email)
        {
            return _accountService.DeleteAccountByEmail(email);
        }

        [HttpGet("GetCustomerRequestType")]
        public EnumViewModel GetCustomerRequestType() => _accountService.GetCustomerRequestType();

        [HttpPut("ActiveAccount/{email}")]
        public bool ActiveAccount(string email)
        {
            return _accountService.ActiveAccount(email);
        }
    }
}
