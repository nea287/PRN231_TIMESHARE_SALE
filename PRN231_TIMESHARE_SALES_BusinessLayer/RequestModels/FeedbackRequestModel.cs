using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class FeedbackRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer Id is Invalid!")]
        public int? CustomerId { get; set; }
        [RegularExpression(@"^[1-5]$", ErrorMessage = "Rating input from 1 to 5")]
        public double? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? FeedbackDate { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Department Id is Invalid!")]
        public int? DepartmentId { get; set; }
    }
}
