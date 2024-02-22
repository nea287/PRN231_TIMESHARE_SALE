using PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class DepartmentRequestModel
    {
        public DepartmentRequestModel()
        {
            AvailableTimes = new HashSet<AvailableTimeViewModel>();
            CustomerRequests = new HashSet<CustomerRequestViewModel>();
            Facilities = new HashSet<FacilityViewModel>();
            Feedbacks = new HashSet<FeedbackViewModel>();
            UsageHistories = new HashSet<UsageHistoryViewModel>();
        }

        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? ProjectId { get; set; }
        public int? OwnerId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int? Floors { get; set; }
        public decimal? Price { get; set; }
        public int? ConstructionType { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public int? Capacity { get; set; }

        public ICollection<AvailableTimeViewModel>? AvailableTimes { get; set; }
        public ICollection<CustomerRequestViewModel>? CustomerRequests { get; set; }
        public ICollection<FacilityViewModel>? Facilities { get; set; }
        public ICollection<FeedbackViewModel>? Feedbacks { get; set; }
        public ICollection<UsageHistoryViewModel>? UsageHistories { get; set; }
    }
}
