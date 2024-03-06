using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using Microsoft.AspNetCore.Cors;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthenticateController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login/{email}/{password}")]
        public UserLoginResponse Login(string email, string password)
        {
            return _accountService.Login(email, password);
        }

        [HttpPost("SendVerificationCode/{email}")]
        public bool SendVerificationCode(string email)
        {
            return _accountService.SendVerificationCode(email);
        }

        [HttpPost("Register/{verificationCode}")]
        public ResponseResult<AccountViewModel> Register([FromBody] AccountRequestModel request, string verificationCode)
        {
            return _accountService.Register(request, verificationCode);
        }
    }
}
