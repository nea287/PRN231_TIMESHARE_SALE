namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class UsageHistoryViewModel
    {
        public int? UsageId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? Status { get; set; }
        public int? DepartmentId { get; set; }
    }
}