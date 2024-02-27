using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;

namespace PRN231_TIMESHARE_SALES_API.Controllers
{
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
    }
}
