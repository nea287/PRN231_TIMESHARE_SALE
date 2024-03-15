using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class UsageHistoryRequestModel
    {
        [Required(ErrorMessage = "Please enter CustomerId")]
        public int CustomerId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CheckInDate { get; set; }
        [DataType(DataType.DateTime)]
        [GreaterThanOrEqualDate("CheckInDate", ErrorMessage = "The end date must be greater than the check in out.")]
        public DateTime? CheckOutDate { get; set; }
        public int? Status { get; set; }
        [Required(ErrorMessage = "Please enter DepartmentId")]
        public int DepartmentId { get; set; }
    }
}
