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
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        #region Create
        public ResponseResult<DepartmentViewModel> CreateDepartment(DepartmentRequestModel request)
        {
            DepartmentViewModel result = new DepartmentViewModel();
            try
            {
                lock (_departmentRepository)
                {
                    var data = _mapper.Map<Department>(request);
                    _departmentRepository.Insert(data);
                    _departmentRepository.SaveChages();

                    result = _mapper.Map<DepartmentViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<DepartmentViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<DepartmentViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Read
        #region Get Department
        public ResponseResult<DepartmentViewModel> GetDepartment(int id)
        {
            ResponseResult<DepartmentViewModel> result = new ResponseResult<DepartmentViewModel>();
            try
            {
                lock (_departmentRepository)
                {
                    var data = _mapper.Map<DepartmentViewModel>(_departmentRepository
                        .FistOrDefault(x => x.DepartmentId == id && x.Status != 0));

                    result = data == null ?
                        new ResponseResult<DepartmentViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<DepartmentViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<DepartmentViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }
        #endregion
        #region Get list department
        public DynamicModelResponse.DynamicModelsResponse<DepartmentViewModel> GetDepartments(DepartmentViewModel filter, PagingRequest paging)
        {
            (int, IQueryable<DepartmentViewModel>) result;
            try
            {
                lock (_departmentRepository)
                {
                    result = _departmentRepository.GetAll(filter: x => x.Status != 0,
                                                  includeProperties: String.Join(",",
                                                  SupportingFeature.GetNameIncludedProperties<Department>()))
                        .AsQueryable()
                        .ProjectTo<DepartmentViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<DepartmentViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<DepartmentViewModel>()
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
        public ResponseResult<DepartmentViewModel> UpdateDepartment(DepartmentRequestModel request, int id)
        {
            DepartmentViewModel result = new DepartmentViewModel();
            try
            {
                lock (_departmentRepository)
                {
                    var data = _mapper.Map<Department>(request);

                    if (_departmentRepository.Any(x => x.DepartmentId == id && x.Status != 0) == false)
                    {
                        return new ResponseResult<DepartmentViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<DepartmentViewModel>(data)
                        };
                    }

                    data.DepartmentId = id;
                    _departmentRepository.UpdateById(data, id);
                    _departmentRepository.SaveChages();

                    result = _mapper.Map<DepartmentViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<DepartmentViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<DepartmentViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Delele
        public ResponseResult<DepartmentViewModel> DeleteDepartment(int id)
        {
            try
            {
                lock (_departmentRepository)
                {
                    var data = _departmentRepository.FistOrDefault(x => x.DepartmentId == id && x.Status != 0);
                    if (data == null)
                    {
                        return new ResponseResult<DepartmentViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    data.Status = 0;
                    _departmentRepository.UpdateById(data, id);
                    _departmentRepository.SaveChages();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<DepartmentViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<DepartmentViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

    }
}