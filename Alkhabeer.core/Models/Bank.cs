
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alkhabeer.Core.Models
{
    public partial class Bank : ObservableValidator
    {
        [Key]
        public int Id { get; set; }

        [ObservableProperty]
        [Required, MaxLength(100)]
        private string bankName = "البنك الرئيسي";

        [ObservableProperty]
        [MaxLength(100)]
        private string accountName = "البنك الرئيسي";

        [ObservableProperty]
        [Required, MaxLength(50)]
        private string accountNumber = "0000000000";

        [ObservableProperty]
        [MaxLength(100)]
        private string? iban;

        [ObservableProperty]
        [MaxLength(300)]
        private string? notes;

        [ObservableProperty]
        private bool isActive = true;

        [ObservableProperty]
        private DateTime createdAt = DateTime.Now;

        [ObservableProperty]
        private DateTime updatedAt = DateTime.Now;
    }
}
