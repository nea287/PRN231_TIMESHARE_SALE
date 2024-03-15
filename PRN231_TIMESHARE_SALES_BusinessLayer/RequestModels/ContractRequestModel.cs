using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class ContractRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Staff Id is Invalid!")]
        public int? StaffId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer Id is Invalid!")]
        public int CustomerId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Available Id is Invalid!")]
        public int AvailableTimeId { get; set; }
        public string ContractName { get; set; } = null!;
        public DateTime? ContractDate { get; set; }
        [GreaterThanDate(nameof(ContractDate), ErrorMessage = "The contract term must greater than contract date")]
        public DateTime? ContractTerm { get; set; }
        public decimal? ContractAmount { get; set; }
        [InRageDate(nameof(ContractDate), nameof(ContractTerm), ErrorMessage = "regular payment date must be greater than the contract date and less than the contract term.")]
        public DateTime? RegularPaymentDate { get; set; }
        public int? Status { get; set; }
        public int? ContractType { get; set; }
        public decimal? RegularPaymentAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public int? NumberYears { get; set; }
        public int? NumberMonths { get; set; }
    }
}
