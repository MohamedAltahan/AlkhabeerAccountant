using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.Helpers
{
    public static class FormResetHelper
    {
        public static void Reset<T>(T obj)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.CanWrite);

            foreach (var prop in props)
            {
                // Skip collections, lists, or complex objects
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType)
                    && prop.PropertyType != typeof(string))
                    continue;

                object? defaultValue = prop.PropertyType.IsValueType
                    ? Activator.CreateInstance(prop.PropertyType)
                    : null;

                prop.SetValue(obj, defaultValue);

                //f you want strings or any thing to become empty instead of null
                if (prop.PropertyType == typeof(bool))
                    prop.SetValue(obj, true);
            }
        }
    }
}
