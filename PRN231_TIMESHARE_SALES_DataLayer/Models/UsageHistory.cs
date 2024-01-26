using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class UsageHistory
    {
        public int UsageId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int? Status { get; set; }
        public int DepartmentId { get; set; }

        public virtual Account Customer { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
    }
}
