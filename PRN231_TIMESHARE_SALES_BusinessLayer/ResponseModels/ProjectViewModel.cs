using PRN231_TIMESHARE_SALES_DataLayer.Models;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            DepartmentOfProjects = new HashSet<DepartmentOfProjectViewModel>();

            StaffOfProjects = new HashSet<StaffOfProjectsViewModel>();
        }

        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? PriorityType { get; set; }
        public string? ProjectCode { get; set; }
        public int? TotalSlot { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public DateTime? RegistrationOpeningDate { get; set; }
        public decimal? TotalRevenue { get; set; }
        public ICollection<DepartmentOfProjectViewModel>? DepartmentOfProjects { get; set; }
        public ICollection<StaffOfProjectsViewModel>? StaffOfProjects { get; set; }
    }
}