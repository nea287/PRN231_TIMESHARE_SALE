using PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels;
using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface IDashboardService
    {
        public Task<DashboardResponse<int>> GetDashboardByMonth(MonthRequestModel request);
        public Task<DashboardResponse<DateTime>> GetDashboardByDate(DateRequestModel request);
        public Task<DashboardResponse<int>> GetDashboardByYear(YearRequestModel request);
    }
}
