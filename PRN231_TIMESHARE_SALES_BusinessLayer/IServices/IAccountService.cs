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
    public interface IAccountService
    {
        public ResponseResult<AccountViewModel> GetAccountById(int id);
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(AccountViewModel filter, PagingRequest paging, AccountOrderFilter orderFilter);
        public ResponseResult<AccountViewModel> UpdateAccountById(int id, AccountRequestModel request);
        public ResponseResult<AccountViewModel> UpdateAccountByEmail(string email, AccountRequestModel request);
        public ResponseResult<AccountViewModel> DeleteAccountById(int id);
        public ResponseResult<AccountViewModel> DeleteAccountByEmail(string email);
        public ResponseResult<AccountViewModel> CreateAccount(AccountRequestModel request);
        public UserLoginResponse Login(string email, string password);
        public bool SendVerificationCode(string receiverMail);
        public ResponseResult<AccountViewModel> Register(AccountRequestModel request, string verificationCode);
    }
}
