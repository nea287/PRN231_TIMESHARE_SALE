using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class OwnerViewModel
    {
        public OwnerViewModel()
        {
            Departments = new HashSet<DepartmentViewModel>();
        }

        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Status { get; set; }

        public ICollection<DepartmentViewModel>? Departments { get; set; }
    }
}
