using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using PRN231_TIMESHARE_SALES_DataLayer.Models;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class ContractViewModel
    {
        public int? ContractId { get; set; }
        public int? StaffId { get; set; }
        public int? CustomerId { get; set; }
        public int? AvailableTimeId { get; set; }
        public string? ContractName { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractTerm { get; set; }
        public decimal? ContractAmount { get; set; }
        public DateTime? RegularPaymentDate { get; set; }
        public int? Status { get; set; }
        public int? ContractType { get; set; }
        public decimal? RegularPaymentAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public int? NumberYears { get; set; }
        public int? NumberMonths { get; set; }
        public string? ProjectName { get; set; }
        public int? DepartmentContructionType { get; set; }
        private string? _departmentContructionTypeName;
        [Skip]
        public string? DepartmentContructionTypeName
        {
            get
            {
                if (_departmentContructionTypeName == null)
                {
                    _departmentContructionTypeName = GetDepartmentContructionTypeName();
                }

                return _departmentContructionTypeName;
            }
        }

        private string? GetDepartmentContructionTypeName()
        {
            return Enum.GetName(typeof(DepartmentConstructionType), DepartmentContructionType ?? 1);
        }

    }
}