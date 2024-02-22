using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Project
    {
        public Project()
        {
            Departments = new HashSet<Department>();
            StaffOfProjects = new HashSet<StaffOfProject>();
        }

        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? PriorityType { get; set; }
        public string? ProjectCode { get; set; }
        public int? TotalSlot { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public DateTime? RegistrationOpeningDate { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        //public virtual ICollection<Account> staff { get; set; }
        public virtual ICollection<StaffOfProject> StaffOfProjects { get; set; }
    }
}
