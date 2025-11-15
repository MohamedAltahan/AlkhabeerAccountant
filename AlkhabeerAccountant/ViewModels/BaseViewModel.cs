using Alkhabeer.Data.Repositories;
using Alkhabeer.Service.Base;
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
    public partial class BaseViewModel<T> : ObservableValidator where T : class
    {
        protected BaseService<T> _service;
        protected BaseViewModel(BaseService<T> service)
        {
            _service = service;
        }

        protected BaseViewModel()
        {
        }

        // ================= Generic T CRUD section =================
        protected virtual async Task AddAsync(T entity)
        {
            var result = await _service.AddAsync(entity);

            if (result.IsSuccess)
                ToastService.Success();
            else
                ToastService.Warning(result.ErrorMessage);
        }

        protected virtual async Task UpdateAsync(T entity)
        {
            var result = await _service.UpdateAsync(entity);

            if (result.IsSuccess)
                ToastService.Updated();
            else
                ToastService.Error(result.ErrorMessage);
        }
        public virtual async Task<List<T>?> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            if (result.IsSuccess)
                return result.Value;
            else
                ToastService.Error();

            return null; // Ensure all code paths return a value
        }


        //validate inputs and return show toaster
        protected bool ValidateForm()
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
