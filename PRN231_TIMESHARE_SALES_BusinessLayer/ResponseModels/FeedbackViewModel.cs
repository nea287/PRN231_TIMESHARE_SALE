﻿namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels
{
    public class FeedbackViewModel
    {
        public int? FeedbackId { get; set; }
        public int? CustomerId { get; set; }
        public double? Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }

    }
}