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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class UsageRightService : IUsageRightService
    {
        private readonly IUsageRightRepository _usageRightRepository;
        private readonly IMapper _mapper;

        public UsageRightService(IUsageRightRepository usageRightRepository, IMapper mapper)
        {
            _usageRightRepository = usageRightRepository;
            _mapper = mapper;
        }

        #region Create
        public ResponseResult<UsageRightViewModel> CreateUsageRight(UsageRightRequestModel request)
        {
            UsageRight result = new UsageRight();
            try
            {
                lock (_usageRightRepository)
                {
                    if (_usageRightRepository.Any(x => x.ReservationId == request.ReservationId
                        && x.CustomerId == request.CustomerId && x.Status != 0) == true)
                    {
                        return new ResponseResult<UsageRightViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    result = _mapper.Map<UsageRight>(request);
                    _usageRightRepository.Insert(result);
                    _usageRightRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageRightViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<UsageRightViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                Value = _mapper.Map<UsageRightViewModel>(result),
                result = true,
            };
        }
        #endregion


        #region Delete
        public ResponseResult<UsageRightViewModel> DeleteUsageRight(int id)
        {
            try
            {
                lock (_usageRightRepository)
                {

                    UsageRight result = _usageRightRepository.GetFirstOrDefault(x => x.UsageRightId == id && x.Status != 0);
                    if (result == null)
                    {
                        return new ResponseResult<UsageRightViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        };
                    }

                    result.Status = 0;

                    _usageRightRepository.UpdateById(result, id);
                    _usageRightRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageRightViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<UsageRightViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Read
        public ResponseResult<UsageRightViewModel> GetUsageRight(int id)
        {
            UsageRight result = new UsageRight();
            try
            {
                lock (_usageRightRepository)
                {
                    result = _usageRightRepository.GetFirstOrDefault(x => x.UsageRightId == id && x.Status != 0);

                    if (result == null)
                    {
                        return new ResponseResult<UsageRightViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }


                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageRightViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<UsageRightViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = _mapper.Map<UsageRightViewModel>(result),
                result = true
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<UsageRightViewModel> GetUsageRights(PagingRequest paging, UsageRightViewModel filter)
        {
            (int, IQueryable<UsageRightViewModel>) result;

            try
            {
                lock (_usageRightRepository)
                {
                    result = _usageRightRepository.GetAll(x => x.Status != 0)
                            .AsQueryable()
                            .ProjectTo<UsageRightViewModel>(_mapper.ConfigurationProvider)
                            .DynamicFilter(filter)
                            .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<UsageRightViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<UsageRightViewModel>()
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
        public ResponseResult<UsageRightViewModel> UpdateUsageRight(UsageRightRequestModel request, int id)
        {
            UsageRight result = new UsageRight();
            try
            {
                lock (_usageRightRepository)
                {
                    if (_usageRightRepository.Any(x => x.UsageRightId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<UsageRightViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    result = _mapper.Map<UsageRight>(request);
                    result.UsageRightId = id;

                    _usageRightRepository.UpdateById(result, id);
                    _usageRightRepository.SaveChages();

                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<UsageRightViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<UsageRightViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<UsageRightViewModel>(result)
            };
        }
        #endregion
    }
}
