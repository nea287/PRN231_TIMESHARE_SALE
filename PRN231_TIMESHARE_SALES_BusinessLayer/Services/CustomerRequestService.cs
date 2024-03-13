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
using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRequestRepository _customerRequestRepository;

        public CustomerRequestService(IMapper mapper, ICustomerRequestRepository customerRequestRepository)
        {
            _mapper = mapper;
            _customerRequestRepository = customerRequestRepository;
        }

        #region Create
        public ResponseResult<CustomerRequestViewModel> CreateCustomerRequest(CustomerRequestRequestModel request)
        {
            CustomerRequestViewModel result = new CustomerRequestViewModel();
            try
            {
                lock (_customerRequestRepository)
                {
                    var data = _mapper.Map<CustomerRequest>(request);
                    _customerRequestRepository.Insert(data);
                    _customerRequestRepository.SaveChages();

                    result = _mapper.Map<CustomerRequestViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<CustomerRequestViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<CustomerRequestViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get CustomerRequest
        public ResponseResult<CustomerRequestViewModel> GetCustomerRequest(int id)
        {
            ResponseResult<CustomerRequestViewModel> result = new ResponseResult<CustomerRequestViewModel>();
            try
            {
                lock (_customerRequestRepository)
                {
                    var data = _mapper.Map<CustomerRequestViewModel>(_customerRequestRepository
                        .FistOrDefault(x => x.CustomerRequestId == id && x.Status != 0));

                    result = data == null ?
                        new ResponseResult<CustomerRequestViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<CustomerRequestViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<CustomerRequestViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list customerRequest
        public DynamicModelResponse.DynamicModelsResponse<CustomerRequestViewModel> GetCustomerRequests(CustomerRequestViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<CustomerRequestViewModel>) result;
            try
            {
                lock (_customerRequestRepository)
                {
                    result = _customerRequestRepository.GetAll(filter: x => x.Status != 0,
                                                       includeProperties: String.Join(",",
                                                       SupportingFeature.GetNameIncludedProperties<CustomerRequest>()))
                        .AsQueryable()
                        .ProjectTo<CustomerRequestViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<CustomerRequestViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<CustomerRequestViewModel>()
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
        public ResponseResult<CustomerRequestViewModel> UpdateCustomerRequest(CustomerRequestRequestModel request, int id)
        {
            CustomerRequestViewModel result = new CustomerRequestViewModel();
            try
            {
                lock (_customerRequestRepository)
                {
                    var data = _mapper.Map<CustomerRequest>(request);

                    if (_customerRequestRepository.Any(x => x.CustomerRequestId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<CustomerRequestViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<CustomerRequestViewModel>(data)
                        };
                    }

                    data.CustomerRequestId = id;
                    _customerRequestRepository.UpdateById(data, id);
                    _customerRequestRepository.SaveChages();

                    result = _mapper.Map<CustomerRequestViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<CustomerRequestViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<CustomerRequestViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<CustomerRequestViewModel> DeleteCustomerRequest(int id)
        {
            try
            {
                lock (_customerRequestRepository)
                {
                    var data = _customerRequestRepository.FistOrDefault(x => x.CustomerRequestId == id && x.Status != 0);
                    if (data == null)
                    {
                        return new ResponseResult<CustomerRequestViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    data.Status = 0;
                    _customerRequestRepository.UpdateById(data, id);
                    _customerRequestRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<CustomerRequestViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<CustomerRequestViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}
