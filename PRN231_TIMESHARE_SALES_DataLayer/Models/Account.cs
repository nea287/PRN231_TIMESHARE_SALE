using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Account
    {
        public Account()
        {
            ContractCustomers = new HashSet<Contract>();
            ContractStaffs = new HashSet<Contract>();
            CustomerRequests = new HashSet<CustomerRequest>();
            Feedbacks = new HashSet<Feedback>();
            Reservations = new HashSet<Reservation>();
            UsageHistories = new HashSet<UsageHistory>();
            UsageRights = new HashSet<UsageRight>();
            Projects = new HashSet<Project>();
        }

        public int AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<Contract> ContractCustomers { get; set; }
        public virtual ICollection<Contract> ContractStaffs { get; set; }
        public virtual ICollection<CustomerRequest> CustomerRequests { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UsageHistory> UsageHistories { get; set; }
        public virtual ICollection<UsageRight> UsageRights { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
