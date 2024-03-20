using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class AvailableTimeRequestModel
    {
        public DateTime? StartDate { get; set; }

        [GreaterThanDate(nameof(StartDate), ErrorMessage = "The end date must greater than or equal to the start date.")]
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string? DepartmentProjectCode { get; set; }

    }
}
