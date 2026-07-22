using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Urlaubsmanagementsystem.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        // In User.cs ergänzen:
        [NotMapped]
        public string Fullname => $"{Firstname} {Lastname}";
    }
}
