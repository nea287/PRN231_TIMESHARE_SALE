using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class AvailableTime
    {
        public AvailableTime()
        {
            Contracts = new HashSet<Contract>();
            Reservations = new HashSet<Reservation>();
        }

        public int AvailableTimeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Status { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
