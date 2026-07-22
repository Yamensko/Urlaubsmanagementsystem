using System.ComponentModel.DataAnnotations;

namespace Urlaubsmanagementsystem.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
