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
            if (value == null)
                return ValidationResult.Success; // ignore null — RequiredEx will handle emptiness

            var strValue = value.ToString();

            // ✅ only allow digits 0-9
            if (!Regex.IsMatch(strValue, @"^\d+$"))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
