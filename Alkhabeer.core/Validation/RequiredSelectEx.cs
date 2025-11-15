using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Core.Validation
{
    //used for require combobox selection
    public class RequiredSelectEx : ValidationAttribute
    {
        public RequiredSelectEx()
        {
            ErrorMessage = "يجب اختيار قيمة";
        }

        public override bool IsValid(object value)
        {
            // null = not selected
            if (value == null) return false;

            // integer 0 = not selected
            if (value is int intVal)
                return intVal > 0;

            // default fallback
            return true;
        }
    }
}
