using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Core.Validation
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredExAttribute : RequiredAttribute
    {
        public RequiredExAttribute()
        {
            ErrorMessage = "هناك حقل مطلوب";
        }

        private static string GetDefaultMessage()
        {
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return lang switch
            {
                "ar" => "هذا الحقل مطلوب",
                _ => "This field is requiredd"
            };
        }

    }
}
