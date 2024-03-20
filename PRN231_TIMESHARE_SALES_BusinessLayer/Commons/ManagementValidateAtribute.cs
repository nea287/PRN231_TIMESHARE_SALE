using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Commons
{
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
    using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;

    #region Less than by int
    public class LessThanByIntAttribute : ValidationAttribute
    {
        private readonly string _valuePropertyName;

        public LessThanByIntAttribute(string valuePropertyName)
        {
            _valuePropertyName = valuePropertyName;
        }
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueProperty = validationContext.ObjectType.GetProperty(_valuePropertyName);
            if (valueProperty == null)
            {
                throw new ArgumentException("Property with this name not found!");
            }

            var valueValue = (int)valueProperty.GetValue(validationContext.ObjectInstance);
            var currentMonth = (int)value;

            if (currentMonth >= valueValue)
            {
                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(valueProperty, OtherProperty);
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }

    #endregion

    #region Less than date

    public class LessThanDateAttribute : ValidationAttribute
    {
        private readonly string _datePropertyName;

        public LessThanDateAttribute(string datePropertyName)
        {
            _datePropertyName = datePropertyName;
        }
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateProperty = validationContext.ObjectType.GetProperty(_datePropertyName);
            if (dateProperty == null)
            {
                throw new ArgumentException("Property with this name not found!");
            }

            var dateValue = (DateTime)dateProperty.GetValue(validationContext.ObjectInstance);
            var currentDate = (DateTime)value;

            if (currentDate >= dateValue)
            {
                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(dateProperty, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }

    #endregion

    #region Greater than or equal date
    public class GreaterThanOrEqualDateAttribute : ValidationAttribute
    {
        private readonly string _datePropertyName;

        public GreaterThanOrEqualDateAttribute(string datePropertyName)
        {
            _datePropertyName = datePropertyName;
        }
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateProperty = validationContext.ObjectType.GetProperty(_datePropertyName);

            if (dateProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var dateValue = (DateTime)dateProperty.GetValue(validationContext.ObjectInstance);
            var currentDate = (DateTime)value;

            if (currentDate < dateValue)
            {

                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(dateProperty, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

    }

    #endregion

    #region Greater than or equal by int
    public class GreaterThanOrEqualByIntAttribute : ValidationAttribute
    {
        private readonly string _valuePropertyName;

        public GreaterThanOrEqualByIntAttribute(string valuePropertyName)
        {
            _valuePropertyName = valuePropertyName;
        }
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueProperty = validationContext.ObjectType.GetProperty(_valuePropertyName);

            if (valueProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var valueValue = (int)valueProperty.GetValue(validationContext.ObjectInstance);
            var currentMonth = (int)value;

            if (currentMonth < valueValue)
            {

                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(valueProperty, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    #region Greater than date
    public class GreaterThanDateAttribute : ValidationAttribute
    {
        private readonly string _datePropertyName;

        public GreaterThanDateAttribute(string datePropertyName)
        {
            _datePropertyName = datePropertyName;
        }

        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateProperty = validationContext.ObjectType.GetProperty(_datePropertyName);

            if (dateProperty == null)
            {
                throw new ArgumentException("The property name not found!");
            }

            var dateValue = (DateTime)dateProperty.GetValue(validationContext.ObjectInstance);
            var currentDate = (DateTime)value;

            if (currentDate <= dateValue)
            {
                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(dateProperty, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    #region Greater than by int
    public class GreaterThanByIntAttribute : ValidationAttribute
    {
        private readonly string _valuePropertyName;

        public GreaterThanByIntAttribute(string valuePropertyName)
        {
            _valuePropertyName = valuePropertyName;
        }
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueProperty = validationContext.ObjectType.GetProperty(_valuePropertyName);

            if (valueProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var valueValue = (int)valueProperty.GetValue(validationContext.ObjectInstance);
            var currentMonth = (int)value;

            if (currentMonth <= valueValue)
            {

                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(valueProperty, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    #region InRageDate
    public class InRageDate : ValidationAttribute
    {
        private readonly string _startDate;
        private readonly string _endDate;

        public InRageDate(string startDate, string endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDate);
            var endDateProperty = validationContext.ObjectType.GetProperty(_endDate);

            if (startDateProperty == null || endDateProperty == null)
            {
                throw new ArgumentException("The property name is not found!");
            }

            var startDateValue = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            var endDateValue = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance);

            var currentDate = (DateTime)value;

            if (startDateValue > currentDate || endDateValue < currentDate)
            {
                OtherPropertyDisplayName = OtherPropertyDisplayName ?? SupportingFeature.Instance.GetDisplayNameForProperty(startDateProperty, OtherProperty);
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    public class InRangeOneHundredAttribute : ValidationAttribute
    {
        public string OtherProperty { get; }

        public string? OtherPropertyDisplayName { get; internal set; }
        public override string FormatErrorMessage(string name) =>
        string.Format(
        CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int? currentValue = (int?)value?? DateTime.Now.Year; 
            


            if (currentValue.Value < (DateTime.Now.Year - 50) || currentValue.Value > (DateTime.Now.Year + 50))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
