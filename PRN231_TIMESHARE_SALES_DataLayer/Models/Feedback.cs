using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public double Rating { get; set; }
        public string? Content { get; set; }
        public DateTime FeedbackDate { get; set; }
        public int DepartmentId { get; set; }

        public virtual Account Customer { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
    }
}
