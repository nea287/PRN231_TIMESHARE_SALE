using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers
{
    public class DashboardResponse<T>
    {
        public string Message { get; set; }
        public Dictionary<T, decimal> Values { get; set; }
    }
}
