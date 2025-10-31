using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Alkhabeer.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NumbersOnlyExAttribute : ValidationAttribute
    {
        public NumbersOnlyExAttribute()
        {
            // Arabic default message
            ErrorMessage = "يجب أن يحتوي هذا الحقل على أرقام فقط";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //  Skip if nothing entered
            if (value == null)
                return ValidationResult.Success;

            var strValue = value.ToString()?.Trim();

            //  Skip if empty or whitespace after trimming
            if (string.IsNullOrEmpty(strValue))
                return ValidationResult.Success;

            //  Check digits only (0–9)
            if (!Regex.IsMatch(strValue, @"^\d+$"))
                return new ValidationResult(ErrorMessage);

            //  Update the property value (remove spaces)
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName!);
            if (property != null && property.CanWrite)
                property.SetValue(validationContext.ObjectInstance, strValue);

            return ValidationResult.Success;
        }
    }
}
