using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using PRN231_TIMESHARE_SALES_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDistributedCache _cache;
        private readonly ITokenService _token;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, ITokenService token, IDistributedCache cache)
        {
            _token = token;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _cache = cache; 
        }
        #region Create
        public ResponseResult<AccountViewModel> CreateAccount(AccountRequestModel request)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                lock (_accountRepository)
                {
                    if (_accountRepository.Any(x =>
                    x.Email.ToLower().Equals(request.Email.ToLower())
                    && x.Status != 0))
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Account>(request);
                    _accountRepository.Insert(data);
                    _accountRepository.SaveChages();

                    result = _mapper.Map<AccountViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delete
        public ResponseResult<AccountViewModel> DeleteAccountByEmail(string email)
        {
            try
            {
                lock (_accountRepository)
                {
                    var data = _accountRepository.FistOrDefault(
                        x => x.Email.ToLower().Equals(email.ToLower()) && x.Status != 0);

                    if (data == null)
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    data.Status = 0;
                    _accountRepository.UpdateById(data, data.AccountId);
                    _accountRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<AccountViewModel> DeleteAccountById(int id)
        {
            try
            {
                lock (_accountRepository)
                {
                    var data = _accountRepository.FistOrDefault(x => x.AccountId == id && x.Status != 0);
                    if (data == null)
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    data.Status = 0;
                    _accountRepository.UpdateById(data, id);
                    _accountRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        #endregion

        #region Read
        public ResponseResult<AccountViewModel> GetAccountById(int id)
        {
            ResponseResult<AccountViewModel> result = new ResponseResult<AccountViewModel>();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<AccountViewModel>(_accountRepository
                        .FistOrDefault(x => x.AccountId == id && x.Status != 0));

                    result = data == null ?
                        new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(AccountViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<AccountViewModel>) result;
            try
            {
                lock (_accountRepository)
                {
                    result = _accountRepository.GetAll(x => x.Status != 0)
                        .AsQueryable()
                        //.Include(x => x.StaffOfProjects)
                        //.Include(x => x.Feedbacks)
                        //.Include(x => x.ContractCustomers)
                        //.Include(x => x.UsageHistories)
                        //.Include(x => x.ContractStaffs)
                        //.Include(x => x.Reservations)
                        .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AccountViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }

        #endregion

        #region Update
        public ResponseResult<AccountViewModel> UpdateAccountByEmail(string email, AccountRequestModel request)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                lock (_accountRepository)
                {
                    var data = _accountRepository.GetFirstOrDefault(x =>
                        x.Email.ToLower().Equals(email.ToLower()) && x.Status != 0);

                    int id = data.AccountId;
                    data = _mapper.Map<Account>(request);

                    if (data == null)
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<AccountViewModel>(data)
                        };
                    }

                    data.AccountId = id;
                    _accountRepository.UpdateById(data, id);
                    _accountRepository.SaveChages();

                    result = _mapper.Map<AccountViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public ResponseResult<AccountViewModel> UpdateAccountById(int id, AccountRequestModel request)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<Account>(request);

                    if (_accountRepository.Any(x => x.AccountId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<AccountViewModel>(data)
                        };
                    }

                    data.AccountId = id;
                    _accountRepository.UpdateById(data, id);
                    _accountRepository.SaveChages();

                    result = _mapper.Map<AccountViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Authenticate
        public UserLoginResponse Login(string email, string password)
        {
            UserLoginResponse result = new UserLoginResponse();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<AccountViewModel>(_accountRepository
                        .FistOrDefault(x => x.Email.ToLower().Equals(email.ToLower())
                            && x.Password.Equals(x.Password)
                            && x.Status != 0));

                    if (data != null)
                    {
                        string roleName = Enum.GetName(typeof(AccountRole), data.Role);

                        var dataToken = _token.GenerateAccessToken(email, roleName);
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            Result = true,
                            AccessToken = dataToken.accessToken,
                            RefreshToken = dataToken.refreshToken
                        };
                    }
                    else
                    {
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.NOT_FOUND,
                            Result = false
                        };

                    }

                }
            }
            catch (Exception ex)
            {
                result = new UserLoginResponse()
                {
                    Message = Constraints.LOAD_FAILED,
                    Result = false
                };
            }

            return result;
        }

        public bool SendVerificationCode(string receiverMail)
        {
            try
            {
                string code = SupportingFeature.Instance.GenerateCode();
                SupportingFeature.Instance.SendEmail(receiverMail, code, "Mã xác thực");

                SetCache(code, "verification-code", 5);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public ResponseResult<AccountViewModel> Register(AccountRequestModel request, string verificationCode)
        {
            AccountViewModel result = new AccountViewModel();

            if (verificationCode != GetCache("verification-code"))
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.INVALID_VERIFICATION_CODE,
                    result = false,
                };
            }

            try
            {
                lock (_accountRepository)
                {
                    if (_accountRepository.Any(x =>
                    x.Email.ToLower().Equals(request.Email.ToLower())
                    && x.Status != 0))
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Account>(request);
                    _accountRepository.Insert(data);
                    _accountRepository.SaveChages();

                    result = _mapper.Map<AccountViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Redis

        public void SetCache(string value, string nameValue, int minutes)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(minutes))
            .SetSlidingExpiration(TimeSpan.FromMinutes(minutes));

            var dataToCache = Encoding.UTF8.GetBytes(value);
            _cache.Set(nameValue, dataToCache, options);
        }

        public string? GetCache(string nameValue)
        {
            var data = _cache.Get(nameValue);
            
            return data != null? Encoding.UTF8.GetString(data) : null;
        }

        #endregion
    }
}
