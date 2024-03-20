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
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IMapper _mapper;

        public ContractService(IContractRepository contractRepository, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _mapper = mapper;
        }

        #region Create
        public ResponseResult<ContractViewModel> CreateContract(ContractRequestModel request)
        {
            Contract result = new Contract();
            try
            {
                lock (_contractRepository)
                {
                    //if (_contractRepository.Any(x => x.ReservationId == request.ReservationId
                    //    && x.CustomerId == request.CustomerId && x.Status != 0) == true)
                    //{
                    //    return new ResponseResult<ContractViewModel>()
                    //    {
                    //        Message = Constraints.INFORMATION_EXISTED,
                    //        result = false,
                    //    };
                    //}

                    result = _mapper.Map<Contract>(request);
                    _contractRepository.Insert(result);
                    _contractRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<ContractViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ContractViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                Value = _mapper.Map<ContractViewModel>(result),
                result = true,
            };
        }
        #endregion

        #region Delete
        public ResponseResult<ContractViewModel> DeleteContract(int id)
        {
            try
            {
                lock (_contractRepository)
                {

                    Contract result = _contractRepository.GetFirstOrDefault(x => x.ContractId == id && x.Status != 0);
                    if (result == null)
                    {
                        return new ResponseResult<ContractViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        };
                    }

                    result.Status = 0;

                    _contractRepository.UpdateById(result, id);
                    _contractRepository.SaveChages();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<ContractViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<ContractViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Read
        public ResponseResult<ContractViewModel> GetContract(int id)
        {
            Contract result = new Contract();
            try
            {
                lock (_contractRepository)
                {
                    result = _contractRepository.GetFirstOrDefault(x => x.ContractId == id && x.Status != 0);

                    if (result == null)
                    {
                        return new ResponseResult<ContractViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }


                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<ContractViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ContractViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = _mapper.Map<ContractViewModel>(result),
                result = true
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<ContractViewModel> GetContracts(PagingRequest paging, ContractViewModel filter)
        {
            (int, IQueryable<ContractViewModel>) result;

            try
            {
                lock (_contractRepository)
                {
                    result = _contractRepository.GetAll(filter: x => x.Status != 0,
                                                 includeProperties: String.Join(",", 
                                                 SupportingFeature.GetNameIncludedProperties<Contract>()))
                            .AsQueryable()
                            .ProjectTo<ContractViewModel>(_mapper.ConfigurationProvider)
                            .DynamicFilter(filter)
                            .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ContractViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ContractViewModel>()
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

        #region ContractType
        public EnumViewModel GetConstractType() => new EnumViewModel()
        {
            Message = Constraints.INFORMATION,
            Results = SupportingFeature.Instance.GetEnumName<ContractType>() 
        };
        #endregion

        #endregion

        #region Update
        public ResponseResult<ContractViewModel> UpdateContract(ContractRequestModel request, int id)
        {
            Contract result = new Contract();
            try
            {
                lock (_contractRepository)
                {
                    if (_contractRepository.Any(x => x.ContractId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<ContractViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    result = _mapper.Map<Contract>(request);
                    result.ContractId = id;

                    _contractRepository.UpdateById(result, id);
                    _contractRepository.SaveChages();

                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<ContractViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ContractViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ContractViewModel>(result)
            };
        }
        #endregion
    }
}
