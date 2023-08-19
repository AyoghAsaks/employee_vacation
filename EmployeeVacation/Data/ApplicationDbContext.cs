
using EmployeeVacation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeVacation.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN"},
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
        }
        
        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    }
}
