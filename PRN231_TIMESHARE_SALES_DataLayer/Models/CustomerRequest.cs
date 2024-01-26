using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class CustomerRequest
    {
        public int CustomerRequestId { get; set; }
        public int CustomerId { get; set; }
        public int? RequestType { get; set; }
        public string Description { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public int? Status { get; set; }
        public int DepartmentId { get; set; }

        public virtual Account Customer { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
    }
}
