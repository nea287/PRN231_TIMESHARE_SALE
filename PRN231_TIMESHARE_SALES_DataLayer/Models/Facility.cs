using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Facility
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string Description { get; set; }
        public int? FacilityType { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
