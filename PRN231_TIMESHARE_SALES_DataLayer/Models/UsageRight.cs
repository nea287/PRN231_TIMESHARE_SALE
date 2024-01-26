using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class UsageRight
    {
        public int UsageRightId { get; set; }
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }
        public int? Status { get; set; }

        public virtual Account Customer { get; set; } = null!;
        public virtual Reservation Reservation { get; set; } = null!;
    }
}
