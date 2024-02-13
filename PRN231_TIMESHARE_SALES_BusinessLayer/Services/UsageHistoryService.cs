using AutoMapper;
using AutoMapper.QueryableExtensions;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class UsageHistoryService : IUsageHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IUsageHistoryRepository _usageRepository;

        public UsageHistoryService(IMapper mapper, IUsageHistoryRepository usageRepository)
        {
            _mapper = mapper;
            _usageRepository = usageRepository;
        }
        #region Create
        public ResponseResult<UsageHistoryViewModel> CreateUsageHistory(UsageHistoryRequestModel request)
        {
            UsageHistory result = new UsageHistory();
            try
            {

                if(_usageRepository.Any(x => 
                    x.CustomerId == request.CustomerId 
                    && x.DepartmentId == request.DepartmentId
                    && x.Status != 0))
                {
                    return new ResponseResult<UsageHistoryViewModel>()
                    {
                        Message = Constraints.INFORMATION_EXISTED,
                        result = false,
                    };
                }

                result = _mapper.Map<UsageHistory>(request);

                _usageRepository.Insert(result);
                _usageRepository.SaveChages();

            }catch(Exception ex)
            {
                return new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<UsageHistoryViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<UsageHistoryViewModel>(result)
            };
        }

        #endregion

        #region Delete
        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistoryById(int id)
        {
            try
            {
                var data = _usageRepository.GetById(id).Result;
                if(data == null)
                {
                    return new ResponseResult<UsageHistoryViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false
                    };
                }

                data.Status = 0;

                _usageRepository.UpdateById(data, id);
                _usageRepository.SaveChages();

            }catch(Exception ex)
            {
                return new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<UsageHistoryViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public ResponseResult<UsageHistoryViewModel> DeleteUsageHistory(int customerId, int depId)
        {
            try
            {
                var data = _usageRepository.GetFirstOrDefault(x => x.CustomerId == customerId 
                    && x.DepartmentId == depId && x.Status != 0);
                if (data == null)
                {
                    return new ResponseResult<UsageHistoryViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false
                    };
                }

                data.Status = 0;

                _usageRepository.UpdateById(data, data.UsageId);
                _usageRepository.SaveChages();

            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<UsageHistoryViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }
        #endregion

        #region Read
        public ResponseResult<UsageHistoryViewModel> GetUsageHistoryById(int id)
        {
            ResponseResult<UsageHistoryViewModel> result = new ResponseResult<UsageHistoryViewModel>();
            try
            {
                lock (_usageRepository)
                {
                    var data = _mapper.Map<UsageHistoryViewModel>(_usageRepository
                        .FistOrDefault(x => x.UsageId == id && x.Status != 0));

                    result = data == null ?
                        new ResponseResult<UsageHistoryViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<UsageHistoryViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<UsageHistoryViewModel> GetUsageHistories(UsageHistoryViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<UsageHistoryViewModel>) result;
            try
            {
                lock (_usageRepository)
                {
                    result = _usageRepository.GetAll(x => x.Status != 0)
                        .AsQueryable()
                        .ProjectTo<UsageHistoryViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<UsageHistoryViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<UsageHistoryViewModel>()
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
        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistoryById(UsageHistoryRequestModel request, int id)
        {
            UsageHistoryViewModel result = new UsageHistoryViewModel();
            try
            {
                lock (_usageRepository)
                {
                    var data = _mapper.Map<UsageHistory>(request);

                    if (_usageRepository.Any(x => x.UsageId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<UsageHistoryViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<UsageHistoryViewModel>(data)
                        };
                    }

                    data.UsageId = id;
                    _usageRepository.UpdateById(data, id);
                    _usageRepository.SaveChages();

                    result = _mapper.Map<UsageHistoryViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<UsageHistoryViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public ResponseResult<UsageHistoryViewModel> UpdateUsageHistory(UsageHistoryRequestModel request, int customerId, int depId)
        {
            UsageHistoryViewModel result = new UsageHistoryViewModel();
            try
            {
                lock (_usageRepository)
                {
                    var data = _usageRepository.FistOrDefault(x => x.CustomerId == customerId
                        && x.DepartmentId == depId
                        && x.Status != 0);

                    int id = data.UsageId;

                     data = _mapper.Map<UsageHistory>(request);

                    if (data == null)
                    {
                        return new ResponseResult<UsageHistoryViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<UsageHistoryViewModel>(data)
                        };
                    }

                    data.UsageId = id;
                    _usageRepository.UpdateById(data, id);
                    _usageRepository.SaveChages();

                    result = _mapper.Map<UsageHistoryViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageHistoryViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<UsageHistoryViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion
    }
}
