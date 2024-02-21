using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Filters
{
    public class StaffOfProjectFilter
    {
        #region Project
        public int? ProjectId { get; set; }
        #endregion

        #region Account
        public int? AccountId { get; set; }
        #endregion
    }
}
