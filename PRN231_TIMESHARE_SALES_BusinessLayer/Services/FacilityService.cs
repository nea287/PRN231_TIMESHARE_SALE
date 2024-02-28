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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly IMapper _mapper;
        private readonly IFacilityRepository _facilityRepository;

        public FacilityService(IMapper mapper, IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _facilityRepository = facilityRepository;
        }

        #region Create
        public ResponseResult<FacilityViewModel> CreateFacility(FacilityRequestModel request)
        {
            FacilityViewModel result = new FacilityViewModel();
            try
            {
                lock (_facilityRepository)
                {
                    var data = _mapper.Map<Facility>(request);
                    _facilityRepository.Insert(data);
                    _facilityRepository.SaveChages();

                    result = _mapper.Map<FacilityViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<FacilityViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<FacilityViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get Facility
        public ResponseResult<FacilityViewModel> GetFacility(int id)
        {
            ResponseResult<FacilityViewModel> result = new ResponseResult<FacilityViewModel>();
            try
            {
                lock (_facilityRepository)
                {
                    var data = _mapper.Map<FacilityViewModel>(_facilityRepository
                        .FistOrDefault(x => x.FacilityId == id));

                    result = data == null ?
                        new ResponseResult<FacilityViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<FacilityViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<FacilityViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list facility
        public DynamicModelResponse.DynamicModelsResponse<FacilityViewModel> GetFacilitys(FacilityViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<FacilityViewModel>) result;
            try
            {
                lock (_facilityRepository)
                {
                    result = _facilityRepository.GetAll()
                        .AsQueryable()
                        .ProjectTo<FacilityViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FacilityViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<FacilityViewModel>()
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
        #endregion

        #region Update
        public ResponseResult<FacilityViewModel> UpdateFacility(FacilityRequestModel request, int id)
        {
            FacilityViewModel result = new FacilityViewModel();
            try
            {
                lock (_facilityRepository)
                {
                    var data = _mapper.Map<Facility>(request);

                    if (_facilityRepository.Any(x => x.FacilityId == id) == false)
                    {
                        return new ResponseResult<FacilityViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<FacilityViewModel>(data)
                        };
                    }

                    data.FacilityId = id;
                    _facilityRepository.UpdateById(data, id);
                    _facilityRepository.SaveChages();

                    result = _mapper.Map<FacilityViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<FacilityViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<FacilityViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<FacilityViewModel> DeleteFacility(int id)
        {
            try
            {
                lock (_facilityRepository)
                {
                    var data = _facilityRepository.FistOrDefault(x => x.FacilityId == id);
                    if (data == null)
                    {
                        return new ResponseResult<FacilityViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    _facilityRepository.UpdateById(data, id);
                    _facilityRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<FacilityViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<FacilityViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}