using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            UsageRights = new HashSet<UsageRight>();
        }

        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int AvailableTimeId { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal? ReservationFee { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public virtual AvailableTime AvailableTime { get; set; }
        [JsonIgnore]
        public virtual Account Customer { get; set; }
        public virtual ICollection<UsageRight> UsageRights { get; set; }
    }
}
