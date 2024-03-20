using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class FacilityRequestModel
    {
        public string? FacilityName { get; set; }
        public string? Description { get; set; }
        public int? FacilityType { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Department Id is Invalid!")]
        public int? DepartmentId { get; set; }
    }
}
