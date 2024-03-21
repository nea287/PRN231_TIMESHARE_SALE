using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class UsageHistory
    {
        public int UsageId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? Status { get; set; }
        public int DepartmentId { get; set; }
        //[JsonIgnore]
        public virtual Account? Customer { get; set; }
        //[JsonIgnore]
        public virtual Department? Department { get; set; }
    }
}
