using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            ContractCustomers = new HashSet<ContractViewModel>();
            ContractStaffs = new HashSet<ContractViewModel>();
            CustomerRequests = new HashSet<CustomerRequestViewModel>();
            Feedbacks = new HashSet<FeedbackViewModel>();
            Reservations = new HashSet<ReservationViewModel>();
            UsageHistories = new HashSet<UsageHistoryViewModel>();
            UsageRights = new HashSet<UsageRightViewModel>();
            Projects = new HashSet<ProjectViewModel>();
        }

        public int? AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Status { get; set; }

        public ICollection<ContractViewModel>? ContractCustomers { get; set; }
        public ICollection<ContractViewModel>? ContractStaffs { get; set; }
        public ICollection<CustomerRequestViewModel>? CustomerRequests { get; set; }
        public ICollection<FeedbackViewModel>? Feedbacks { get; set; }
        public ICollection<ReservationViewModel>? Reservations { get; set; }
        public ICollection<UsageHistoryViewModel>? UsageHistories { get; set; }
        public ICollection<UsageRightViewModel>? UsageRights { get; set; }

        public ICollection<ProjectViewModel>? Projects { get; set; }
    }
}
