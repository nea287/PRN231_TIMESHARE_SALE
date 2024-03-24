using Microsoft.EntityFrameworkCore;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using PRN231_TIMESHARE_SALES_Repository.IRepository;
using System.Linq.Dynamic.Core;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IContractRepository _contractRepository;

        public DashboardService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        #region Dashboard month
        public async Task<DashboardResponse<int>> GetDashboardByMonth(MonthRequestModel request)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            try
            {
                request.StartMonth = request.StartMonth ?? DateTime.Now.Month;
                request.EndMonth = request.EndMonth ?? DateTime.Now.Month;

                if (request.Month.HasValue)
                {
                    result.Add(request.Month.Value,
                              _contractRepository
                                      .GetAll(x => x.ContractDate.Value.Month == request.Month
                                       && x.ContractDate.Value.Year == request.Year)
                                      .Sum(x => x.ContractAmount.Value));
                }
                else
                {
                    var lst = _contractRepository.GetAll(contract => contract.ContractDate.Value.Month >= request.StartMonth
                                                         && contract.ContractDate.Value.Month <= request.EndMonth
                                                         && contract.ContractDate.Value.Year == request.Year)
                                                         .GroupBy(contract => contract.ContractDate.Value.Month)
                                                         .Select(group => new 
                                                         {
                                                             Month = group.Key,
                                                             TotalAmount = group.Sum(contract => contract.ContractAmount.Value) 
                                                         });

                    await lst.ForEachAsync(x =>
                    {
                        result.Add(x.Month, x.TotalAmount);
                    });
                }


            }
            catch(Exception ex)
            {
                return new DashboardResponse<int>()
                {
                    Message = Constraints.LOAD_FAILED,
                    Values = result
                };
            }

            return new DashboardResponse<int>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        #endregion

        #region Dashboard date

        public async Task<DashboardResponse<DateTime>> GetDashboardByDate(DateRequestModel request)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {
                request.StartDate = request.StartDate ?? DateTime.Now.Date;

                request.EndDate = request.EndDate 
                    ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                if (request.Date.HasValue)
                {
                    result.Add(request.Date.Value.Date,
                              _contractRepository
                                      .GetAll(x => x.ContractDate.Value.Date == request.Date.Value.Date)
                                      .Sum(x => x.ContractAmount.Value));
                }
                else
                {

                    var lst = _contractRepository.GetAll(contract => contract.ContractDate.Value >= request.StartDate.Value
                                                         && contract.ContractDate.Value <= request.EndDate.Value)
                                                         .GroupBy(contract => contract.ContractDate.Value.Date)
                                                         .Select(group => new
                                                         {
                                                             Date = group.Key,
                                                             TotalAmount = group.Sum(contract => contract.ContractAmount.Value)
                                                         });

                    await lst.ForEachAsync(x =>
                    {
                        result.Add(x.Date, x.TotalAmount);
                    });
                }


            }
            catch (Exception ex)
            {
                return new DashboardResponse<DateTime>()
                {
                    Message = Constraints.LOAD_FAILED,
                    Values = result
                };
            }

            return new DashboardResponse<DateTime>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        #endregion

        #region Dashboard Year

        public async Task<DashboardResponse<int>> GetDashboardByYear(YearRequestModel request)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            try
            {
                if (request.Year.HasValue)
                {
                    result.Add(request.Year.Value,
                              _contractRepository
                                      .GetAll(x => x.ContractDate.Value.Year == request.Year)
                                      .Sum(x => x.ContractAmount.Value));
                }
                else
                {

                    var lst = _contractRepository.GetAll(contract => contract.ContractDate.Value.Year >= request.FromYear
                                                         && contract.ContractDate.Value.Year <= request.ToYear)
                                                         .GroupBy(contract => contract.ContractDate.Value.Year)
                                                         .Select(group => new
                                                         {
                                                             Year = group.Key,
                                                             TotalAmount = group.Sum(contract => contract.ContractAmount.Value)
                                                         });
                    foreach(var e in lst)
                    {
                        result.Add(e.Year, e.TotalAmount);
                    }

                }


            }
            catch (Exception ex)
            {
                return new DashboardResponse<int>()
                {
                    Message = Constraints.LOAD_FAILED,
                    Values = result
                };
            }

            return new DashboardResponse<int>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        #endregion

        #region Dashboard by DepartmentType
        public DashboardResponse<string> GetRevenueContructionTypeName(DepartmentConstructionType? request = null)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            try
            {
                if (request != null)
                {
                    result.Add(Enum.GetName(typeof(DepartmentConstructionType), request.Value),
                              _contractRepository
                                      .GetAll(contract => 
                                        contract.AvailableTime.DepartmentProjectCodeNavigation.Department.ConstructionType == (int)request.Value)
                                      
                                      .Sum(x => x.ContractAmount.Value));
                }
                else
                {

                    var lst = _contractRepository.GetAll()
                                                 .GroupBy(contract => contract.AvailableTime.DepartmentProjectCodeNavigation.Department.ConstructionType)
                                                         .Select(group => new
                                                         {
                                                             ContructionType = group.Key,
                                                             TotalAmount = group.Sum(contract => contract.ContractAmount.Value)
                                                         });
                    foreach (var e in lst)
                    {
                        result.Add(Enum.GetName(typeof(DepartmentConstructionType), e.ContructionType), e.TotalAmount);
                    }

                }
            }
            catch (Exception ex)
            {
                return new DashboardResponse<string>()
                {
                    Message = Constraints.LOAD_FAILED,
                    Values = result
                };
            }

            return new DashboardResponse<string>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }
        #endregion

        #region Dashboard by project Name
        public DashboardResponse<string> GetRevenueProjectName(string? projectName = null)
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            try
            {
                if (projectName != null)
                {
                    result.Add(projectName,
                              _contractRepository
                                      .GetAll(contract =>
                                        contract.AvailableTime.DepartmentProjectCodeNavigation.Project.ProjectName.Contains(projectName))

                                      .Sum(x => x.ContractAmount.Value));
                }
                else
                {

                    var lst = _contractRepository.GetAll()
                                                 .GroupBy(contract => contract.AvailableTime.DepartmentProjectCodeNavigation.Project.ProjectName)
                                                         .Select(group => new
                                                         {
                                                             ProjectName = group.Key,
                                                             TotalAmount = group.Sum(contract => contract.ContractAmount.Value)
                                                         });
                    foreach (var e in lst)
                    {
                        result.Add(e.ProjectName, e.TotalAmount);
                    }

                }
            }
            catch (Exception ex)
            {
                return new DashboardResponse<string>()
                {
                    Message = Constraints.LOAD_FAILED,
                    Values = result
                };
            }

            return new DashboardResponse<string>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }
        #endregion

    }
}
