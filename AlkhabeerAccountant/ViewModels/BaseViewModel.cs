using Alkhabeer.Data.Repositories;
using AlkhabeerAccountant.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkhabeerAccountant.ViewModels
{
    public partial class BaseViewModel : ObservableValidator
    {
        protected bool ValidateFormWithToast()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                var firstError = GetErrors(null)
                    .Cast<ValidationResult>()
                    .FirstOrDefault()?.ErrorMessage;

                if (!string.IsNullOrWhiteSpace(firstError))
                    ToastService.Warning(firstError);

                return false; // فيه أخطاء
            }
            return true; // سليم
        }


    }
}
