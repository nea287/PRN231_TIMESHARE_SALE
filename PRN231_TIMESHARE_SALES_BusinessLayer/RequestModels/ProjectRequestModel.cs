using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System.ComponentModel.DataAnnotations;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class ProjectRequestModel
    {
        [Required(ErrorMessage = "Please enter project name!")]
        public string ProjectName { get; set; }
        public int? PriorityType { get; set; }

        [Required(ErrorMessage = "Please enter project code!")]
        public string ProjectCode { get; set; }

        public int? TotalSlot { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [GreaterThanDate("StartDate", ErrorMessage ="End date must be greater than start date")]
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RegistrationEndDate { get; set; }

        [DataType(DataType.DateTime)]
        [LessThanDate("RegistrationEndDate", ErrorMessage = "Registration opening date must less than Registration end date!")]
        public DateTime? RegistrationOpeningDate { get; set; }
    }
}
