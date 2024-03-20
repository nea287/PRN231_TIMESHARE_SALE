using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class CustomerRequestRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer request Id is Invalid!")]
        public int? CustomerRequestId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer Id is Invalid!")]
        public int? CustomerId { get; set; }
        public int? RequestType { get; set; }
        public string? Description { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? Status { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Department Id is Invalid!")]
        public int? DepartmentId { get; set; }
    }
}
