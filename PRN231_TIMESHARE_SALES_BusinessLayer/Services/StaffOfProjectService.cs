using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.Filters;
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
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class StaffOfProjectService : IStaffOfProjectService
    {
        private readonly IStaffOfProjectRepository _repository;
        private readonly IMapper _mapper;

        public StaffOfProjectService(IMapper mapper, IStaffOfProjectRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region Read
        public ResponseResult<StaffOfProjectsViewModel> GetStaffOfProject(StaffOfProjectRequestModel request)
        {

            ResponseResult<StaffOfProjectsViewModel> result = new ResponseResult<StaffOfProjectsViewModel>();
            try
            {
                lock (_repository)
                {
                    var data = _mapper.Map<StaffOfProjectsViewModel>(_repository
                        .FistOrDefault(x => x.StaffId == request.StaffId && x.ProjectId == request.ProjectId));
                       //.FirstOrDefault(a => a.ProjectId == proId));
                    //Projects.FirstOrDefault(a => a.ProjectId == proId) != null)
                    result = data == null ?
                        new ResponseResult<StaffOfProjectsViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<StaffOfProjectsViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<StaffOfProjectsViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        
        public DynamicModelResponse.DynamicModelsResponse<StaffOfProjectsViewModel> GetStaffOfProjects(StaffOfProjectsViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<StaffOfProjectsViewModel>) result;
            try
            {
                lock (_repository)
                {
                    result = _repository.GetAll(includeProperties: 
                                        String.Join(",", SupportingFeature
                                              .GetNameIncludedProperties<StaffOfProject>()))
                        .AsQueryable()
                        .ProjectTo<StaffOfProjectsViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_mapper.Map<StaffOfProjectsViewModel>(filter))
                        .PagingIQueryable(paging.page, paging.pageSize, 
                            Constraints.LimitPaging, Constraints.DefaultPaging);


                    
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<StaffOfProjectsViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<StaffOfProjectsViewModel>()
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
        public ResponseResult<StaffOfProjectsViewModel> CreateStaffOfProject(StaffOfProjectRequestModel request)
        {
            var result = new StaffOfProject();    
            try
            {
                lock(_repository)
                {
                    result = _repository.FistOrDefault(x => x.StaffId == request.StaffId && x.ProjectId == request.ProjectId);
                    if(result != null)
                    {
                        return new ResponseResult<StaffOfProjectsViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                            Value = _mapper.Map<StaffOfProjectsViewModel>(result)   
                        };
                    }

                    result = _mapper.Map<StaffOfProject>(request);
                    _repository.Insert(result);
                    _repository.SaveChages();
                    
                }
            }catch(Exception ex)
            {
                return new ResponseResult<StaffOfProjectsViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<StaffOfProjectsViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = false,
                Value = _mapper.Map<StaffOfProjectsViewModel>(request)
            };
        }
        #endregion

        #region Update

        public ResponseResult<StaffOfProjectsViewModel> UpdateStaffOfProject(int staffId, int projectId, StaffOfProjectRequestModel request)
        {
            var result = new StaffOfProject();
            try
            {
                lock (_repository)
                {
                    result = _repository.GetFirstOrDefault(x => x.StaffId == staffId && x.ProjectId == projectId);
                    if(result == null)
                    {
                        return new ResponseResult<StaffOfProjectsViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<StaffOfProjectsViewModel>(request)
                        };
                    }

                    _repository.Delete(result);
                    _repository.Insert(_mapper.Map<StaffOfProject>(request));
                    _repository.SaveChages();
                }
            }catch(Exception ex)
            {
                return new ResponseResult<StaffOfProjectsViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = _mapper.Map<StaffOfProjectsViewModel>(request)
                };
            }
            return new ResponseResult<StaffOfProjectsViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<StaffOfProjectsViewModel>(request)
            };
        }

        #endregion
        #region Delete
        public ResponseResult<StaffOfProjectsViewModel> DeleteStaffOfProject(StaffOfProjectRequestModel request)
        {
            try
            {

                lock (_repository)
                {
                    if (_repository.Any(x => x.StaffId == request.StaffId && x.ProjectId == request.ProjectId) == false)
                    {
                        return new ResponseResult<StaffOfProjectsViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        };
                    }
                }
                var data = _repository.GetFirstOrDefault(x => x.StaffId == request.StaffId && x.ProjectId == request.ProjectId);
                _repository.Delete(data) ;
                _repository.SaveChages();

            }catch(Exception ex)
            {
                return new ResponseResult<StaffOfProjectsViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<StaffOfProjectsViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}
