namespace Alkhabeer.Core.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        //navigation property that gives you access to the related User entity
        public User User { get; set; } = default!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
