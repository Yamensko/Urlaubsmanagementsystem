using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Urlaubsmanagementsystem.Models
{
    public class LeaveRequest
    {
        [Key]
        public Guid RequestId { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public byte DecisionId { get; set; }

        public User User { get; set; } = default!;

        public Decision Decision { get; set; } = default!;

        
        [NotMapped]
        public string UserName => User != null
    ? $"{User.Firstname} {User.Lastname}"
    : "Unknown";
    }
}