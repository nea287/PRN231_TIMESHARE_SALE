using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class StaffOfProjectsViewModel
    {
        public int? StaffId { get; set; }

        public int? ProjectId { get; set; }

        public string? StaffName { get; set; }

        public string? ProjectName { get; set; }    
    }
}
