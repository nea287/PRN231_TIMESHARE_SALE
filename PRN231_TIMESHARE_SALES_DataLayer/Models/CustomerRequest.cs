using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class CustomerRequest
    {
        public int CustomerRequestId { get; set; }
        public int CustomerId { get; set; }
        public int? RequestType { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public int? Status { get; set; }
        public int DepartmentId { get; set; }
        //[JsonIgnore]
        public virtual Account Customer { get; set; }
        //[JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
