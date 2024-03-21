using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        //[JsonIgnore]
        public virtual Account? Customer { get; set; }
        //[JsonIgnore]
        public virtual Department? Department { get; set; }
    }
}
