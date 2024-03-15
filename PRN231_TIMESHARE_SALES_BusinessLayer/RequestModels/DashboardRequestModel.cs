using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.RequestModels
{
    public class MonthRequestModel
    {
        [RegularExpression(@"^(1[0-2]?|[1-9])$", ErrorMessage = "Must input from 1 to 12")]
        public int? Month { get; set; }
        [RegularExpression(@"^(1[0-2]?|[1-9])$", ErrorMessage = "Must input from 1 to 12")]
        public int? StartMonth { get; set; } = (int)Commons.Month.January;

        [RegularExpression(@"^(1[0-2]?|[1-9])$", ErrorMessage = "Must input from 1 to 12")]
        [GreaterThanOrEqualByInt(nameof(StartMonth), ErrorMessage = "The end month must greater than or equal to the start month.")]
        public int? EndMonth { get; set; } = (int)Commons.Month.December;
        [InRangeOneHundred(ErrorMessage = "Over the course of 50 years")]
        public int? Year { get; set; } = DateTime.Now.Year;
    }

    public class DateRequestModel
    {
        public DateTime? StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        [GreaterThanOrEqualDate(nameof(StartDate), ErrorMessage = "The end date must greater than or equal to the start date.")]
        public DateTime? EndDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 1, 23, 59, 59);
        public DateTime? Date { get; set; }
    }

    public class YearRequestModel
    {
        [InRangeOneHundred(ErrorMessage = "Over the course of 50 years")]
        public int? Year { get; set; } 
        [InRangeOneHundred(ErrorMessage = "Over the course of 50 years")]
        public int? FromYear { get; set; } = DateTime.Now.Year - 50;
        [GreaterThanOrEqualByInt(nameof(FromYear), ErrorMessage = "To year must greater than or equals to from year.")]
        [InRangeOneHundred(ErrorMessage = "Over the course of 50 years")]
        public int? ToYear { get; set; } = DateTime.Now.Year + 50;

    }

}
