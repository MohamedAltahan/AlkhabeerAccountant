using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.Helpers
{
    public static class FormResetHelper
    {
        public static void Reset(object obj)
        {
            if (obj == null) return;

            var props = obj.GetType().GetProperties()   // child class properties
                .Where(p => p.CanWrite);

            foreach (var prop in props)
            {
                // Skip collections except string
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType)
                    && prop.PropertyType != typeof(string))
                    continue;

                // Skip paging properties
                if (prop.Name is "PageSize" or "CurrentPage" or "TotalPages" or "TotalCount")
                    continue;

                object? defaultValue = prop.PropertyType.IsValueType
                    ? Activator.CreateInstance(prop.PropertyType)
                    : null;

                // Set the default value
                prop.SetValue(obj, defaultValue);

                // Optional: boolean fields default to TRUE
                if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(obj, true);
                }

            }
        }
    }

}
