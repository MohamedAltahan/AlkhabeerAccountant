using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alkhabeer.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? PasswordHash { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsAdmin { get; set; } = false;

        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Role? Role { get; set; }

    }
}
