using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class DepartmentOfProjectViewModel
    {
        public DepartmentOfProjectViewModel()
        {
            AvailableTimes = new HashSet<AvailableTimeViewModel>();
        }
        public int? DepartmentId { get; set; }
        public int? ProjectId { get; set; }
        public string? DepartmentProjectCode { get; set; }
        public ICollection<AvailableTimeViewModel>? AvailableTimes { get; set; }

    }
}
