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
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
        }

        #region Create
        public ResponseResult<FeedbackViewModel> CreateFeedback(FeedbackRequestModel request)
        {
            FeedbackViewModel result = new FeedbackViewModel();
            try
            {
                lock (_feedbackRepository)
                {
                    var data = _mapper.Map<Feedback>(request);
                    _feedbackRepository.Insert(data);
                    _feedbackRepository.SaveChages();

                    result = _mapper.Map<FeedbackViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<FeedbackViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get Feedback
        public ResponseResult<FeedbackViewModel> GetFeedback(int id)
        {
            ResponseResult<FeedbackViewModel> result = new ResponseResult<FeedbackViewModel>();
            try
            {
                lock (_feedbackRepository)
                {
                    var data = _mapper.Map<FeedbackViewModel>(_feedbackRepository
                        .FistOrDefault(x => x.FeedbackId == id));

                    result = data == null ?
                        new ResponseResult<FeedbackViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<FeedbackViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<FeedbackViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list feedback
        public DynamicModelResponse.DynamicModelsResponse<FeedbackViewModel> GetFeedbacks(FeedbackViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<FeedbackViewModel>) result;
            try
            {
                lock (_feedbackRepository)
                {
                    result = _feedbackRepository.GetAll()
                        .AsQueryable()
                        .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FeedbackViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<FeedbackViewModel>()
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
        public ResponseResult<FeedbackViewModel> UpdateFeedback(FeedbackRequestModel request, int id)
        {
            FeedbackViewModel result = new FeedbackViewModel();
            try
            {
                lock (_feedbackRepository)
                {
                    var data = _mapper.Map<Feedback>(request);

                    if (_feedbackRepository.Any(x => x.FeedbackId == id) == false)
                    {
                        return new ResponseResult<FeedbackViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<FeedbackViewModel>(data)
                        };
                    }

                    data.FeedbackId = id;
                    _feedbackRepository.UpdateById(data, id);
                    _feedbackRepository.SaveChages();

                    result = _mapper.Map<FeedbackViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<FeedbackViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<FeedbackViewModel> DeleteFeedback(int id)
        {
            try
            {
                lock (_feedbackRepository)
                {
                    var data = _feedbackRepository.FistOrDefault(x => x.FeedbackId == id);
                    if (data == null)
                    {
                        return new ResponseResult<FeedbackViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    _feedbackRepository.UpdateById(data, id);
                    _feedbackRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<FeedbackViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}