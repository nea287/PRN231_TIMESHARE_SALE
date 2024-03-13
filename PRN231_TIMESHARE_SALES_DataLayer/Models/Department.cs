using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Department
    {
        public Department()
        {
            AvailableTimes = new HashSet<AvailableTime>();
            CustomerRequests = new HashSet<CustomerRequest>();
            Facilities = new HashSet<Facility>();
            Feedbacks = new HashSet<Feedback>();
            UsageHistories = new HashSet<UsageHistory>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? ProjectId { get; set; }
        public int? OwnerId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Floors { get; set; }
        public decimal? Price { get; set; }
        public int? ConstructionType { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? Capacity { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<AvailableTime> AvailableTimes { get; set; }
        public virtual ICollection<CustomerRequest> CustomerRequests { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<UsageHistory> UsageHistories { get; set; }
    }
}
