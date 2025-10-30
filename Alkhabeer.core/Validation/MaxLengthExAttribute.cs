using System;
using System.ComponentModel.DataAnnotations;

namespace Alkhabeer.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaxLengthExAttribute : MaxLengthAttribute
    {
        public MaxLengthExAttribute(int length) : base(length)
        {
            ErrorMessage = $"الحد الأقصى للطول هو {length} حرفًا";
        }
    }
}
