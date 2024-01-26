using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public int? StaffId { get; set; }
        public int CustomerId { get; set; }
        public int AvailableTimeId { get; set; }
        public string ContractName { get; set; } = null!;
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractTerm { get; set; }
        public decimal? ContractAmount { get; set; }
        public DateTime? RegularPaymentDate { get; set; }
        public int? Status { get; set; }
        public int? ContractType { get; set; }
        public decimal? RegularPaymentAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public int? NumberYears { get; set; }

        public virtual AvailableTime AvailableTime { get; set; } = null!;
        public virtual Account Customer { get; set; } = null!;
        public virtual Account? Staff { get; set; }
    }
}
