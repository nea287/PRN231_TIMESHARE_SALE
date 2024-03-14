using AutoMapper;
using AutoMapper.QueryableExtensions;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;
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
    public class OwnerService : IOwnerService
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IMapper mapper, IOwnerRepository ownerRepository)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;
        }

        #region Create
        public ResponseResult<OwnerViewModel> CreateOwner(OwnerRequestModel request)
        {
            OwnerViewModel result = new OwnerViewModel();
            try
            {
                lock (_ownerRepository)
                {
                    var data = _mapper.Map<Owner>(request);
                    _ownerRepository.Insert(data);
                    _ownerRepository.SaveChages();

                    result = _mapper.Map<OwnerViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<OwnerViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<OwnerViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get Owner
        public ResponseResult<OwnerViewModel> GetOwner(int id)
        {
            ResponseResult<OwnerViewModel> result = new ResponseResult<OwnerViewModel>();
            try
            {
                lock (_ownerRepository)
                {
                    var data = _mapper.Map<OwnerViewModel>(_ownerRepository
                        .FistOrDefault(x => x.OwnerId == id));

                    result = data == null ?
                        new ResponseResult<OwnerViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<OwnerViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<OwnerViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list owner
        public DynamicModelResponse.DynamicModelsResponse<OwnerViewModel> GetOwners(OwnerViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<OwnerViewModel>) result;
            try
            {
                lock (_ownerRepository)
                {
                    result = _ownerRepository.GetAll(includeProperties: String.Join(",", 
                                                     SupportingFeature.GetNameIncludedProperties<Owner>()))
                        .AsQueryable()
                        .ProjectTo<OwnerViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<OwnerViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<OwnerViewModel>()
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
        public ResponseResult<OwnerViewModel> UpdateOwner(OwnerRequestModel request, int id)
        {
            OwnerViewModel result = new OwnerViewModel();
            try
            {
                lock (_ownerRepository)
                {
                    var data = _mapper.Map<Owner>(request);

                    if (_ownerRepository.Any(x => x.OwnerId == id) == false)
                    {
                        return new ResponseResult<OwnerViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<OwnerViewModel>(data)
                        };
                    }

                    data.OwnerId = id;
                    _ownerRepository.UpdateById(data, id);
                    _ownerRepository.SaveChages();

                    result = _mapper.Map<OwnerViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<OwnerViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<OwnerViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<OwnerViewModel> DeleteOwner(int id)
        {
            try
            {
                lock (_ownerRepository)
                {
                    var data = _ownerRepository.FistOrDefault(x => x.OwnerId == id);
                    if (data == null)
                    {
                        return new ResponseResult<OwnerViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    _ownerRepository.UpdateById(data, id);
                    _ownerRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<OwnerViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<OwnerViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}