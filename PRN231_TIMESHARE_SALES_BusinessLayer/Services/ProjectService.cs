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
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        #region Create
        public ResponseResult<ProjectViewModel> CreateProject(ProjectRequestModel request)
        {
            ProjectViewModel result = new ProjectViewModel();
            try
            {
                lock (_projectRepository)
                {
                    if(_projectRepository.Any(x => 
                    x.ProjectCode.ToLower().Equals(request.ProjectCode.ToLower())
                    && x.Status != 0))
                    {
                        return new ResponseResult<ProjectViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map <Project>(request);
                    _projectRepository.Insert(data);
                    _projectRepository.SaveChages();

                    result = _mapper.Map<ProjectViewModel>(data);
                };

            }catch(Exception ex)
            {
                return new ResponseResult<ProjectViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ProjectViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get Project
        public ResponseResult<ProjectViewModel> GetProject(int id)
        {
            ResponseResult<ProjectViewModel> result = new ResponseResult<ProjectViewModel>();
            try
            {
                lock (_projectRepository)
                {
                    var data = _mapper.Map<ProjectViewModel>(_projectRepository
                        .FistOrDefault(x => x.ProjectId == id && x.Status != 0));

                    result = data == null ?
                        new ResponseResult<ProjectViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<ProjectViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<ProjectViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list project
        public DynamicModelResponse.DynamicModelsResponse<ProjectViewModel> GetProjects(
            ProjectViewModel filter, PagingRequest paging, ProjectOrderFilter orderFilter)
        {
            (int, IQueryable<ProjectViewModel>) result;
            try
            {
                lock (_projectRepository)
                {



                    var data = _projectRepository.GetAll(filter: x => x.Status != 0,
                                                includeProperties: String.Join(",",
                                                               SupportingFeature.GetNameIncludedProperties<Project>())
                                                )
                        .AsQueryable()
                        .ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? getName = Enum.GetName(typeof(ProjectOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, getName).AsQueryable();
                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                                  Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ProjectViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ProjectViewModel>()
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
        public ResponseResult<ProjectViewModel> UpdateProject(ProjectRequestModel request, int id)
        {
            ProjectViewModel result = new ProjectViewModel();
            try
            {
                lock(_projectRepository)
                {
                    var data = _mapper.Map<Project>(request);

                    if(_projectRepository.Any(x => x.ProjectId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<ProjectViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<ProjectViewModel>(data)
                        };
                    }

                    data.ProjectId = id;
                    _projectRepository.UpdateById(data, id);
                    _projectRepository.SaveChages();
                    
                    result = _mapper.Map<ProjectViewModel>(data);
                };
                
                
            }
            catch (Exception ex)
            {
                return new ResponseResult<ProjectViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }
            
            return new ResponseResult<ProjectViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<ProjectViewModel> DeleteProject(int id)
        {
            try
            {
                lock (_projectRepository)
                {
                    var data = _projectRepository.FistOrDefault(x => x.ProjectId == id && x.Status != 0);
                    if (data == null)
                    {
                        return new ResponseResult<ProjectViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    data.Status = 0;
                    _projectRepository.UpdateById(data, id);
                    _projectRepository.SaveChages();
                }

            }catch(Exception ex)
            {
                return new ResponseResult<ProjectViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ProjectViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}
