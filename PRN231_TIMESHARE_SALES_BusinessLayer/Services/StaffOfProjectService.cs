using AutoMapper;
using AutoMapper.QueryableExtensions;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class StaffOfProjectService : IStaffOfProjectService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public StaffOfProjectService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        #region Read
        public ResponseResult<AccountViewModel> GetStaffOfProject(int accId, int proId)
        {

            ResponseResult<AccountViewModel> result = new ResponseResult<AccountViewModel>();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<AccountViewModel>(_accountRepository
                        .FistOrDefault(x => x.AccountId == accId && x.Status != 0)
                        .Projects.FirstOrDefault(a => a.ProjectId == proId));

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
        
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetStaffOfProjects(StaffOfProjectFilter filter, PagingRequest paging)
        {
            (int, IQueryable<AccountViewModel>) result;
            try
            {
                lock (_accountRepository)
                {
                    result = _accountRepository.GetAll(x => x.Status != 0 && x.Projects != null)
                        .AsQueryable()
                        .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_mapper.Map<AccountViewModel>(filter))
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

        #region Create
        public ResponseResult<(AccountViewModel, ProjectViewModel)> CreateStaffOfProject(int accId, int proId)
        {
            throw new Exception();
        }
        #endregion

    }
}
