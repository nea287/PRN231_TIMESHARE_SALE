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
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        #region Create
        public ResponseResult<ReservationViewModel> CreateReservation(ReservationRequestModel request)
        {
            Reservation result = new Reservation();
            try
            {
                lock (_reservationRepository)
                {
                    if(_reservationRepository.Any(x => x.CustomerId == request.CustomerId 
                        && x.AvailableTimeId == request.AvailableTimeId && x.Status != 0) == true)
                    {
                        return new ResponseResult<ReservationViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    result = _mapper.Map<Reservation>(request);
                    _reservationRepository.Insert(result);
                    _reservationRepository.SaveChages();
                }
            }catch(Exception ex)
            {
                return new ResponseResult<ReservationViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ReservationViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                Value = _mapper.Map<ReservationViewModel>(result),
                result = true,
            };
        }
        #endregion


        #region Delete
        public ResponseResult<ReservationViewModel> DeleteReservation(int id)
        {
            try
            {
                lock (_reservationRepository)
                {

                    Reservation result = _reservationRepository.GetFirstOrDefault(x => x.ReservationId == id && x.Status != 0);
                    if(result == null)
                    {
                        return new ResponseResult<ReservationViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        };
                    }

                    result.Status = 0;

                    _reservationRepository.UpdateById(result, id);
                    _reservationRepository.SaveChages();
                }
            }catch(Exception ex)
            {
                return new ResponseResult<ReservationViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<ReservationViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Read
        public ResponseResult<ReservationViewModel> GetReservation(int id)
        {
            Reservation result = new Reservation();
            try
            {
                lock (_reservationRepository)
                {
                    result = _reservationRepository.GetFirstOrDefault(x => x.ReservationId == id && x.Status != 0);

                    if(result == null)
                    {
                        return new ResponseResult<ReservationViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }


                }
            }catch(Exception ex)
            {
                return new ResponseResult<ReservationViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ReservationViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = _mapper.Map<ReservationViewModel>(result),
                result = true
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<ReservationViewModel> GetReservations(PagingRequest paging, ReservationViewModel filter)
        {
            (int, IQueryable<ReservationViewModel>) result;

            try
            {
                lock (_reservationRepository)
                {
                    result = _reservationRepository.GetAll(filter: x => x.Status != 0,
                                                    includeProperties: String.Join(",", 
                                                           SupportingFeature.GetNameIncludedProperties<Reservation>()))
                            .AsQueryable()
                            .ProjectTo<ReservationViewModel>(_mapper.ConfigurationProvider)
                            .DynamicFilter(filter)
                            .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }catch(Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ReservationViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ReservationViewModel>()
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

        #region GetReservationStatus
        public EnumViewModel GetReservationStatus() => new EnumViewModel()
        {
            Message = Constraints.INFORMATION,
            Results = SupportingFeature.Instance.GetEnumName<ReservationStatus>()
        };
        #endregion

        #endregion

        #region Update
        public ResponseResult<ReservationViewModel> UpdateReservation(ReservationRequestModel request, int id)
        {
            Reservation result = new Reservation();
            try
            {
                lock (_reservationRepository)
                {
                    if(_reservationRepository.Any(x => x.ReservationId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<ReservationViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    result = _mapper.Map<Reservation>(request);
                    result.ReservationId = id;

                    _reservationRepository.UpdateById(result, id);
                    _reservationRepository.SaveChages();

                }
            }catch(Exception ex)
            {
                return new ResponseResult<ReservationViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ReservationViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ReservationViewModel>(result)
            };
        }
        #endregion
    }
}
