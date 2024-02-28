using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class OwnerRequestModel
    {
        public OwnerRequestModel()
        {
            Departments = new HashSet<DepartmentRequestModel>();
        }

        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Status { get; set; }

        public ICollection<DepartmentRequestModel>? Departments { get; set; }
    }
}
