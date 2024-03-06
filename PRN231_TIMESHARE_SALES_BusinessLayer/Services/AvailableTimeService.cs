using AutoMapper;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class AvailableTimeService : IAvailableTimeService
    {
        private readonly IAvailableTimeRepository _availableTimeRepository;
        private readonly IMapper _mapper;

        public AvailableTimeService(IAvailableTimeRepository availableTimeRepository, IMapper mapper)
        {
            _availableTimeRepository = availableTimeRepository;
            _mapper = mapper;
        }

        #region Create
        public ResponseResult<AvailableTimeViewModel> CreateAvailableTime(AvailableTimeRequestModel request)
        {
            AvailableTime result = new AvailableTime();
            try
            {
                lock (_availableTimeRepository)
                {
                    if (_availableTimeRepository.Any(x => x.DepartmentId == request.DepartmentId
                        && x.StartDate == request.StartDate && x.Status != 0) == true)
                    {
                        return new ResponseResult<AvailableTimeViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    result = _mapper.Map<AvailableTime>(request);
                    _availableTimeRepository.Insert(result);
                    _availableTimeRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<AvailableTimeViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AvailableTimeViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                Value = _mapper.Map<AvailableTimeViewModel>(result),
                result = true,
            };
        }
        #endregion


        #region Delete
        public ResponseResult<AvailableTimeViewModel> DeleteAvailableTime(int id)
        {
            try
            {
                lock (_availableTimeRepository)
                {

                    AvailableTime result = _availableTimeRepository.GetFirstOrDefault(x => x.AvailableTimeId == id && x.Status != 0);
                    if (result == null)
                    {
                        return new ResponseResult<AvailableTimeViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        };
                    }

                    result.Status = 0;

                    _availableTimeRepository.UpdateById(result, id);
                    _availableTimeRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<AvailableTimeViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<AvailableTimeViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Read
        public ResponseResult<AvailableTimeViewModel> GetAvailableTime(int id)
        {
            AvailableTime result = new AvailableTime();
            try
            {
                lock (_availableTimeRepository)
                {
                    result = _availableTimeRepository.GetFirstOrDefault(x => x.AvailableTimeId == id && x.Status != 0);

                    if (result == null)
                    {
                        return new ResponseResult<AvailableTimeViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }


                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<AvailableTimeViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AvailableTimeViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = _mapper.Map<AvailableTimeViewModel>(result),
                result = true
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<AvailableTimeViewModel> GetAvailableTimes(PagingRequest paging, AvailableTimeViewModel filter)
        {
            (int, IQueryable<AvailableTimeViewModel>) result;

            try
            {
                lock (_availableTimeRepository)
                {
                    result = _availableTimeRepository.GetAll(x => x.Status != 0)
                            .AsQueryable()
                            .ProjectTo<AvailableTimeViewModel>(_mapper.ConfigurationProvider)
                            .DynamicFilter(filter)
                            .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AvailableTimeViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<AvailableTimeViewModel>()
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
        public ResponseResult<AvailableTimeViewModel> UpdateAvailableTime(AvailableTimeRequestModel request, int id)
        {
            AvailableTime result = new AvailableTime();
            try
            {
                lock (_availableTimeRepository)
                {
                    if (_availableTimeRepository.Any(x => x.AvailableTimeId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<AvailableTimeViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    result = _mapper.Map<AvailableTime>(request);
                    result.AvailableTimeId = id;

                    _availableTimeRepository.UpdateById(result, id);
                    _availableTimeRepository.SaveChages();

                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<AvailableTimeViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AvailableTimeViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<AvailableTimeViewModel>(result)
            };
        }
        #endregion
    }
}
