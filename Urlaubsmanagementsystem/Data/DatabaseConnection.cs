using Microsoft.EntityFrameworkCore;
using Urlaubsmanagementsystem.Models;

namespace Urlaubsmanagementsystem.Data
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Decision> Decisions { get; set; }


    }
}
